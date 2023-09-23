using Acutipupu.Components;

namespace Acutipupu.Behaviors;

/// <summary>
/// The max character <see cref="Behavior{T}"/>
/// </summary>
public class MaxCharacterBehavior<T> : Behavior<T>
    where T : Component, ITextChanged 
{
    /// <summary>
    /// Initialize new <see cref="MaxCharacterBehavior{T}"/> instance.
    /// </summary>
    public MaxCharacterBehavior()
    {
    }

    /// <summary>
    /// Initialize new <see cref="MaxCharacterBehavior{T}"/> instance.
    /// </summary>
    /// <param name="limit">The character limit.</param>
    public MaxCharacterBehavior(int limit)
    {
        Limit = limit;
    }

    /// <summary>
    /// The character limit.
    /// </summary>
    public int Limit { get; set; } = -1;

    /// <inheritdoc />
    protected override void OnAttachedTo(T bindable) => bindable.TextChanged += OnTextChanged;

    /// <inheritdoc />
    protected override void OnDetachingFrom(T bindable) => bindable.TextChanged -= OnTextChanged;

    private void OnTextChanged(object? sender, TextChangedEventArgs args)
    {
        if (Limit == -1)
        {
            return;
        }

        if (sender is ITextChanged text && args.NewTextValue.Length > Limit)
        {
            text.Text = args.OldTextValue!;
        }
        
        // if (sender is Entry input && args.NewTextValue.Length > Limit)
        // {
        //     input.CursorPosition -= Math.Abs(args.NewTextValue.Length - args.OldTextValue!.Length);
        // }
    }
}
