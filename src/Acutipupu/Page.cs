using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Container = Acutipupu.Components.Container;

namespace Acutipupu;

/// <summary>
/// The page.
/// </summary>
public abstract partial class Page : Container, INavegable
{
    /// <summary>
    /// Flag indicating if the page is active.
    /// </summary>
    [ObservableProperty] private bool _isActive;

    /// <summary>
    /// Initializes a new instance of the <see cref="Page"/> class.
    /// </summary>
    /// <param name="pageViewModel">The view model.</param>
    /// <param name="messenger">The <see cref="IMessenger"/>.</param>
    protected Page(INotifyPropertyChanged? pageViewModel, IMessenger messenger)
    {
        Messenger = messenger;
        PageViewModel = pageViewModel;
        BindingContext = pageViewModel;
        ComponentsChanged += OnComponentChanged;
    }

    private void OnComponentChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (args.NewItems is not null)
        {
            for (var index = args.NewStartingIndex; index < args.NewItems.Count; index++)
            {
                if (args.NewItems[index] is Page page)
                {
                    page.Parent = this;
                }
            }
        }

        if (args.OldItems is not null)
        {
            for (var index = args.OldStartingIndex; index < args.OldItems.Count; index++)
            {
                if (args.OldItems[index] is Page page && page.Parent == this)
                {
                    page.Parent = null;
                }
            }
        }
    }

    /// <summary>
    /// The page view model. 
    /// </summary>
    protected INotifyPropertyChanged? PageViewModel { get; }

    /// <summary>
    /// The parent <see cref="Page"/>.
    /// </summary>
    protected Page? Parent { get; set; }

    /// <summary>
    /// Gets the <see cref="IMessenger"/> instance in use.
    /// </summary>
    protected IMessenger Messenger { get; }

    /// <summary>
    /// Invoked whenever the <see cref="IsActive"/> property is set to <see langword="true"/>.
    /// Use this method to register to messages and do other initialization for this instance.
    /// </summary>
    /// <remarks>
    /// The base implementation registers all messages for this recipients that have been declared
    /// explicitly through the <see cref="IRecipient{TMessage}"/> interface, using the default channel.
    /// For more details on how this works, see the <see cref="IMessengerExtensions.RegisterAll"/> method.
    /// If you need more fine tuned control, want to register messages individually or just prefer
    /// the lambda-style syntax for message registration, override this method and register manually.
    /// </remarks>
    [RequiresUnreferencedCode(
        "This method requires the generated CommunityToolkit.Mvvm.Messaging.__Internals.__IMessengerExtensions type not to be removed to use the fast path. " +
        "If this type is removed by the linker, or if the target recipient was created dynamically and was missed by the source generator, a slower fallback " +
        "path using a compiled LINQ expression will be used. This will have more overhead in the first invocation of this method for any given recipient type. " +
        "Alternatively, OnActivated() can be manually overwritten, and registration can be done individually for each required message for this recipient.")]
    protected virtual void OnActivated()
    {
        Messenger.RegisterAll(this);
    }

    /// <summary>
    /// Invoked whenever the <see cref="IsActive"/> property is set to <see langword="false"/>.
    /// Use this method to unregister from messages and do general cleanup for this instance.
    /// </summary>
    /// <remarks>
    /// The base implementation unregisters all messages for this recipient. It does so by
    /// invoking <see cref="IMessenger.UnregisterAll"/>, which removes all registered
    /// handlers for a given subscriber, regardless of what token was used to register them.
    /// That is, all registered handlers across all subscription channels will be removed.
    /// </remarks>
    protected virtual void OnDeactivated()
    {
        Messenger.UnregisterAll(this);
    }
    
    partial void OnIsActiveChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            OnActivated();
        }
        else
        {
            OnDeactivated();
        }
    }

    /// <inheritdoc />
    public override void Dispose()
    {
        base.Dispose();
        ComponentsChanged -= OnComponentChanged;
    }

    /// <summary>
    /// Create the <see cref="IConstraint"/> as length with the given <paramref name="length"/>.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <returns>The length <see cref="IConstraint"/>.</returns>
    protected static IConstraint Length(int length) => Boto.Layouts.Constraints.Length(length);

    /// <summary>
    /// Create the <see cref="IConstraint"/> as percentage with the given <paramref name="percentage"/>.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <returns>The percentage <see cref="IConstraint"/>.</returns>
    protected static IConstraint Percentage(int percentage) => Boto.Layouts.Constraints.Percentage(percentage);

    /// <summary>
    /// Create the <see cref="IConstraint"/> as ratio with the given <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="density">The density.</param>
    /// <returns>The ratio <see cref="IConstraint"/>.</returns>
    protected static IConstraint Ratio(int value, int density) => Boto.Layouts.Constraints.Ratio(value, density);

    /// <summary>
    /// Create the <see cref="IConstraint"/> as min with the given <paramref name="min"/>.
    /// </summary>
    /// <param name="min">The min value.</param>
    /// <returns>The min <see cref="IConstraint"/>.</returns>
    protected static IConstraint Min(int min) => Boto.Layouts.Constraints.Min(min);

    /// <summary>
    /// Create the <see cref="IConstraint"/> as max with the given <paramref name="max"/>.
    /// </summary>
    /// <param name="max">The max value.</param>
    /// <returns>The min <see cref="IConstraint"/>.</returns>
    protected static IConstraint Max(int max) => Boto.Layouts.Constraints.Max(max);
}

/// <summary>
/// The page.
/// </summary>
/// <typeparam name="T">The page view model.</typeparam>
public abstract class Page<T> : Page
    where T : class, INotifyPropertyChanged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Page{T}"/> class.
    /// </summary>
    /// <param name="pageViewModel"> <typeparamref name="T"/>.</param>
    /// <param name="messenger">The <see cref="IMessenger"/>.</param>
    protected Page(T pageViewModel, IMessenger messenger)
        : base(pageViewModel, messenger)
    {
        PageViewModel = pageViewModel;
        BindingContext = pageViewModel;
    }

    /// <summary>
    /// The page view model. 
    /// </summary>
    public new T PageViewModel { get; }
}
