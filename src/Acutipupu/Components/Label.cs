using Acutipupu.Bindings;
using Boto.Texts;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The label component.
/// </summary>
public partial class Label : Component
{
    private readonly Paragraph _paragraph = new();

    /// <summary>
    /// The label <see cref="Components.Style"/>.
    /// </summary>
    [ObservableProperty] private Style _style = new();

    /// <summary>
    /// The text value of the label.
    /// </summary>
    [ObservableProperty] private string _text = string.Empty;

    /// <summary>
    /// The formatted text value of the label.
    /// </summary>
    /// <remarks>
    /// The <see cref="Text"/> and <see cref="FormattedText"/> are mutually exclusive
    /// and the <see cref="FormattedText"/> has priority over <see cref="Text"/>.
    /// </remarks>
    [ObservableProperty] private Text? _formattedText;

    /// <summary>
    /// Flag indicating whether the label should be trimmed.
    /// </summary>
    [ObservableProperty] private bool? _trim;

    /// <summary>
    /// Initialize a new instance of <see cref="Label"/>.
    /// </summary>
    public Label()
    {
    }

    partial void OnStyleChanging(Style? oldValue, Style newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= MarkAsDirty;
        }

        newValue.PropertyChanged += MarkAsDirty;
    }

    /// <summary>
    /// The value <see cref="IBindableProperty"/>.
    /// </summary>
    public static IBindableProperty<Label, string> TextProperty { get; } = new BindableProperty<Label, string>(
        nameof(Text),
        input => input.Text,
        (input, value) => input.Text = value);

    /// <summary>
    /// The trim <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Label, bool?> TrimProperty { get; } = new BindableProperty<Label, bool?>(
        nameof(Trim),
        input => input.Trim,
        (input, value) => input.Trim = value);

    /// <summary>
    /// The style <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Label, Style> StyleProperty { get; } = new BindableProperty<Label, Style>(
        nameof(Style),
        input => input.Style,
        (input, value) => input.Style = value);

    /// <summary>
    /// The formatted text <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Label, Text?> FormattedTextProperty { get; } = new BindableProperty<Label, Text?>(
        nameof(FormattedText),
        input => input.FormattedText,
        (input, value) => input.FormattedText = value);

    /// <inheritdoc />
    public override void Render(IRenderContext context)
    {
        if (IsDirty)
        {
            Update();
        }

        _paragraph.Render(context.Area, context.Buffer);
    }

    private void Update()
    {
        _paragraph
            .SetBlock(Style.ToBlock()!)
            .SetAlignment(Style.Alignment)
            .SetTrim(Trim);

        if (FormattedText is not null)
        {
            _paragraph.SetText(FormattedText);
        }
        else
        {
            _paragraph.SetText(Text, Style.ToBotoStyle());
        }

        IsDirty = false;
    }
}
