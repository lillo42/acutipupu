using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Entry;

/// <summary>
/// The base for entry command. 
/// </summary>
public abstract class EntryCommand : IKeyCommand 
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntryCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    protected EntryCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
    {
        Accessor = accessor;
        Options = options;
    }

    /// <summary>
    /// The <see cref="IComponentAccessor"/>.
    /// </summary>
    protected virtual IComponentAccessor Accessor { get; }

    /// <summary>
    /// The system options.
    /// </summary>
    protected virtual IOptionsMonitor<AcutipupuAppOptions> Options { get; }

    /// <inheritdoc />
    public virtual KeyCommandMatchResult Match(ImmutableList<KeyMessage> keys)
    {
        var component = Accessor.Component;
        return component is not Components.Entry entry
            ? KeyCommandMatchResult.NoMatch
            : GetKey(entry.KeyMap ?? Options.CurrentValue.EntryKeyMap).IsMatch(keys);
    }

    /// <inheritdoc />
    public virtual ValueTask ExecuteAsync(ImmutableList<KeyMessage> keys, CancellationToken cancellationToken = default)
    {
        if (Accessor.Component is Components.Entry text)
        {
            OnExecute(keys, text);
        }

        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Gets the key binding.
    /// </summary>
    /// <param name="map">The <see cref="EntryKeyMap"/>.</param>
    /// <returns>The <see cref="KeyBindingCollection"/> for that action.</returns>
    protected abstract KeyBindingCollection GetKey(EntryKeyMap map);

    /// <summary>
    /// Execute a text action.
    /// </summary>
    /// <param name="keys">The <see cref="KeyMessage"/> collection.</param>
    /// <param name="entry">The <see cref="Components.Entry"/>.</param>
    protected abstract void OnExecute(ImmutableList<KeyMessage> keys, Components.Entry entry);
}
