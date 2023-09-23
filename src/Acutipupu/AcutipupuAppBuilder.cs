using System.Text;
using Acutipupu.Extensions;
using Acutipupu.SystemBehaviors;
using Acutipupu.SystemBehaviors.Emacs;
using Boto.Terminals;
using Boto.Tutu;
using Clipboard;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NodaTime;
using Tutu;
using Tutu.Events;
using Tutu.Terminal;
using ITerminal = Boto.Terminals.ITerminal;

namespace Acutipupu;

/// <summary>
/// The <see cref="AcutipupuApp"/> builder.
/// </summary>
public class AcutipupuAppBuilder
{
    private readonly ServiceCollection _services = new();
    private readonly Lazy<ConfigurationManager> _configuration;
    private Func<IServiceProvider>? _createServiceProvider;
    private ILoggingBuilder? _logging;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcutipupuAppBuilder"/> class.
    /// </summary>
    public AcutipupuAppBuilder()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Lazy-load the ConfigurationManager, so it isn't created if it is never used.
        // Don't capture the 'this' variable in AddSingleton, so AcutipupuAppBuilder can be GC'd.
        var configuration = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
        Services.AddSingleton<IConfiguration>(_ => configuration.Value);
        _configuration = configuration;

        Services.AddSingleton<IClock>(SystemClock.Instance)
            .AddSingleton(SystemEventReader.Instance)
            .AddSingleton(SystemTerminal.Instance)
            .AddSingleton(SystemClipboard.Instance)
            .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
            // .AddSingleton<INavigation, NavigationControl>()
            .AddSingleton<ITerminal>(new Terminal(new TutuBackend()))
            .AddSingleton<ISystemBehavior, EmacsSystemBehavior>()
            .AddSingleton<IExecutableCommand>(_ => new ExecutableCommand(Console.Out))
            .UseEmacs();
    }

    /// <summary>
    /// The <see cref="IServiceCollection"/>.
    /// </summary>
    public IServiceCollection Services => _services;

    /// <summary>
    /// A collection of configuration providers for the application to compose. This is useful for adding new configuration sources and providers.
    /// </summary>
    public ConfigurationManager ConfigurationManager => _configuration.Value;

    /// <summary>
    /// A collection of logging providers for the application to compose. This is useful for adding new logging providers.
    /// </summary>
    public ILoggingBuilder Logging
    {
        get
        {
            return _logging ??= InitializeLogging();

            ILoggingBuilder InitializeLogging()
            {
                // if someone accesses the Logging builder, ensure Logging has been initialized.
                Services.AddLogging();
                return new LoggingBuilder(Services);
            }
        }
    }

    /// <summary>
    /// Registers a <see cref="IServiceProviderFactory{TBuilder}" /> instance to be used to create the <see cref="IServiceProvider" />.
    /// </summary>
    /// <param name="factory">The <see cref="IServiceProviderFactory{TBuilder}" />.</param>
    /// <param name="configure">
    /// A delegate used to configure the <typeparamref T="TBuilder" />. This can be used to configure services using
    /// APIS specific to the <see cref="IServiceProviderFactory{TBuilder}" /> implementation.
    /// </param>
    /// <typeparam name="TBuilder">The type of builder provided by the <see cref="IServiceProviderFactory{TBuilder}" />.</typeparam>
    /// <remarks>
    /// <para>
    /// <see cref="ConfigureContainer{TBuilder}(IServiceProviderFactory{TBuilder}, Action{TBuilder})"/> is called by <see cref="Build"/>
    /// and so the delegate provided by <paramref name="configure"/> will run after all other services have been registered.
    /// </para>
    /// <para>
    /// Multiple calls to <see cref="ConfigureContainer{TBuilder}(IServiceProviderFactory{TBuilder}, Action{TBuilder})"/> will replace
    /// the previously stored <paramref name="factory"/> and <paramref name="configure"/> delegate.
    /// </para>
    /// </remarks>
    public void ConfigureContainer<TBuilder>(IServiceProviderFactory<TBuilder> factory,
        Action<TBuilder>? configure = null) where TBuilder : notnull
    {
        if (factory == null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        _createServiceProvider = () =>
        {
            var container = factory.CreateBuilder(Services);
            configure?.Invoke(container);
            return factory.CreateServiceProvider(container);
        };
    }

    /// <summary>
    /// The <see cref="AcutipupuApp"/>.
    /// </summary>
    /// <returns></returns>
    public AcutipupuApp Build()
    {
        ConfigureDefaultLogging();

        _ = _createServiceProvider?.Invoke() ?? Services.BuildServiceProvider();

        // Mark the service collection as read-only to prevent future modifications
        _services.MakeReadOnly();

        var provider = Services.BuildServiceProvider();
        return provider.GetRequiredService<AcutipupuApp>();
    }


    private void ConfigureDefaultLogging()
    {
        // By default, if no one else has configured logging, add a "no-op" LoggerFactory
        // and Logger services with no providers. This way when components try to get an
        // ILogger<> from the IServiceProvider, they don't get 'null'.
        Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, NullLoggerFactory>());
        Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(NullLogger<>)));
    }

    private sealed class LoggingBuilder : ILoggingBuilder
    {
        public LoggingBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
