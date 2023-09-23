using Acutipupu;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Styles;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;
using Style = Acutipupu.Components.Style;

namespace ListSimple;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    private static readonly string[] s_items =
    {
        "Ramen", "Tomato Soup", "Hamburgers", "Cheeseburgers", "Currywurst", "Okonomiyaki", "Pasta", "Pizza",
        "Sushi", "Fillet Mignon", "Caviar", "Just Wine"
    };

    public MainPage(MainPageViewModel pageViewModel, IMessenger messenger)
        : base(pageViewModel, messenger)
    {
        Style = new() { Margin = new Margin(1, 1) };

        Add(Percentage(100), new ListView()
            .SetStyle(new ListStyle
            {
                Base = new Style { Title = "What do you want for dinner?" },
                Highlight = new Style { Foreground = Color.Indexed(170) }
            })
            .AddItems(s_items)
            .SetBinding(ListView.SelectedItemIndexProperty, MainPageViewModel.SelectedIndexProperty));
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

        if (message.Key is KeyCode.EnterKeyCode)
        {
            var index = PageViewModel.SelectedIndex ?? 0;
            Components.Clear();
            Constraints.Clear();
            Add(Percentage(100), new Label()
                .SetText($"{s_items[index]} Sound good to me"));
            Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
            Messenger.Send(new QuitMessage());
        }
    }
}

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty] private int? _selectedIndex;

    public static IBindableProperty<MainPageViewModel, int?> SelectedIndexProperty { get; } =
        new BindableProperty<MainPageViewModel, int?>(
            nameof(SelectedIndex),
            input => input.SelectedIndex,
            (input, value) => input.SelectedIndex = value);
}
