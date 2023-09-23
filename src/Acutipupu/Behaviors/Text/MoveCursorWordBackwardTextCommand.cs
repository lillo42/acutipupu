using System.Collections.Immutable;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to word backward.
/// </summary>
public abstract class MoveCursorWordBackwardTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorWordBackwardTextCommand "/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    protected MoveCursorWordBackwardTextCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <summary>
    /// Flag indicating whether to move cursor to start or end of the word.
    /// </summary>
    protected virtual bool StartOfWord { get; init; }

    /// <summary>
    /// Flag indicating whether to ignore punctuation.
    /// </summary>
    protected virtual bool IgnorePunctuation { get; init; }

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var cursorPosition = text.CursorPosition;
        var currentText = text.Text;
        while (number > 0 && cursorPosition is not { Row: 0, Column: 0 })
        {
            cursorPosition = cursorPosition with { Column = cursorPosition.Column - 1 };
            if (cursorPosition.Column == -1)
            {
                cursorPosition = MoveUp(cursorPosition);
            }

            if ((StartOfWord && IsFirstLetterOfWord(cursorPosition))
                || (!StartOfWord && IsLastLetterOfWord(cursorPosition)))
            {
                number--;
            }
        }


        text.CursorPosition = cursorPosition;
        text.LastColumnPosition = new AtCursorPosition(cursorPosition.Column);
        return;

        CursorPosition MoveUp(CursorPosition position)
        {
            if (position.Row == 0)
            {
                return new CursorPosition(0, 0);
            }

            var row = position.Row - 1;

            return position with { Row = row, Column = Math.Max(0, indexes[row].Length - 1) };
        }

        bool IsFirstLetterOfWord(CursorPosition position)
        {
            var (row, column) = position;

            if ((row == 0 && column == 0) || (column == 0 && indexes[row].Length == 0))
            {
                return true;
            }

            var current = currentText[indexes[row][column]];
            if (column == 0 && !char.IsWhiteSpace(current))
            {
                return true;
            }

            // If given position is whitespace it's mean the current position isn't the first letter of the word.
            if (char.IsWhiteSpace(current))
            {
                return false;
            }

            if (column == 0)
            {
                return true;
            }

            var previous = currentText[indexes[row][column - 1]];
            // If the previous character is whitespace it's mean the current position is the first letter of the word.
            if (char.IsWhiteSpace(previous))
            {
                return true;
            }

            if (IgnorePunctuation)
            {
                return false;
            }

            if (char.IsNumber(current) && !char.IsNumber(previous))
            {
                return true;
            }

            if (char.IsSymbol(current) && !char.IsSymbol(previous))
            {
                return true;
            }

            if (char.IsSeparator(current) && !char.IsSeparator(previous))
            {
                return true;
            }

            if (char.IsLetter(current) && !char.IsLetter(previous))
            {
                return true;
            }

            if (char.IsPunctuation(current) && !char.IsPunctuation(previous))
            {
                return true;
            }

            return false;
        }

        bool IsLastLetterOfWord(CursorPosition position)
        {
            var (row, column) = position;

            if ((row == 0 && column == 0)
                || (column == 0 && indexes[row].Length == 0)
                || (column == indexes[row].Length - 1 && !char.IsWhiteSpace(currentText[indexes[row][column]])))
            {
                return true;
            }

            var current = currentText[indexes[row][column]];
            if (char.IsWhiteSpace(current))
            {
                return false;
            }

            var next = currentText[indexes[row][column + 1]];
            if (char.IsWhiteSpace(next))
            {
                return true;
            }

            if (IgnorePunctuation)
            {
                return false;
            }

            if (char.IsNumber(current) && !char.IsNumber(next))
            {
                return true;
            }

            if (char.IsSymbol(current) && !char.IsSymbol(next))
            {
                return true;
            }

            if (char.IsSeparator(current) && !char.IsSeparator(next))
            {
                return true;
            }

            if (char.IsLetter(current) && !char.IsLetter(next))
            {
                return true;
            }

            if (char.IsPunctuation(current) && !char.IsPunctuation(next))
            {
                return true;
            }

            return false;
        }
    }
}
