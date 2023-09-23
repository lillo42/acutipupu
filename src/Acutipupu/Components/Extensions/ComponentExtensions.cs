using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Acutipupu.Behaviors;
using Acutipupu.Bindings;

namespace Acutipupu.Components.Extensions;

/// <summary>
/// The <see cref="Component"/> extension methods.
/// </summary>
public static class ComponentExtensions
{
    /// <summary>
    /// Change the <see cref="Component.BindingContext"/>.
    /// </summary>
    /// <param name="component">The <typeparamref name="T"/>.</param>
    /// <param name="bindingContext">The binding context.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The <paramref name="component"/> with <see cref="Component.BindingContext"/> as <paramref name="bindingContext"/>.</returns>
    public static T SetBindingContext<T>(this T component, object? bindingContext)
        where T : Component
    {
        component.BindingContext = bindingContext;
        return component;
    }

    /// <summary>
    /// Add the <see cref="Behavior"/> to <see cref="Component.Behaviors"/>.
    /// </summary>
    /// <param name="component">The <typeparamref name="T"/>.</param>
    /// <param name="behavior">The <see cref="Behavior"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The <paramref name="component"/> with <see cref="Component.Behaviors"/> + <paramref name="behavior"/>.</returns>
    public static T AddBehavior<T>(this T component, Behavior behavior)
        where T : Component
    {
        component.Behaviors.Add(behavior);
        return component;
    }

    internal static T SetBinding<T,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
        TSource,
        TSourceType>(this T component,
        IBindableProperty componentBindableProperty,
        Expression<Func<TSource, TSourceType>> property,
        BindingMode mode = BindingMode.TwoWay)
        where T : Component
    {
        if (property.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("The expression must be a member expression.", nameof(property));
        }

        var propertyInfo = typeof(TSource).GetProperty(memberExpression.Member.Name);
        if (propertyInfo is null)
        {
            throw new ArgumentException(
                $"The property {memberExpression.Member.Name} does not exist in {typeof(TSource).Name}.",
                nameof(property));
        }

        Action<TSource, TSourceType> setter = (_, _) => { };
        if (mode is BindingMode.TwoWay or BindingMode.OneWayFromSource)
        {
            var setMethod = propertyInfo.GetSetMethod(nonPublic: true);
            if (setMethod is null)
            {
                throw new InvalidOperationException("The property must have a setter.");
            }

            setter = setMethod.CreateDelegate<Action<TSource, TSourceType>>();
        }


        component.SetBinding(componentBindableProperty,
            new BindableProperty<TSource, TSourceType>(memberExpression.Member.Name,
                property.Compile(), setter),
            mode);

        return component;
    }

    internal static T SetBinding<T, TSource, TSourceType>(this T component, IBindableProperty componentBindableProperty,
        string propertyName,
        Func<TSource, TSourceType>? getter = null,
        Action<TSource, TSourceType>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        where T : Component
    {
        if (getter == null && setter == null)
        {
            throw new ArgumentException("Either getter or setter must be not null.", nameof(getter));
        }

        if (getter != null && setter == null)
        {
            setter = (_, _) => { };
            mode = BindingMode.OneWayFromSource;
        }
        else if (getter == null && setter != null)
        {
            getter = _ => default!;
            mode = BindingMode.OneWay;
        }

        component.SetBinding(componentBindableProperty,
            new BindableProperty<TSource, TSourceType>(propertyName, getter!, setter!),
            mode);

        return component;
    }

    internal static T SetBinding<T, TSourceType>(this T component, IBindableProperty componentBindableProperty,
        string propertyName,
        Func<object, TSourceType>? getter = null,
        Action<object, TSourceType>? setter = null,
        BindingMode mode = BindingMode.TwoWay)
        where T : Component
    {
        if (getter == null && setter == null)
        {
            throw new ArgumentException("Either getter or setter must be not null.", nameof(getter));
        }

        if (getter != null && setter == null)
        {
            setter = (_, _) => { };
            mode = BindingMode.OneWayFromSource;
        }
        else if (getter == null && setter != null)
        {
            getter = _ => default!;
            mode = BindingMode.OneWay;
        }

        component.SetBinding(componentBindableProperty,
            new BindableProperty(propertyName,
                vm => getter!(vm),
                (vm, val) => setter!(vm, (TSourceType)val!)),
            mode);

        return component;
    }
}
