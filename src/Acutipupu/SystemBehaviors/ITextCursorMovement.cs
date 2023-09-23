namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The cursor text movement.
/// </summary>
public interface ITextCursorMovement
{
    /// <summary>
    /// The text movement key map.
    /// </summary>
    TextKeyMap? KeyMap { get; }

    /// <summary>
    /// Moves the cursor up.
    /// </summary>
    /// <param name="lines">The number of line to be move up.</param>
    void MoveCursorUp(int lines);

    /// <summary>
    /// Moves the cursor down.
    /// </summary>
    /// <param name="lines">The number of line to be move down.</param>
    void MoveCursorDown(int lines);

    /// <summary>
    /// Moves the cursor left.
    /// </summary>
    /// <param name="columns">The number of columns to be move left.</param>
    void MoveCursorLeft(int columns);

    /// <summary>
    /// Moves the cursor right.
    /// </summary>
    /// <param name="columns">The number of columns to be move right.</param>
    void MoveCursorRight(int columns);

    /// <summary>
    /// Moves the cursor to the beginning of the line.
    /// </summary>
    void MoveCursorBeginningOfLine();

    /// <summary>
    /// Moves the cursor to the end of the line.
    /// </summary>
    void MoveCursorEndOfLine();

    /// <summary>
    /// Move cursor to the beginning of the buffer.
    /// </summary>
    void MoveCursorBeginningOfBuffer();

    /// <summary>
    /// Move cursor to the end of the buffer.
    /// </summary>
    void MoveCursorEndOfBuffer();

    /// <summary>
    /// Move cursor to the specified line.
    /// </summary>
    /// <param name="line">The line number to be moved.</param>
    void MoveCursorLine(int line);

    /// <summary>
    /// Move cursor to the first non-blank character.
    /// </summary>
    void MoveCursorFirstNonBlankCharacter();

    /// <summary>
    /// Move cursor to the last non-blank character.
    /// </summary>
    void MoveCursorLastNonBlankCharacter();

    /// <summary>
    /// Move cursor backwards to the start of a word 
    /// </summary>
    /// <param name="numberOfWords">The number of words to be back.</param>
    /// <param name="ignorePunctuation">Flag indicating if should ignore any punctuation.</param>
    /// <param name="startOfWord">Flag indicating if the cursor should move to start of the word.</param>
    void MoveCursorWordBackward(int numberOfWords, bool ignorePunctuation, bool startOfWord);

    /// <summary>
    /// Move cursor forwards to the start of a word 
    /// </summary>
    /// <param name="numberOfWords">The number of words to be back.</param>
    /// <param name="ignorePunctuation">Flag indicating if should ignore any punctuation.</param>
    /// <param name="startOfWord">Flag indicating if the cursor should move to start of the word.</param>
    void MoveCursorWordForward(int numberOfWords, bool ignorePunctuation, bool startOfWord);

    /// <summary>
    /// Move cursor to the next paragraph. 
    /// </summary>
    /// <param name="numberOfParagraphs">The number of paragraphs to be moved.</param>
    void MoveCursorNextParagraph(int numberOfParagraphs);

    /// <summary>
    /// Move cursor to the previous paragraph.
    /// </summary>
    /// <param name="numberOfParagraphs">The number of paragraphs to be moved.</param>
    void MoveCursorPreviousParagraph(int numberOfParagraphs);

    /// <summary>
    /// Move cursor to the next occurence of the specified text.
    /// </summary>
    /// <param name="text">The search text</param>
    /// <param name="numberOfOccurence">The number of occurence to move forward.</param>
    /// <param name="beforeTheOccurence">Flag indicating if it should move cursor before find the occurence.</param>
    void MoveCursorNextOccurenceInCurrentLine(string text, int numberOfOccurence, bool beforeTheOccurence);

    /// <summary>
    /// Move cursor to the previous occurence of the specified text.
    /// </summary>
    /// <param name="text">The search text</param>
    /// <param name="numberOfOccurence">The number of occurence to move backward.</param>
    /// <param name="afterTheOccurence">Flag indicating if it should move cursor before find the occurence.</param>
    void MoveCursorPreviousOccurenceInCurrentLine(string text, int numberOfOccurence, bool afterTheOccurence);
}
