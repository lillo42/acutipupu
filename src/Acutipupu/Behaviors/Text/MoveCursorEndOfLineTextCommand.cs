using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor end of line.
/// </summary>
public class MoveCursorEndOfLineTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorEndOfLineTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorEndOfLineTextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
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
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.EndOfLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var column = indexes[text.CursorPosition.Row].Length;
        if (!IsMaxColumnSameAsRowLength)
        {
            column = Math.Max(0, column - 1);
        }

        text.CursorPosition = text.CursorPosition with { Column = column };
    }
}
