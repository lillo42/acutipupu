using Acutipupu.Messages;
using Tutu.Events;

namespace Acutipupu.Bindings;

/// <summary>
/// The number key binding.
/// </summary>
public sealed record NumberKeyBinding(KeyModifiers? Modifiers = null, KeyEventState? KeyboardState = null) : IKeyBinding
{
    /// <summary>
    /// Check if the key binding matches a number.
    /// </summary>
    /// <param name="message">The <see cref="KeyMessage"/>.</param>
    /// <returns>Returns true if match; otherwise false.</returns>
    public bool IsMatch(KeyMessage message)
    {
        if (message.Key is not KeyCode.CharKeyCode ch
            || string.IsNullOrEmpty(ch.Character)
            || char.IsDigit(ch.Character[0]))
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
        if (messages.Count != 1)
        {
            return KeyCommandMatchResult.NoMatch;
        }

        var message = messages.First();
        return IsMatch(message) ? KeyCommandMatchResult.Match : KeyCommandMatchResult.NoMatch;
    }
}
