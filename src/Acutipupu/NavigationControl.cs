/*
using System.Collections.Specialized;
using System.ComponentModel;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Component = Acutipupu.Components.Component;
using Container = Acutipupu.Components.Container;

namespace Acutipupu;

/// <summary>
/// Controls the navigation.
/// </summary>
public class NavigationControl : INavigation 
{
    private NavigationNode? _previousContainerNode;

    /// <summary>
    /// Marks the graph as dirty. 
    /// </summary>
    protected bool IsDirty { get; set; } = true;

    /// <summary>
    /// The current <see cref="NavigationNode"/>.
    /// </summary>
    protected NavigationNode? CurrentNode { get; set; }

    private Page? _page;

    /// <summary>
    /// The main <see cref="Page"/>.
    /// </summary>
    public Page? MainPage
    {
        get => _page;
        set
        {
            if (_page is not null)
            {
                _page.PropertyChanged -= OnPageChanged;
                RemoveSubscription(_page);
            }

            _page = value;

            if (_page is not null)
            {
                _page.PropertyChanged += OnPageChanged;
                AddSubscription(_page);
            }
        }
    }

    /// <summary>
    /// The current <see cref="Components.Component"/>.
    /// </summary>
    public Component? CurrentComponent { get; set; }


    /// <inheritdoc />
    public virtual void MoveToNext()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            if (node.Component is Container)
            {
                node = node.Children[0];
                if (node.Component is INavegable)
                {
                    break;
                }

                continue;
            }

            var local = node.Next;
            if (local is not null)
            {
                node = local;
                if (node.Component is INavegable)
                {
                    break;
                }

                continue;
            }

            local = node.Parent;
            if (local is not null)
            {
                int index;
                while (true)
                {
                    var tmp = local;
                    local = local.Parent;
                    if (local is null)
                    {
                        return;
                    }

                    index = local.Children.IndexOf(tmp);
                    if (index < local.Children.Count - 1)
                    {
                        break;
                    }
                }

                node = local.Children[index + 1];
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode.Parent;
        }
    }

    /// <inheritdoc />
    public virtual void MoveToPrevious()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            if (node.Component is Container)
            {
                node = node.Children[^1];
                if (node.Component is INavegable)
                {
                    break;
                }

                continue;
            }

            var local = node.Previous;
            if (local is not null)
            {
                node = local;
                if (node.Component is INavegable)
                {
                    break;
                }

                continue;
            }

            local = node.Parent;
            if (local is not null)
            {
                int index;
                while (true)
                {
                    var tmp = local;
                    local = local.Parent;
                    if (local is null)
                    {
                        return;
                    }

                    index = local.Children.IndexOf(tmp);
                    if (index > 0)
                    {
                        break;
                    }
                }

                node = local.Children[index - 1];
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode?.Parent;
        }
    }

    /// <inheritdoc />
    public virtual void MoveUp()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            node = node.Up ?? node.Parent;
            if (node?.Component is INavegable)
            {
                CurrentComponent = node.Component;
                Active(CurrentComponent);
                CurrentNode = node;
                _previousContainerNode = node.Parent;
                break;
            }

            if (node is { Component: Container, Children.Count: > 0 } && node != _previousContainerNode)
            {
                node = node.Children[0];
                if (node.Component is INavegable)
                {
                    break;
                }
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode?.Parent;
        }
    }

    /// <inheritdoc />
    public virtual void MoveDown()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            node = node.Down ?? node.Parent;
            if (node?.Component is INavegable navegable)
            {
                CurrentComponent = node.Component;
                CurrentNode = node;
                navegable.IsActive = true;
                _previousContainerNode = node.Parent;
                break;
            }

            if (node is { Component: Container, Children.Count: > 0 } && node != _previousContainerNode)
            {
                node = node.Children[0];
                if (node.Component is INavegable)
                {
                    break;
                }
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode?.Parent;
        }
    }

    /// <inheritdoc />
    public virtual void MoveLeft()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            node = node.Left ?? node.Parent;
            if (node?.Component is INavegable)
            {
                CurrentComponent = node.Component;
                Active(CurrentComponent);
                CurrentNode = node;
                _previousContainerNode = node.Parent;
                break;
            }

            if (node is { Component: Container, Children.Count: > 0 } && node != _previousContainerNode)
            {
                node = node.Children[0];
                if (node.Component is INavegable)
                {
                    break;
                }
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode?.Parent;
        }
    }

    /// <inheritdoc />
    public virtual void MoveRight()
    {
        if (IsDirty)
        {
            RecreateGraph();
        }

        var node = CurrentNode;
        while (node is not null)
        {
            node = node.Right ?? node.Parent;
            if (node?.Component is INavegable)
            {
                CurrentComponent = node.Component;
                Active(CurrentComponent);
                CurrentNode = node;
                _previousContainerNode = node.Parent;
                break;
            }

            if (node is { Component: Container, Children.Count: > 0 } && node != _previousContainerNode)
            {
                node = node.Children[0];
                if (node.Component is INavegable)
                {
                    break;
                }
            }
        }

        if (node is null)
        {
            return;
        }

        Deactivate(CurrentNode?.Component);

        CurrentNode = node;
        CurrentComponent = node.Component;
        if (CurrentComponent is not null)
        {
            Active(CurrentComponent);
            _previousContainerNode = CurrentNode?.Parent;
        }
    }

    private void OnPageChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(Container.SplitDirection))
        {
            IsDirty = true;
        }
    }

    private void OnPageComponentsChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        IsDirty = true;

        if (args.OldItems is not null)
        {
            for (var index = args.OldStartingIndex; index < args.OldItems.Count; index++)
            {
                var item = args.OldItems[index];
                if (item is Component component)
                {
                    // component.IsActive = false;
                    RemoveSubscription(component);
                }
            }
        }

        if (args.NewItems is not null)
        {
            for (var index = args.NewStartingIndex; index < args.NewItems.Count; index++)
            {
                var item = args.NewItems[index];
                if (item is Component component)
                {
                    // component.IsActive = false;
                    AddSubscription(component);
                }
            }
        }
    }

    /// <summary>
    /// Recreates the graph.
    /// </summary>
    protected virtual void RecreateGraph()
    {
        IsDirty = false;
        if (MainPage is null)
        {
            return;
        }

        CurrentNode = CreateGraph(MainPage);
    }

    private static NavigationNode? CreateGraph(Component component)
    {
        if (component is not Container container)
        {
            return component is INavegable ? new NavigationNode(component, Direction.Horizontal) : null;
        }

        var root = new NavigationNode(container, container.SplitDirection);
        NavigationNode? current = null;

        foreach (var child in container.Components)
        {
            var node = CreateGraph(child);

            if (node is null)
            {
                continue;
            }

            if (current is null)
            {
                current = node;
            }
            else
            {
                current.Next = node;
                node.Previous = current;
                current = node;
            }

            current.Parent = root;
            root.Children.Add(current);
        }

        return root.Children.Count == 0 ? null : root;
    }

    /// <summary>
    /// Remove the subscription changed events in recursively mode.
    /// </summary>
    /// <param name="component">The <see cref="Components.Component"/>.</param>
    protected virtual void RemoveSubscription(Component component)
    {
        if (component is not Container container)
        {
            return;
        }

        container.ComponentsChanged -= OnPageComponentsChanged;
        foreach (var child in container.Components)
        {
            if (child is Container c)
            {
                RemoveSubscription(c);
            }

            if (child is Page page)
            {
                page.PropertyChanged -= OnPageChanged;
            }
        }
    }

    /// <summary>
    /// Add the subscription changed events in recursively mode.
    /// </summary>
    /// <param name="component">The <see cref="Components.Component"/>.</param>
    protected virtual void AddSubscription(Component component)
    {
        if (component is not Container container)
        {
            return;
        }

        container.ComponentsChanged += OnPageComponentsChanged;
        foreach (var child in container.Components)
        {
            if (child is Container c)
            {
                AddSubscription(c);
            }

            if (child is Page page)
            {
                page.PropertyChanged += OnPageChanged;
            }
        }
    }

    private static void Active(Component? component) => SetIsActive(component, true);

    private static void Deactivate(Component? component) => SetIsActive(component, false);

    private static void SetIsActive(Component? component, bool value)
    {
        if (component is INavegable nav)
        {
            nav.IsActive = value;
        }
    }
}
*/
