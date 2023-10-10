using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move screen page up.
/// </summary>
public class MoveScreenPageUpEntryCommand : EntryCommand
{
    private readonly ILastTextColumn _lastTextColumn;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveScreenPageUpEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="lastTextColumn">The <see cref="ILastTextColumn"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveScreenPageUpEntryCommand(IComponentAccessor accessor,
        ILastTextColumn lastTextColumn,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
        _lastTextColumn = lastTextColumn;
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.BeginningOfLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        var number = keys.GetNumber();
        var screenArea = entry.ScreenArea with { Y = Math.Max(entry.ScreenArea.Y - number * entry.ScreenArea.Height, 0) };
        (var cursorPosition, screenArea) = ScreenCal.CalculateCursorPosition(entry, screenArea, _lastTextColumn);
        entry.ScreenArea = screenArea;
        entry.CursorPosition = cursorPosition;
        entry.LastColumnPosition = new AtColumnPosition(cursorPosition.Column);
    }
}
