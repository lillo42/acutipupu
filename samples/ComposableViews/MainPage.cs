using Acutipupu;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Styles;
using Boto.Widgets;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;
using Style = Acutipupu.Components.Style;

namespace ComposableViews;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    public MainPage(MainPageViewModel pageViewModel, IMessenger messenger)
        : base(pageViewModel, messenger)
    {
        PageViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(MainPageViewModel.Seconds) && PageViewModel.Seconds < 0)
            {
                Messenger.Send(new QuitMessage());
            }
        };

        SplitDirection = Direction.Vertical;
        Style = new() { Margin = new Margin(1, 1) };

        Add(Length(6), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .DisableExpandToFill()
            .Add(Length(10), new Container()
                .SetSplitDirection(Direction.Vertical)
                .Add(Length(2), new Container())
                .Add(Length(1), new Label()
                    .SetText<MainPageViewModel>(vm => vm.TimerText))
                .Add(Length(2), new Container())
                .SetStyle<MainPageViewModel>(vm => vm.TimerStyle))
            .Add(Length(10), new Container()
                .SetSplitDirection(Direction.Vertical)
                .Add(Length(2), new Container())
                .Add(Length(1), new Spinner()
                    .SetConfiguration<MainPageViewModel>(x => x.SpinnerConfiguration))
                .SetStyle<MainPageViewModel>(x => x.SpinnerStyle)
            ));

        Add(Length(10), new Label()
            .SetStyle(new() { Foreground = Color.Indexed(241) })
            .SetText("tab: focus next • n: new timer/spinner • esc/q: exit"));
    }

    public void Receive(KeyMessage message)
    {
        if (message is not { Kind: KeyEventKind.Press })
        {
            return;
        }

        if (message.Key is KeyCode.EscKeyCode)
        {
            Messenger.Send(new QuitMessage());
            return;
        }

        if (message.Key is KeyCode.CharKeyCode ch)
        {
            if (ch.Character == "q")
            {
                Messenger.Send(new QuitMessage());
            }

            if (ch.Character == "n")
            {
                PageViewModel.ResetTimer();
                PageViewModel.NextSpinner();
            }

            if (ch.Character == "t")
            {
                (PageViewModel.TimerStyle, PageViewModel.SpinnerStyle) =
                    (PageViewModel.SpinnerStyle, PageViewModel.TimerStyle);
            }
        }
    }
}

public partial class MainPageViewModel : ObservableObject, IDisposable
{
    private readonly SpinnerConfiguration[] _spinnerConfigurations =
    {
        SpinnerConfiguration.Line, SpinnerConfiguration.Dot, SpinnerConfiguration.MiniDot,
        SpinnerConfiguration.Jump, SpinnerConfiguration.Pulse, SpinnerConfiguration.Points,
        SpinnerConfiguration.Globe, SpinnerConfiguration.Moon, SpinnerConfiguration.Monkey,
        SpinnerConfiguration.Meter, SpinnerConfiguration.Hamburger, SpinnerConfiguration.Ellipsis,
    };

    [ObservableProperty] private string _helpText = "t: focus next • n: new timer • q: exit";

    [ObservableProperty] private string _timerText = "";

    [ObservableProperty] private SpinnerConfiguration _spinnerConfiguration = SpinnerConfiguration.Line;

    [ObservableProperty] private int _seconds = 61;

    [ObservableProperty] private Style _timerStyle = new()
    {
        Borders = Borders.All, BorderStyle = new() { Foreground = Color.Indexed(69) },
    };

    [ObservableProperty] private Style _spinnerStyle = new();

    private readonly Timer _timer;

    public MainPageViewModel()
    {
        _timer = new Timer(state =>
            {
                var vm = (MainPageViewModel)state!;
                vm.Seconds--;
                if (vm.Seconds < 0)
                {
                    return;
                }

                TimerText = $"{vm.Seconds}s";
            }, this,
            TimeSpan.FromSeconds(0),
            TimeSpan.FromSeconds(1));
    }

    public void ResetTimer()
    {
        Seconds = 60;
        TimerText = $"{Seconds}s";
    }

    public void NextSpinner()
    {
        var index = Array.IndexOf(_spinnerConfigurations, SpinnerConfiguration);
        index = (index + 1) % _spinnerConfigurations.Length;
        SpinnerConfiguration = _spinnerConfigurations[index];
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}
