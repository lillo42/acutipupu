using System.ComponentModel;
using Acutipupu.Behaviors;
using Acutipupu.Bindings;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The base for generalized components.
/// </summary>
public abstract partial class Component : ObservableObject, IDisposable
{
    [ObservableProperty] private object? _bindingContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Component"/> class.
    /// </summary>
    protected Component()
    {
        var behaviors = new AttachedCollection<Behavior>();
        behaviors.AttachTo(this);
        Behaviors = behaviors;
    }

    /// <summary>
    /// Flag indicating if any property was changed.
    /// </summary>
    protected bool IsDirty { get; set; }

    /// <summary>
    /// Mark the component as dirty when a property change.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/>.</param>
    protected virtual void MarkAsDirty(object? sender, PropertyChangedEventArgs e) => IsDirty = true;

    /// <summary>
    /// The bindings between the target and source.
    /// </summary>
    public virtual Dictionary<string, BindableProperties> ComponentToViewModelBindings { get; } = new();

    /// <summary>
    /// The bindings between the target and source.
    /// </summary>
    public virtual Dictionary<string, BindableProperties> ViewModelToComponentBindings { get; } = new();

    /// <summary>
    /// The identifier of the component.
    /// </summary>
    public virtual Guid Id { get; } = Guid.NewGuid();

    /// <summary>
    /// The list of Behaviors associated to this element.
    /// </summary>
    public virtual IList<Behavior> Behaviors { get; }

    /// <summary>
    /// Render the current component.
    /// </summary>
    /// <param name="context">The <see cref="IRenderContext"/>.</param>
    public abstract void Render(IRenderContext context);

    partial void OnBindingContextChanging(object? oldValue, object? newValue)
    {
        if (oldValue is INotifyPropertyChanged oldNotify)
        {
            oldNotify.PropertyChanged -= OnBindingContextPropertyChanged;
        }

        if (newValue is INotifyPropertyChanged newNotify)
        {
            foreach (var (_, (component, viewModel)) in ViewModelToComponentBindings)
            {
                component.Setter(this, viewModel.Getter(newValue));
            }

            newNotify.PropertyChanged += OnBindingContextPropertyChanged;
        }
    }

    private void OnBindingContextPropertyChanged(object? sender, PropertyChangedEventArgs arg)
    {
        if (sender is not null
            && !string.IsNullOrEmpty(arg.PropertyName)
            && ViewModelToComponentBindings.TryGetValue(arg.PropertyName, out var binding))
        {
            var (component, viewModel) = binding;
            component.Setter(this, viewModel.Getter(sender));
        }
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        IsDirty = true;

        if (!string.IsNullOrEmpty(e.PropertyName)
            && BindingContext is not null
            && ComponentToViewModelBindings.TryGetValue(e.PropertyName, out var binding))
        {
            var (viewModel, component) = binding;
            viewModel.Setter(BindingContext, component.Getter(this));
        }
    }

    /// <inheritdoc />
    public virtual void Dispose()
    {
        if (BindingContext is INotifyPropertyChanged notify)
        {
            notify.PropertyChanged -= OnBindingContextPropertyChanged;
        }

        ComponentToViewModelBindings.Clear();
        ViewModelToComponentBindings.Clear();
    }

    /// <summary>
    /// Set binding between two properties.
    /// </summary>
    /// <param name="componentProperty">The target property.</param>
    /// <param name="viewModelProperty">The source target.</param>
    /// <param name="mode">The <see cref="BindingMode"/>.</param>
    public virtual Component SetBinding(IBindableProperty componentProperty, IBindableProperty viewModelProperty,
        BindingMode mode = BindingMode.TwoWay)
    {
        if (mode == BindingMode.TwoWay)
        {
            ComponentToViewModelBindings.Add(componentProperty.PropertyName, new(viewModelProperty, componentProperty));
            ViewModelToComponentBindings.Add(viewModelProperty.PropertyName, new(componentProperty, viewModelProperty));

            if (BindingContext is not null)
            {
                componentProperty.Setter(this, viewModelProperty.Getter(BindingContext));
            }
        }
        else if (mode == BindingMode.OneWay)
        {
            ComponentToViewModelBindings.Add(componentProperty.PropertyName, new(viewModelProperty, componentProperty));
            if (BindingContext is not null)
            {
                componentProperty.Setter(this, viewModelProperty.Getter(BindingContext));
            }
        }
        else if (mode == BindingMode.OneWayFromSource)
        {
            ViewModelToComponentBindings.Add(viewModelProperty.PropertyName, new(componentProperty, viewModelProperty));
        }

        return this;
    }
}
