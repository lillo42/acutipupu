using Acutipupu.Bindings;
using Acutipupu.Components;

namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The navigation behavior key map.
/// </summary>
public class NavigationKeyMap
{
    /// <summary>
    /// The move to the next <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Next { get; set; } = new();

    /// <summary>
    /// The move to the previous <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Previous { get; set; } = new();

    /// <summary>
    /// The move to the top <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Up { get; set; } = new();

    /// <summary>
    /// The move to the bottom <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Down { get; set; } = new();

    /// <summary>
    /// The move to the left <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Left { get; set; } = new();

    /// <summary>
    /// The move to the right <see cref="Component"/> key binding.
    /// </summary>
    public KeyBindingCollection Right { get; set; } = new();
}
