namespace Acutipupu.Components;

/// <summary>
/// The text value.
/// </summary>
public interface ITextChanged
{
    /// <summary>
    /// Current text value.
    /// </summary>
    string Text { get; set; }

    /// <summary>
    /// The text changed event.
    /// </summary>
    event EventHandler<TextChangedEventArgs>? TextChanged;
}
