using System.Runtime.InteropServices;
using Acutipupu.Components;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NodaTime;
using Tutu;
using Tutu.Events;
using IBotoTerminal = Boto.Terminals.ITerminal;
using ITutuTerminal = Tutu.Terminal.ITerminal;

namespace Acutipupu;

/// <summary>
/// The Acutipupu application.
/// </summary>
public abstract class AcutipupuApp : IDisposable, IAsyncDisposable,
    IRecipient<QuitMessage>,
    IRecipient<ClearScreenMessage>,
    IRecipient<EnterAltScreenMessage>,
    IRecipient<LeaveAltScreenMessage>,
    IRecipient<EnableMouseCaptureMessage>,
    IRecipient<DisableMouseCaptureMessage>,
    IRecipient<ShowCursorMessage>,
    IRecipient<HideCursorMessage>
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly List<PosixSignalRegistration> _osSignals = new();
    private Action<Frame> _onRenderFrame = _ => { };


    /// <summary>
    /// Initializes a new instance of the <see cref="AcutipupuApp"/> class.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
    /// <param name="options">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <param name="botoTerminal">The <see cref="IBotoTerminal"/>.</param>
    /// <param name="tutuTerminal">The <see cref="ITutuTerminal"/>.</param>
    /// <param name="eventReader">The <see cref="IEventReader"/>.</param>
    /// <param name="executableCommand">The <see cref="IExecutableCommand"/>.</param>
    /// <param name="messenger">The <see cref="IMessenger"/>.</param>
    /// <param name="navigationControl">The <see cref="Navigation"/>.</param>
    /// <param name="logger">The <see cref="ILogger"/>.</param>
    protected AcutipupuApp(IServiceProvider serviceProvider,
        IOptions<AcutipupuAppOptions> options,
        IBotoTerminal botoTerminal,
        ITutuTerminal tutuTerminal,
        IEventReader eventReader,
        IExecutableCommand executableCommand,
        IMessenger messenger,
        INavigation navigationControl,
        ILogger logger)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value ?? throw new ArgumentNullException(nameof(options));
        ExecutableCommand = executableCommand ?? throw new ArgumentNullException(nameof(executableCommand));
        BotoTerminal = botoTerminal ?? throw new ArgumentNullException(nameof(botoTerminal));
        TutuTerminal = tutuTerminal ?? throw new ArgumentNullException(nameof(tutuTerminal));
        EventReader = eventReader ?? throw new ArgumentNullException(nameof(eventReader));
        Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Navigation = navigationControl ?? throw new ArgumentNullException(nameof(navigationControl));
    }

    /// <summary>
    /// The <see cref="AcutipupuAppOptions"/>.
    /// </summary>
    protected AcutipupuAppOptions Options { get; }

    /// <summary>
    /// The <see cref="IBotoTerminal"/>.
    /// </summary>
    protected IBotoTerminal BotoTerminal { get; }

    /// <summary>
    /// The <see cref="ITutuTerminal"/>.
    /// </summary>
    protected ITutuTerminal TutuTerminal { get; }

    /// <summary>
    /// The <see cref="IEventReader"/>.
    /// </summary>
    protected IEventReader EventReader { get; }

    /// <summary>
    /// The <see cref="IExecutableCommand"/>.
    /// </summary>
    protected IExecutableCommand ExecutableCommand { get; }

    /// <summary>
    /// The <see cref="IMessenger"/>.
    /// </summary>
    protected IMessenger Messenger { get; }

    /// <summary>
    /// The <see cref="CancellationTokenSource"/>.
    /// </summary>
    protected CancellationTokenSource? CancellationTokenSource { get; set; }

    /// <summary>
    /// The <see cref="ILogger"/>.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// The <see cref="Navigation"/>.
    /// </summary>
    protected INavigation Navigation { get; }

    /// <summary>
    /// The application <see cref="IServiceProvider"/>.
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// The application <see cref="IConfiguration"/>.
    /// </summary>
    public IConfiguration Configuration => ServiceProvider.GetRequiredService<IConfiguration>();

    /// <summary>
    /// Create the <see cref="AcutipupuAppOptions"/>.
    /// </summary>
    /// <returns>New instance of <see cref="AcutipupuAppOptions"/>.</returns>
    public static AcutipupuAppBuilder CreateBuilder() => new();

    /// <summary>
    /// Run the application.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public virtual async Task RunAsync(CancellationToken cancellationToken = default)
    {
        if (CancellationTokenSource is not null)
        {
            throw new InvalidOperationException("The application is already running.");
        }

        CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        await using var scope = ServiceProvider.CreateAsyncScope();

        EnableStartupFlags(Options.StartupOptions);

        Messenger.RegisterAll(this);

        var page = CreatePage(scope.ServiceProvider);
        page.IsActive = true;

        Navigation.MainPage = page;
        Navigation.MoveToNext(1);

        var inputTask = Task.Factory.StartNew(() => HandlerInputAsync(CancellationTokenSource.Token),
            TaskCreationOptions.LongRunning).ConfigureAwait(false);
        await HandlerRenderAsync(page, CancellationTokenSource.Token).ConfigureAwait(false);
        await inputTask;

        Messenger.UnregisterAll(this);

        DisableStartupFlags(Options.StartupOptions);
        CancellationTokenSource = null;
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public virtual void Dispose()
    {
        CancellationTokenSource?.Cancel();
        DisposeConfiguration();
        (ServiceProvider as IDisposable)?.Dispose();
    }

    /// <inheritdoc cref="IAsyncDisposable.DisposeAsync"/>
    public virtual async ValueTask DisposeAsync()
    {
        CancellationTokenSource?.Cancel();
        DisposeConfiguration();
        if (ServiceProvider is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
        else
        {
            (ServiceProvider as IDisposable)?.Dispose();
        }
    }

    private void DisposeConfiguration()
    {
        // Explicitly dispose the Configuration, since it is added as a singleton object that the ServiceProvider
        // won't dispose.
        (Configuration as IDisposable)?.Dispose();
    }

    /// <summary>
    /// Enable the startup flags.
    /// </summary>
    /// <param name="options">The <see cref="StartupOptions"/>.</param>
    protected virtual void EnableStartupFlags(StartupOptions options)
    {
        var commands = new List<ICommand>();
        if (options.HasFlag(StartupOptions.WithRawMode))
        {
            TutuTerminal.EnableRawMode();
        }

        if (options.HasFlag(StartupOptions.WithAltScreen))
        {
            commands.Add(Tutu.Commands.Terminal.EnterAlternateScreen);
        }

        if (options.HasFlag(StartupOptions.WithMouseCapture))
        {
            commands.Add(Tutu.Commands.Events.EnableMouseCapture);
        }

        if (options.HasFlag(StartupOptions.WithSignalHandler))
        {
            Action<PosixSignalContext> quit = _ => Messenger.Send(new QuitMessage());
            _osSignals.Add(PosixSignalRegistration.Create(PosixSignal.SIGHUP, quit));
            _osSignals.Add(PosixSignalRegistration.Create(PosixSignal.SIGINT, quit));
            _osSignals.Add(PosixSignalRegistration.Create(PosixSignal.SIGQUIT, quit));
            _osSignals.Add(PosixSignalRegistration.Create(PosixSignal.SIGTERM, quit));
        }

        ExecutableCommand.Execute(commands.ToArray());
        BotoTerminal.HideCursor();
    }

    /// <summary>
    /// Disable the startup flags.
    /// </summary>
    /// <param name="options">The <see cref="StartupOptions"/>.</param>
    protected virtual void DisableStartupFlags(StartupOptions options)
    {
        var commands = new List<ICommand>();
        if (options.HasFlag(StartupOptions.WithRawMode))
        {
            TutuTerminal.DisableRawMode();
        }

        if (options.HasFlag(StartupOptions.WithAltScreen))
        {
            commands.Add(Tutu.Commands.Terminal.LeaveAlternateScreen);
        }

        if (options.HasFlag(StartupOptions.WithMouseCapture))
        {
            commands.Add(Tutu.Commands.Events.DisableMouseCapture);
        }

        if (options.HasFlag(StartupOptions.WithSignalHandler))
        {
            foreach (var signal in _osSignals)
            {
                signal.Dispose();
            }

            _osSignals.Clear();
        }

        ExecutableCommand.Execute(commands.ToArray());
        BotoTerminal.ShowCursor();
    }

    /// <summary>
    /// The handler for input events.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    protected virtual void HandlerInputAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (!EventReader.Poll(Duration.FromMilliseconds(10)))
                {
                    continue;
                }

                var @event = EventReader.Read();
                if (@event is Event.ScreenResizeEvent resize)
                {
                    Messenger.Send(new WindowResizeMessage(resize));
                }
                else if (@event is Event.KeyEventEvent key)
                {
                    Messenger.Send(new KeyMessage(key.Event));
                }
                else if (@event is Event.MouseEventEvent mouse)
                {
                    Messenger.Send(new MouseMessage(mouse.Event));
                }
                else if (@event is Event.FocusGainedEvent)
                {
                    Messenger.Send(FocusGainMessage.Instance);
                }
                else if (@event is Event.FocusLostEvent)
                {
                    Messenger.Send(FocusLostMessage.Instance);
                }
                else if (@event is Event.TextPastedEvent pasted)
                {
                    Messenger.Send(new TextPastedMessage(pasted.Text));
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling input");
            }
        }
    }

    /// <summary>
    /// The handler for render.
    /// </summary>
    /// <param name="page">The <see cref="Page"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    protected virtual async Task HandlerRenderAsync(Page page, CancellationToken cancellationToken)
    {
        var frameRate = Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / Options.FrameRate);
        await Task.Yield();
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(frameRate.ToTimeSpan(), cancellationToken).ConfigureAwait(false);

                BotoTerminal.Draw(frame =>
                {
                    var context = new AcutipupuRenderContext(frame.Size, frame);
                    page.Render(context);

                    _semaphore.Wait();

                    _onRenderFrame(frame);
                    _onRenderFrame = _ => { };

                    _semaphore.Release();
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error rendering");
            }
        }
    }

    /// <summary>
    /// Create the main <see cref="Page"/>.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/>.</param>
    /// <returns>The <see cref="Page"/>.</returns>
    protected abstract Page CreatePage(IServiceProvider provider);

    /// <inheritdoc />
    public virtual void Receive(QuitMessage message)
    {
        if (CancellationTokenSource == null)
        {
            throw new InvalidOperationException("Cannot quit before starting");
        }

        CancellationTokenSource.Cancel();
    }

    /// <inheritdoc />
    public virtual void Receive(ClearScreenMessage message) => BotoTerminal.Clear();

    /// <inheritdoc />
    public virtual void Receive(EnterAltScreenMessage message)
    {
        ExecutableCommand.Execute(Tutu.Commands.Terminal.EnterAlternateScreen);
        BotoTerminal.Clear();
    }

    /// <inheritdoc />
    public virtual void Receive(LeaveAltScreenMessage message)
    {
        ExecutableCommand.Execute(Tutu.Commands.Terminal.LeaveAlternateScreen);
        BotoTerminal.Clear();
    }

    /// <inheritdoc />
    public virtual void Receive(EnableMouseCaptureMessage message) =>
        ExecutableCommand.Execute(Tutu.Commands.Events.EnableMouseCapture);

    /// <inheritdoc />
    public virtual void Receive(DisableMouseCaptureMessage message) =>
        ExecutableCommand.Execute(Tutu.Commands.Events.DisableMouseCapture);

    /// <inheritdoc />
    public virtual void Receive(ShowCursorMessage message)
    {
        _semaphore.Wait();
        _onRenderFrame = frame => frame.CursorPosition = BotoTerminal.Cursor;
        _semaphore.Release();
    }

    /// <inheritdoc />
    public virtual void Receive(HideCursorMessage message)
    {
        _semaphore.Wait();
        _onRenderFrame = frame => frame.CursorPosition = null;
        _semaphore.Release();
    }
}

