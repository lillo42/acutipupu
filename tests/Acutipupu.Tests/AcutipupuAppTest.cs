using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using AutoFixture;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NodaTime;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Tutu;
using Tutu.Events;
using IBotoTerminal = Boto.Terminals.ITerminal;
using ITutuTerminal = Tutu.Terminal.ITerminal;

namespace Acutipupu.Tests;

public class AcutipupuAppTest
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AcutipupuAppOptions _options;
    private readonly IBotoTerminal _botoTerminal;
    private readonly ITutuTerminal _tutuTerminal;
    private readonly IEventReader _eventReader;
    private readonly IExecutableCommand _executableCommand;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigationControl;
    private readonly AcutipupuApp<MainPage> _app;

    public AcutipupuAppTest()
    {
        new Fixture();

        _serviceProvider = Substitute.For<IServiceProvider>();
        _options = Substitute.For<AcutipupuAppOptions>();
        _botoTerminal = Substitute.For<IBotoTerminal>();
        _tutuTerminal = Substitute.For<ITutuTerminal>();
        _eventReader = Substitute.For<IEventReader>();
        _executableCommand = Substitute.For<IExecutableCommand>();
        _messenger = Substitute.For<IMessenger>();
        _navigationControl = Substitute.For<INavigation>();

        _app = new AcutipupuApp<MainPage>(
            _serviceProvider,
            Options.Create(_options),
            _botoTerminal,
            _tutuTerminal,
            _eventReader,
            _executableCommand,
            _messenger,
            _navigationControl,
            new NullLogger<AcutipupuApp<MainPage>>());
    }

    [Fact]
    public void ReceiveQuitMessage_Should_Throw_When_ItNotStart()
        => Assert.Throws<InvalidOperationException>(() => _app.Receive(new QuitMessage()));

    [Fact]
    public void ReceiveClearScreenMessage()
    {
        _app.Receive(new ClearScreenMessage());

        _botoTerminal
            .Received(1)
            .Clear();
    }

    [Fact]
    public void ReceiveEnterAltScreenMessage()
    {
        _app.Receive(new EnterAltScreenMessage());

        _executableCommand
            .Received(1)
            .Execute(Tutu.Commands.Terminal.EnterAlternateScreen);

        _botoTerminal
            .Received(1)
            .Clear();
    }

    [Fact]
    public void ReceiveLeaveAltScreenMessage()
    {
        _app.Receive(new LeaveAltScreenMessage());

        _executableCommand
            .Received(1)
            .Execute(Tutu.Commands.Terminal.LeaveAlternateScreen);

        _botoTerminal
            .Received(1)
            .Clear();
    }

    [Fact]
    public void ReceiveEnableMouseCaptureMessage()
    {
        _app.Receive(new EnableMouseCaptureMessage());

        _executableCommand
            .Received(1)
            .Execute(Tutu.Commands.Events.EnableMouseCapture);
    }

    [Fact]
    public void ReceiveDisableMouseCaptureMessage()
    {
        _app.Receive(new DisableMouseCaptureMessage());

        _executableCommand
            .Received(1)
            .Execute(Tutu.Commands.Events.DisableMouseCapture);
    }

    [Fact(Timeout = 10_000)]
    public async Task RunAsync()
    {
        var scope = Substitute.For<IServiceScope>();
        var scopeFactory = Substitute.For<IServiceScopeFactory>();
        scopeFactory.CreateScope()
            .Returns(scope);

        _serviceProvider.GetService(typeof(IServiceScopeFactory))
            .Returns(scopeFactory);

        var serviceProvider = Substitute.For<IServiceProvider>();
        scope.ServiceProvider
            .Returns(serviceProvider);


        serviceProvider.GetService(typeof(MainPage))
            .Returns(new MainPage(new MainViewModel(), _messenger));

        _options.StartupOptions =
            StartupOptions.WithAltScreen | StartupOptions.WithMouseCapture | StartupOptions.WithRawMode;

        var task = _app.RunAsync();

        await Task.Delay(1_00);

        _app.Receive(new QuitMessage());

        await task;

        _tutuTerminal
            .Received(1)
            .EnableRawMode();

        _executableCommand
            .Received(1)
            .Execute(Arg.Is<ICommand[]>(commands => commands.Length == 2
                                                    && commands[0] == Tutu.Commands.Terminal.EnterAlternateScreen
                                                    && commands[1] == Tutu.Commands.Events.EnableMouseCapture));
        _botoTerminal.HideCursor();

        _tutuTerminal
            .Received(1)
            .DisableRawMode();

        _executableCommand
            .Received(1)
            .Execute(Arg.Is<ICommand[]>(commands => commands.Length == 2
                                                    && commands[0] == Tutu.Commands.Terminal.LeaveAlternateScreen
                                                    && commands[1] == Tutu.Commands.Events.DisableMouseCapture));

        _botoTerminal.ShowCursor();

        _messenger
            .Received(1)
            .UnregisterAll(_app);

        scopeFactory
            .Received(1)
            .CreateScope();

        _serviceProvider
            .Received(1)
            .GetService(typeof(IServiceScopeFactory));

        serviceProvider
            .Received(1)
            .GetService(typeof(MainPage));
    }

    [Fact(Timeout = 10_000)]
    public async Task RunAsyncHandleInput()
    {
        var fixture = new Fixture();

        var scope = Substitute.For<IServiceScope>();
        var scopeFactory = Substitute.For<IServiceScopeFactory>();
        scopeFactory.CreateScope()
            .Returns(scope);

        _serviceProvider.GetService(typeof(IServiceScopeFactory))
            .Returns(scopeFactory);

        var serviceProvider = Substitute.For<IServiceProvider>();
        scope.ServiceProvider
            .Returns(serviceProvider);

        serviceProvider.GetService(typeof(MainPage))
            .Returns(new MainPage(new MainViewModel(), _messenger));

        _options.StartupOptions = StartupOptions.None;

        var task = _app.RunAsync();

        _eventReader.Poll(Arg.Any<Duration?>())
            .Returns(true, true, true, true, true, true, false);

        _eventReader.Read()
            .Returns(Event.Resize(fixture.Create<ushort>(), fixture.Create<ushort>()),
                Event.Key(new KeyEvent(KeyCode.Enter, KeyModifiers.None)),
                Event.Mouse(new MouseEvent(MouseEventKind.Moved, fixture.Create<int>(), fixture.Create<int>(),
                    KeyModifiers.None)),
                Event.FocusGained, Event.FocusLost,
                Event.Pasted(fixture.Create<string>()));

        await Task.Delay(1_000);

        _app.Receive(new QuitMessage());

        await task;
        _eventReader
            .Received(Quantity.Within(6, int.MaxValue))
            .Poll(Arg.Any<Duration?>());

        _eventReader
            .Received(6)
            .Read();
    }

    public class MainPage : Page<MainViewModel>
    {
        public MainPage(MainViewModel pageViewModel, IMessenger messenger)
            : base(pageViewModel, messenger)
        {
        }
    }

    public class MainViewModel : ObservableObject
    {
    }
}
