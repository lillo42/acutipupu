namespace Acutipupu.Messages;

/// <summary>
/// A signal to hide the cursor.
/// </summary>
public record HideCursorMessage : IMessage
{
    /// <summary>
    /// The default instance of <see cref="HideCursorMessage"/>.
    /// </summary>
    public static IMessage Instance { get; } = new HideCursorMessage();
}
