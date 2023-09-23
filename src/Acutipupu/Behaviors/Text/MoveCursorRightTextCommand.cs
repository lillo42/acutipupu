using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor right.
/// </summary>
public class MoveCursorRightTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorRightTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorRightTextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
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
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.Right;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys,IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var row = text.CursorPosition.Row;
        var column = Math.Min(indexes[row].Length - (IsMaxColumnSameAsRowLength ? 0 : 1),
            text.CursorPosition.Column + number);
        text.CursorPosition = new CursorPosition(row, column);
        text.LastColumnPosition = new AtCursorPosition(column);
    }
}
