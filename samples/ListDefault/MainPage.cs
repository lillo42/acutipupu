using Acutipupu;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;
using Style = Acutipupu.Components.Style;

namespace ListDefault;

public class MainPage : Page<MainPageViewModel>, IRecipient<KeyMessage>
{
    public MainPage(MainPageViewModel pageViewModel, IMessenger messenger)
        : base(pageViewModel, messenger)
    {
        Style = new Style { Margin = new(1, 1) };
        SplitDirection = Direction.Vertical;

        Add(Length(1), new Label()
            .SetText("My Fave Things")
            .SetStyle(new() { Background = Color.Indexed(62), Foreground = Color.Indexed(230) }));

        Add(Min(1), new ListView()
            .SetStyle(new() { Highlight = new Style { Foreground = Color.Indexed(170) } })
            .SetBinding(ListView.ItemsProperty, MainPageViewModel.ItemsProperty));
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

        if (message.Key is KeyCode.CharKeyCode { Character: "/" } ch)
        {
        }
    }
}

public partial class MainPageViewModel : ObservableObject
{
    public static readonly IList<IListItem> DefaultItems = new List<IListItem>
    {
        new ListItem("Raspberry Pi’s", "I have ’em all over my house"),
        new ListItem("Nutella", "It's good on toast"),
        new ListItem("Bitter melon", "It cools you down"),
        new ListItem("Nice socks", "And by that I mean socks without holes"),
        new ListItem("Eight hours of sleep", "I had this once"),
        new ListItem("Cats", "Usually"),
        new ListItem("Plantasia, the album", "My plants love it too"),
        new ListItem("Pour over coffee", "It takes forever to make though"),
        new ListItem("VR", "Virtual reality...what is there to say?"),
        new ListItem("Noguchi Lamps", "Such pleasing organic forms"),
        new ListItem("Linux", "Pretty much the best OS"),
        new ListItem("Business school", "Just kidding"),
        new ListItem("Pottery", "Wet clay is a great feeling"),
        new ListItem("Shampoo", "Nothing like clean hair"),
        new ListItem("Table tennis", "It’s surprisingly exhausting"),
        new ListItem("Milk crates", "Great for packing in your extra stuff"),
        new ListItem("Afternoon tea", "Especially the tea sandwich part"),
        new ListItem("Stickers", "The thicker the vinyl the better"),
        new ListItem("20° Weather", "Celsius, not Fahrenheit"),
        new ListItem("Warm light", "Like around 2700 Kelvin"),
        new ListItem("The vernal equinox", "The autumnal equinox is pretty good too"),
        new ListItem("Gaffer’s tape", "Basically sticky fabric"),
        new ListItem("Terrycloth", "In other words, towel fabric")
    };

    [ObservableProperty] private int? _selectedIndex;

    public static IBindableProperty<MainPageViewModel, int?> SelectedIndexProperty { get; } =
        new BindableProperty<MainPageViewModel, int?>(
            nameof(SelectedIndex),
            input => input.SelectedIndex,
            (input, value) => input.SelectedIndex = value);


    [ObservableProperty] private IList<IListItem> _items = DefaultItems;

    public static IBindableProperty<MainPageViewModel, IList<IListItem>> ItemsProperty { get; } =
        new BindableProperty<MainPageViewModel, IList<IListItem>>(
            nameof(Items),
            viewModel => viewModel.Items,
            (viewModel, value) => viewModel.Items = value);
}

public class ListItem : IListItem
{
    public ListItem(string title, string description)
    {
        Title = title;
        Description = description;
        Text = new Text(new List<Spans>
        {
            new(Title, new() { AddModifier = Modifier.Bold }),
            new(Description, new() { Foreground = Color.Indexed(240) })
        });
    }

    public string Title { get; }
    public string Description { get; }
    public Text Text { get; }
    public Style? Style => null;
}
