namespace Acutipupu.Bindings;

/// <summary>
/// The bindable property.
/// </summary>
public interface IBindableProperty
{
    /// <summary>
    /// The property name.
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// The property getter.
    /// </summary>
    Func<object, object?> Getter { get; }

    /// <summary>
    /// The property setter.
    /// </summary>
    Action<object, object?> Setter { get; }
}

/// <summary>
/// The bindable property.
/// </summary>
/// <typeparam name="TSource">The source.</typeparam>
/// <typeparam name="TProperty">The property type.</typeparam>
public interface IBindableProperty<in TSource, TProperty> : IBindableProperty
{
    Func<object, object?> IBindableProperty.Getter => source => Getter((TSource)source);

    Action<object, object?> IBindableProperty.Setter => (source, value) => Setter((TSource)source, (TProperty)value!);

    /// <summary>
    /// The property getter.
    /// </summary>
    new Func<TSource, TProperty> Getter { get; }

    /// <summary>
    /// The property setter.
    /// </summary>
    new Action<TSource, TProperty> Setter { get; }
}

/// <summary>
/// The bindable property.
/// </summary>
/// <param name="PropertyName">The property name.</param>
/// <param name="Getter">The property getter.</param>
/// <param name="Setter">The property setter.</param>
public record BindableProperty(string PropertyName, 
    Func<object, object?> Getter, 
    Action<object, object?> Setter) : IBindableProperty;

/// <summary>
/// The bindable property.
/// </summary>
/// <param name="PropertyName">The property name.</param>
/// <param name="Getter">The property getter.</param>
/// <param name="Setter">The property setter.</param>
/// <typeparam name="TSource">The source.</typeparam>
/// <typeparam name="TProperty">The property type.</typeparam>
public record BindableProperty<TSource, TProperty>(string PropertyName,
        Func<TSource, TProperty> Getter,
        Action<TSource, TProperty> Setter) : IBindableProperty<TSource, TProperty>;

/// <summary>
/// The bindable property. 
/// </summary>
/// <param name="Setter">The target <see cref="IBindableProperty"/>.</param>
/// <param name="Getter">The source <see cref="IBindableProperty"/>.</param>
public record BindableProperties(IBindableProperty Setter, IBindableProperty Getter);
