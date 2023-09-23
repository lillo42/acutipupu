namespace Acutipupu.Messages;

/// <summary>
/// A signal used when the window is focused.
/// </summary>
public record FocusGainMessage : IMessage
{
    /// <summary>
    /// The default instance of <see cref="FocusGainMessage"/>.
    /// </summary>
    public static IMessage Instance { get; } = new FocusGainMessage();
}
