using Boto.Layouts;
using Boto.Terminals;
using Boto.Texts;
using Boto.Widgets.Extensions;

namespace Acutipupu.Components;

public partial class Entry
{
    private Rect _lastTextArea = new(0, 0, 0, 0);

    private void UpdateParagraph()
    {
        _paragraph
            .SetText(_finalText)
            .SetBlock(Style.Base.ToBlock()!)
            .SetStyle(Style.Base.ToBotoStyle());

        IsDirty = false;
    }

    /// <inheritdoc />
    public override void Render(IRenderContext context)
    {
        if (IsDirty)
        {
            UpdateParagraph();
        }

        var area = context.Area;
        if (MultiLine)
        {
            while (_finalText.Lines.Count < _indexes.Count + area.Height)
            {
                _finalText.Lines.Add(new Spans(EndOfBufferCharacter));
            }
        }

        if (Scroll != null)
        {
            _paragraph.Scroll = Scroll.Value;

            if (IsActive)
            {
                context.CursorPosition = null;
            }
        }
        else
        {
            var screenArea = ScreenArea;
            if (screenArea.Width != area.Width || screenArea.Height != area.Height)
            {
                screenArea = screenArea with { Height = context.Area.Height, Width = context.Area.Width };
            }

            var cursorPosition = CursorPosition;

            // Set X position
            if (cursorPosition.Row < screenArea.Top || cursorPosition.Row > screenArea.Bottom)
            {
                screenArea = screenArea with
                {
                    Y = Math.Clamp(0, _indexes.Count - 1, cursorPosition.Row - screenArea.Height / 2)
                };
            }
            else
            {
                var margin = Math.Min(Margin.Vertical, screenArea.Height / 2);
                var diff = cursorPosition.Row - screenArea.Top;
                if (diff < margin)
                {
                    screenArea = screenArea with { Y = Math.Max(0, cursorPosition.Row - margin) };
                }

                diff = screenArea.Bottom - cursorPosition.Row;
                if (diff < margin)
                {
                    screenArea = screenArea with { Y = Math.Min(_indexes.Count - 1, cursorPosition.Row + margin) };
                }
            }

            // Set Y position
            if (cursorPosition.Column < screenArea.Left || cursorPosition.Column > screenArea.Right)
            {
                screenArea = screenArea with
                {
                    X = Math.Clamp(0, _indexes[cursorPosition.Row].Length - 1,
                        cursorPosition.Column - screenArea.Width / 2)
                };
            }
            else
            {
                var margin = Math.Min(Margin.Horizontal, screenArea.Width / 2);
                var diff = cursorPosition.Column - screenArea.Left;
                if (diff < margin)
                {
                    screenArea = screenArea with { X = Math.Max(0, cursorPosition.Column - margin) };
                }

                diff = screenArea.Right - cursorPosition.Column;
                if (diff < margin)
                {
                    screenArea = screenArea with
                    {
                        X = Math.Min(_indexes[cursorPosition.Row].Length - 1, cursorPosition.Column + margin)
                    };
                }
            }

            _lastTextArea = screenArea;
            _paragraph.Scroll = (screenArea.X, screenArea.Y);

            if (IsActive)
            {
                var relativeRow = cursorPosition.Row - screenArea.Y;
                var relativeColumn = cursorPosition.Column - screenArea.X;
                context.CursorPosition = new CursorPosition(area.Y + relativeRow, area.X + relativeColumn);
            }
        }

        _paragraph.Render(context.Area, context.Buffer);
    }
}
