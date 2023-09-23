namespace Acutipupu;

/// <summary>
/// The <see cref="AcutipupuApp"/> startup options.
/// </summary>
[Flags]
public enum StartupOptions : byte
{
    /// <summary>
    /// No options.
    /// </summary>
    None = 0,

    /// <summary>
    /// Enable alt screen.
    /// </summary>
    WithAltScreen = 1,

    /// <summary>
    /// Enable mouse cell motion. 
    /// </summary>
    WithMouseCapture = 2,

    /// <summary>
    /// Enable terminal raw mode.
    /// </summary>
    /// <remarks>
    /// When raw mode is enabled, the terminal will not perform any processing of the input.
    /// </remarks>
    WithRawMode = 4,

    /// <summary>
    /// Enable signal handler.
    /// </summary>
    /// <remarks>
    /// The signal handler will handle the following signals in unix systems:
    /// <list type="bullet">
    ///     <item><description>SIGINT</description></item>
    ///     <item><description>SIGTERM</description></item>
    /// </list>
    /// </remarks>
    WithSignalHandler = 8,
}
