using Acutipupu.Messages;

namespace Acutipupu.Bindings;

/// <summary>
/// The key binding.
/// </summary>
public interface IKeyBinding
{
    /// <summary>
    /// Check if the key binding matches the messages.
    /// </summary>
    /// <param name="messages">The <see cref="KeyMessage"/> pressed.</param>
    /// <returns>Returns true if match; otherwise false.</returns>
    KeyCommandMatchResult Match(IReadOnlyCollection<KeyMessage> messages);
}
