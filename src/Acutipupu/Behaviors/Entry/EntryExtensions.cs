using Acutipupu.Components;
using Boto.Layouts;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// The entry extensions method.
/// </summary>
public static class EntryExtensions
{
    /// <summary>
    /// Calculate the screen area based on <see cref="Entry.CursorPosition"/> respecting <see cref="Entry.Margin"/>. 
    /// </summary>
    /// <param name="entry">The <see cref="Components.Entry"/>.</param>
    /// <returns>A new <see cref="Rect"/>.</returns>
    public static Rect CalculateScreenArea(this Components.Entry entry)
    {
        var screenArea = entry.ScreenArea;
        var indexes = entry.Indexes;
        var margin = entry.Margin;
        var (row, col) = entry.CursorPosition;

        var topWithMargin = Math.Max(row - margin.Vertical, 0);
        var bottomWithMargin = Math.Min(row + margin.Vertical, Math.Max(0, indexes.Count - 1));

        if (screenArea.Height <= margin.Vertical * 2 + 1)
        {
            topWithMargin = bottomWithMargin = row;
        }

        if (screenArea.Top > topWithMargin)
        {
            screenArea = screenArea with { Y = topWithMargin };
        }
        else if (screenArea.Bottom < bottomWithMargin)
        {
            screenArea = screenArea with { Y = Math.Max(0, bottomWithMargin - screenArea.Height) };
        }

        var leftWithMargin = Math.Max(col - margin.Horizontal, 0);
        var rightWithMargin = Math.Min(col + margin.Horizontal, indexes[row].Length);

        if (screenArea.Width <= margin.Horizontal * 2 + 1)
        {
            leftWithMargin = rightWithMargin = col;
        }

        if (screenArea.Left > leftWithMargin)
        {
            screenArea = screenArea with { X = leftWithMargin };
        }
        else if (screenArea.Right < rightWithMargin)
        {
            screenArea = screenArea with { X = Math.Max(0, rightWithMargin - screenArea.Width) };
        }

        return screenArea;
    }

    /// <summary>
    /// Calculate the index text position based on <see cref="Entry.CursorPosition"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Components.Entry"/>.</param>
    /// <returns>The text index position.</returns>
    public static int CalculateIndexTextPosition(this Components.Entry entry)
    {
        var (row, col) = entry.CursorPosition;
        var text = entry.Text;
        var index = 0;

        while (index < row)
        {
            index = text.IndexOf('\n', index) + 1;
        }

        index += col;

        return index;
    }
}
