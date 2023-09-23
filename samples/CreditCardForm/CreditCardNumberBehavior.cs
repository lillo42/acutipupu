using Acutipupu.Behaviors;
using Acutipupu.Components;

namespace CreditCardForm;

public class CreditCardNumberBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable) => bindable.TextChanged += OnTextChanged;

    protected override void OnDetachingFrom(Entry bindable) => bindable.TextChanged -= OnTextChanged;

    private void OnTextChanged(object? sender, TextChangedEventArgs args)
    {
        if(sender is not Entry input || input.Text == string.Empty)
        {
            return;
        }

        var value = args.NewTextValue;
        if(value.Length > 19
           || !long.TryParse(value.Replace(" ", ""), out _)
           || (value.Length is 5 or 10 or 15 && value[^1] != ' ')
           || (value.Length != 5 && value.Length != 10 && value.Length != 15 && !char.IsNumber(value[^1])))
        {
            input.Text = args.OldTextValue!;
            // input.CursorPosition -= Math.Abs(args.NewTextValue.Length - args.OldTextValue!.Length);
            return;
        }

        if (value.Length is 4 or 9 or 14)
        {
            input.Text += " ";
            // input.CursorPosition++;
        }
    }
}
