using Acutipupu.Messages;
using Tutu.Events;

namespace Acutipupu.Bindings;

/// <summary>
/// The key binding.
/// </summary>
public sealed record KeyBinding(KeyCode.IKeyCode? Key = null,
    KeyModifiers? Modifiers = null,
    KeyEventState? KeyboardState = null) : IKeyBinding
{
    /// <summary>
    /// Check if the key binding matches the message.
    /// </summary>
    /// <param name="message">The <see cref="KeyMessage"/>.</param>
    /// <returns>Returns true if match; otherwise false.</returns>
    public bool IsMatch(KeyMessage message)
    {
        if (Key is not null && !Equals(Key, message.Key))
        {
            return false;
        }

        if (Modifiers is not null && Modifiers != message.Modifiers)
        {
            return false;
        }

        if (KeyboardState is not null && KeyboardState != message.KeyboardState)
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public KeyCommandMatchResult Match(IReadOnlyCollection<KeyMessage> messages)
    {
        return messages.Count == 1 && IsMatch(messages.First())
            ? KeyCommandMatchResult.Match
            : KeyCommandMatchResult.NoMatch;
    }
}
