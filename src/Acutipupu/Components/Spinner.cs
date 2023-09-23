using Acutipupu.Bindings;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using NodaTime;

namespace Acutipupu.Components;

/// <summary>
/// The spinner component.
/// </summary>
public partial class Spinner : Component, IAsyncDisposable
{
    private readonly Paragraph _paragraph = new();
    private Timer? _timer;

    /// <summary>
    /// The <see cref="SpinnerConfiguration"/>.
    /// </summary>
    [ObservableProperty] private SpinnerConfiguration _configuration = SpinnerConfiguration.Line;

    /// <summary>
    /// The spinner <see cref="Components.Style"/>.
    /// </summary>
    [ObservableProperty] private Style _style = new();

    /// <summary>
    /// The current frame index.
    /// </summary>
    [ObservableProperty] private int _currentFrameIndex;

    /// <summary>
    /// The current frame.
    /// </summary>
    [ObservableProperty] private string? _currentFrame;

    /// <summary>
    /// Initialize a new instance of <see cref="Spinner"/>.
    /// </summary>
    public Spinner()
    {
        Start();
    }

    /// <summary>
    /// The spinner <see cref="SpinnerConfiguration"/> bind property.
    /// </summary>
    public static IBindableProperty<Spinner, SpinnerConfiguration> ConfigurationProperty { get; } =
        new BindableProperty<Spinner, SpinnerConfiguration>(
            nameof(Configuration),
            spinner => spinner.Configuration,
            (spinner, value) => spinner.Configuration = value);

    /// <summary>
    /// The spinner <see cref="Style"/> bind property.
    /// </summary>
    public static IBindableProperty<Spinner, Style> StyleProperty { get; } =
        new BindableProperty<Spinner, Style>(
            nameof(Style),
            spinner => spinner.Style,
            (spinner, value) => spinner.Style = value);

    /// <summary>
    /// The spinner <see cref="CurrentFrameIndex"/> bind property.
    /// </summary>
    public static IBindableProperty<Spinner, int> CurrentFrameIndexProperty { get; } =
        new BindableProperty<Spinner, int>(
            nameof(CurrentFrameIndex),
            spinner => spinner.CurrentFrameIndex,
            (spinner, value) => spinner.CurrentFrameIndex = value);
    
    /// <summary>
    /// The spinner <see cref="CurrentFrame"/> bind property.
    /// </summary>
    public static IBindableProperty<Spinner, string?> CurrentFrameProperty { get; } =
        new BindableProperty<Spinner, string?>(
            nameof(CurrentFrame),
            spinner => spinner.CurrentFrame,
            (spinner, value) => spinner.CurrentFrame = value);

    /// <inheritdoc />
    public override void Render(IRenderContext context)
    {
        if (IsDirty)
        {
            Update();
        }

        _paragraph
            .SetText(CurrentFrame ?? string.Empty)
            .Render(context.Area, context.Buffer);
    }

    private void Update()
    {
        _paragraph
            .SetBlock(Style.ToBlock()!)
            .SetStyle(Style.ToBotoStyle())
            .SetScroll((0, 0));

        IsDirty = false;
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        _timer?.Dispose();
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        base.Dispose();
        if (_timer is not null)
        {
            await _timer.DisposeAsync();
            _timer = null;
        }
    }

    /// <summary>
    /// Start the spinner.
    /// </summary>
    public void Start()
    {
        _timer?.Dispose();
        _timer = new Timer(OnTick, this,
            Configuration.FrameRate.ToTimeSpan(),
            Configuration.FrameRate.ToTimeSpan());
    }

    /// <summary>
    /// Stop the spinner.
    /// </summary>
    public void Stop()
    {
        _timer?.Dispose();
        _timer = null;
    }

    partial void OnConfigurationChanged(SpinnerConfiguration value)
    {
        var frameRate = value.FrameRate.ToTimeSpan();
        _timer?.Change(frameRate, frameRate);
    }

    private static void OnTick(object? state)
    {
        if(state is not Spinner spinner)
        {
            return;
        }
        spinner.CurrentFrameIndex = (spinner.CurrentFrameIndex + 1) % spinner.Configuration.Frames.Count;
        spinner.CurrentFrame = spinner.Configuration.Frames[spinner.CurrentFrameIndex];
    }
}

/// <summary>
/// The spinner configuration.
/// </summary>
/// <param name="Frames">The frame collection.</param>
/// <param name="FrameRate">The frame rate.</param>
public record SpinnerConfiguration(IReadOnlyList<string> Frames, Duration FrameRate)
{
    /// <summary>
    /// The line spinner.
    /// </summary>
    public static SpinnerConfiguration Line { get; } =
        new(new[] { "|", "/", "-", "\\" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 10));

    /// <summary>
    /// The dot spinner.
    /// </summary>
    public static SpinnerConfiguration Dot { get; } =
        new(new[] { "⣾ ", "⣽ ", "⣻ ", "⢿ ", "⡿ ", "⣟ ", "⣯ ", "⣷ " },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 10));

    /// <summary>
    /// The mini dot spinner.
    /// </summary>
    public static SpinnerConfiguration MiniDot { get; } =
        new(new[] { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 12));

    /// <summary>
    /// The mini dot spinner.
    /// </summary>
    public static SpinnerConfiguration Jump { get; } =
        new(new[] { "⢄", "⢂", "⢁", "⡁", "⡈", "⡐", "⡠" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 10));

    /// <summary>
    /// The pulse spinner.
    /// </summary>
    public static SpinnerConfiguration Pulse { get; } =
        new(new[] { "█", "▓", "▒", "░" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 8));

    /// <summary>
    /// The points spinner.
    /// </summary>
    public static SpinnerConfiguration Points { get; } =
        new(new[] { "∙∙∙", "●∙∙", "∙●∙", "∙∙●" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 7));

    /// <summary>
    /// The globe spinner.
    /// </summary>
    public static SpinnerConfiguration Globe { get; } =
        new(new[] { "🌍", "🌎", "🌏" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 4));

    /// <summary>
    /// The moon spinner.
    /// </summary>
    public static SpinnerConfiguration Moon { get; } =
        new(new[] { "🌑", "🌒", "🌓", "🌔", "🌕", "🌖", "🌗", "🌘" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 8));

    /// <summary>
    /// The monkey spinner.
    /// </summary>
    public static SpinnerConfiguration Monkey { get; } =
        new(new[] { "🙈", "🙉", "🙊" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 3));

    /// <summary>
    /// The meter spinner.
    /// </summary>
    public static SpinnerConfiguration Meter { get; } =
        new(new[] { "▱▱▱", "▰▱▱", "▰▰▱", "▰▰▰", "▰▰▱", "▰▱▱", "▱▱▱" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 7));

    /// <summary>
    /// The hamburger spinner.
    /// </summary>
    public static SpinnerConfiguration Hamburger { get; } =
        new(new[] { "☱", "☲", "☴", "☲" },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 3));

    /// <summary>
    /// The ellipsis spinner.
    /// </summary>
    public static SpinnerConfiguration Ellipsis { get; } =
        new(new[] { "", ".", "..", "..." },
            Duration.FromMilliseconds(NodaConstants.MillisecondsPerSecond / 3));
}
