namespace Acutipupu.Messages;

/// <summary>
/// A signal used when the window is unfocused.
/// </summary>
public record FocusLostMessage : IMessage
{
    /// <summary>
    /// The instance of the message.
    /// </summary>
    public static IMessage Instance { get; } = new FocusLostMessage();
}
