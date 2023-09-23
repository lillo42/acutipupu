using Acutipupu;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Styles;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;
using Style = Acutipupu.Components.Style;

namespace CreditCardForm;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    public MainPage(MainPageViewModel viewModel, IMessenger messenger)
        : base(viewModel, messenger)
    {
        SplitDirection = Direction.Vertical;
        Style = new Style { Margin = new(1, 1) };

        Add(Length(1), new Label()
            .SetText("Total: $21.50"));

        Add(Length(3), new Container()
            .SetSplitDirection(Direction.Vertical)
            .Add(Length(1), new Container())
            .Add(Length(1), new Label()
                .SetText("Credit card number")
                .SetStyle(new() { Foreground = Color.LightMagenta }))
            .Add(Length(1), new Entry()
                .SetPlaceholder("4505 **** **** 1234")
                .AddBehavior(new CreditCardNumberBehavior())));

        Add(Length(3), new Container()
            .SetSplitDirection(Direction.Horizontal)
            .Add(Length(5), new Container()
                .SetSplitDirection(Direction.Vertical)
                .Add(Length(1), new Container())
                .Add(Length(1), new Label()
                    .SetText("EXP")
                    .SetStyle(new() { Foreground = Color.LightMagenta }))
                .Add(Length(1), new Entry()
                    .SetPlaceholder("MM/YY")
                    .AddBehavior(new ExpirationDateBehavior())))
            .Add(Length(2), new Container())
            .Add(Length(3), new Container()
                .SetSplitDirection(Direction.Vertical)
                .Add(Length(1), new Container())
                .Add(Length(1), new Label()
                    .SetText("CVV")
                    .SetStyle(new() { Foreground = Color.LightMagenta }))
                .Add(Length(1), new Entry()
                    .SetPlaceholder("XXX")
                    .AddBehavior(new CcvNumberBehavior()))));

        Add(Length(1), new Container());

        Add(Length(1), new Label()
            .SetText("Continue ->")
            .SetStyle(new() { Foreground = Color.DarkGray }));
    }

    protected override void OnDeactivated(){ }

    public void Receive(KeyMessage message)
    {
        if (message is { Kind: KeyEventKind.Press, Key: KeyCode.EscKeyCode })
        {
            Messenger.Send(new QuitMessage());
        }
    }
}

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private string _cardNumber = string.Empty;
    [ObservableProperty] private string _validThru = string.Empty;
    [ObservableProperty] private string _ccv = string.Empty;
}
