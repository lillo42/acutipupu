using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Boto.Terminals;

namespace Acutipupu.Behaviors.Entry;

internal static class ScreenCal
{
    /// <summary>
    /// Calculate the <see cref="CursorPosition"/> in the way it will be show inside the screen area.
    /// </summary>
    /// <param name="entry">The <see cref="Components.Entry"/>.</param>
    /// <param name="screenArea">The screen area as <see cref="Rect"/>.</param>
    /// <param name="lastTextColumn"></param>
    /// <returns></returns>
    public static (CursorPosition, Rect) CalculateCursorPosition(Components.Entry entry,
        Rect screenArea,
        ILastTextColumn lastTextColumn)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return (new CursorPosition(0, 0), screenArea with { X = 0, Y = 0 });
        }

        screenArea = screenArea with { Y = Math.Min(screenArea.Y, indexes.Count - 1) };
        screenArea = screenArea with
        {
            X = Math.Min(screenArea.X, lastTextColumn.LastColumn(indexes[screenArea.Y].Length))
        };

        var margin = entry.Margin;
        var cursorPosition = entry.CursorPosition;

        var topWithMargin = Math.Min(screenArea.Top + margin.Vertical, indexes.Count - 1);
        var bottomWithMargin = Math.Clamp(screenArea.Bottom - margin.Vertical, 0, indexes.Count - 1);

        if (screenArea.Height <= margin.Vertical * 2 + 1)
        {
            topWithMargin = screenArea.Top;
            bottomWithMargin = screenArea.Bottom;
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
        if (maxCol < screenArea.Left)
        {
            screenArea = screenArea with { X = Math.Max(maxCol - (margin.Horizontal * 2 + 1), 0) };
        }

        var leftWithMargin = Math.Min(screenArea.Left + margin.Horizontal, maxCol);
        var rightWithMargin = Math.Clamp(screenArea.Right - margin.Horizontal, 0, maxCol);

        if (screenArea.Width <= margin.Horizontal * 2 + 1)
        {
            leftWithMargin = screenArea.Left;
            rightWithMargin = screenArea.Right;
        }

        if (leftWithMargin > cursorPosition.Column)
        {
            cursorPosition = cursorPosition with { Column = leftWithMargin };
        }
        else if (rightWithMargin < cursorPosition.Column)
        {
            cursorPosition = cursorPosition with { Column = rightWithMargin };
        }

        return (cursorPosition, screenArea);
    }
}
