using System.Collections;
using Acutipupu.Messages;

namespace Acutipupu.Bindings;

/// <summary>
/// The key binding collection.
/// </summary>
public class KeyBindingCollection : IList<IKeyBinding>
{
    /// <summary>
    /// The key bindings.
    /// </summary>
    public List<IKeyBinding> Keys { get; set; } = new();

    /// <summary>
    /// Check if the current list match with the <paramref name="messages"/>.
    /// </summary>
    /// <param name="messages">The <see cref="KeyMessage"/>.</param>
    /// <returns>true if match; otherwise false.</returns>
    public KeyCommandMatchResult IsMatch(IReadOnlyCollection<KeyMessage> messages)
    {
        var match = KeyCommandMatchResult.NoMatch;

        foreach (var result in Keys.Select(key => key.Match(messages)))
        {
            if (result == KeyCommandMatchResult.Match)
            {
                return result;
            }

            if (result == KeyCommandMatchResult.PartialMatch)
            {
                match = result;
            }
        }

        return match;
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<IKeyBinding> GetEnumerator() => Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc cref="ICollection{T}.Add"/>
    public void Add(IKeyBinding item) => Keys.Add(item);

    /// <inheritdoc cref="ICollection{T}.Clear"/>
    public void Clear() => Keys.Clear();

    /// <inheritdoc cref="ICollection{T}.Contains"/>
    public bool Contains(IKeyBinding item) => Keys.Contains(item);

    /// <inheritdoc cref="ICollection{T}.CopyTo"/>
    public void CopyTo(IKeyBinding[] array, int arrayIndex) => Keys.CopyTo(array, arrayIndex);

    /// <inheritdoc cref="ICollection{T}.Remove"/>
    public bool Remove(IKeyBinding item) => Keys.Remove(item);

    /// <inheritdoc cref="ICollection{T}.Remove"/>
    public int Count => Keys.Count;

    /// <inheritdoc cref="ICollection{T}.Remove"/>
    public bool IsReadOnly => false;

    /// <inheritdoc cref="IList{T}.IndexOf"/>
    public int IndexOf(IKeyBinding item) => Keys.IndexOf(item);

    /// <inheritdoc cref="IList{T}.IndexOf"/>
    public void Insert(int index, IKeyBinding item) => Keys.Insert(index, item);

    /// <inheritdoc cref="IList{T}.IndexOf"/>
    public void RemoveAt(int index) => Keys.RemoveAt(index);

    /// <inheritdoc cref="IList{T}.this"/>
    public IKeyBinding this[int index]
    {
        get => Keys[index];
        set => Keys[index] = value;
    }
}
