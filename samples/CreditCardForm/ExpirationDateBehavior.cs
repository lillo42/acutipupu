using Acutipupu.Behaviors;
using Acutipupu.Components;

namespace CreditCardForm;

public class ExpirationDateBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable) => bindable.TextChanged += OnTextChanged;

    protected override void OnDetachingFrom(Entry bindable) => bindable.TextChanged -= OnTextChanged;

    private static void OnTextChanged(object? sender, TextChangedEventArgs args)
    {
        if (sender is not Entry input || input.Text == string.Empty)
        {
            return;
        }

        if (input.Text.Length > 5
            || (input.Text.Length == 3 && input.Text[2] != '/')
            || (input.Text.Length != 3 && !char.IsNumber(input.Text[^1])))
        {
            input.Text = args.OldTextValue!;
            // input.CursorPosition -= Math.Abs(args.NewTextValue.Length - args.OldTextValue!.Length);
        }
    }
}
