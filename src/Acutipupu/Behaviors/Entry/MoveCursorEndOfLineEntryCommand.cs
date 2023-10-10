using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor end of line.
/// </summary>
public class MoveCursorEndOfLineEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorEndOfLineEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorEndOfLineEntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
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
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.EndOfLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var column = indexes[entry.CursorPosition.Row].Length;
        if (!IsMaxColumnSameAsRowLength)
        {
            column = Math.Max(0, column - 1);
        }

        entry.CursorPosition = entry.CursorPosition with { Column = column };
        entry.ScreenArea = entry.CalculateScreenArea();
    }
}
