using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Acutipupu.Bindings;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// This <see cref="Spinner"/> extension methods.
/// </summary>
public static class SpinnerExtensions
{
    #region Set SpinnerConfiguration

    /// <summary>
    /// Change the <see cref="Spinner.Configuration" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="configuration">The <see cref="SpinnerConfiguration"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.Configuration"/> as <paramref name="configuration"/>.</returns>
    public static Spinner SetConfiguration(this Spinner spinner, SpinnerConfiguration configuration)
    {
        spinner.Configuration = configuration;
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.Configuration" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.Configuration"/> binding with given information.</returns>
    public static Spinner SetConfiguration(this Spinner spinner, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        spinner.SetBinding(Spinner.ConfigurationProperty, property, mode);
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.Configuration" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="property">The view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Configuration"/> binding with given information.</returns>
    public static Spinner SetConfiguration<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Spinner container, Expression<Func<TSource, SpinnerConfiguration>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.ConfigurationProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.Configuration" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Configuration"/> binding with given information.</returns>
    public static Spinner SetConfiguration<TSource>(this Spinner container, string propertyName,
        Func<TSource, SpinnerConfiguration>? getter = null,
        Action<TSource, SpinnerConfiguration>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.ConfigurationProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.Configuration" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Configuration"/> binding with given information.</returns>
    public static Spinner SetConfiguration(this Spinner container, string propertyName,
        Func<object, SpinnerConfiguration>? getter = null,
        Action<object, SpinnerConfiguration>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.ConfigurationProperty, propertyName, getter, setter, mode);

    #endregion

    #region Set Style

    /// <summary>
    /// Change the <see cref="Spinner.Style" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.Style"/> as <paramref name="style"/>.</returns>
    public static Spinner SetStyle(this Spinner spinner, Style style)
    {
        spinner.Style = style;
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.Style" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.Style"/> binding with given information.</returns>
    public static Spinner SetStyle(this Spinner spinner, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        spinner.SetBinding(Spinner.StyleProperty, property, mode);
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="property">The view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Style"/> binding with given information.</returns>
    public static Spinner SetStyle<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Spinner container, Expression<Func<TSource, Style>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.StyleProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Style"/> binding with given information.</returns>
    public static Spinner SetStyle<TSource>(this Spinner container, string propertyName,
        Func<TSource, Style>? getter = null,
        Action<TSource, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.StyleProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.Style"/> binding with given information.</returns>
    public static Spinner SetStyle(this Spinner container, string propertyName,
        Func<object, Style>? getter = null,
        Action<object, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.StyleProperty, propertyName, getter, setter, mode);

    #endregion

    #region Set CurrentFrame

    /// <summary>
    /// Change the <see cref="Spinner.CurrentFrame" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="currentFrame">The current frame.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.CurrentFrame"/> as <paramref name="currentFrame"/>.</returns>
    public static Spinner SetCurrentFrame(this Spinner spinner, string? currentFrame)
    {
        spinner.CurrentFrame = currentFrame;
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrame" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.CurrentFrame"/> binding with given information.</returns>
    public static Spinner SetCurrentFrame(this Spinner spinner, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        spinner.SetBinding(Spinner.CurrentFrameProperty, property, mode);
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrame" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="property">The view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrame"/> binding with given information.</returns>
    public static Spinner SetCurrentFrame<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Spinner container, Expression<Func<TSource, string?>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrame" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrame"/> binding with given information.</returns>
    public static Spinner SetCurrentFrame<TSource>(this Spinner container, string propertyName,
        Func<TSource, string?>? getter = null,
        Action<TSource, string?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrame" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrame"/> binding with given information.</returns>
    public static Spinner SetCurrentFrame(this Spinner container, string propertyName,
        Func<object, string?>? getter = null,
        Action<object, string?>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameProperty, propertyName, getter, setter, mode);

    #endregion

    #region Set CurrentFrameIndex

    /// <summary>
    /// Change the <see cref="Spinner.CurrentFrameIndex" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="currentFrameIndex">The current frame index.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.CurrentFrameIndex"/> as <paramref name="currentFrameIndex"/>.</returns>
    public static Spinner SetCurrentFrameIndex(this Spinner spinner, int currentFrameIndex)
    {
        spinner.CurrentFrameIndex = currentFrameIndex;
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrameIndex" />.
    /// </summary>
    /// <param name="spinner">The <see cref="Spinner"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="spinner"/> with <see cref="Spinner.CurrentFrameIndex"/> binding with given information.</returns>
    public static Spinner SetCurrentFrameIndex(this Spinner spinner, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        spinner.SetBinding(Spinner.CurrentFrameIndexProperty, property, mode);
        return spinner;
    }

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrameIndex" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="property">The view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrameIndex"/> binding with given information.</returns>
    public static Spinner SetCurrentFrameIndex<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Spinner container, Expression<Func<TSource, int>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameIndexProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrameIndex" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrameIndex"/> binding with given information.</returns>
    public static Spinner SetCurrentFrameIndex<TSource>(this Spinner container, string propertyName,
        Func<TSource, int>? getter = null,
        Action<TSource, int>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameIndexProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Spinner.CurrentFrameIndex" />.
    /// </summary>
    /// <param name="container">The <see cref="Spinner"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Spinner.CurrentFrameIndex"/> binding with given information.</returns>
    public static Spinner SetCurrentFrameIndex(this Spinner container, string propertyName,
        Func<object, int>? getter = null,
        Action<object, int>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Spinner.CurrentFrameIndexProperty, propertyName, getter, setter, mode);

    #endregion
}
