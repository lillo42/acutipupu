using System.Collections.Immutable;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;

namespace Acutipupu;

/// <summary>
/// The text
/// </summary>
public interface IText
{
    /// <summary>
    /// The <see cref="Boto.Terminals.CursorPosition"/> 
    /// </summary>
    CursorPosition CursorPosition { get; set; }
    
    /// <summary>
    /// The <see cref="IColumnPosition"/> to save the last column cursor position.
    /// </summary>
    /// <remarks>
    /// It's used when the cursor is moved up or down or move to a specific line.
    /// </remarks>
    IColumnPosition LastColumnPosition { get; set; }

    /// <summary>
    /// The component text.
    /// </summary>
    string Text { get; }

    /// <summary>
    /// The <see cref="TextKeyMap"/>.
    /// </summary>
    TextKeyMap? KeyMap { get; }

    /// <summary>
    /// The <see cref="Text"/> indexes.
    /// </summary>
    ImmutableList<ImmutableArray<int>> Indexes { get; }
}

/// <summary>
/// The entity to save the last cursor position.
/// </summary>
public interface IColumnPosition
{
}

/// <summary>
/// Mark the last cursor position at the specified column.
/// </summary>
/// <param name="Column">The column.</param>
public record AtCursorPosition(int Column) : IColumnPosition;

/// <summary>
/// Mark the last cursor position at the end of the line.
/// </summary>
public record EndOfLine : IColumnPosition
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static IColumnPosition Instance { get; } = new EndOfLine();
}
