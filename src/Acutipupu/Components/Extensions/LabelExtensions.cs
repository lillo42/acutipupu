using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Acutipupu.Bindings;
using Boto.Texts;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="Label"/> extensions method.
/// </summary>
public static class LabelExtensions
{
    #region Style

    /// <summary>
    /// Change the <see cref="Label.Style" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="style"/> with <see cref="Label.Style"/> as <paramref name="style"/>.</returns>
    public static Label SetStyle(this Label label, Style style)
    {
        label.Style = style;
        return label;
    }

    /// <summary>
    /// Change the <see cref="Label.Style" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Style"/> binding with <paramref name="property"/>.</returns>
    public static Label SetStyle(this Label label, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        label.SetBinding(Label.StyleProperty, property, mode);
        return label;
    }

    /// <summary>
    /// Change the <see cref="Label.Style" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Style"/> binding with <paramref name="property"/>.</returns>
    public static Label SetStyle<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TSource>(
        this Label label, Expression<Func<TSource, Style>> property, BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.StyleProperty, property, mode);


    /// <summary>
    /// Change the <see cref="Label.Style" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Style"/> binding with <paramref name="getter"/>.</returns>
    public static Label SetStyle<TSource>(this Label label, string propertyName,
        Func<TSource, Style>? getter = null,
        Action<TSource, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.StyleProperty, propertyName, getter, setter, mode);


    /// <summary>
    /// Change the <see cref="Label.Style" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Style"/> binding with <paramref name="getter"/>.</returns>
    public static Label SetStyle(this Label label, string propertyName,
        Func<object, Style>? getter = null,
        Action<object, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.StyleProperty, propertyName, getter, setter, mode);

    #endregion

    #region Text

    /// <summary>
    /// Change the <see cref="Label.Text" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="text">The value.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Text"/> as <paramref name="text"/>.</returns>
    public static Label SetText(this Label label, string text)
    {
        label.Text = text;
        return label;
    }

    /// <summary>
    /// Add binding to the <see cref="Label.Text" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Text"/> binding with given information.</returns>
    public static Label SetText(this Label label, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        label.SetBinding(Label.TextProperty, property, mode);
        return label;
    }

    /// <summary>
    /// Add binding to the <see cref="Label.Text" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Text"/> binding with given information.</returns>
    public static Label SetText<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TSource>(
        this Label label, Expression<Func<TSource, string>> property, BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TextProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Label.Text" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Text"/> binding with given information.</returns>
    public static Label SetText<TSource>(this Label label, string propertyName,
        Func<TSource, string>? getter = null,
        Action<TSource, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TextProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Label.Text" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Text"/> binding with given information.</returns>
    public static Label SetText(this Label label, string propertyName,
        Func<object, string>? getter = null,
        Action<object, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TextProperty, propertyName, getter, setter, mode);

    #endregion

    #region Trim

    /// <summary>
    /// Change the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="trim">The trim flag.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> as <paramref name="trim"/>.</returns>
    public static Label SetTrim(this Label label, bool? trim)
    {
        label.Trim = trim;
        return label;
    }

    /// <summary>
    /// Change the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> as true.</returns>
    public static Label EnableTrim(this Label label)
    {
        label.Trim = true;
        return label;
    }

    /// <summary>
    /// Change the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> as false.</returns>
    public static Label DisableTrim(this Label label)
    {
        label.Trim = false;
        return label;
    }

    /// <summary>
    /// Add binding the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetTrim(this Label label, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        label.SetBinding(Label.TrimProperty, property, mode);
        return label;
    }

    /// <summary>
    /// Add binding the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetTrim<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TSource>(
        this Label label, Expression<Func<TSource, bool?>> property, BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TrimProperty, property, mode);


    /// <summary>
    /// Add binding the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetTrim<TSource>(this Label label, string propertyName,
        Func<TSource, bool?>? getter = null,
        Action<TSource, bool?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TrimProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding the <see cref="Label.Trim" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetTrim(this Label label, string propertyName,
        Func<object, bool?>? getter = null,
        Action<object, bool?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.TrimProperty, propertyName, getter, setter, mode);

    #endregion

    #region Formatted text 

    /// <summary>
    /// Change the <see cref="Label.FormattedText" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="text">The <see cref="Text"/> value.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.FormattedText"/> as <paramref name="text"/>.</returns>
    public static Label SetFormattedText(this Label label, Text? text)
    {
        label.FormattedText = text;
        return label;
    }

    /// <summary>
    /// Add binding to the <see cref="Label.FormattedText" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetFormattedText(this Label label, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        label.SetBinding(Label.FormattedTextProperty, property, mode);
        return label;
    }

    /// <summary>
    /// Add binding to the <see cref="Label.FormattedText" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetFormattedText<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TSource>(
        this Label label, Expression<Func<TSource, Text?>> property, BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.FormattedTextProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Label.FormattedText" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetFormattedText<TSource>(this Label label, string propertyName,
        Func<TSource, Text?>? getter = null,
        Action<TSource, Text?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.FormattedTextProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Label.FormattedText" />.
    /// </summary>
    /// <param name="label">The <see cref="Label"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="label"/> with <see cref="Label.Trim"/> binding with given information.</returns>
    public static Label SetFormattedText(this Label label, string propertyName,
        Func<object, Text?>? getter = null,
        Action<object, Text?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => label.SetBinding(Label.FormattedTextProperty, propertyName, getter, setter, mode);

    #endregion
}
