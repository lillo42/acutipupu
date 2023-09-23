using System.Diagnostics.CodeAnalysis;
using Boto.Layouts;
using Boto.Terminals;
using Buffer = Boto.Buffers.Buffer;

namespace Acutipupu.Components;

/// <summary>
/// The render context.
/// </summary>
public interface IRenderContext
{
    /// <summary>
    /// The <see cref="Boto.Buffers.Buffer"/>.
    /// </summary>
    Buffer Buffer { get; }

    /// <summary>
    /// The <see cref="Rect"/> to be render.
    /// </summary>
    Rect Area { get; }

    /// <summary>
    /// Where should the cursor be after drawing this frame?
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the cursor is hidden and its position is controlled by the backend. If `(x, y)`,
    /// the cursor is shown and placed at `(x, y)` after the call to <see cref="ITerminal.Draw"/>.
    /// </remarks>
    CursorPosition? CursorPosition { get; set; }

    /// <summary>
    /// Whether the cursor is currently hidden
    /// </summary>
    [MemberNotNullWhen(false, nameof(CursorPosition))]
    bool IsCursorHidden { get; }

    /// <summary>
    /// Creates a new <see cref="IRenderContext"/> with the given <see cref="Rect"/>.
    /// </summary>
    /// <param name="area">The new area.</param>
    /// <returns>The <see cref="IRenderContext"/> with the give <paramref name="area"/>.</returns>
    IRenderContext New(Rect area);
}
