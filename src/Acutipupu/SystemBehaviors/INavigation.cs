using Acutipupu.Components;

namespace Acutipupu.SystemBehaviors;

/// <summary>
/// The navigation action.
/// </summary>
public interface INavigation
{
    /// <summary>
    /// The main page.
    /// </summary>
    Page? MainPage { get; set; }

    /// <summary>
    /// The current <see cref="Component"/>.
    /// </summary>
    Component? CurrentComponent { get; set; }

    /// <summary>
    /// Move to next <see cref="Component"/>.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveToNext(int count);

    /// <summary>
    /// Move to previous <see cref="Component"/>.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveToPrevious(int count);

    /// <summary>
    /// Move to <see cref="Component"/> up.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveUp(int count);

    /// <summary>
    /// Move to <see cref="Component"/> down.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveDown(int count);

    /// <summary>
    /// Move to <see cref="Component"/> left.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveLeft(int count);

    /// <summary>
    /// Move to <see cref="Component"/> right.
    /// </summary>
    /// <param name="count">The number of next component.</param>
    void MoveRight(int count);
}
