using System.Collections.Immutable;
using Acutipupu.Messages;
using Boto.Terminals;
using Microsoft.Extensions.Options;
using Tutu.Events;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// Insert character entry command. 
/// </summary>
public abstract class InsertCharacterEntryCommand : EntryCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InsertCharacterEntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The <see cref="AcutipupuAppOptions"/>.</param>
    protected InsertCharacterEntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
        : base(accessor, options)
    {
    }

    /// <inheritdoc/>
    protected override void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry)
    {
        if (keys.First().Key is not KeyCode.CharKeyCode ch)
        {
            return;
        }

        var (row, col) = entry.CursorPosition;
        var index = entry.CalculateIndexTextPosition();
        var sb = Pools.StringBuilderPool.Get();

        try
        {
            entry.Text = sb.Append(entry.Text)
                .Insert(index, ch.Character)
                .ToString();
            
            entry.CursorPosition = new CursorPosition(row, col + 1);
            entry.ScreenArea = entry.CalculateScreenArea();
        }
        finally
        {
            sb.Clear();
            Pools.StringBuilderPool.Return(sb);
        }
    }
}
