namespace Acutipupu.Components;

/// <summary>
/// Event arguments for text changed event.
/// </summary>
public class TextChangedEventArgs : EventArgs
{
    /// <summary>
    /// The text changed event.
    /// </summary>
    /// <param name="oldTextValue">The old text.</param>
    /// <param name="newTextValue">The new text value.</param>
    public TextChangedEventArgs(string? oldTextValue, string newTextValue)
    {
        OldTextValue = oldTextValue;
        NewTextValue = newTextValue;
    }

    /// <summary>
    /// The old text value.
    /// </summary>
    public string? OldTextValue { get; } 
    
    /// <summary>
    /// The new text value.
    /// </summary>
    public string NewTextValue { get; }
}
