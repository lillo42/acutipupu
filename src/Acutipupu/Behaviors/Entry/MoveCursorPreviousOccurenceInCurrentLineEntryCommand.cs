using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;
using Tutu.Events;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor to the next character occurence in current line.
/// </summary>
public abstract class MoveCursorPreviousOccurenceInCurrentLineEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorLastNonBlankCharacterInTheLineEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    protected MoveCursorPreviousOccurenceInCurrentLineEntryCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <summary>
    /// Move the cursor to the char before the text.
    /// </summary>
    protected virtual bool AfterTheOccurence { get; init; }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.PreviousCharOccurence;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
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
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var text = entry.Text;
        var (row, column) = entry.CursorPosition;
        for (var i = column - 1; i >= 0 && number > 0; i--)
        {
            var ch = text[indexes[row][i]];
            if (char.IsHighSurrogate(ch) && char.IsHighSurrogate(search[0]))
            {
                if (ch == search[0] && text[indexes[row][i + 1]] == search[1])
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
            if (AfterTheOccurence && column < indexes[row].Length)
            {
                column++;
            }

            entry.CursorPosition = new CursorPosition(row, column);
            entry.LastColumnPosition = new AtColumnPosition(entry.CursorPosition.Column);
            entry.ScreenArea = entry.CalculateScreenArea();
        }
    }
}
