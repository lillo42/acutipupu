namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The screen text movement.
/// </summary>
public interface ITextScreenMovement
{
    /// <summary>
    /// Move screen on the way where cursor is in the center. 
    /// </summary>
    void CursorOnCenter();

    /// <summary>
    /// Move screen on the way where cursor is in the top.
    /// </summary>
    void CursorOnTop();

    /// <summary>
    /// Move screen on the way where cursor is in the bottom.
    /// </summary>
    void CursorOnBottom();

    /// <summary>
    /// Move screen up.
    /// </summary>
    /// <param name="lines">Number of lines to be moved.</param>
    void MoveScreenUp(int lines);

    /// <summary>
    /// Move screen down.
    /// </summary>
    /// <param name="lines">Number of lines to be moved.</param>
    void MoveScreenDown(int lines);

    /// <summary>
    /// Move screen and cursor half page up.
    /// </summary>
    void MoveScreenHalfPageUp();

    /// <summary>
    /// Move screen and cursor half page down.
    /// </summary>
    void MoveScreenHalfPageDown();

    /// <summary>
    /// Move screen and cursor page up.
    /// </summary>
    void MoveScreenPageUp();

    /// <summary>
    /// Move screen and cursor page down.
    /// </summary>
    void MoveScreenPageDown();
}
