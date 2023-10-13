using Acutipupu.Bindings;

namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The cursor text movement key binding.
/// </summary>
public class EntryKeyMap
{
    /// <summary>
    /// The key binding to move the cursor up.
    /// </summary>
    public KeyBindingCollection Up { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor down.
    /// </summary>
    public KeyBindingCollection Down { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor left.
    /// </summary>
    public KeyBindingCollection Left { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor right.
    /// </summary>
    public KeyBindingCollection Right { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to line.
    /// </summary>
    public KeyBindingCollection GoToLine { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the beginning of the line.
    /// </summary>
    public KeyBindingCollection BeginningOfLine { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the end of the line.
    /// </summary>
    public KeyBindingCollection EndOfLine { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the beginning of the buffer.
    /// </summary>
    public KeyBindingCollection BeginningOfBuffer { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the end of the buffer.
    /// </summary>
    public KeyBindingCollection EndOfBuffer { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the first non-blank character.
    /// </summary>
    public KeyBindingCollection FirstNonBlankCharacter { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the last non-blank character.
    /// </summary>
    public KeyBindingCollection LastNonBlankCharacter { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the previous word.
    /// </summary>
    public KeyBindingCollection WordBackward { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the next word.
    /// </summary>
    public KeyBindingCollection WordForward { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the next paragraph.
    /// </summary>
    public KeyBindingCollection NextParagraph { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the previous paragraph.
    /// </summary>
    public KeyBindingCollection PreviousParagraph { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the next character occurence.
    /// </summary>
    public KeyBindingCollection NextCharOccurence { get; set; } = new();

    /// <summary>
    /// The key binding to move the cursor to the previous character occurence.
    /// </summary>
    public KeyBindingCollection PreviousCharOccurence { get; set; } = new();
    
    /// <summary>
    /// The key binding to insert new line.
    /// </summary>
    public KeyBindingCollection NewLine { get; set; } = new();
}
