using Acutipupu.Components;

namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The interface to mark an <see cref="Component"/> as navigable.
/// </summary>
public interface INavegable
{
    /// <summary>
    /// Flag to indicate if the <see cref="Component"/> is active.
    /// </summary>
    bool IsActive { get; set; }
}
