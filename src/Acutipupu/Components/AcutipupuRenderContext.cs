using Boto.Layouts;
using Boto.Terminals;
using Buffer = Boto.Buffers.Buffer;

namespace Acutipupu.Components;

/// <summary>
/// The default implementation of <see cref="IRenderContext"/>.
/// </summary>
public class AcutipupuRenderContext : IRenderContext
{
    private readonly Frame _frame;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcutipupuRenderContext"/> class.
    /// </summary>
    /// <param name="area">The <see cref="Rect"/>.</param>
    /// <param name="frame">The <see cref="Frame"/>.</param>
    public AcutipupuRenderContext(Rect area, Frame frame)
    {
        _frame = frame;
        Area = area;
    }

    /// <inheritdoc cref="IRenderContext.Buffer"/>
    public Buffer Buffer => _frame.Terminal.CurrentBuffer;

    /// <inheritdoc cref="IRenderContext.Area"/>
    public Rect Area { get; }

    /// <inheritdoc cref="IRenderContext.CursorPosition"/>
    public CursorPosition? CursorPosition
    {
        get => _frame.CursorPosition;
        set => _frame.CursorPosition = value;
    }

    /// <inheritdoc cref="IRenderContext.IsCursorHidden"/>
    public bool IsCursorHidden => CursorPosition is null;

    /// <inheritdoc cref="IRenderContext.New"/>
    public IRenderContext New(Rect area) => new AcutipupuRenderContext(area, _frame);
}
