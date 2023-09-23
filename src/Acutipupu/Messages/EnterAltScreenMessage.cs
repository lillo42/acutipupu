namespace Acutipupu.Messages;

/// <summary>
/// An internal message signals that the program should
/// enter alternate screen buffer. 
/// </summary>
public record EnterAltScreenMessage : IMessage
{
    /// <summary>
    /// The default instance.
    /// </summary>
    public static EnterAltScreenMessage Instance { get; } = new();
}
