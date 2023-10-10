using Acutipupu.Behaviors.Entry;
using Boto.Terminals;
using Boto.Texts;
using Boto.Widgets.Extensions;

namespace Acutipupu.Components;

public partial class Entry
{
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

        if (ScreenArea.Height != area.Height || ScreenArea.Width != area.Width)
        {
            ScreenArea = ScreenArea with { Width = area.Width, Height = area.Height };
            // CursorPosition = this.CalculateCursorPosition();
        }
        
        _paragraph.Scroll = (ScreenArea.X, ScreenArea.Y);
        _paragraph.Render(context.Area, context.Buffer);

        if (IsActive)
        {
            var (row, col) = CursorPosition;
            var screenArea = ScreenArea;

            if (screenArea.Top <= row
                && screenArea.Bottom >= row
                && screenArea.Right >= col
                && screenArea.Left <= col)
            {
                var newRow = row - screenArea.Top;
                var newColumn = col - screenArea.Left;

                context.CursorPosition = new CursorPosition(area.Y + newRow, area.X + newColumn);
            }
            else
            {
                context.CursorPosition = null;
            }
        }
    }
}
