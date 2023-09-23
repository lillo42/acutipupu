using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to line.
/// </summary>
public class MoveCursorToLineTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorRightTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorToLineTextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.GoToLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys,IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var currentText = text.Text;
        var row = Math.Min(indexes.Count - 1, number);
        int column;
        for (column = 0; column < indexes[row].Length; column++)
        {
            if (!char.IsWhiteSpace(currentText[indexes[row][column]]))
            {
                break;
            }
        }

        text.CursorPosition = new CursorPosition(row, column);
        text.LastColumnPosition = new AtCursorPosition(column);
    }
}
