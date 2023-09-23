using Tutu.Events;

namespace Acutipupu.Messages;

/// <summary>
/// A signal used when the window is resized. 
/// </summary>
/// <param name="Width">The width.</param>
/// <param name="Height">The height.</param>
public record WindowResizeMessage(int Width, int Height) : IMessage
{
    /// <summary>
    /// Initialize a new instance of <see cref="WindowResizeMessage"/>.
    /// </summary>
    /// <param name="event">The <see cref="Event.ScreenResizeEvent"/>.</param>
    public WindowResizeMessage(Event.ScreenResizeEvent @event)
        : this(@event.Column, @event.Row)
    {
    }
}

