using System.Collections.ObjectModel;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Boto.Texts;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Tutu.Events;

namespace Acutipupu.Components;

/// <summary>
/// The list <see cref="Component"/>
/// </summary>
public partial class ListView : Component, INavegable, IRecipient<KeyMessage>
{
    private readonly List _list = new();
    private readonly ListState _state = new();
    private readonly ObservableCollection<IListItem> _items = new();

    /// <summary>
    /// The <see cref="ListStyle"/>.
    /// </summary>
    [ObservableProperty] private ListStyle _style = new();

    /// <summary>
    /// The highlight symbol.
    /// </summary>
    [ObservableProperty] private string? _highlightSymbol;

    /// <summary>
    /// Flag indicating to repeat the highlight symbol.
    /// </summary>
    [ObservableProperty] private bool _repeatHighlightSymbol;

    /// <summary>
    /// The selected item index.
    /// </summary>
    [ObservableProperty] private int? _selectedItemIndex;

    /// <summary>
    /// The item offset.
    /// </summary>
    [ObservableProperty] private int _offsetItem;

    /// <summary>
    /// The list key map.
    /// </summary>
    [ObservableProperty] private ListKeyMap _keyMap = new();

    /// <summary>
    /// Initialize a new instance of <see cref="ListView"/>.
    /// </summary>
    public ListView()
    {
        _items.CollectionChanged += (_, _) => IsDirty = true;
    }

    /// <summary>
    /// The <see cref="IListItem"/> collection.
    /// </summary>
    public IList<IListItem> Items => _items;

    /// <summary>
    /// The current selected item index.
    /// </summary>
    public Text? SelectedItem => SelectedItemIndex is null ? null : _items[SelectedItemIndex!.Value].Text;

    /// <inheritdoc />
    public override void Render(IRenderContext context)
    {
        if (IsDirty)
        {
            Update();
        }

        _list.Render(context.Area, context.Buffer, _state);
    }

    /// <inheritdoc />
    public void Receive(KeyMessage message)
    {
        /*var selected = SelectedItemIndex;
        if (KeyMap.Up.Match(message))
        {
            selected ??= Items.Count;
            SelectedItemIndex = Math.Max(0, selected.Value - 1);
        }
        else if (KeyMap.Down.Match(message))
        {
            selected ??= -1;
            SelectedItemIndex = Math.Min(Items.Count - 1, selected.Value + 1);
        }
        else if (KeyMap.GoToStart.Match(message))
        {
            SelectedItemIndex = 0;
        }
        else if (KeyMap.GoToEnd.Match(message))
        {
            SelectedItemIndex = Items.Count - 1;
        }*/
    }

    /// <summary>
    /// The style <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, ListStyle> StyleProperty { get; } =
        new BindableProperty<ListView, ListStyle>(
            nameof(Style),
            input => input.Style,
            (input, value) => input.Style = value);

    /// <summary>
    /// The highlight symbol <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, string?> HighlightSymbolProperty { get; } =
        new BindableProperty<ListView, string?>(
            nameof(HighlightSymbol),
            input => input.HighlightSymbol,
            (input, value) => input.HighlightSymbol = value);

    /// <summary>
    /// The repeat highlight symbol <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, bool> RepeatHighlightSymbolProperty { get; } =
        new BindableProperty<ListView, bool>(
            nameof(RepeatHighlightSymbol),
            input => input.RepeatHighlightSymbol,
            (input, value) => input.RepeatHighlightSymbol = value);

    /// <summary>
    /// The selected item index <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, int?> SelectedItemIndexProperty { get; } =
        new BindableProperty<ListView, int?>(
            nameof(SelectedItemIndex),
            input => input.SelectedItemIndex,
            (input, value) => input.SelectedItemIndex = value);

    /// <summary>
    /// The offset item <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, int> OffsetItemProperty { get; } =
        new BindableProperty<ListView, int>(
            nameof(OffsetItem),
            input => input.OffsetItem,
            (input, value) => input.OffsetItem = value);

    /// <summary>
    /// The offset item <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<ListView, IList<IListItem>> ItemsProperty { get; } =
        new BindableProperty<ListView, IList<IListItem>>(
            nameof(Items),
            input => input.Items,
            (input, value) =>
            {
                input.Items.Clear();
                foreach (var item in value)
                {
                    input.Items.Add(item);
                }
            });

    private void Update()
    {
        _list
            .SetBlock(Style.Base.ToBlock()!)
            .SetStyle(Style.Base.ToBotoStyle())
            .SetStartCorner(Style.StartCorner)
            .SetHighlightStyle(Style.Highlight.ToBotoStyle())
            .SetHighlightSymbol(HighlightSymbol!)
            .SetRepeatHighlightSymbol(RepeatHighlightSymbol);

        _list.Items.Clear();
        _list.AddItems(_items,
            item => new Boto.Widgets.ListItem(item.Text, item.Style?.ToBotoStyle() ?? Style.Items.ToBotoStyle()));

        IsDirty = false;
    }

    partial void OnSelectedItemIndexChanged(int? value)
    {
        _state.Selected = value;
    }

    partial void OnOffsetItemChanged(int value)
    {
        _state.Offset = value;
    }

    /// <inheritdoc />
    public bool IsActive { get; set; }
}

/// <summary>
/// The <see cref="ListView"/> items.
/// </summary>
public interface IListItem
{
    /// <summary>
    /// The item <see cref="Text"/>.
    /// </summary>
    Text Text { get; }

    /// <summary>
    /// The item <see cref="Style"/>.
    /// </summary>
    Style? Style { get; }
}

/// <summary>
/// The <see cref="ListView"/> items.
/// </summary>
/// <param name="Text">The <see cref="Boto.Texts.Text"/>.</param>
/// <param name="Style">The <see cref="Components.Style"/>.</param>
public record ListItem(Text Text, Style? Style) : IListItem;

/// <summary>
/// The <see cref="ListView"/> style.
/// </summary>
public partial class ListStyle : ObservableObject
{
    /// <summary>
    /// The <see cref="Corner"/> to start the list.
    /// </summary>
    [ObservableProperty] private Corner _startCorner = Corner.TopLeft;

    /// <summary>
    /// The base <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _base = new();

    /// <summary>
    /// The highlight <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _highlight = new();

    /// <summary>
    /// The items <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _items = new();
}

/// <summary>
/// The key map for <see cref="ListView"/>.
/// </summary>
public class ListKeyMap
{
    /// <summary>
    /// The up key bindings.
    /// </summary>
    public KeyBindingCollection Up { get; set; } = new()
    {
        new KeyBinding(KeyCode.Up), new KeyBinding(new KeyCode.CharKeyCode("k"))
    };

    /// <summary>
    /// The down key bindings.
    /// </summary>
    public KeyBindingCollection Down { get; set; } = new()
    {
        new KeyBinding(KeyCode.Down), new KeyBinding(new KeyCode.CharKeyCode("j"))
    };

    /// <summary>
    /// The go to start key bindings.
    /// </summary>
    public KeyBindingCollection GoToStart { get; set; } = new()
    {
        new KeyBinding(KeyCode.Home), new KeyBinding(new KeyCode.CharKeyCode("g"))
    };

    /// <summary>
    /// The go to end key bindings.
    /// </summary>
    public KeyBindingCollection GoToEnd { get; set; } = new()
    {
        new KeyBinding(KeyCode.End), new KeyBinding(new KeyCode.CharKeyCode("g"), KeyModifiers.Alt)
    };
}
