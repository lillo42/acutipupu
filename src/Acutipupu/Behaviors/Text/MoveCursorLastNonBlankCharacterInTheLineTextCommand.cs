using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to last non-blank character in current line.
/// </summary>
public class MoveCursorLastNonBlankCharacterInTheLineTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorLastNonBlankCharacterInTheLineTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorLastNonBlankCharacterInTheLineTextCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.LastNonBlankCharacter;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys,IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var currentText = text.Text;
        var row = text.CursorPosition.Row;
        for (var i = indexes[row].Length - 1; i >= 0; i--)
        {
            if (!char.IsWhiteSpace(currentText[indexes[row][i]]))
            {
                text.CursorPosition = text.CursorPosition with { Column = i };
                text.LastColumnPosition = new AtCursorPosition(i);
                return;
            }
        }
    }
}
