using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor to line.
/// </summary>
public class MoveCursorToLineEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorRightEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorToLineEntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.GoToLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var currentText = entry.Text;
        var row = Math.Min(indexes.Count - 1, number);
        int column;
        for (column = 0; column < indexes[row].Length; column++)
        {
            if (!char.IsWhiteSpace(currentText[indexes[row][column]]))
            {
                break;
            }
        }

        entry.CursorPosition = new CursorPosition(row, column);
        entry.LastColumnPosition = new AtColumnPosition(column);
        entry.ScreenArea = entry.CalculateScreenArea();
    }
}
