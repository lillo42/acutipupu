using Acutipupu.Components;

namespace Acutipupu;

/// <summary>
/// The component context.
/// </summary>
public interface IComponentAccessor
{
    /// <summary>
    /// The component.
    /// </summary>
    Component? Component { get; }
}
