using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Acutipupu.Bindings;
using Boto.Layouts;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="Container"/> extensions.
/// </summary>
public static class ContainerExtensions
{
    #region Split Direction

    /// <summary>
    /// Change the <see cref="Container.SplitDirection"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="direction">The <see cref="Direction"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.SplitDirection"/> as <paramref name="direction"/>.</returns>
    public static Container SetSplitDirection(this Container container, Direction direction)
    {
        container.SplitDirection = direction;
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.SplitDirection" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.SplitDirection"/> binding with given information.</returns>
    public static Container SetSplitDirection(this Container container, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        container.SetBinding(Container.SplitDirectionProperty, property, mode);
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.SplitDirection" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.SplitDirection"/> binding with given information.</returns>
    public static Container SetSplitDirection<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Container container, Expression<Func<TSource, Direction>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.SplitDirectionProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.SplitDirection" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.SplitDirection"/> binding with given information.</returns>
    public static Container SetSplitDirection<TSource>(this Container container, string propertyName,
        Func<TSource, Direction>? getter = null,
        Action<TSource, Direction>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.SplitDirectionProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.SplitDirection" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.SplitDirection"/> binding with given information.</returns>
    public static Container SetSplitDirection(this Container container, string propertyName,
        Func<object, Direction>? getter = null,
        Action<object, Direction>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.SplitDirectionProperty, propertyName, getter, setter, mode);

    #endregion

    #region Set Style

    /// <summary>
    /// Change the <see cref="Container.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.Style"/> as <paramref name="style"/>.</returns>
    public static Container SetStyle(this Container container, Style style)
    {
        container.Style = style;
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.Style"/> binding with given information.</returns>
    public static Container SetStyle(this Container container, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        container.SetBinding(Container.StyleProperty, property, mode);
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.Style"/> binding with given information.</returns>
    public static Container SetStyle<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Container container, Expression<Func<TSource, Style>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.StyleProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.Style"/> binding with given information.</returns>
    public static Container SetStyle<TSource>(this Container container, string propertyName,
        Func<TSource, Style>? getter = null,
        Action<TSource, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.StyleProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.Style" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.Style"/> binding with given information.</returns>
    public static Container SetStyle(this Container container, string propertyName,
        Func<object, Style>? getter = null,
        Action<object, Style>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.StyleProperty, propertyName, getter, setter, mode);

    #endregion

    #region Set Expand to fill

    /// <summary>
    /// Change the <see cref="Container.ExpandToFill"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="expandToFill">The <see cref="Direction"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> as <paramref name="expandToFill"/>.</returns>
    public static Container SetExpandToFill(this Container container, bool expandToFill)
    {
        container.ExpandToFill = expandToFill;
        return container;
    }

    /// <summary>
    /// Enable the <see cref="Container.ExpandToFill"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> as true.</returns>
    public static Container EnableExpandToFill(this Container container)
    {
        container.ExpandToFill = true;
        return container;
    }

    /// <summary>
    /// Enable the <see cref="Container.ExpandToFill"/>.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> as false.</returns>
    public static Container DisableExpandToFill(this Container container)
    {
        container.ExpandToFill = false;
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.ExpandToFill" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The <see cref="IBindableProperty"/>.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> binding with given information.</returns>
    public static Container SetExpandToFill(this Container container, IBindableProperty property,
        BindingMode mode = BindingMode.TwoWay)
    {
        container.SetBinding(Container.ExpandToFillProperty, property, mode);
        return container;
    }

    /// <summary>
    /// Add binding to the <see cref="Container.ExpandToFill" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="property">The getter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> binding with given information.</returns>
    public static Container SetExpandToFill<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource>(this Container container, Expression<Func<TSource, bool>> property,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.ExpandToFillProperty, property, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.ExpandToFill" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> binding with given information.</returns>
    public static Container SetExpandToFill<TSource>(this Container container, string propertyName,
        Func<TSource, bool>? getter = null,
        Action<TSource, bool>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.ExpandToFillProperty, propertyName, getter, setter, mode);

    /// <summary>
    /// Add binding to the <see cref="Container.ExpandToFill" />.
    /// </summary>
    /// <param name="container">The <see cref="Container"/>.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="getter">The getter view model property.</param>
    /// <param name="setter">The setter view model property.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    /// <returns>The given <paramref name="container"/> with <see cref="Container.ExpandToFill"/> binding with given information.</returns>
    public static Container SetExpandToFill(this Container container, string propertyName,
        Func<object, bool>? getter = null,
        Action<object, bool>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        => container.SetBinding(Container.ExpandToFillProperty, propertyName, getter, setter, mode);

    #endregion
}
