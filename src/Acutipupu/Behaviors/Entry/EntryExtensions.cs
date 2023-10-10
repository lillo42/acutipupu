using Acutipupu.Components;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Boto.Terminals;

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
    /// Calculate what should be cursor position inside <see cref="Entry.ScreenArea"/> respecting <see cref="Entry.Margin"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Components.Entry"/>.</param>
    /// <param name="lastTextColumn">The <see cref="ILastTextColumn"/>.</param>
    /// <returns>A new <see cref="Boto.Terminals.CursorPosition"/>.</returns>
    public static CursorPosition CalculateCursorPosition(this Components.Entry entry, ILastTextColumn lastTextColumn)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return new CursorPosition(0, 0);
        }

        var value = entry.ScreenArea;
        var margin = entry.Margin;
        var cursorPosition = entry.CursorPosition;

        var topWithMargin = Math.Min(value.Top + margin.Vertical, indexes.Count - 1);
        var bottomWithMargin = Math.Clamp(value.Bottom - margin.Vertical, 0, indexes.Count - 1);

        if (value.Height <= margin.Vertical * 2 + 1)
        {
            topWithMargin = value.Top;
            bottomWithMargin = value.Bottom;
        }

        if (topWithMargin > cursorPosition.Row)
        {
            cursorPosition = cursorPosition with { Row = topWithMargin };
        }
        else if (bottomWithMargin < cursorPosition.Row)
        {
            cursorPosition = cursorPosition with { Row = bottomWithMargin };
        }
        
        var maxCol = lastTextColumn.LastColumn(indexes[cursorPosition.Row].Length);
        var leftWithMargin = Math.Min(value.Left + margin.Horizontal, maxCol);
        var rightWithMargin = Math.Clamp(value.Right - margin.Horizontal, 0, maxCol);

        if (value.Width <= margin.Horizontal * 2 + 1)
        {
            leftWithMargin = value.Left;
            rightWithMargin = value.Right;
        }

        if (leftWithMargin > cursorPosition.Column)
        {
            cursorPosition = cursorPosition with { Column = leftWithMargin };
        }
        else if (rightWithMargin < cursorPosition.Column)
        {
            cursorPosition = cursorPosition with { Column = rightWithMargin };
        }

        return cursorPosition;
    }
}
