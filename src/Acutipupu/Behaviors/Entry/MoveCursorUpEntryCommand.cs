using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor up.
/// </summary>
public class MoveCursorUpEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorUpEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorUpEntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <summary>
    /// Is the max column same as the row length. 
    /// </summary>
    /// <remarks>
    /// It's useful for vim-like behavior.
    /// </remarks>
    protected virtual bool IsMaxColumnSameAsRowLength => true;

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.Up;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var row = Math.Max(0, entry.CursorPosition.Row - number);
        var endOfLine = Math.Max(0, indexes[row].Length - (IsMaxColumnSameAsRowLength ? 0 : 1));
        var column = Math.Min(entry.LastColumnPosition.Column, endOfLine);
        if (column == -1)
        {
            column = endOfLine;
        }

        entry.CursorPosition = new CursorPosition(row, column);
        entry.ScreenArea = entry.CalculateScreenArea();
    }
}
