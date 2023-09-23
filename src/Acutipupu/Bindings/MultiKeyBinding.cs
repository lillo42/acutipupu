using Acutipupu.Messages;

namespace Acutipupu.Bindings;

/// <summary>
/// The multi key binding. 
/// </summary>
/// <param name="Bindings">The list of all key binding</param>
public sealed record MultiKeyBinding(IReadOnlyList<IKeyBinding> Bindings) : IKeyBinding
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MultiKeyBinding"/> class.
    /// </summary>
    /// <param name="Bindings"></param>
    public MultiKeyBinding(params IKeyBinding[] Bindings)
        : this(Bindings.ToList())
    {
    }

    /// <inheritdoc />
    public KeyCommandMatchResult Match(IReadOnlyCollection<KeyMessage> messages)
    {
        if (messages.Count > Bindings.Count)
        {
            return KeyCommandMatchResult.NoMatch;
        }

        var index = 0;
        foreach (var message in messages)
        {
            var binding = Bindings[index];
            if (binding is KeyBinding kb && !kb.IsMatch(message))
            {
                return KeyCommandMatchResult.NoMatch;
            }

            if (binding is NumberKeyBinding nkb && !nkb.IsMatch(message))
            {
                return KeyCommandMatchResult.NoMatch;
            }

            index++;
        }

        return messages.Count == Bindings.Count ? KeyCommandMatchResult.Match : KeyCommandMatchResult.PartialMatch;
    }
}
