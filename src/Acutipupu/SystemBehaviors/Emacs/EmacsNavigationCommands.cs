using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Messages;
using Microsoft.Extensions.Options;

namespace Acutipupu.SystemBehaviors.Emacs;

internal abstract record EmacsNavigationCommand(INavigation Navigation,
    IOptionsMonitor<AcutipupuAppOptions> Options) : INavigationCommand 
{
    /// <summary>
    /// The key binding.
    /// </summary>
    protected abstract KeyBindingCollection Key { get; }

    /// <summary>
    /// The command to execute.
    /// </summary>
    protected abstract void Execute();

    /// <inheritdoc />
    public KeyCommandMatchResult Match(ImmutableList<KeyMessage> keys)
        => Key.IsMatch(keys);

    /// <inheritdoc />
    public ValueTask ExecuteAsync(ImmutableList<KeyMessage> keys, CancellationToken cancellationToken = default)
    {
        Execute();
        return ValueTask.CompletedTask;
    }
}

internal sealed record EmacsNavigationUpCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Up;
    protected override void Execute() => Navigation.MoveUp(1);
}

internal sealed record EmacsNavigationDownCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Down;
    protected override void Execute() => Navigation.MoveDown(1);
}

internal sealed record EmacsNavigationLeftCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Left;
    protected override void Execute() => Navigation.MoveLeft(1);
}

internal sealed record EmacsNavigationRightCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Right;
    protected override void Execute() => Navigation.MoveRight(1);
}

internal sealed record EmacsNavigationNextCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Next;
    protected override void Execute() => Navigation.MoveToNext(1);
}

internal sealed record EmacsNavigationPreviousCommand(INavigation Navigation, IOptionsMonitor<AcutipupuAppOptions> Options)
    : EmacsNavigationCommand(Navigation, Options)
{
    protected override KeyBindingCollection Key => Options.CurrentValue.NavigationKeyMap.Previous;
    protected override void Execute() => Navigation.MoveToPrevious(1);
}
