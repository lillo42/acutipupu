using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Acutipupu.Bindings;
using Boto.Layouts;
using Boto.Terminals;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="Entry"/> extension methods. 
/// </summary>
public static class EntryExtensions
{
    #region Text

    /// <summary>
    /// Change the <see cref="Entry.Text"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="text">The value.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Text"/> as <paramref name="text"/>.</returns>
    public static Entry SetText(this Entry entry, string text)
    {
        entry.Text = text;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Text" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Text"/> binding with given information.</returns>
    public static Entry SetText(this Entry entry, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.TextProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Text" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Text"/> binding with given information.</returns>
    public static Entry SetText<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TSource>(
        this Entry entry, Expression<Func<TSource, string>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TextProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Text" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Text"/> binding with given information.</returns>
    public static Entry SetText<TSource>(this Entry entry, string propertyName,
        Func<TSource, string>? getter = null,
        Action<TSource, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TextProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Text" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Text"/> binding with given information.</returns>
    public static Entry SetText(this Entry entry, string propertyName,
        Func<object, string>? getter = null,
        Action<object, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TextProperty, propertyName, getter, setter, mode);

    #endregion

    #region Placeholder

    /// <summary>
    /// Change the <see cref="Entry.Placeholder"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="placeholder">The placeholder.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Placeholder"/> as <paramref name="placeholder"/>.</returns>
    public static Entry SetPlaceholder(this Entry entry, string placeholder)
    {
        entry.Placeholder = placeholder;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Placeholder" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Placeholder"/> binding with given information.</returns>
    public static Entry SetPlaceholder(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.PlaceholderProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Placeholder" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Placeholder"/> binding with given information.</returns>
    public static Entry SetPlaceholder<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, string>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.PlaceholderProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Placeholder" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Placeholder"/> binding with given information.</returns>
    public static Entry SetPlaceholder<TSource>(this Entry entry, string propertyName,
        Func<TSource, string>? getter = null,
        Action<TSource, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.PlaceholderProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Placeholder" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Placeholder"/> binding with given information.</returns>
    public static Entry SetPlaceholder(this Entry entry, string propertyName,
        Func<object, string>? getter = null,
        Action<object, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.PlaceholderProperty, propertyName, getter, setter, mode);

    #endregion

    #region Style

    /// <summary>
    /// Change the <see cref="Entry.Style"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="style">The <see cref="EntryStyle"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Style"/> as <paramref name="style"/>.</returns>
    public static Entry SetStyle(this Entry entry, EntryStyle style)
    {
        entry.Style = style;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Style" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Style"/> binding with given information.</returns>
    public static Entry SetStyle(this Entry entry, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.StyleProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Style" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Style"/> binding with given information.</returns>
    public static Entry SetStyle<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, EntryStyle>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.StyleProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Style" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Style"/> binding with given information.</returns>
    public static Entry SetStyle<TSource>(this Entry entry, string propertyName,
        Func<TSource, EntryStyle>? getter = null,
        Action<TSource, EntryStyle>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.StyleProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Style" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Style"/> binding with given information.</returns>
    public static Entry SetStyle(this Entry entry, string propertyName,
        Func<object, EntryStyle>? getter = null,
        Action<object, EntryStyle>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.StyleProperty, propertyName, getter, setter, mode);

    #endregion

    #region Trim

    /// <summary>
    /// Change the <see cref="Entry.Trim"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="trim">The trim flag.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> as <paramref name="trim"/>.</returns>
    public static Entry SetTrim(this Entry entry, bool? trim)
    {
        entry.Trim = trim;
        return entry;
    }

    /// <summary>
    /// Change the <see cref="Entry.Trim"/> as true.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> as true.</returns>
    public static Entry EnableTrim(this Entry entry)
    {
        entry.Trim = true;
        return entry;
    }

    /// <summary>
    /// Change the <see cref="Entry.Trim"/> as false.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> as false.</returns>
    public static Entry DisableTrim(this Entry entry)
    {
        entry.Trim = false;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Trim" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> binding with given information.</returns>
    public static Entry SetTrim(this Entry entry, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.TrimProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Trim" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> binding with given information.</returns>
    public static Entry SetTrim<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, bool?>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TrimProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Trim" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> binding with given information.</returns>
    public static Entry SetTrim<TSource>(this Entry entry, string propertyName,
        Func<TSource, bool?>? getter = null,
        Action<TSource, bool?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TrimProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Trim" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Trim"/> binding with given information.</returns>
    public static Entry SetTrim(this Entry entry, string propertyName,
        Func<object, bool?>? getter = null,
        Action<object, bool?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.TrimProperty, propertyName, getter, setter, mode);

    #endregion

    #region MultiLine

    /// <summary>
    /// Change the <see cref="Entry.MultiLine"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="multiLine">The trim flag.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> as <paramref name="multiLine"/>.</returns>
    public static Entry SetMultiLine(this Entry entry, bool multiLine)
    {
        entry.MultiLine = multiLine;
        return entry;
    }

    /// <summary>
    /// Change the <see cref="Entry.MultiLine"/> as true.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> as true.</returns>
    public static Entry EnableMultiLine(this Entry entry)
    {
        entry.MultiLine = true;
        return entry;
    }

    /// <summary>
    /// Change the <see cref="Entry.MultiLine"/> as false.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> as false.</returns>
    public static Entry DisableMultiLine(this Entry entry)
    {
        entry.MultiLine = false;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.MultiLine" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> binding with given information.</returns>
    public static Entry SetMultiLine(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.MultiLineProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.MultiLine" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> binding with given information.</returns>
    public static Entry SetMultiLine<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, bool>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MultiLineProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.MultiLine" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> binding with given information.</returns>
    public static Entry SetMultiLine<TSource>(this Entry entry, string propertyName,
        Func<TSource, bool>? getter = null,
        Action<TSource, bool>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MultiLineProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.MultiLine" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.MultiLine"/> binding with given information.</returns>
    public static Entry SetMultiLine(this Entry entry, string propertyName,
        Func<object, bool>? getter = null,
        Action<object, bool>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MultiLineProperty, propertyName, getter, setter, mode);

    #endregion

    #region Echo Mode

    /// <summary>
    /// Change the <see cref="Entry.EchoMode"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="echoMode">The <see cref="EntryEchoMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoMode"/> as <paramref name="echoMode"/>.</returns>
    public static Entry SetEchoMode(this Entry entry, EntryEchoMode echoMode)
    {
        entry.EchoMode = echoMode;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoMode" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoMode"/> binding with given information.</returns>
    public static Entry SetEchoMode(this Entry entry, IBindableProperty property, BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.EchoModeProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoMode" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoMode"/> binding with given information.</returns>
    public static Entry SetEchoMode<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, EntryEchoMode>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoModeProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoMode" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoMode"/> binding with given information.</returns>
    public static Entry SetEchoMode<TSource>(this Entry entry, string propertyName,
        Func<TSource, EntryEchoMode>? getter = null,
        Action<TSource, EntryEchoMode>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoModeProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoMode" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoMode"/> binding with given information.</returns>
    public static Entry SetEchoMode(this Entry entry, string propertyName,
        Func<object, EntryEchoMode>? getter = null,
        Action<object, EntryEchoMode>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoModeProperty, propertyName, getter, setter, mode);

    #endregion

    #region Echo Character

    /// <summary>
    /// Change the <see cref="Entry.EchoCharacter"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="echoCharacter">The echo character.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoCharacter"/> as <paramref name="echoCharacter"/>.</returns>
    public static Entry SetEchoCharacter(this Entry entry, string echoCharacter)
    {
        entry.EchoCharacter = echoCharacter;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoCharacter"/> binding with given information.</returns>
    public static Entry SetEchoCharacter(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.EchoCharacterProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoCharacter"/> binding with given information.</returns>
    public static Entry SetEchoCharacter<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, string>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoCharacterProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoCharacter"/> binding with given information.</returns>
    public static Entry SetEchoCharacter<TSource>(this Entry entry, string propertyName,
        Func<TSource, string>? getter = null,
        Action<TSource, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoCharacterProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EchoCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EchoCharacter"/> binding with given information.</returns>
    public static Entry SetEchoCharacter(this Entry entry, string propertyName,
        Func<object, string>? getter = null,
        Action<object, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EchoCharacterProperty, propertyName, getter, setter, mode);

    #endregion

    #region End of Buffer Character

    /// <summary>
    /// Change the <see cref="Entry.EndOfBufferCharacter"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="endOfBufferCharacter">The end of buffer character.</param>
    /// <returns>The given <paramref name="entry"/> with <seeecho  cref="Entry.EndOfBufferCharacter"/> as <paramref name="endOfBufferCharacter"/>.</returns>
    public static Entry SetEndOfBufferCharacter(this Entry entry, string endOfBufferCharacter)
    {
        entry.EndOfBufferCharacter = endOfBufferCharacter;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EndOfBufferCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EndOfBufferCharacter"/> binding with given information.</returns>
    public static Entry SetEndOfBufferCharacter(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.EndOfTheBufferCharacterProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.EndOfBufferCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EndOfBufferCharacter"/> binding with given information.</returns>
    public static Entry SetEndOfBufferCharacter<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, string>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EndOfTheBufferCharacterProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EndOfBufferCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EndOfBufferCharacter"/> binding with given information.</returns>
    public static Entry SetEndOfBufferCharacter<TSource>(this Entry entry, string propertyName,
        Func<TSource, string>? getter = null,
        Action<TSource, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EndOfTheBufferCharacterProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.EndOfBufferCharacter" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.EndOfTheBufferCharacterProperty"/> binding with given information.</returns>
    public static Entry SetEndOfBufferCharacter(this Entry entry, string propertyName,
        Func<object, string>? getter = null,
        Action<object, string>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.EndOfTheBufferCharacterProperty, propertyName, getter, setter, mode);

    #endregion

    #region Cursor Position

    /// <summary>
    /// Change the <see cref="Entry.CursorPosition"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="cursorPosition">The <see cref="CursorPosition"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.CursorPosition"/> as <paramref name="cursorPosition"/>.</returns>
    public static Entry SetCursorPosition(this Entry entry, CursorPosition cursorPosition)
    {
        entry.CursorPosition = cursorPosition;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.CursorPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.CursorPosition"/> binding with given information.</returns>
    public static Entry SetCursorPosition(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.CursorPositionProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.CursorPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.CursorPosition"/> binding with given information.</returns>
    public static Entry SetCursorPosition<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, CursorPosition>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.CursorPositionProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.CursorPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.CursorPosition"/> binding with given information.</returns>
    public static Entry SetCursorPosition<TSource>(this Entry entry, string propertyName,
        Func<TSource, CursorPosition>? getter = null,
        Action<TSource, CursorPosition>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.CursorPositionProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.CursorPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.CursorPosition"/> binding with given information.</returns>
    public static Entry SetCursorPosition(this Entry entry, string propertyName,
        Func<object, CursorPosition>? getter = null,
        Action<object, CursorPosition>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.CursorPositionProperty, propertyName, getter, setter, mode);

    #endregion

    #region Margin

    /// <summary>
    /// Change the <see cref="Entry.Margin"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="margin">The <see cref="Margin"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Margin"/> as <paramref name="margin"/>.</returns>
    public static Entry SetMargin(this Entry entry, Margin margin)
    {
        entry.Margin = margin;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Margin" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Margin"/> binding with given information.</returns>
    public static Entry SetMargin(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.MarginProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.Margin" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Margin"/> binding with given information.</returns>
    public static Entry SetMargin<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, Margin>> property, BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MarginProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Margin" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Margin"/> binding with given information.</returns>
    public static Entry SetMargin<TSource>(this Entry entry, string propertyName,
        Func<TSource, Margin>? getter = null,
        Action<TSource, Margin>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MarginProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.Margin" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.Margin"/> binding with given information.</returns>
    public static Entry SetMargin(this Entry entry, string propertyName,
        Func<object, Margin>? getter = null,
        Action<object, Margin>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.MarginProperty, propertyName, getter, setter, mode);

    #endregion

    #region LastColumnPosition

    /// <summary>
    /// Change the <see cref="Entry.LastColumnPosition"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="columnPosition">The <see cref="IColumnPosition"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.LastColumnPosition"/> as <paramref name="columnPosition"/>.</returns>
    public static Entry SetLastColumnPosition(this Entry entry, AtColumnPosition columnPosition)
    {
        entry.LastColumnPosition = columnPosition;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.LastColumnPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.LastColumnPosition"/> binding with given information.</returns>
    public static Entry SetLastColumnPosition(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.LastColumnPositionProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.LastColumnPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.LastColumnPosition"/> binding with given information.</returns>
    public static Entry SetLastColumnPosition<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, AtColumnPosition>> property,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.LastColumnPositionProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.LastColumnPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.LastColumnPosition"/> binding with given information.</returns>
    public static Entry SetLastColumnPosition<TSource>(this Entry entry, string propertyName,
        Func<TSource, AtColumnPosition>? getter = null,
        Action<TSource, AtColumnPosition>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.LastColumnPositionProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.LastColumnPosition" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.LastColumnPosition"/> binding with given information.</returns>
    public static Entry SetLastColumnPosition(this Entry entry, string propertyName,
        Func<object, AtColumnPosition>? getter = null,
        Action<object, AtColumnPosition>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.LastColumnPositionProperty, propertyName, getter, setter, mode);

    #endregion

    #region Screen Area

    /// <summary>
    /// Change the <see cref="Entry.ScreenArea"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="screenArea">The <see cref="IColumnPosition"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.ScreenArea"/> as <paramref name="screenArea"/>.</returns>
    public static Entry SetScreenArea(this Entry entry, Rect screenArea)
    {
        entry.ScreenArea = screenArea;
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.ScreenArea" />.
    /// </summary>
    /// <param name="entry">The <see cref="Label"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.ScreenArea"/> binding with given information.</returns>
    public static Entry SetScreenArea(this Entry entry, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        entry.SetBinding(Entry.ScreenAreaProperty, property, mode);
        return entry;
    }

    /// <summary>
    /// Add binding to the <see cref="Entry.ScreenArea" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.ScreenArea"/> binding with given information.</returns>
    public static Entry SetScreenArea<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(
        this Entry entry, Expression<Func<TSource, Rect>> property,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.ScreenAreaProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.ScreenArea" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.ScreenArea"/> binding with given information.</returns>
    public static Entry SetScreenArea<TSource>(this Entry entry, string propertyName,
        Func<TSource, Rect>? getter = null,
        Action<TSource, Rect>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.ScreenAreaProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Entry.ScreenArea" />.
    /// </summary>
    /// <param name="entry">The <see cref="Entry"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="entry"/> with <see cref="Entry.ScreenArea"/> binding with given information.</returns>
    public static Entry SetScreenArea(this Entry entry, string propertyName,
        Func<object, Rect>? getter = null,
        Action<object, Rect>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => entry.SetBinding(Entry.ScreenAreaProperty, propertyName, getter, setter, mode);

    #endregion

}

/// <summary>
/// The extension methods for <see cref="EntryStyle"/>.
/// </summary>
public static class EntryStyleExtensions
{
    /// <summary>
    /// Change the <see cref="EntryStyle.Base"/>.
    /// </summary>
    /// <param name="entryStyle">The <see cref="EntryStyle"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="entryStyle"/> with <see cref="EntryStyle.Base"/> as <paramref name="style"/>.</returns>
    public static EntryStyle SetBase(this EntryStyle entryStyle, Style style)
    {
        entryStyle.Base = style;
        return entryStyle;
    }

    /// <summary>
    /// Change the <see cref="EntryStyle.Text"/>.
    /// </summary>
    /// <param name="entryStyle">The <see cref="EntryStyle"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="entryStyle"/> with <see cref="EntryStyle.Text"/> as <paramref name="style"/>.</returns>
    public static EntryStyle SetText(this EntryStyle entryStyle, Style style)
    {
        entryStyle.Text = style;
        return entryStyle;
    }

    /// <summary>
    /// Change the <see cref="EntryStyle.Placeholder"/>.
    /// </summary>
    /// <param name="entryStyle">The <see cref="EntryStyle"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="entryStyle"/> with <see cref="EntryStyle.Placeholder"/> as <paramref name="style"/>.</returns>
    public static EntryStyle SetPlaceholder(this EntryStyle entryStyle, Style style)
    {
        entryStyle.Placeholder = style;
        return entryStyle;
    }
}
