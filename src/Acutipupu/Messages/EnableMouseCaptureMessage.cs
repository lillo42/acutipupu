namespace Acutipupu.Messages;

/// <summary>
/// A special command that signals to start
/// listening for "cell motion" type mouse events (ESC[?1002l).
/// </summary>
public record EnableMouseCaptureMessage : IMessage
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static IMessage Instance { get; } = new EnableMouseCaptureMessage();
}
