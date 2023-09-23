using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to the beginning of the buffer. 
/// </summary>
public class MoveCursorBeginningOfBufferTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorBeginningOfBufferTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorBeginningOfBufferTextCommand(IComponentAccessor accessor,
        IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.BeginningOfBuffer;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        text.CursorPosition = new CursorPosition(0, 0);
        text.LastColumnPosition = new AtCursorPosition(0);
    }
}
