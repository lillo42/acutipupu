namespace Acutipupu.Messages;

/// <summary>
/// An internal message signals that the program should exit
/// alternate screen buffer.
/// </summary>
public record LeaveAltScreenMessage : IMessage
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static LeaveAltScreenMessage Instance { get; } = new();
}
