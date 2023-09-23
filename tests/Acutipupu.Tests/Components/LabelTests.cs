using System.ComponentModel;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using AutoFixture;
using Boto.Buffers;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using FluentAssertions;
using NSubstitute;
using Buffer = Boto.Buffers.Buffer;
using Style = Acutipupu.Components.Style;

namespace Acutipupu.Tests.Components;

public class LabelTests
{
    private readonly Fixture _fixture = new();
    private readonly Label _component = new();

    #region Set Style

    [Fact]
    public void SetStyle()
    {
        var style = new Style();
        _component.SetStyle(style);
        _component.Style.Should().Be(style);
    }

    [Fact]
    public void SetStyleWithBindableProperty()
    {
        var value = new Style { Background = Color.Cyan };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle(ViewModel.StyleProperty)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new Style { Foreground = Color.Black };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithExpression()
    {
        var value = new Style { Background = Color.Cyan };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle<ViewModel>(vm => vm.Style)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new Style { Foreground = Color.Black };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithDelegate()
    {
        var value = new Style { Background = Color.Cyan };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle<ViewModel>(
                nameof(ViewModel.Style),
                vm => vm.Style,
                (vm, val) => vm.Style = val)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new Style { Foreground = Color.Black };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithObjectDelegate()
    {
        var value = new Style { Background = Color.Cyan };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle(
                nameof(ViewModel.Style),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.Style;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.Style = val;
                })
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new Style { Foreground = Color.Black };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    #endregion

    #region Set Text

    [Fact]
    public void SetText()
    {
        var value = _fixture.Create<string>();
        _component.SetText(value)
            .Text.Should().Be(value);
    }

    [Fact]
    public void SetTextWithBindableProperty()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText(ViewModel.TextProperty)
            .SetText(value);

        viewModel.Text.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Text = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Text));
        _component.Text.Should().Be(value);
    }

    [Fact]
    public void SetTextWithExpression()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText<ViewModel>(vm => vm.Text)
            .SetText(value);

