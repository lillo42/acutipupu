using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Acutipupu.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    public static IServiceCollection UseAcutipupu<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TPage>(
        this IServiceCollection services,
        Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page
    {
        services.AddTransient<TPage>();
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    public static IServiceCollection UseAcutipupu<TPage>(this IServiceCollection services, TPage page,
        Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page
    {
        services.AddSingleton(page);
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection UseAcutipupu<
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TPage,
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
            TModel>
        (this IServiceCollection services, Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddTransient<TPage>();
        services.AddTransient<TModel>();
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection UseAcutipupu<TPage,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TModel>(this IServiceCollection services,
        TPage page,
        Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddSingleton(page);
        services.AddTransient<TModel>();
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="model">The <typeparamref name="TModel"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection UseAcutipupu<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TPage,
        TModel>(this IServiceCollection services,
        TModel model,
        Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddTransient<TPage>();
        services.AddSingleton(model);
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="AcutipupuApp"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <param name="model">The <typeparamref name="TModel"/>.</param>
    /// <param name="configure">The configuration of <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection UseAcutipupu<TPage, TModel>(this IServiceCollection services, TPage page,
        TModel model,
        Action<AcutipupuAppOptions>? configure = null)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddSingleton(page);
        services.AddSingleton(model);
        services.AddTransient<AcutipupuApp<TPage>>();
        services.AddTransient<AcutipupuApp>(provider => provider.GetRequiredService<AcutipupuApp<TPage>>());
        services.Configure<AcutipupuAppOptions>(opt => configure?.Invoke(opt));
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page"/>.</typeparam>
    public static IServiceCollection AddPage<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TPage>(
        this IServiceCollection services)
        where TPage : Page
    {
        services.AddTransient<TPage>();
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <returns>The <paramref name="services"/></returns>
    /// <typeparam name="TPage">The <see cref="Page"/>.</typeparam>
    public static IServiceCollection AddPage<TPage>(this IServiceCollection services, TPage page)
        where TPage : Page
    {
        services.AddSingleton(page);
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <paramref name="services"/>.</returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection AddPage<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TPage,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TModel>(this IServiceCollection services)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddTransient<TPage>();
        services.AddTransient<TModel>();
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <returns>The <paramref name="services"/>.</returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection AddPage<TPage,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TModel>(this IServiceCollection services, TPage page)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddSingleton(page);
        services.AddTransient<TModel>();
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="model">The <typeparamref name="TModel"/>.</param>
    /// <returns>The <paramref name="services"/>.</returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection AddPage<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        TPage, TModel>(this IServiceCollection services, TModel model)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddTransient<TPage>();
        services.AddSingleton(model);
        return services;
    }

    /// <summary>
    /// Add a <see cref="Page"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="page">The <typeparamref name="TPage"/>.</param>
    /// <param name="model">The <typeparamref name="TModel"/>.</param>
    /// <returns>The <paramref name="services"/>.</returns>
    /// <typeparam name="TPage">The <see cref="Page{T}"/>.</typeparam>
    /// <typeparam name="TModel">The view model.</typeparam>
    public static IServiceCollection AddPage<TPage, TModel>(this IServiceCollection services, TPage page, TModel model)
        where TPage : Page<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        services.AddSingleton(page);
        services.AddSingleton(model);
        return services;
    }

    public static IServiceCollection AddKeyCommand<TCommand>(this IServiceCollection services)
        where TCommand : class, IKeyCommand
    {
        services.AddTransient<IKeyCommand, TCommand>();
        return services;
    }

    public static IServiceCollection AddKeyCommand<TCommand>(this IServiceCollection services, TCommand command)
        where TCommand : class, IKeyCommand
    {
        services.AddSingleton(command);
        return services;
    }

    public static IServiceCollection AddKeyCommand<TCommand>(this IServiceCollection services,
        Func<IServiceProvider, TCommand> factory,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommand : class, IKeyCommand
    {
        services.Add(new ServiceDescriptor(typeof(IKeyCommand), factory, lifetime));
        return services;
    }
}
