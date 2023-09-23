using Acutipupu;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Styles;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;

namespace AltscreenToogle;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    public MainPage(MainPageViewModel viewModel, IMessenger messenger)
        : base(viewModel, messenger)
    {
        SplitDirection = Direction.Vertical;
        ExpandToFill = false;
        Style = new() { Margin = new(10, 10) };

        Add(Length(1), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(10), new Label()
                .SetText("You're in ")
                .SetStyle(new() { Foreground = Color.White }))
            .Add(Length(16), new Label()
                .SetStyle(new() { Foreground = Color.Indexed(204), Background = Color.Indexed(235) })
                .SetText<MainPageViewModel>(vm => vm.Value)));

        Add(Length(1), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(30), new Label()
                .SetText("space: switch modes • q: exit ")
                .SetStyle(new() { Foreground = Color.Indexed(241) })));
    }

    public void Receive(KeyMessage message)
    {
        if (message.Kind != KeyEventKind.Press)
        {
            return;
        }

        if (message.Key is KeyCode.EscKeyCode or KeyCode.CharKeyCode { Character: "q" }
            || message is { Key: KeyCode.CharKeyCode { Character: "c" }, Modifiers: KeyModifiers.Control })
        {
            PageViewModel.Value = "Bye!";

            Messenger.Send(new QuitMessage());
        }

        if (message.Key is KeyCode.CharKeyCode { Character: " " })
        {
            if (PageViewModel.Altscreen)
            {
                PageViewModel.Altscreen = false;
                PageViewModel.Value = " inline mode ";
                Messenger.Send(new LeaveAltScreenMessage());
            }
            else
            {
                PageViewModel.Altscreen = true;
                PageViewModel.Value = " altscreen mode ";
                Messenger.Send(new EnterAltScreenMessage());
            }
        }
    }
}

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private string _value = " altscreen mode ";

    [ObservableProperty] private bool _altscreen = true;
}
