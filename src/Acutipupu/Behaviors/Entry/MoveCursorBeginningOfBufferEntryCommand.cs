using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Move the cursor to the beginning of the buffer. 
/// </summary>
public class MoveCursorBeginningOfBufferEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorBeginningOfBufferEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorBeginningOfBufferEntryCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.BeginningOfBuffer;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        var indexes = entry.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        entry.CursorPosition = new CursorPosition(0, 0);
        entry.LastColumnPosition = new AtColumnPosition(0);
        entry.ScreenArea = entry.CalculateScreenArea();
    }
}
