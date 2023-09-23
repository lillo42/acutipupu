using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor end of buffer.
/// </summary>
public class MoveCursorEndOfBufferTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorEndOfBufferTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorEndOfBufferTextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
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
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.EndOfBuffer;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys,IText text) 
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var row = indexes.Count - 1;
        var column = indexes[row].Length;
        if (!IsMaxColumnSameAsRowLength)
        {
            column = Math.Max(0, column - 1);
        }

        text.CursorPosition = new CursorPosition(row, column);
        text.LastColumnPosition = new AtCursorPosition(column);
    }
}
