using System.Text;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Tutu.Events;

namespace Acutipupu;

/// <summary>
/// The <see cref="AcutipupuApp"/> options.
/// </summary>
public class AcutipupuAppOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AcutipupuAppOptions"/> class.
    /// </summary>
    public AcutipupuAppOptions()
    {
        Encoding = Encoding.UTF8;
    }

    /// <summary>
    /// The frame rate.
    /// </summary>
    public int FrameRate { get; set; } = 120;

    /// <summary>
    /// The <see cref="StartupOptions"/>.
    /// </summary>
    public StartupOptions StartupOptions { get; set; } = StartupOptions.WithAltScreen
                                                         | StartupOptions.WithMouseCapture
                                                         | StartupOptions.WithRawMode
                                                         | StartupOptions.WithSignalHandler;

    /// <summary>
    /// The text <see cref="Encoding"/>.
    /// </summary>
    public Encoding Encoding
    {
        get => Console.OutputEncoding;
        set => Console.OutputEncoding = value;
    }

    /// <summary>
    /// The text movement key map.
    /// </summary>
    public EntryKeyMap EntryKeyMap { get; set; } = new();
    
    /// <summary>
    /// The navigation key map.
    /// </summary>
    public NavigationKeyMap NavigationKeyMap { get; set; } = new();
    
    
}
