using Acutipupu;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;

namespace Login;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    public MainPage(MainPageViewModel pageViewModel, IMessenger messenger)
        : base(pageViewModel, messenger)
    {
        Style = new Style { Margin = new(1, 1) };
        SplitDirection = Direction.Vertical;

        Add(Length(1), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(10), new Label()
                .SetText("username: "))
            .Add(Min(1), new Entry()
                .SetPlaceholder("test")));

        Add(Length(1), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(10), new Label()
                .SetText("password: "))
            .Add(Min(1), new Entry()
                .SetPlaceholder("star password")
                .SetEchoMode(EntryEchoMode.Password)));

        Add(Length(1), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(18), new Label()
                .SetText("confirm password: "))
            .Add(Min(1), new Entry()
                .SetPlaceholder("hidden password")
                .SetEchoMode(EntryEchoMode.Hidden)));
    }

    protected override void OnDeactivated()
    {
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
            Messenger.Send(new QuitMessage());
        }
    }
}

public partial class MainPageViewModel : ObservableObject
{
}
