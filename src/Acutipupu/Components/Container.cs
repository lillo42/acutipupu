using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Acutipupu.Bindings;
using Boto.Layouts;
using Boto.Widgets;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The container.
/// </summary>
public partial class Container : Component
{
    private readonly ObservableCollection<IConstraint> _constraints;
    private readonly ObservableCollection<Component> _components;
    private readonly Layout _layout = new();
    private Block? _block;

    /// <summary>
    /// The style <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _style = new();

    /// <summary>
    /// The flag indicating whether the container should expand to fill the available space.
    /// </summary>
    [ObservableProperty] private bool _expandToFill = true;

    /// <summary>
    /// The split <see cref="Direction"/>.
    /// </summary>
    [ObservableProperty] private Direction _splitDirection;

    /// <summary>
    /// Initialize a new instance of <see cref="Container"/>.
    /// </summary>
    public Container()
    {
        _constraints = new ObservableCollection<IConstraint>();
        _constraints.CollectionChanged += (_, _) => IsDirty = true;

        _components = new ObservableCollection<Component>();
    }

    /// <summary>
    /// The <see cref="IList{T}"/> of <see cref="Component"/>.
    /// </summary>
    /// <remarks>The <see cref="Constraints"/> should have the same amount of element of <see cref="Components"/>.</remarks>
    public IList<Component> Components => _components;

    /// <summary>
    /// The <see cref="IList{T}"/> of <see cref="IConstraint"/>.
    /// </summary>
    /// <remarks>The <see cref="Components"/> should have the same amount of element of <see cref="Constraints"/>.</remarks>
    public IList<IConstraint> Constraints => _constraints;

    /// <summary>
    /// Occurs when the collection changes, either by adding or removing an item.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? ComponentsChanged
    {
        add => _components.CollectionChanged += value;
        remove => _components.CollectionChanged -= value;
    }

    /// <summary>
    /// The <see cref="Style"/> property as <see cref="IBindableProperty{TSource,TProperty}"/>
    /// </summary>
    public static IBindableProperty<Container, Style> StyleProperty { get; } = new BindableProperty<Container, Style>(
        nameof(Style),
        container => container.Style,
        (container, value) => container.Style = value);

    /// <summary>
    /// The <see cref="ExpandToFill"/> property as <see cref="IBindableProperty{TSource,TProperty}"/>
    /// </summary>
    public static IBindableProperty<Container, bool> ExpandToFillProperty { get; } =
        new BindableProperty<Container, bool>(
            nameof(ExpandToFill),
            container => container.ExpandToFill,
            (container, value) => container.ExpandToFill = value);

    /// <summary>
    /// The <see cref="ExpandToFill"/> property as <see cref="IBindableProperty{TSource,TProperty}"/>
    /// </summary>
    public static IBindableProperty<Container, Direction> SplitDirectionProperty { get; } =
        new BindableProperty<Container, Direction>(
            nameof(SplitDirection),
            container => container.SplitDirection,
            (container, value) => container.SplitDirection = value);

    /// <inheritdoc />
    public override void Render(IRenderContext context)
    {
        if (IsDirty)
        {
            Update();
        }

        var area = context.Area;
        if (_block is not null)
        {
            _block.Render(area, context.Buffer);
            area = _block.Inner(context.Area);
        }

        var areas = _layout.Split(area);

        Parallel.ForEach(Components, (component, _, index) =>
        {
            var tmp = context.New(areas[(int)index]);
            try
            {
                component.Render(tmp);
            }
            catch
            {
                // Ignore
            }
        });
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        Style.PropertyChanged -= MarkAsDirty;

        foreach (var component in Components)
        {
            component.Dispose();
        }
    }

    /// <summary>
    /// Add <see cref="Component"/> associated with <see cref="IConstraint"/>.
    /// </summary>
    /// <param name="constraint">The <see cref="IConstraint"/>.</param>
    /// <param name="component">The <see cref="Component"/>.</param>
    /// <returns>Current container.</returns>
    public Container Add(IConstraint constraint, Component component)
    {
        _components.Add(component);
        _constraints.Add(constraint);
        return this;
    }

    partial void OnStyleChanging(Style? oldValue, Style newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= MarkAsDirty;
        }

        newValue.PropertyChanged += MarkAsDirty;
    }

    private void Update()
    {
        _layout
            .SetDirection(SplitDirection)
            .SetMargin(Style.Margin)
            .SetExpandToFill(ExpandToFill)
            .SetConstraints(Constraints);

        _block = Style.ToBlock();
        IsDirty = false;
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(BindingContext))
        {
            foreach (var component in _components)
            {
                component.BindingContext = BindingContext;
            }
        }
    }
}
