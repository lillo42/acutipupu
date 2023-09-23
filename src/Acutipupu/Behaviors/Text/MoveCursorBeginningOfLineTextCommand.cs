using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// Move the cursor to the beginning of line. 
/// </summary>
public class MoveCursorBeginningOfLineTextCommand : TextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCursorBeginningOfLineTextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    public MoveCursorBeginningOfLineTextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(TextKeyMap map) => map.BeginningOfLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, IText text)
    {
        var indexes = text.Indexes;
        if (indexes.Count == 0)
        {
            return;
        }

        text.CursorPosition = text.CursorPosition with { Column = 0 };
        text.LastColumnPosition = new AtCursorPosition(0);
    }
}
