using Tutu.Events;

namespace Acutipupu.Messages;

/// <summary>
/// A signal used when a key event happened.
/// </summary>
/// <param name="Key">The <see cref="KeyCode.IKeyCode"/>.</param>
/// <param name="Modifiers">The <see cref="KeyModifiers"/>.</param>
/// <param name="Kind">The <see cref="KeyEventKind"/>.</param>
/// <param name="KeyboardState">The <see cref="KeyEventState"/>.</param>
public record KeyMessage(
    KeyCode.IKeyCode Key,
    KeyModifiers Modifiers,
    KeyEventKind Kind,
    KeyEventState KeyboardState) : IMessage
{
    /// <summary>
    /// Initialize a new instance of <see cref="KeyMessage"/>.
    /// </summary>
    /// <param name="event">The <see cref="KeyEvent"/>.</param>
    public KeyMessage(KeyEvent @event)
        : this(@event.Code, @event.Modifiers, @event.Kind, @event.State)
    {
    }
}
