using System.Collections.Immutable;
using System.Globalization;
using System.Text;
using Acutipupu.Bindings;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Texts;
using Boto.Widgets;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Acutipupu.Components;

/// <summary>
/// The input component.
/// </summary>
public partial class Entry : Component, INavegable, ITextChanged, IStyled 
{
    private Text _finalText;
    private readonly Paragraph _paragraph = new();
    private readonly StringBuilder _sb = new();
    private readonly List<int[]> _indexes = new();

    /// <inheritdoc />
    public ImmutableList<ImmutableArray<int>> Indexes { get; private set; } = ImmutableList<ImmutableArray<int>>.Empty;

    /// <summary>
    /// The cursor position relative to the text.
    /// </summary>
    [ObservableProperty] private CursorPosition _cursorPosition = new(0, 0);

    /// <summary>
    /// The last column position.
    /// </summary>
    [ObservableProperty] private AtColumnPosition _lastColumnPosition = new(0);

    /// <summary>
    /// The text value.
    /// </summary>
    [ObservableProperty] private string _text = string.Empty;

    /// <summary>
    /// The placeholder.
    /// </summary>
    [ObservableProperty] private string _placeholder = string.Empty;

    /// <summary>
    /// The echo character.
    /// </summary>
    [ObservableProperty] private string _echoCharacter = "*";

    /// <summary>
    /// The end of buffer character.
    /// </summary>
    [ObservableProperty] private string _endOfBufferCharacter = "~";

    /// <summary>
    /// Flag indicating with content should be trimmed.
    /// </summary>
    [ObservableProperty] private bool? _trim;

    /// <summary>
    /// Flag indicating if the entry is multiline.
    /// </summary>
    [ObservableProperty] private bool _multiLine = true;

    /// <summary>
    /// The <see cref="EntryEchoMode"/>.
    /// </summary>
    [ObservableProperty] private EntryEchoMode _echoMode;

    /// <inheritdoc cref="INavegable.IsActive"/>
    [ObservableProperty] private bool _isActive;

    /// <summary>
    /// The <see cref="EntryStyle"/>.
    /// </summary>
    [ObservableProperty] private EntryStyle _style = new() { Placeholder = new() { Foreground = Color.Indexed(240) } };

    /// <summary>
    /// The <see cref="Boto.Layouts.Margin"/> used during the render for calculate the distance between cursor position and the render area.
    /// </summary>
    [ObservableProperty] private Margin _margin = new(6, 6);

    /// <summary>
    /// The screen area.
    /// </summary>
    /// <remarks>
    /// It is used to calculate the area where the cursor is visible.
    /// </remarks>
    [ObservableProperty] private Rect _screenArea = new(0, 0, 0, 0);

    /// <summary>
    /// Initialize a new instance of <see cref="Entry"/>.
    /// </summary>
    public Entry()
    {
        _finalText = new Text(Placeholder, Style.Placeholder.ToBotoStyle());
        Style.PropertyChanged += MarkAsDirty;
    }

    /// <inheritdoc />
    public event EventHandler<TextChangedEventArgs>? TextChanged;

    IStyle IStyled.Style
    {
        get => Style;
        set => Style = (EntryStyle)value;
    }

    /// <inheritdoc />
    public EntryKeyMap? KeyMap { get; set; }

