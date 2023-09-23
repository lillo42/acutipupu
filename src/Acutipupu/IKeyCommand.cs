using System.Collections.Immutable;
using Acutipupu.Messages;

namespace Acutipupu;

/// <summary>
/// The key command.
/// </summary>
public interface IKeyCommand
{
    /// <summary>
    /// Check if the key command matches the provided keys.
    /// </summary>
    /// <param name="keys">The <see cref="KeyMessage"/> collections.</param>
    /// <returns>The <see cref="KeyCommandMatchResult"/>.</returns>
    KeyCommandMatchResult Match(ImmutableList<KeyMessage> keys);

    /// <summary>
    /// Execute the key command.
    /// </summary>
    /// <param name="keys">The <see cref="KeyMessage"/> collection.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    ValueTask ExecuteAsync(ImmutableList<KeyMessage> keys, CancellationToken cancellationToken = default);
}

/// <summary>
/// The key match result.
/// </summary>
public enum KeyCommandMatchResult
{
    /// <summary>
    /// No match.
    /// </summary>
    NoMatch,

    /// <summary>
    /// Partially match the provided keys.
    /// </summary>
    PartialMatch,

    /// <summary>
    /// Fully match the provided keys.
    /// </summary>
    Match
}
