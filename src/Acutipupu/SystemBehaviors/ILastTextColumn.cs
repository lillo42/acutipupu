namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The last text column allow.
/// </summary>
/// <remarks>
/// It's useful for different text behavior like VIM and Emacs, where Emacs always allow you to go to last column + 1
/// and VIM always allow you to go to last column in visual mode and last column + 1 in insert mode.
/// </remarks>
public interface ILastTextColumn
{
    /// <summary>
    /// The maximum column.
    /// </summary>
    /// <param name="maxColumn">The current max column.</param>
    /// <returns>The max allowed column.</returns>
    int LastColumn(int maxColumn);
}
