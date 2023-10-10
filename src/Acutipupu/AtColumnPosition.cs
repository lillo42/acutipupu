namespace Acutipupu;

/// <summary>
/// Mark the last cursor position at the specified column.
/// </summary>
/// <param name="Column">The column.</param>
/// <remarks>If <paramref name="Column"/> is -1, it means last column.</remarks>
public readonly record struct AtColumnPosition(int Column)
{
    /// <summary>
    /// Get current column based on <paramref name="maxColumn"/>. 
    /// </summary>
    /// <param name="maxColumn">The max column in current row.</param>
    /// <returns>The current column.</returns>
    public int CurrentColumn(int maxColumn) => Column == -1 ? maxColumn : Math.Min(Column, maxColumn);
}
