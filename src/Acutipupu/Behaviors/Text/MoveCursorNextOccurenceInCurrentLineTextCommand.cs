using System.Collections.Immutable;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Boto.Terminals;
using Microsoft.Extensions.Options;
using Tutu.Events;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to the next character occurence in current line.
/// </summary>
public abstract class MoveCursorNextOccurenceInCurrentLineTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorLastNonBlankCharacterInTheLineTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    protected MoveCursorNextOccurenceInCurrentLineTextCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <summary>
    /// Move the cursor to the char before the text.
    /// </summary>
    protected virtual bool BeforeTheOccurence { get; init; }

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys,IText text)
    {
        var search = string.Empty;

        for (var i = keys.Count - 1; i >= 0; i++)
        {
            if (keys[i].Key is not KeyCode.CharKeyCode ch)
            {
                continue;
            }

            search = ch.Character;
            break;
        }

        if (string.IsNullOrEmpty(search))
        {
            return;
        }

        var number = keys.GetNumber();
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }
        
        var currentText = text.Text;
        var (row, column) = text.CursorPosition;
        for (var i = column + 1; i < indexes[row].Length && number > 0; i++)
        {
            var ch = currentText[indexes[row][i]];
            if (char.IsHighSurrogate(ch) && char.IsHighSurrogate(search[0]))
            {
                if (ch == search[0] && currentText[indexes[row][i + 1]] == search[1])
                {
                    column = i;
                    number--;
                }
            }
            else if (search[0] == ch)
            {
                column = i;
                number--;
            }
        }

        if (number == 0)
        {
            if (BeforeTheOccurence && column > 0)
            {
                column--;
            }

            text.CursorPosition = new CursorPosition(row, column);
            text.LastColumnPosition = new AtCursorPosition(text.CursorPosition.Column);
        }
    }
}
