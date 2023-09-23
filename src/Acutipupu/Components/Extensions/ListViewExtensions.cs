namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="ListView"/> extensions method.
/// </summary>
public static class ListViewExtensions
{
    /// <summary>
    /// Change the <see cref="ListView.Style" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="ListView.Style"/> as <paramref name="style"/>.</returns>
    public static ListView SetStyle(this ListView list, ListStyle style)
    {
        // list.Style = style;
        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.HighlightSymbol" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="highlightSymbol">The highlight symbol.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.HighlightSymbol"/> as <paramref name="highlightSymbol"/>.</returns>
    public static ListView SetHighlightSymbol(this ListView list, string? highlightSymbol)
    {
        list.HighlightSymbol = highlightSymbol;
        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.RepeatHighlightSymbol" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="repeatHighlightSymbol">Flag indication if the list should repeat highlight symbol.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.RepeatHighlightSymbol"/> as <paramref name="repeatHighlightSymbol"/>.</returns>
    public static ListView SetRepeatHighlightSymbol(this ListView list, bool repeatHighlightSymbol)
    {
        list.RepeatHighlightSymbol = repeatHighlightSymbol;
        return list;
    }

    /// <summary>
    /// Enable the <see cref="ListView.RepeatHighlightSymbol" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.RepeatHighlightSymbol"/> as true.</returns>
    public static ListView EnableRepeatHighlightSymbol(this ListView list)
    {
        list.RepeatHighlightSymbol = true;
        return list;
    }

    /// <summary>
    /// Disable the <see cref="ListView.RepeatHighlightSymbol" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.RepeatHighlightSymbol"/> as false.</returns>
    public static ListView DisableRepeatHighlightSymbol(this ListView list)
    {
        list.RepeatHighlightSymbol = false;
        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.SelectedItemIndex" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="selectedItemIndex">The current selected item.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.SelectedItemIndex"/> as <paramref name="selectedItemIndex"/>.</returns>
    public static ListView SetSelectedItemIndex(this ListView list, int? selectedItemIndex)
    {
        list.SelectedItemIndex = selectedItemIndex;
        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.OffsetItem" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="offsetItem">The current offset item.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.OffsetItem"/> as <paramref name="offsetItem"/>.</returns>
    public static ListView SetOffsetItem(this ListView list, int offsetItem)
    {
        list.OffsetItem = offsetItem;
        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The new item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> as <paramref name="items"/>.</returns>
    public static ListView SetItems(this ListView list, params IListItem[] items)
    {
        list.Items.Clear();
        foreach (var item in items)
        {
            list.Items.Add(item);
        }

        return list;
    }

    /// <summary>
    /// Change the <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The new item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> as <paramref name="items"/>.</returns>
    public static ListView SetItems(this ListView list, IEnumerable<IListItem> items)
    {
        list.Items.Clear();
        foreach (var item in items)
        {
            list.Items.Add(item);
        }

        return list;
    }

    /// <summary>
    /// Add items to <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> with <paramref name="items"/>.</returns>
    public static ListView AddItems(this ListView list, params IListItem[] items)
    {
        foreach (var item in items)
        {
            list.Items.Add(item);
        }

        return list;
    }

    /// <summary>
    /// Add items to <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> with <paramref name="items"/>.</returns>
    public static ListView AddItems(this ListView list, IEnumerable<IListItem> items)
    {
        foreach (var item in items)
        {
            list.Items.Add(item);
        }

        return list;
    }

    /// <summary>
    /// Add items to <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> with <paramref name="items"/>.</returns>
    public static ListView AddItems(this ListView list, params string[] items)
    {
        foreach (var item in items)
        {
            list.Items.Add(new ListItem(item, null));
        }

        return list;
    }

    /// <summary>
    /// Add items to <see cref="ListView.Items" />.
    /// </summary>
    /// <param name="list">The <see cref="ListView"/>.</param>
    /// <param name="items">The item collection.</param>
    /// <returns>The given <paramref name="list"/> with <see cref="ListView.Items"/> with <paramref name="items"/>.</returns>
    public static ListView AddItems(this ListView list, IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            list.Items.Add(new ListItem(item, null));
        }

        return list;
    }
}
