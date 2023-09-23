using Acutipupu.Components;

namespace Acutipupu;

/// <summary>
/// The component accessor.
/// </summary>
public class SettableComponentAccessor : IComponentAccessor
{
    private readonly AsyncLocal<Component?> _component = new();

    /// <inheritdoc />
    public Component? Component
    {
        get => _component.Value;
        set => _component.Value = value;
    }
}
