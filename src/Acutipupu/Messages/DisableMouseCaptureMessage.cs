namespace Acutipupu.Messages;

/// <summary>
/// An internal message that signals to stop listening
/// for mouse events.
/// </summary>
public record DisableMouseCaptureMessage : IMessage
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static IMessage Instance { get; } = new DisableMouseCaptureMessage();
}

