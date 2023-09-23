using Boto.Layouts;
using Boto.Styles;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The style.
/// </summary>
public partial class Style : ObservableObject
{
    private Modifier _modifier = Modifier.Empty;

    /// <summary>
    /// The under <see cref="Boto.Styles.Color"/>.
    /// </summary>
    [ObservableProperty] private Color? _underColor;

    /// <summary>
    /// The <see cref="Boto.Styles.Color"/> of the foreground.
    /// </summary>
    [ObservableProperty] private Color? _foreground;

    /// <summary>
    /// The <see cref="Boto.Styles.Color"/> of the background.
    /// </summary>
    [ObservableProperty] private Color? _background;

    /// <summary>
    /// The title.
    /// </summary>
    [ObservableProperty] private string _title = string.Empty;

    /// <summary>
    /// The title <see cref="Boto.Layouts.Alignment"/>.
    /// </summary>
    [ObservableProperty] private Alignment _titleAlignment = Alignment.Left;

    /// <summary>
    /// The <see cref="Boto.Layouts.Alignment"/>.
    /// </summary>
    [ObservableProperty] private Alignment _alignment = Alignment.Left;

    /// <summary>
    /// The <see cref="Boto.Widgets.Borders"/>.
    /// </summary>
    [ObservableProperty] private Borders _borders = Borders.None;

    /// <summary>
    /// The <see cref="Boto.Widgets.BorderType"/>.
    /// </summary>
    [ObservableProperty] private BorderType _borderType = BorderType.Plain;

    /// <summary>
    /// The <see cref="Boto.Layouts.Margin"/>.
    /// </summary>
    [ObservableProperty] private Margin _margin;

    /// <summary>
    /// The bold style.
    /// </summary>
    [ObservableProperty] private bool _bold;

    /// <summary>
    /// The border style.
    /// </summary>
    [ObservableProperty] private Style? _borderStyle;

    partial void OnBoldChanged(bool value) => SetModifier(Modifier.Bold, value);

    /// <summary>
    /// The italic style.
    /// </summary>
    [ObservableProperty] private bool _italic;

    partial void OnItalicChanged(bool value) => SetModifier(Modifier.Italic, value);

    /// <summary>
    /// The dim style.
    /// </summary>
    [ObservableProperty] private bool _dim;

    partial void OnDimChanged(bool value) => SetModifier(Modifier.Dim, value);

    /// <summary>
    /// The crossed out style.
    /// </summary>
    [ObservableProperty] private bool _crossedOut;

    partial void OnCrossedOutChanged(bool value) => SetModifier(Modifier.CrossedOut, value);

    /// <summary>
    /// The reverse style.
    /// </summary>
    [ObservableProperty] private bool _reversed;

    partial void OnReversedChanged(bool value) => SetModifier(Modifier.Reversed, value);

    /// <summary>
    /// The slow blink style.
    /// </summary>
    [ObservableProperty] private bool _slowBlink;

    partial void OnSlowBlinkChanged(bool value) => SetModifier(Modifier.SlowBlink, value);

    /// <summary>
    /// The rapid blink style.
    /// </summary>
    [ObservableProperty] private bool _rapidBlink;

    partial void OnRapidBlinkChanged(bool value) => SetModifier(Modifier.RapidBlink, value);

    /// <summary>
    /// The <see cref="Components.UnderType"/>.
    /// </summary>
    public UnderType UnderType
    {
        get
        {
            if (_modifier.HasFlag(Modifier.Underlined))
            {
                return UnderType.Line;
            }

            if (_modifier.HasFlag(Modifier.UnderCurled))
            {
                return UnderType.Curl;
            }

            if (_modifier.HasFlag(Modifier.UnderDotted))
            {
                return UnderType.Dotted;
            }

            if (_modifier.HasFlag(Modifier.UnderDashed))
            {
                return UnderType.Dashed;
            }

            if (_modifier.HasFlag(Modifier.DoubleUnderlined))
            {
                return UnderType.DoubleLine;
            }

            return UnderType.None;
        }
        set
        {
            _modifier &= ~Modifier.Underlined;
            _modifier &= ~Modifier.UnderCurled;
            _modifier &= ~Modifier.UnderDotted;
            _modifier &= ~Modifier.UnderDashed;

            OnPropertyChanging();

            if (value == UnderType.Line)
            {
                _modifier |= Modifier.Underlined;
            }

            if (value == UnderType.DoubleLine)
            {
                _modifier |= Modifier.DoubleUnderlined;
            }

            if (value == UnderType.Curl)
            {
                _modifier |= Modifier.UnderCurled;
            }

            if (value == UnderType.Dotted)
            {
                _modifier |= Modifier.UnderDotted;
            }

            if (value == UnderType.Dashed)
            {
                _modifier |= Modifier.UnderDashed;
            }

            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Converts the <see cref="Style"/> to a <see cref="Boto.Styles.Style"/>.
    /// </summary>
    /// <returns>The <see cref="Boto.Styles.Style"/>.</returns>
    public Boto.Styles.Style ToBotoStyle() =>
        new() { Foreground = Foreground, Background = Background, Underline = UnderColor, AddModifier = _modifier };

    /// <summary>
    /// Converts the <see cref="Style"/> to a <see cref="Boto.Widgets.Block"/>.
    /// </summary>
    /// <returns>The <see cref="Boto.Widgets.Block"/>.</returns>
    public Block? ToBlock()
    {
        if (string.IsNullOrEmpty(Title) && Borders == Borders.None)
        {
            return null;
        }

        return new Block()
            .SetStyle(BorderStyle?.ToBotoStyle() ?? new Boto.Styles.Style())
            .SetBorderType(BorderType)
            .SetBorders(Borders)
            .SetTitle(Title)
            .SetTitleAlignment(TitleAlignment);
    }

    private void SetModifier(Modifier flags, bool add)
    {
        if (add)
        {
            _modifier |= flags;
        }
        else
        {
            _modifier &= ~flags;
        }
    }
}

/// <summary>
/// The under type.
/// </summary>
public enum UnderType
{
    /// <summary>
    /// None underline.
    /// </summary>
    None,

    /// <summary>
    /// The single underline.
    /// </summary>
    Line,

    /// <summary>
    /// The double underline.
    /// </summary>
    DoubleLine,

    /// <summary>
    /// The curls underline.
    /// </summary>
    Curl,

    /// <summary>
    /// The dot underline.
    /// </summary>
    Dotted,

    /// <summary>
    /// The dash underline.
    /// </summary>
    Dashed,
}
