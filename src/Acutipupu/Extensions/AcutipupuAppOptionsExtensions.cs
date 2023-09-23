namespace Acutipupu.Extensions;

/// <summary>
/// The <see cref="AcutipupuAppOptions"/> builder extensions.
/// </summary>
public static class AcutipupuAppOptionsExtensions
{
    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.FrameRate"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <param name="frameRate">The frame rate.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> as <paramref name="frameRate"/>.</returns>
    public static AcutipupuAppOptions SetFrameRate(this AcutipupuAppOptions configuration, int frameRate)
    {
        configuration.FrameRate = frameRate;
        return configuration;
    }

    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.StartupOptions"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <param name="startupOptions">The <see cref="StartupOptions"/>.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> as <paramref name="startupOptions"/>.</returns>
    public static AcutipupuAppOptions SetStartupOptions(this AcutipupuAppOptions configuration,
        StartupOptions startupOptions)
    {
        configuration.StartupOptions = startupOptions;
        return configuration;
    }

    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.StartupOptions"/> plus <see cref="StartupOptions.WithAltScreen"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> plus <see cref="StartupOptions.WithAltScreen"/>.</returns>
    public static AcutipupuAppOptions WithAltScreen(this AcutipupuAppOptions configuration)
    {
        configuration.StartupOptions |= StartupOptions.WithAltScreen;
        return configuration;
    }

    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.StartupOptions"/> plus <see cref="StartupOptions.WithMouseCapture"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> plus <see cref="StartupOptions.WithMouseCapture"/>.</returns>
    public static AcutipupuAppOptions WithMouseCapture(this AcutipupuAppOptions configuration)
    {
        configuration.StartupOptions |= StartupOptions.WithMouseCapture;
        return configuration;
    }

    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.StartupOptions"/> plus <see cref="StartupOptions.WithRawMode"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> plus <see cref="StartupOptions.WithRawMode"/>.</returns>
    public static AcutipupuAppOptions WithRawMode(this AcutipupuAppOptions configuration)
    {
        configuration.StartupOptions |= StartupOptions.WithRawMode;
        return configuration;
    }

    /// <summary>
    /// Change the <see cref="AcutipupuAppOptions.StartupOptions"/> plus <see cref="StartupOptions.WithSignalHandler"/>.
    /// </summary>
    /// <param name="configuration">The <see cref="AcutipupuAppOptions"/>.</param>
    /// <returns>The <paramref name="configuration"/> with <see cref="AcutipupuAppOptions.FrameRate"/> plus <see cref="StartupOptions.WithSignalHandler"/>.</returns>
    public static AcutipupuAppOptions WithoutSignalHandler(this AcutipupuAppOptions configuration)
    {
        configuration.StartupOptions |= StartupOptions.WithSignalHandler;
        return configuration;
    }
}
