using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor to last non-blank character in current line.
/// </summary>
public class MoveCursorLastNonBlankCharacterInTheLineEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorLastNonBlankCharacterInTheLineEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorLastNonBlankCharacterInTheLineEntryCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.LastNonBlankCharacter;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var text = entry.Text;
        var row = entry.CursorPosition.Row;
        for (var i = indexes[row].Length - 1; i >= 0; i--)
        {
            if (!char.IsWhiteSpace(text[indexes[row][i]]))
            {
                entry.CursorPosition = entry.CursorPosition with { Column = i };
                entry.LastColumnPosition = new AtColumnPosition(i);
                entry.ScreenArea = entry.CalculateScreenArea();
                return;
            }
        }
    }
}
