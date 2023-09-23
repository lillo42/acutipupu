using System.ComponentModel;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using AutoFixture;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using FluentAssertions;

namespace Acutipupu.Tests.Components;

public class EntryTest
{
    private readonly Fixture _fixture = new();
    private readonly Entry _component = new();

    [Fact]
    public void OnTextChange_Should_RemoveNewLine_When_MultiLineIsFalse()
    {
        _component.MultiLine = false;
        _component.Text = "Hello\nWorld";
        _component.Text.Should().Be("HelloWorld");
    }

    [Fact]
    public void OnTextChange_Should_RemoveNotChangeText_When_MultiLineIsTrue()
    {
        _component.MultiLine = true;
        _component.Text = "Hello\nWorld";
        _component.Text.Should().Be("Hello\nWorld");
    }

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
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Text;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Text = val;
                })
            .SetText(value);

        viewModel.Text.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Text = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Text));
        _component.Text.Should().Be(value);
    }

    #endregion

    #region Set Placeholder

    [Fact]
    public void SetPlaceholder()
    {
        var value = _fixture.Create<string>();
        _component.SetPlaceholder(value)
            .Placeholder.Should().Be(value);
    }

    [Fact]
    public void SetPlaceholderWithBindableProperty()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetPlaceholder(ViewModel.PlaceholderProperty)
            .SetPlaceholder(value);

        viewModel.Placeholder.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Placeholder = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Placeholder));
        _component.Placeholder.Should().Be(value);
    }

    [Fact]
    public void SetPlaceholderWithExpression()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetPlaceholder<ViewModel>(vm => vm.Placeholder)
            .SetPlaceholder(value);

        viewModel.Placeholder.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Placeholder = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Placeholder));
        _component.Placeholder.Should().Be(value);
    }

    [Fact]
    public void SetPlaceholderWithDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetPlaceholder<ViewModel>(
                nameof(ViewModel.Placeholder),
                vm => vm.Placeholder,
                (vm, val) => vm.Placeholder = val)
            .SetPlaceholder(value);

        viewModel.Placeholder.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Placeholder = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Placeholder));
        _component.Placeholder.Should().Be(value);
    }

    [Fact]
    public void SetPlaceholderWithObjectDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetPlaceholder(
                nameof(ViewModel.Placeholder),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Placeholder;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Placeholder = val;
                })
            .SetPlaceholder(value);

        viewModel.Placeholder.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.Placeholder = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Placeholder));
        _component.Placeholder.Should().Be(value);
    }

    #endregion

    #region Set Style

    [Fact]
    public void SetStyle()
    {
        var style = new EntryStyle();
        _component.SetStyle(style)
            .Style.Should().Be(style);
    }

    [Fact]
    public void SetStyleWithBindableProperty()
    {
        var value = new EntryStyle { Base = new() { Background = Color.Cyan } };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle(ViewModel.StyleProperty)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new EntryStyle { Base = new() { Foreground = Color.Black } };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithExpression()
    {
        var value = new EntryStyle { Base = new() { Background = Color.Cyan } };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle<ViewModel>(vm => vm.Style)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new EntryStyle { Base = new() { Foreground = Color.Black } };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithDelegate()
    {
        var value = new EntryStyle { Base = new() { Background = Color.Cyan } };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle<ViewModel>(
                nameof(ViewModel.Style),
                vm => vm.Style,
                (vm, val) => vm.Style = val)
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new EntryStyle { Base = new() { Foreground = Color.Black } };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
    }

    [Fact]
    public void SetStyleWithObjectDelegate()
    {
        var value = new EntryStyle { Base = new() { Background = Color.Cyan } };
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetStyle(
                nameof(ViewModel.Style),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Style;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Style = val;
                })
            .SetStyle(value);

        viewModel.Style.Should().Be(value);

        value = new EntryStyle { Base = new() { Foreground = Color.Black } };
        viewModel.Style = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Style));
        _component.Style.Should().Be(value);
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
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Trim;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Trim = val;
                })
            .SetTrim(value);

        viewModel.Trim.Should().Be(value);

        value = _fixture.Create<bool?>();
        viewModel.Trim = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Trim));
        _component.Trim.Should().Be(value);
    }

    #endregion

    #region Set MultiLine

    [Fact]
    public void SetMultiLine()
    {
        var value = _fixture.Create<bool>();
        _component.SetMultiLine(value)
            .MultiLine.Should().Be(value);
    }

    [Fact]
    public void EnableMultiLine()
    {
        _component.EnableMultiLine()
            .MultiLine.Should().BeTrue();
    }

    [Fact]
    public void DisableMultiLine()
    {
        _component.DisableMultiLine()
            .MultiLine.Should().BeFalse();
    }

    [Fact]
    public void SetMultiLineWithBindableProperty()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMultiLine(ViewModel.MultiLineProperty)
            .SetMultiLine(value);

        viewModel.MultiLine.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.MultiLine = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.MultiLine));
        _component.MultiLine.Should().Be(value);
    }

    [Fact]
    public void SetMultiLineWithExpression()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMultiLine<ViewModel>(vm => vm.MultiLine)
            .SetMultiLine(value);

        viewModel.MultiLine.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.MultiLine = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.MultiLine));
        _component.MultiLine.Should().Be(value);
    }

    [Fact]
    public void SetMultiLineWithDelegate()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMultiLine<ViewModel>(
                nameof(ViewModel.MultiLine),
                vm => vm.MultiLine,
                (vm, val) => vm.MultiLine = val)
            .SetMultiLine(value);

        viewModel.MultiLine.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.MultiLine = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.MultiLine));
        _component.MultiLine.Should().Be(value);
    }

    [Fact]
    public void SetMultiLIneWithObjectDelegate()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMultiLine(
                nameof(ViewModel.MultiLine),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.MultiLine;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.MultiLine = val;
                })
            .SetMultiLine(value);

        viewModel.MultiLine.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.MultiLine = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.MultiLine));
        _component.MultiLine.Should().Be(value);
    }

    #endregion

    #region Set Echo Mode

    [Fact]
    public void SetEchoMode()
    {
        var value = _fixture.Create<EntryEchoMode>();
        _component.SetEchoMode(value)
            .EchoMode.Should().Be(value);
    }

    [Fact]
    public void SetEchoModeWithBindableProperty()
    {
        var value = _fixture.Create<EntryEchoMode>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoMode(ViewModel.EchoModeProperty)
            .SetEchoMode(value);

        viewModel.EchoMode.Should().Be(value);

        value = _fixture.Create<EntryEchoMode>();
        viewModel.EchoMode = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoMode));
        _component.EchoMode.Should().Be(value);
    }

    [Fact]
    public void SetEchoModeWithExpression()
    {
        var value = _fixture.Create<EntryEchoMode>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoMode<ViewModel>(vm => vm.EchoMode)
            .SetEchoMode(value);

        viewModel.EchoMode.Should().Be(value);

        value = _fixture.Create<EntryEchoMode>();
        viewModel.EchoMode = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoMode));
        _component.EchoMode.Should().Be(value);
    }

    [Fact]
    public void SetEchoModeWithDelegate()
    {
        var value = _fixture.Create<EntryEchoMode>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoMode<ViewModel>(
                nameof(ViewModel.EchoMode),
                vm => vm.EchoMode,
                (vm, val) => vm.EchoMode = val)
            .SetEchoMode(value);

        viewModel.EchoMode.Should().Be(value);

        value = _fixture.Create<EntryEchoMode>();
        viewModel.EchoMode = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoMode));
        _component.EchoMode.Should().Be(value);
    }

    [Fact]
    public void SetEchoModeWithObjectDelegate()
    {
        var value = _fixture.Create<EntryEchoMode>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoMode(
                nameof(ViewModel.EchoMode),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.EchoMode;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.EchoMode = val;
                })
            .SetEchoMode(value);

        viewModel.EchoMode.Should().Be(value);

        value = _fixture.Create<EntryEchoMode>();
        viewModel.EchoMode = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoMode));
        _component.EchoMode.Should().Be(value);
    }

    #endregion

    #region Set Echo Character

    [Fact]
    public void SetEchoCharacter()
    {
        var value = _fixture.Create<string>();
        _component.SetEchoCharacter(value)
            .EchoCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEchoCharacterWithBindableProperty()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoCharacter(ViewModel.EchoCharacterProperty)
            .SetEchoCharacter(value);

        viewModel.EchoCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EchoCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoCharacter));
        _component.EchoCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEchoCharacterWithExpression()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoCharacter<ViewModel>(vm => vm.EchoCharacter)
            .SetEchoCharacter(value);

        viewModel.EchoCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EchoCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoCharacter));
        _component.EchoCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEchoCharacterWithDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoCharacter<ViewModel>(
                nameof(ViewModel.EchoCharacter),
                vm => vm.EchoCharacter,
                (vm, val) => vm.EchoCharacter = val)
            .SetEchoCharacter(value);

        viewModel.EchoCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EchoCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoCharacter));
        _component.EchoCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEchoCharacterWithObjectDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEchoCharacter(
                nameof(ViewModel.EchoCharacter),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.EchoCharacter;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.EchoCharacter = val;
                })
            .SetEchoCharacter(value);

        viewModel.EchoCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EchoCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EchoCharacter));
        _component.EchoCharacter.Should().Be(value);
    }

    #endregion

    #region Set End of Buffer Character

    [Fact]
    public void SetEndOfBufferCharacter()
    {
        var value = _fixture.Create<string>();
        _component.SetEndOfBufferCharacter(value)
            .EndOfBufferCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEndOfBufferCharacterWithBindableProperty()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEndOfBufferCharacter(ViewModel.EndOfBufferCharacterProperty)
            .SetEndOfBufferCharacter(value);

        viewModel.EndOfBufferCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EndOfBufferCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EndOfBufferCharacter));
        _component.EndOfBufferCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEndOfBufferCharacterWithExpression()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEndOfBufferCharacter<ViewModel>(vm => vm.EndOfBufferCharacter)
            .SetEndOfBufferCharacter(value);

        viewModel.EndOfBufferCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EndOfBufferCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EndOfBufferCharacter));
        _component.EndOfBufferCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEndOfBufferCharacterWithDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEndOfBufferCharacter<ViewModel>(
                nameof(ViewModel.EndOfBufferCharacter),
                vm => vm.EndOfBufferCharacter,
                (vm, val) => vm.EndOfBufferCharacter = val)
            .SetEndOfBufferCharacter(value);

        viewModel.EndOfBufferCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EndOfBufferCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EndOfBufferCharacter));
        _component.EndOfBufferCharacter.Should().Be(value);
    }

    [Fact]
    public void SetEndOfBufferCharacterWithObjectDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetEndOfBufferCharacter(
                nameof(ViewModel.EndOfBufferCharacter),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.EndOfBufferCharacter;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.EndOfBufferCharacter = val;
                })
            .SetEndOfBufferCharacter(value);

        viewModel.EndOfBufferCharacter.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.EndOfBufferCharacter = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.EndOfBufferCharacter));
        _component.EndOfBufferCharacter.Should().Be(value);
    }

    #endregion

    #region Set CursorPosition

    [Fact]
    public void SetCursorPosition()
    {
        var value = new CursorPosition(0, 5);
        _component
            .SetText("Hello World")
            .SetCursorPosition(value)
            .CursorPosition.Should().Be(value);
    }

    [Fact]
    public void SetCursorPositionWithBindableProperty()
    {
        var value = new CursorPosition(0, 3);
        var viewModel = new ViewModel();

        _component
            .SetText("Hello\nWorld")
            .SetBindingContext(viewModel)
            .SetCursorPosition(ViewModel.CursorPositionProperty)
            .SetCursorPosition(value);

        viewModel.CursorPosition.Should().Be(value);

        value = new CursorPosition(1, 2);
        viewModel.CursorPosition = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CursorPosition));
        _component.CursorPosition.Should().Be(value);
    }

    [Fact]
    public void SetCursorPositionWithExpression()
    {
        var value = new CursorPosition(0, 1);
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText("Hello\nWorld")
            .SetCursorPosition<ViewModel>(vm => vm.CursorPosition)
            .SetCursorPosition(value);

        viewModel.CursorPosition.Should().Be(value);

        value = new CursorPosition(1, 2);
        viewModel.CursorPosition = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CursorPosition));
        _component.CursorPosition.Should().Be(value);
    }

    [Fact]
    public void SetCursorPositionWithDelegate()
    {
        var value = new CursorPosition(1, 2);
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText("Hello\nWorld")
            .SetCursorPosition<ViewModel>(
                nameof(ViewModel.CursorPosition),
                vm => vm.CursorPosition,
                (vm, val) => vm.CursorPosition = val)
            .SetCursorPosition(value);

        viewModel.CursorPosition.Should().Be(value);

        value = new CursorPosition(0, 4);
        viewModel.CursorPosition = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CursorPosition));
        _component.CursorPosition.Should().Be(value);
    }

    [Fact]
    public void SetCursorPositionWithObjectDelegate()
    {
        var value = new CursorPosition(0, 4);
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetText("Hello\nWorld")
            .SetCursorPosition(
                nameof(ViewModel.CursorPosition),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.CursorPosition;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.CursorPosition = val;
                })
            .SetCursorPosition(value);

        viewModel.CursorPosition.Should().Be(value);

        value = new CursorPosition(0, 1);
        viewModel.CursorPosition = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CursorPosition));
        _component.CursorPosition.Should().Be(value);
    }

    #endregion

    #region Set Margin

    [Fact]
    public void SetMargin()
    {
        var value = _fixture.Create<Margin>();
        _component.SetMargin(value)
            .Margin.Should().Be(value);
    }

    [Fact]
    public void SetMarginWithBindableProperty()
    {
        var value = _fixture.Create<Margin>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMargin(ViewModel.MarginProperty)
            .SetMargin(value);

        viewModel.Margin.Should().Be(value);

        value = _fixture.Create<Margin>();
        viewModel.Margin = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Margin));
        _component.Margin.Should().Be(value);
    }

    [Fact]
    public void SetMarginWithExpression()
    {
        var value = _fixture.Create<Margin>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMargin<ViewModel>(vm => vm.Margin)
            .SetMargin(value);

        viewModel.Margin.Should().Be(value);

        value = _fixture.Create<Margin>();
        viewModel.Margin = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Margin));
        _component.Margin.Should().Be(value);
    }

    [Fact]
    public void SetMarginWithDelegate()
    {
        var value = _fixture.Create<Margin>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMargin<ViewModel>(
                nameof(ViewModel.Margin),
                vm => vm.Margin,
                (vm, val) => vm.Margin = val)
            .SetMargin(value);

        viewModel.Margin.Should().Be(value);

        value = _fixture.Create<Margin>();
        viewModel.Margin = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Margin));
        _component.Margin.Should().Be(value);
    }

    [Fact]
    public void SetMarginWithObjectDelegate()
    {
        var value = _fixture.Create<Margin>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetMargin(
                nameof(ViewModel.Margin),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Margin;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Margin = val;
                })
            .SetMargin(value);

        viewModel.Margin.Should().Be(value);

        value = _fixture.Create<Margin>();
        viewModel.Margin = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Margin));
        _component.Margin.Should().Be(value);
    }

    #endregion

    #region Set Scroll

    [Fact]
    public void SetScroll()
    {
        var value = _fixture.Create<(int, int)>();
        _component.SetScroll(value)
            .Scroll.Should().Be(value);
    }

    [Fact]
    public void SetScrollWithBindableProperty()
    {
        var value = _fixture.Create<(int, int)>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetScroll(ViewModel.ScrollProperty)
            .SetScroll(value);

        viewModel.Scroll.Should().Be(value);

        value = _fixture.Create<(int, int)>();
        viewModel.Scroll = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Scroll));
        _component.Scroll.Should().Be(value);
    }

    [Fact]
    public void SetScrollWithExpression()
    {
        var value = _fixture.Create<(int, int)>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetScroll<ViewModel>(vm => vm.Scroll)
            .SetScroll(value);

        viewModel.Scroll.Should().Be(value);

        value = _fixture.Create<(int, int)>();
        viewModel.Scroll = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Scroll));
        _component.Scroll.Should().Be(value);
    }

    [Fact]
    public void SetScrollWithDelegate()
    {
        var value = _fixture.Create<(int, int)>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetScroll<ViewModel>(
                nameof(ViewModel.Scroll),
                vm => vm.Scroll,
                (vm, val) => vm.Scroll = val)
            .SetScroll(value);

        viewModel.Scroll.Should().Be(value);

        value = _fixture.Create<(int, int)>();
        viewModel.Scroll = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Scroll));
        _component.Scroll.Should().Be(value);
    }

    [Fact]
    public void SetScrollWithObjectDelegate()
    {
        var value = _fixture.Create<(int, int)>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetScroll(
                nameof(ViewModel.Scroll),
                o =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    return vm.Scroll;
                },
                (o, val) =>
                {
                    var vm = o.Should().BeOfType<ViewModel>().Subject;
                    vm.Scroll = val;
                })
            .SetScroll(value);

        viewModel.Scroll.Should().Be(value);

        value = _fixture.Create<(int, int)>();
        viewModel.Scroll = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Scroll));
        _component.Scroll.Should().Be(value);
    }

    #endregion

    public class ViewModel : INotifyPropertyChanged
    {
        public string Text { get; set; } = string.Empty;
        public string Placeholder { get; set; } = string.Empty;
        public bool? Trim { get; set; }
        public bool MultiLine { get; set; }
        public EntryStyle Style { get; set; } = new();
        public EntryEchoMode EchoMode { get; set; }
        public string EchoCharacter { get; set; } = string.Empty;
        public string EndOfBufferCharacter { get; set; } = string.Empty;
        public CursorPosition CursorPosition { get; set; }
        public (int Row, int Column)? Scroll { get; set; }
        public Margin Margin { get; set; } = new(0, 0);

        public static IBindableProperty<ViewModel, string> TextProperty { get; } =
            new BindableProperty<ViewModel, string>(nameof(Text),
                viewModel => viewModel.Text,
                (viewModel, value) => viewModel.Text = value);

        public static IBindableProperty<ViewModel, string> PlaceholderProperty { get; } =
            new BindableProperty<ViewModel, string>(nameof(Placeholder),
                viewModel => viewModel.Placeholder,
                (viewModel, value) => viewModel.Placeholder = value);

        public static IBindableProperty<ViewModel, bool?> TrimProperty { get; } =
            new BindableProperty<ViewModel, bool?>(nameof(Trim),
                viewModel => viewModel.Trim,
                (viewModel, value) => viewModel.Trim = value);

        public static IBindableProperty<ViewModel, bool> MultiLineProperty { get; } =
            new BindableProperty<ViewModel, bool>(nameof(MultiLine),
                viewModel => viewModel.MultiLine,
                (viewModel, value) => viewModel.MultiLine = value);

        public static IBindableProperty<ViewModel, EntryStyle> StyleProperty { get; } =
            new BindableProperty<ViewModel, EntryStyle>(nameof(Style),
                viewModel => viewModel.Style,
                (viewModel, value) => viewModel.Style = value);

        public static IBindableProperty<ViewModel, EntryEchoMode> EchoModeProperty { get; } =
            new BindableProperty<ViewModel, EntryEchoMode>(nameof(EchoMode),
                viewModel => viewModel.EchoMode,
                (viewModel, value) => viewModel.EchoMode = value);

        public static IBindableProperty<ViewModel, string> EchoCharacterProperty { get; } =
            new BindableProperty<ViewModel, string>(nameof(EchoCharacter),
                viewModel => viewModel.EchoCharacter,
                (viewModel, value) => viewModel.EchoCharacter = value);

        public static IBindableProperty<ViewModel, CursorPosition> CursorPositionProperty { get; } =
            new BindableProperty<ViewModel, CursorPosition>(nameof(CursorPosition),
                viewModel => viewModel.CursorPosition,
                (viewModel, value) => viewModel.CursorPosition = value);

        public static IBindableProperty<ViewModel, string> EndOfBufferCharacterProperty { get; } =
            new BindableProperty<ViewModel, string>(nameof(EndOfBufferCharacter),
                viewModel => viewModel.EndOfBufferCharacter,
                (viewModel, value) => viewModel.EndOfBufferCharacter = value);

        public static IBindableProperty<ViewModel, Margin> MarginProperty { get; } =
            new BindableProperty<ViewModel, Margin>(nameof(Margin),
                viewModel => viewModel.Margin,
                (viewModel, value) => viewModel.Margin = value);

        public static IBindableProperty<ViewModel, (int, int)?> ScrollProperty { get; } =
            new BindableProperty<ViewModel, (int, int)?>(nameof(Scroll),
                viewModel => viewModel.Scroll,
                (viewModel, value) => viewModel.Scroll = value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