    /// <summary>
    /// The <see cref="Text"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, string> TextProperty { get; } = new BindableProperty<Entry, string>(
        nameof(Text),
        input => input.Text,
        (input, value) => input.Text = value);

    /// <summary>
    /// The <see cref="Placeholder"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, string> PlaceholderProperty { get; } = new BindableProperty<Entry, string>(
        nameof(Placeholder),
        input => input.Placeholder,
        (input, value) => input.Placeholder = value);

    /// <summary>
    /// The <see cref="Style"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, EntryStyle> StyleProperty { get; } = new BindableProperty<Entry, EntryStyle>(
        nameof(Style),
        input => input.Style,
        (input, value) => input.Style = value);

    /// <summary>
    /// The <see cref="Trim"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, bool?> TrimProperty { get; } = new BindableProperty<Entry, bool?>(
        nameof(Trim),
        input => input.Trim,
        (input, value) => input.Trim = value);

    /// <summary>
    /// The <see cref="CursorPosition"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, CursorPosition> CursorPositionProperty { get; } =
        new BindableProperty<Entry, CursorPosition>(
            nameof(CursorPosition),
            input => input.CursorPosition,
            (input, value) => input.CursorPosition = value);

    /// <summary>
    /// The <see cref="EchoCharacter"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, string> EchoCharacterProperty { get; } = new BindableProperty<Entry, string>(
        nameof(EchoCharacter),
        input => input.EchoCharacter,
        (input, value) => input.EchoCharacter = value);

    /// <summary>
    /// The <see cref="EchoMode"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, EntryEchoMode> EchoModeProperty { get; } =
        new BindableProperty<Entry, EntryEchoMode>(
            nameof(EchoMode),
            input => input.EchoMode,
            (input, value) => input.EchoMode = value);

    /// <summary>
    /// The <see cref="Margin"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, Margin> MarginProperty { get; } =
        new BindableProperty<Entry, Margin>(
            nameof(Margin),
            input => input.Margin,
            (input, value) => input.Margin = value);

    /// <summary>
    /// The <see cref="MultiLine"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, bool> MultiLineProperty { get; } =
        new BindableProperty<Entry, bool>(
            nameof(MultiLine),
            input => input.MultiLine,
            (input, value) => input.MultiLine = value);

    /// <summary>
    /// The <see cref="EndOfBufferCharacter"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, string> EndOfTheBufferCharacterProperty { get; } =
        new BindableProperty<Entry, string>(
            nameof(EndOfBufferCharacter),
            input => input.EndOfBufferCharacter,
            (input, value) => input.EndOfBufferCharacter = value);

    /// <summary>
    /// The <see cref="LastColumnPosition"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, AtColumnPosition> LastColumnPositionProperty { get; } =
        new BindableProperty<Entry, AtColumnPosition>(
            nameof(LastColumnPosition),
            input => input.LastColumnPosition,
            (input, value) => input.LastColumnPosition = value);

    /// <summary>
    /// The <see cref="ScreenArea"/> <see cref="IBindableProperty{TSource,TProperty}"/>.
    /// </summary>
    public static IBindableProperty<Entry, Rect> ScreenAreaProperty { get; } =
        new BindableProperty<Entry, Rect>(
            nameof(ScreenArea),
            input => input.ScreenArea,
            (input, value) => input.ScreenArea = value);

    partial void OnStyleChanging(EntryStyle? oldValue, EntryStyle newValue)
    {
        if (oldValue is not null)
        {
            oldValue.PropertyChanged -= MarkAsDirty;
        }

        newValue.PropertyChanged += MarkAsDirty;
    }

    partial void OnTextChanged(string value)
    {
        if (!MultiLine && value.Contains('\n'))
        {
            Text = value
                    .Replace("\r\n", "") // Windows
                    .Replace("\n", "") // Unix
                ;
            return;
        }

        _sb.Clear();
        _sb.Append(value);

        _indexes.Clear();

        var newLineAt = value.IndexOf('\n');
        if (newLineAt == -1)
        {
            _indexes.Add(StringInfo.ParseCombiningCharacters(value));
            Indexes = ImmutableList<ImmutableArray<int>>.Empty
                .Add(StringInfo.ParseCombiningCharacters(value).ToImmutableArray());
        }
        else
        {
            var builder = new List<int>();
            var visiblePosition = StringInfo.ParseCombiningCharacters(value);
            var visiblePositionIndex = 0;
            while (visiblePositionIndex < visiblePosition.Length)
            {
                var position = visiblePosition[visiblePositionIndex];
                if (char.IsControl(value[position]) || value[position] == '\n')
                {
                    visiblePositionIndex++;
                    continue;
                }

                if (position < newLineAt || newLineAt == -1)
                {
                    visiblePositionIndex++;
                    builder.Add(position);
                }
                else
                {
                    newLineAt = value.IndexOf('\n', newLineAt + 1);
                    _indexes.Add(builder.ToArray());
                    builder.Clear();
                }
            }

            _indexes.Add(builder.ToArray());
            builder.Clear();

            if (newLineAt == value.Length - 1)
            {
                _indexes.Add(Array.Empty<int>());
            }

            Indexes = _indexes.Select(x => x.ToImmutableArray())
                .ToImmutableList();
        }

        if (string.IsNullOrEmpty(value))
        {
            _finalText = new Text(value, Style.Placeholder.ToBotoStyle());
        }
        else if (EchoMode == EntryEchoMode.Normal)
        {
            _finalText = new Text(value, Style.Text.ToBotoStyle());
        }
        else if (EchoMode == EntryEchoMode.Password)
        {
            var tmp = Pools.StringBuilderPool.Get();
            tmp.Clear();

            while (tmp.Length < value.Length)
            {
                tmp.Append(EchoCharacter);
            }

            tmp.Length = value.Length;

            _finalText = new Text(tmp.ToString(), Style.Text.ToBotoStyle());

            Pools.StringBuilderPool.Return(tmp);
        }
        else if (EchoMode == EntryEchoMode.Hidden)
        {
            _finalText = new Text(string.Empty, Style.Text.ToBotoStyle());
        }
    }

    partial void OnPlaceholderChanged(string value)
    {
        if (string.IsNullOrEmpty(Text))
        {
            _finalText = new Text(value, Style.Placeholder.ToBotoStyle());
        }
    }

    partial void OnTextChanged(string? oldValue, string newValue)
    {
        var handler = TextChanged;
        handler?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
    }

    partial void OnCursorPositionChanging(CursorPosition value)
    {
        if (value.Row != 0 && value.Row >= _indexes.Count)
        {
            throw new ArgumentException("Row is out of range");
        }

        if (value.Column != 0 && value.Column > _indexes[value.Row].Length)
        {
            throw new ArgumentException("Column is out of range");
        }
    }

    partial void OnScreenAreaChanging(Rect value)
    {
        if (value.Y < 0 || value.Y > Indexes.Count)
        {
            throw new InvalidOperationException("Y is out of range");
        }

        var maxCol = Indexes.MaxBy(x => x.Length);
        if (value.X < 0 || value.X > maxCol.Length)
        {
            throw new InvalidOperationException("X is out of range");
        }
    }


    /// <inheritdoc />
    public override void Dispose()
    {
        Style.PropertyChanged -= MarkAsDirty;
        base.Dispose();
    }
}

/// <summary>
/// The entry mode.
/// </summary>
public enum EntryEchoMode
{
    /// <summary>
    /// Displays text as is.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Displays the <see cref="Entry.EchoCharacter"/> mask instead of actual characters.
    /// </summary>
    Password = 1,

    /// <summary>
    /// Displays nothing as characters are entered.
    /// </summary>
    Hidden = 2,
}
