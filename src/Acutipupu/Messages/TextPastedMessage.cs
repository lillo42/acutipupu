namespace Acutipupu.Messages;

/// <summary>
/// A signal used a text is pasted.
/// </summary>
/// <param name="Text">The content.</param>
public record TextPastedMessage(string Text) : IMessage;