/// <summary>
/// The Acutipupu application.
/// </summary>
/// <typeparam name="TPage">The <see cref="Page"/>.</typeparam>
public class AcutipupuApp<TPage> : AcutipupuApp
    where TPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcutipupuApp"/> class.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
    /// <param name="options">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <param name="botoTerminal">The <see cref="IBotoTerminal"/>.</param>
    /// <param name="tutuTerminal">The <see cref="ITutuTerminal"/>.</param>
    /// <param name="eventReader">The <see cref="IEventReader"/>.</param>
    /// <param name="executableCommand">The <see cref="IExecutableCommand"/>.</param>
    /// <param name="messenger">The <see cref="IMessenger"/>.</param>
    /// <param name="navigationControl">The <see cref="NavigationControl"/>.</param>
    /// <param name="logger">The <see cref="ILogger"/>.</param>
    public AcutipupuApp(IServiceProvider serviceProvider,
        IOptions<AcutipupuAppOptions> options,
        IBotoTerminal botoTerminal,
        ITutuTerminal tutuTerminal,
        IEventReader eventReader,
        IExecutableCommand executableCommand,
        IMessenger messenger,
        INavigation navigationControl,
        ILogger<AcutipupuApp<TPage>> logger)
        : base(serviceProvider, options, botoTerminal, tutuTerminal, eventReader,
            executableCommand, messenger, navigationControl, logger)
    {
    }

    /// <inheritdoc />
    protected override Page CreatePage(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<TPage>();
}
