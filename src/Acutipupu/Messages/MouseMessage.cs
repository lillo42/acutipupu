using Tutu.Events;

namespace Acutipupu.Messages;

/// <summary>
/// A signal used when a mouse event happened.
/// </summary>
/// <param name="Column">The column.</param>
/// <param name="Row">The row.</param>
/// <param name="Kind">The <see cref="MouseEventKind.IMouseEventKind"/>.</param>
/// <param name="Modifiers">The <see cref="KeyModifiers"/>.</param>
public record MouseMessage(int Column, int Row, MouseEventKind.IMouseEventKind Kind, KeyModifiers Modifiers) : IMessage
{
    /// <summary>
    /// Initialize a new instance of <see cref="MouseMessage"/>.
    /// </summary>
    /// <param name="event">The <see cref="MouseEvent"/>.</param>
    public MouseMessage(MouseEvent @event)
        : this(@event.Column, @event.Row, @event.Kind, @event.Modifiers)
    {
    }
}
