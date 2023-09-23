using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Bindings;

/// <summary>
/// The direction of changes propagation for bindings.
/// </summary>
public enum BindingMode
{
    /// <summary>
    /// Indicates that the binding should only propagate changes from source (usually the View Model)
    /// to target (the <see cref="ObservableObject"/>). This is the default mode for most BindableProperty values.
    /// </summary>
    OneWay,

    /// <summary>
    /// Indicates that the binding should only propagate changes from target (the <see cref="ObservableObject"/>)
    /// to source (usually the View Model). This is mainly used for read-only BindableProperty values.
    /// </summary>
    OneWayFromSource,

    /// <summary>
    /// Indicates that the binding should propagates changes from source (usually the View Model)
    /// to target (the <see cref="ObservableObject"/>) in both directions.
    /// </summary>
    TwoWay,
}
