using Acutipupu.Components;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Behaviors;

/// <summary>
/// Base class for generalized user-defined behaviors that can respond to arbitrary conditions and events.
/// </summary>
/// <remarks>Application developers should specialize the <see cref="Behavior{T}" /> generic class, instead of directly using <see cref="Behavior" />.</remarks>
public abstract class Behavior : ObservableObject, IAttachedObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Behavior"/> class.
    /// </summary>
    protected Behavior()
        : this(typeof(Component))
    {
    }

    internal Behavior(Type associatedType)
    {
        AssociatedType = associatedType ?? throw new ArgumentNullException(nameof(associatedType));
    }

    /// <summary>
    /// Gets the type of the objects with which this <see cref="Behavior" /> can be associated.
    /// </summary>
    protected Type AssociatedType { get; }

    void IAttachedObject.AttachTo(ObservableObject? bindable)
    {
        if (bindable == null)
        {
            ArgumentNullException.ThrowIfNull(bindable);
        }

        if (!AssociatedType.IsInstanceOfType(bindable))
        {
            throw new InvalidOperationException("bindable not an instance of AssociatedType");
        }

        OnAttachedTo(bindable);
    }

    void IAttachedObject.DetachFrom(ObservableObject? bindable)
    {
        if (bindable == null)
        {
            ArgumentNullException.ThrowIfNull(bindable);
        }

        if (!AssociatedType.IsInstanceOfType(bindable))
        {
            throw new InvalidOperationException("bindable not an instance of AssociatedType");
        }

        OnDetachingFrom(bindable);
    }

    /// <summary>
    /// Application developers override this method to implement the behaviors that will be associated with <paramref name="bindable" />.
    /// </summary>
    /// <param name="bindable">The bindable object to which the behavior was attached.</param>
    protected virtual void OnAttachedTo(ObservableObject bindable)
    {
    }

    /// <summary>
    /// Application developers override this method to remove the behaviors from <paramref name="bindable" />
    /// that were implemented in a previous call to the <see cref="OnAttachedTo(ObservableObject)"/> method.
    /// </summary>
    /// <param name="bindable">The bindable object from which the behavior was detached.</param>
    protected virtual void OnDetachingFrom(ObservableObject bindable)
    {
    }
}

/// <inheritdoc cref="Behavior"/>
public abstract class Behavior<T> : Behavior
    where T : Component 
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Behavior{T}"/> class.
    /// </summary>
    protected Behavior()
        : base(typeof(T))
    {
    }

    /// <inheritdoc cref="Behavior.OnAttachedTo"/>
    protected override void OnAttachedTo(ObservableObject bindable)
    {
        base.OnAttachedTo(bindable);
        OnAttachedTo((T)bindable);
    }

    /// <summary>
    /// Application developers override this method to implement the behaviors that will be associated with <paramref name="bindable" />.
    /// </summary>
    /// <param name="bindable">The bindable object to which the behavior was attached.</param>
    protected virtual void OnAttachedTo(T bindable)
    {
    }

    /// <inheritdoc cref="Behavior.OnDetachingFrom"/>
    protected override void OnDetachingFrom(ObservableObject bindable)
    {
        OnDetachingFrom((T)bindable);
        base.OnDetachingFrom(bindable);
    }

    /// <summary>
    /// Application developers override this method to remove the behaviors from <paramref name="bindable" />
    /// that were implemented in a previous call to the <see cref="OnAttachedTo(T)"/> method.
    /// </summary>
    /// <param name="bindable">The bindable object from which the behavior was detached.</param>
    protected virtual void OnDetachingFrom(T bindable)
    {
    }
}
