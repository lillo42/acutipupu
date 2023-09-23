using System.Collections;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Behaviors;

internal class AttachedCollection<T> : ObservableCollection<T>, IAttachedObject
    where T : ObservableObject, IAttachedObject
{
    private readonly WeakList<ObservableObject> _associatedObjects = new();

    public AttachedCollection()
    {
    }

    public AttachedCollection(IEnumerable<T> collection) : base(collection)
    {
    }

    public AttachedCollection(IList<T> list) : base(list)
    {
    }

    public void AttachTo(ObservableObject? bindable)
    {
        ArgumentNullException.ThrowIfNull(bindable);
        OnAttachedTo(bindable);
    }

    public void DetachFrom(ObservableObject? bindable)
    {
        ArgumentNullException.ThrowIfNull(bindable);
        OnDetachingFrom(bindable);
    }

    protected override void ClearItems()
    {
        foreach (var bindable in _associatedObjects)
        {
            foreach (var item in this)
            {
                item.DetachFrom(bindable);
            }
        }

        base.ClearItems();
    }

    protected override void InsertItem(int index, T item)
    {
        base.InsertItem(index, item);
        foreach (var bindable in _associatedObjects)
        {
            item.AttachTo(bindable);
        }
    }

    protected virtual void OnAttachedTo(ObservableObject bindable)
    {
        lock (_associatedObjects)
        {
            _associatedObjects.Add(bindable);
        }

        foreach (var item in this)
        {
            item.AttachTo(bindable);
        }
    }

    protected virtual void OnDetachingFrom(ObservableObject bindable)
    {
        foreach (var item in this)
        {
            item.DetachFrom(bindable);
        }

        lock (_associatedObjects)
        {
            _associatedObjects.Remove(bindable);
        }
    }

    protected override void RemoveItem(int index)
    {
        var item = this[index];
        foreach (var bindable in _associatedObjects)
        {
            item.DetachFrom(bindable);
        }

        base.RemoveItem(index);
    }

    protected override void SetItem(int index, T item)
    {
        var old = this[index];
        foreach (var bindable in _associatedObjects)
        {
            old.DetachFrom(bindable);
        }

        base.SetItem(index, item);

        foreach (var bindable in _associatedObjects)
        {
            item.AttachTo(bindable);
        }
    }
}

/// <summary>
/// A List type for holding WeakReference's
/// It clears the underlying List based on a threshold of operations: Add() or GetEnumerator()
/// </summary>
internal class WeakList<T> : IEnumerable<T>
    where T : class
{
    private readonly List<WeakReference<T>> _list = new();
    private int _operations = 0;

    public int Count => _list.Count;

    public int CleanupThreshold { get; set; } = 32;

    public void Add(T item)
    {
        CleanupIfNeeded();
        _list.Add(new WeakReference<T>(item));
    }

    public void Remove(T item)
    {
        // A reverse for-loop, means we can call RemoveAt(i) inside the loop
        for (var i = _list.Count - 1; i >= 0; i--)
        {
            var w = _list[i];
            if (w.TryGetTarget(out T? target))
            {
                if (target == item)
                {
                    _list.RemoveAt(i);
                    break;
                }
            }
            else
            {
                // Remove if we found one that is not alive
                _list.RemoveAt(i);
            }
        }
    }

    public void Clear()
    {
        _list.Clear();
        _operations = 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        CleanupIfNeeded();

        foreach (var w in _list)
            if (w.TryGetTarget(out T? item))
                yield return item;
    }

    private void CleanupIfNeeded()
    {
        if (++_operations > CleanupThreshold)
        {
            _operations = 0;
            _list.RemoveAll(w => !w.TryGetTarget(out _));
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
