using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Insert a new line into the entry.
/// </summary>
public class NewLineEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewLineEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The <see cref="AcutipupuAppOptions"/>.</param>
    public NewLineEntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override KeyBindingCollection GetKey(EntryKeyMap map) => map.NewLine;

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        if (!entry.MultiLine)
        {
            return;
        }

        var text = entry.Text;
        var newLine = entry.NewLine ?? Options.CurrentValue.NewLine;
        var (row, _) = entry.CursorPosition;
        var index = entry.CalculateIndexTextPosition();
        var sb = Pools.StringBuilderPool.Get();
        try
        {
            entry.Text = sb.Append(text)
                .Insert(index, newLine)
                .ToString();
            
            entry.CursorPosition = new CursorPosition(row + 1, 0);
            entry.ScreenArea = entry.CalculateScreenArea();
        }
        finally
        {
            sb.Clear();
            Pools.StringBuilderPool.Return(sb);
        }
    }
}
