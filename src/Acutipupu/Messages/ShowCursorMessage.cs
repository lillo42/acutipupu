namespace Acutipupu.Messages;

/// <summary>
/// A signal to show the cursor.
/// </summary>
public record ShowCursorMessage : IMessage
{
    /// <summary>
    /// The default instance of <see cref="ShowCursorMessage"/>.
    /// </summary>
    public static IMessage Instance { get; } = new ShowCursorMessage();
}
