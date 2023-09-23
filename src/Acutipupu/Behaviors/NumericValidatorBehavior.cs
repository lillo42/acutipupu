using Acutipupu.Components;
using Component = Acutipupu.Components.Component;

namespace Acutipupu.Behaviors;

/// <summary>
/// Numeric validator behavior for <see cref="Entry"/>.
/// </summary>
public class NumericValidatorBehavior<T> : Behavior<T>
    where T : Component, ITextChanged, IStyled
{
    /// <summary>
    /// The invalid <see cref="EntryStyle"/>.
    /// </summary>
    public EntryStyle ValidStyle { get; set; } = new();

    /// <summary>
    /// The invalid <see cref="EntryStyle"/>.
    /// </summary>
    public EntryStyle InvalidStyle { get; set; } = new();

    /// <inheritdoc />
    protected override void OnAttachedTo(T bindable)
        => bindable.TextChanged += OnTextChanged;

    /// <inheritdoc />
    protected override void OnDetachingFrom(T bindable)
        => bindable.TextChanged -= OnTextChanged;

    private void OnTextChanged(object? sender, TextChangedEventArgs args)
    {
        if (sender is not (ITextChanged text and IStyled styled))
        {
            return;
        }

        styled.Style = double.TryParse(text.Text, out _) ? ValidStyle : InvalidStyle;
    }
}
