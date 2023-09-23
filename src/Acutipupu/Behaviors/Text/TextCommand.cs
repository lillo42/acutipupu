using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Microsoft.Extensions.Options;

namespace Acutipupu.Behaviors.Text;

/// <summary>
/// The base for text command. 
/// </summary>
public abstract class TextCommand : ITextCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TextCommand"/> class.
    /// </summary>
    /// <param name="accessor">The <see cref="IComponentAccessor"/>.</param>
    /// <param name="options">The system options.</param>
    protected TextCommand(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options)
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
        return component is not IText text
            ? KeyCommandMatchResult.NoMatch
            : GetKey(text.KeyMap ?? Options.CurrentValue.TextKeyMap).IsMatch(keys);
    }

    /// <inheritdoc />
    public virtual ValueTask ExecuteAsync(ImmutableList<KeyMessage> keys, CancellationToken cancellationToken = default)
    {
        if (Accessor.Component is IText text)
        {
            OnExecute(keys, text);
        }

        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Gets the key binding.
    /// </summary>
    /// <param name="map">The <see cref="TextKeyMap"/>.</param>
    /// <returns>The <see cref="KeyBindingCollection"/> for that action.</returns>
    protected abstract KeyBindingCollection GetKey(TextKeyMap map);

    /// <summary>
    /// Execute a text action.
    /// </summary>
    /// <param name="keys">The <see cref="KeyMessage"/> collection.</param>
    /// <param name="text">The <see cref="IText"/>.</param>
    protected abstract void OnExecute(ImmutableList<KeyMessage> keys, IText text);
}
