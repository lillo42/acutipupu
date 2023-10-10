using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor to the next paragraph. 
/// </summary>
public class MoveCursorNextParagraphEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorNextParagraphEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorNextParagraphEntryCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.NextParagraph;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var row = entry.CursorPosition.Row + 1;
        while (number > 0 && row < indexes.Count)
        {
            if (indexes[row].Length == 0)
            {
                number--;
            }

            row++;
        }

        row--;

        entry.CursorPosition = number == 0
            ? new CursorPosition(row, 0)
            : new CursorPosition(indexes.Count - 1, Math.Max(0, indexes[^1].Length - 1));
        entry.LastColumnPosition = new AtColumnPosition(entry.CursorPosition.Column);
        entry.ScreenArea = entry.CalculateScreenArea();
    }
}
