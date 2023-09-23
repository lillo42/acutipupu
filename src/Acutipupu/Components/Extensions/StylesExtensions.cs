using Boto.Layouts;
using Boto.Styles;
using Boto.Widgets;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="Style"/> extensions method.
/// </summary>
public static class StylesExtensions
{
    /// <summary>
    /// Change the <see cref="Style.UnderColor"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.UnderColor"/> as <paramref name="color"/>.</returns>
    public static Style SetUnderColor(this Style style, Color? color)
    {
        style.UnderColor = color;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Foreground"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Foreground"/> as <paramref name="color"/>.</returns>
    public static Style SetForeground(this Style style, Color? color)
    {
        style.Foreground = color;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Background"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Background"/> as <paramref name="color"/>.</returns>
    public static Style SetBackground(this Style style, Color? color)
    {
        style.Background = color;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Title"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Title"/> as <paramref name="title"/>.</returns>
    public static Style SetTitle(this Style style, string title)
    {
        style.Title = title;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Alignment"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="titleAlignment">The <see cref="Alignment"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Alignment"/> as <paramref name="titleAlignment"/>.</returns>
    public static Style SetTitleAlignment(this Style style, Alignment titleAlignment)
    {
        style.TitleAlignment = titleAlignment;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Borders"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="borders">The <see cref="Borders"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Borders"/> as <paramref name="borders"/>.</returns>
    public static Style SetBorders(this Style style, Borders borders)
    {
        style.Borders = borders;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.BorderType"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="borderType">The <see cref="BorderType"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.BorderType"/> as <paramref name="borderType"/>.</returns>
    public static Style SetBorderType(this Style style, BorderType borderType)
    {
        style.BorderType = borderType;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Margin"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="margin">The <see cref="Margin"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Margin"/> as <paramref name="margin"/>.</returns>
    public static Style SetMargin(this Style style, Margin margin)
    {
        style.Margin = margin;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Bold"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if bold is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Bold"/> as <paramref name="flag"/>.</returns>
    public static Style SetBold(this Style style, bool flag)
    {
        style.Bold = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Bold"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Bold"/> as true.</returns>
    public static Style EnableBold(this Style style)
    {
        style.Bold = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Bold"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Bold"/> as false.</returns>
    public static Style DisableBold(this Style style)
    {
        style.Bold = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Italic"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if italic is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Italic"/> as <paramref name="flag"/>.</returns>
    public static Style SetItalic(this Style style, bool flag)
    {
        style.Bold = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Italic"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Italic"/> as true.</returns>
    public static Style EnableItalic(this Style style)
    {
        style.Bold = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Italic"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Italic"/> as false.</returns>
    public static Style DisableItalic(this Style style)
    {
        style.Italic = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Dim"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if dim is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Dim"/> as <paramref name="flag"/>.</returns>
    public static Style SetDim(this Style style, bool flag)
    {
        style.Dim = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Dim"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Dim"/> as true.</returns>
    public static Style EnableDim(this Style style)
    {
        style.Dim = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Dim"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Dim"/> as false.</returns>
    public static Style DisableDim(this Style style)
    {
        style.Dim = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.CrossedOut"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if crossed out is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.CrossedOut"/> as <paramref name="flag"/>.</returns>
    public static Style SetCrossedOut(this Style style, bool flag)
    {
        style.CrossedOut = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.CrossedOut"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.CrossedOut"/> as true.</returns>
    public static Style EnableCrossedOut(this Style style)
    {
        style.CrossedOut = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.CrossedOut"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.CrossedOut"/> as false.</returns>
    public static Style DisableCrossedOut(this Style style)
    {
        style.CrossedOut = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Reversed"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if reversed is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Reversed"/> as <paramref name="flag"/>.</returns>
    public static Style SetReversed(this Style style, bool flag)
    {
        style.Reversed = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Reversed"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Reversed"/> as true.</returns>
    public static Style EnableReversed(this Style style)
    {
        style.Reversed = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.Reversed"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.Reversed"/> as false.</returns>
    public static Style DisableReversed(this Style style)
    {
        style.Reversed = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.SlowBlink"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if slow blink is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.SlowBlink"/> as <paramref name="flag"/>.</returns>
    public static Style SetSlowBlink(this Style style, bool flag)
    {
        style.SlowBlink = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.SlowBlink"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.SlowBlink"/> as true.</returns>
    public static Style EnableSlowBlink(this Style style)
    {
        style.SlowBlink = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.SlowBlink"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.SlowBlink"/> as false.</returns>
    public static Style DisableSlowBlink(this Style style)
    {
        style.SlowBlink = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.RapidBlink"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="flag">The flag indicating if rapid blink is enable.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.RapidBlink"/> as <paramref name="flag"/>.</returns>
    public static Style SetRapidBlink(this Style style, bool flag)
    {
        style.RapidBlink = flag;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.RapidBlink"/> to true.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.RapidBlink"/> as true.</returns>
    public static Style EnableRapidBlink(this Style style)
    {
        style.RapidBlink = true;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.RapidBlink"/> to false.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.RapidBlink"/> as false.</returns>
    public static Style DisableRapidBlink(this Style style)
    {
        style.RapidBlink = false;
        return style;
    }

    /// <summary>
    /// Change the <see cref="Style.UnderType"/>.
    /// </summary>
    /// <param name="style">The <see cref="Entry"/>.</param>
    /// <param name="underType">The <see cref="UnderType"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Style.UnderType"/> as <paramref name="underType"/>.</returns>
    public static Style SetUnderType(this Style style, UnderType underType)
    {
        style.UnderType = underType;
        return style;
    }
}
