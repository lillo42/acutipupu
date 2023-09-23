using System.ComponentModel;
using Acutipupu.Behaviors;
using Acutipupu.Components;

namespace CreditCardForm;

public class CcvNumberBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable) => bindable.PropertyChanged += OnTextChanged;

    protected override void OnDetachingFrom(Entry bindable) => bindable.PropertyChanged -= OnTextChanged;

    private static void OnTextChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is not Entry input 
            || args.PropertyName != nameof(Entry.Text)
            || input.Text == string.Empty)
        {
            return;
        }
        
        if (input.Text.Length > 3 || !int.TryParse(input.Text, out _))
        {
            input.Text = input.Text[..^1];
            // input.CursorPosition--;
        }
    }
}
