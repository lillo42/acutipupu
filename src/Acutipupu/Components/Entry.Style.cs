using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The input style.
/// </summary>
public partial class EntryStyle : ObservableObject, IStyle
{
    /// <summary>
    /// The base <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _base = new();

    /// <summary>
    /// The placeholder <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _placeholder = new();

    /// <summary>
    /// The text <see cref="Style"/>.
    /// </summary>
    [ObservableProperty] private Style _text = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="EntryStyle"/> class. 
    /// </summary>
    public EntryStyle()
    {
        Base.PropertyChanged += PropagatePropertyChanged;
        Placeholder.PropertyChanged += PropagatePropertyChanged;
        Text.PropertyChanged += PropagatePropertyChanged;
    }

    partial void OnBaseChanging(Style? oldValue, Style newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= PropagatePropertyChanged;
        }

        newValue.PropertyChanged += PropagatePropertyChanged;
    }

    partial void OnPlaceholderChanging(Style? oldValue, Style newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= PropagatePropertyChanged;
        }

        newValue.PropertyChanged += PropagatePropertyChanged;
    }

    partial void OnTextChanging(Style? oldValue, Style newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= PropagatePropertyChanged;
        }

        newValue.PropertyChanged += PropagatePropertyChanged;
    }

    private void PropagatePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender == Base)
        {
            OnPropertyChanged(new PropertyChangedEventArgs($"{nameof(Base)}.{e.PropertyName}"));
        }

        if (sender == Placeholder)
        {
            OnPropertyChanged(new PropertyChangedEventArgs($"{nameof(Placeholder)}.{e.PropertyName}"));
        }

        if (sender == Text)
        {
            OnPropertyChanged(new PropertyChangedEventArgs($"{nameof(Text)}.{e.PropertyName}"));
        }
    }
}
