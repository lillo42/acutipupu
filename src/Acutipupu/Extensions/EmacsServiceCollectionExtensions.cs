using Acutipupu.SystemBehaviors;
using Acutipupu.SystemBehaviors.Emacs;
using Microsoft.Extensions.DependencyInjection;

namespace Acutipupu.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> focus on emacs methods.
/// </summary>
public static class EmacsServiceCollectionExtensions
{
    /// <summary>
    /// Add Emacs system behavior.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection UseEmacs(this IServiceCollection services)
    {
        var behavior = services.FirstOrDefault(x => x.ServiceType == typeof(ISystemBehavior));
        if (behavior != null)
        {
            services.Remove(behavior);
        }

        services.AddSingleton<ISystemBehavior, EmacsSystemBehavior>();

        var servicesToBeRemoved = new List<ServiceDescriptor>();

        foreach (var descriptor in services
                     .Where(x => x.ServiceType == typeof(IKeyCommand) && x.ImplementationType != null))
        {
            if (descriptor.ImplementationType!.IsAssignableTo(typeof(INavigationCommand)))
            {
                servicesToBeRemoved.Add(descriptor);
            }

            if (descriptor.ImplementationType!.IsAssignableTo(typeof(ITextCommand)))
            {
                servicesToBeRemoved.Add(descriptor);
            }
        }

        foreach (var service in servicesToBeRemoved)
        {
            services.Remove(service);
        }


        // Add navigation commands
        services
            .AddSingleton<IKeyCommand, EmacsNavigationUpCommand>()
            .AddSingleton<IKeyCommand, EmacsNavigationDownCommand>()
            .AddSingleton<IKeyCommand, EmacsNavigationLeftCommand>()
            .AddSingleton<IKeyCommand, EmacsNavigationRightCommand>()
            .AddSingleton<IKeyCommand, EmacsNavigationNextCommand>()
            .AddSingleton<IKeyCommand, EmacsNavigationPreviousCommand>();

        // Add text commands
        /*
        services
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorUpCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorDownCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorLeftCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorRightCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorBeginningOfLineCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorEndOfLineCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorBeginningOfBufferCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorEndOfBufferCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorLineCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorFirstNonBlankCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorLastNonBlankCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordBackwardStartOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordBackwardStartOfWordIgnoringPunctuationCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordBackwardEndOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordBackwardIgnoringPunctuationEndOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordForwardStartOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordForwardStartOfWordIgnoringPunctuationCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordForwardEndOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorWordForwardIgnoringPunctuationEndOfWordCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorNextParagraphCommand>()
            .AddSingleton<IKeyCommand, EmacsTextMoveCursorPreviousParagraphCommand>();
*/
        return services;
    }
}