        viewModel.Text.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Text = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Text));
        _component.Text.Should().Be(value);
    }

    [Fact]
    public void SetTextWithDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText<ViewModel>(
                nameof(ViewModel.Text),
                vm => vm.Text,
                (vm, val) => vm.Text = val)
            .SetText(value);

        viewModel.Text.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Text = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Text));
        _component.Text.Should().Be(value);
    }

    [Fact]
    public void SetTextWithObjectDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText(
                nameof(ViewModel.Text),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.Text;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.Text = val;
                })
            .SetText(value);

        viewModel.Text.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Text = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Text));
        _component.Text.Should().Be(value);
    }

    #endregion

    #region Set Trim

    [Fact]
    public void SetTrim()
    {
        var value = _fixture.Create<bool?>();
        _component.SetTrim(value)
            .Trim.Should().Be(value);
    }

    [Fact]
    public void EnableTrim()
    {
        _component.EnableTrim()
            .Trim.Should().BeTrue();
    }

    [Fact]
    public void DisableTrim()
    {
        _component.DisableTrim()
            .Trim.Should().BeFalse();
    }

    [Fact]
    public void SetTrimWithBindableProperty()
    {
        var value = _fixture.Create<bool?>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetTrim(ViewModel.TrimProperty)
            .SetTrim(value);

        viewModel.Trim.Should().Be(value);

        value = _fixture.Create<bool?>();
        viewModel.Trim = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Trim));
        _component.Trim.Should().Be(value);
    }

    [Fact]
    public void SetTrimWithExpression()
    {
        var value = _fixture.Create<bool?>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetTrim<ViewModel>(vm => vm.Trim)
            .SetTrim(value);

        viewModel.Trim.Should().Be(value);

        value = _fixture.Create<bool?>();
        viewModel.Trim = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Trim));
        _component.Trim.Should().Be(value);
    }

    [Fact]
    public void SetTrimWithDelegate()
    {
        var value = _fixture.Create<bool?>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetTrim<ViewModel>(
                nameof(ViewModel.Trim),
                vm => vm.Trim,
                (vm, val) => vm.Trim = val)
            .SetTrim(value);

        viewModel.Trim.Should().Be(value);

        value = _fixture.Create<bool?>();
        viewModel.Trim = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Trim));
        _component.Trim.Should().Be(value);
    }

    [Fact]
    public void SetTrimWithObjectDelegate()
    {
        var value = _fixture.Create<bool?>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetTrim(
                nameof(ViewModel.Trim),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.Trim;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.Trim = val;
                })
            .SetTrim(value);

        viewModel.Trim.Should().Be(value);

        value = _fixture.Create<bool?>();
        viewModel.Trim = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Trim));
        _component.Trim.Should().Be(value);
    }

    #endregion

    #region Set Formatted

    [Fact]
    public void SetFormattedText()
    {
        Text value = _fixture.Create<string>();
        _component.SetFormattedText(value)
            .FormattedText.Should().Be(value);
    }

    [Fact]
    public void SetFormattedTextWithBindableProperty()
    {
        Text value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetFormattedText(ViewModel.FormattedTextProperty)
            .SetFormattedText(value);

        viewModel.FormattedText.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.FormattedText = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.FormattedText));
        _component.FormattedText.Should().Be(value);
    }

    [Fact]
    public void SetFormattedTextWithExpression()
    {
        Text value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetFormattedText<ViewModel>(vm => vm.FormattedText)
            .SetFormattedText(value);

        viewModel.FormattedText.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.FormattedText = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.FormattedText));
        _component.FormattedText.Should().Be(value);
    }

    [Fact]
    public void SetFormattedTexWithDelegate()
    {
        Text value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetFormattedText<ViewModel>(
                nameof(ViewModel.FormattedText),
                vm => vm.FormattedText,
                (vm, val) => vm.FormattedText = val)
            .SetFormattedText(value);

        viewModel.FormattedText.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.FormattedText = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.FormattedText));
        _component.FormattedText.Should().Be(value);
    }

    [Fact]
    public void SetFormattedWithObjectDelegate()
    {
        Text value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetFormattedText(
                nameof(ViewModel.FormattedText),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.FormattedText;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.FormattedText = val;
                })
            .SetFormattedText(value);

        viewModel.FormattedText.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.FormattedText = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.FormattedText));
        _component.FormattedText.Should().Be(value);
    }

    #endregion

    #region Render

    [Fact]
    public void Render_When_UseText()
    {
        var renderContext = Substitute.For<IRenderContext>();

        var area = new Rect(0, 0, 100, 100);
        renderContext.Area.Returns(area);
        renderContext.Buffer.Returns(new Buffer(area, new Cell()));

        var text = _fixture.Create<string>();

        _component.SetText(text)
            .Render(renderContext);

        _ = renderContext.Received(1).Buffer;
        _ = renderContext.Received(1).Area;
    }

    [Fact]
    public void Render_When_UseFormattedText()
    {
        var renderContext = Substitute.For<IRenderContext>();

        var area = new Rect(0, 0, 100, 100);
        renderContext.Area.Returns(area);
        renderContext.Buffer.Returns(new Buffer(area, new Cell()));

        Text text = _fixture.Create<string>();

        _component.SetFormattedText(text)
            .Render(renderContext);

        _ = renderContext.Received(1).Buffer;
        _ = renderContext.Received(1).Area;
    }

    #endregion

    public class ViewModel : INotifyPropertyChanged
    {
        public string Text { get; set; } = string.Empty;

        public Style Style { get; set; } = new();

        public bool? Trim { get; set; }

        public Text? FormattedText { get; set; }

        public static IBindableProperty<ViewModel, string> TextProperty { get; } =
            new BindableProperty<ViewModel, string>(nameof(Text),
                viewModel => viewModel.Text,
                (viewModel, value) => viewModel.Text = value);

        public static IBindableProperty<ViewModel, Style> StyleProperty { get; } =
            new BindableProperty<ViewModel, Style>(nameof(Style),
                viewModel => viewModel.Style,
                (viewModel, value) => viewModel.Style = value);

        public static IBindableProperty<ViewModel, bool?> TrimProperty { get; } =
            new BindableProperty<ViewModel, bool?>(nameof(Trim),
                viewModel => viewModel.Trim,
                (viewModel, value) => viewModel.Trim = value);

        public static IBindableProperty<ViewModel, Text?> FormattedTextProperty { get; } =
            new BindableProperty<ViewModel, Text?>(nameof(FormattedText),
                viewModel => viewModel.FormattedText,
                (viewModel, value) => viewModel.FormattedText = value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
