using System.ComponentModel;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using AutoFixture;
using Boto.Layouts;
using Boto.Styles;
using Boto.Widgets;
using FluentAssertions;
using NSubstitute;
using Buffer = Boto.Buffers.Buffer;
using Cell = Boto.Buffers.Cell;
using Container = Acutipupu.Components.Container;
using Style = Acutipupu.Components.Style;

namespace Acutipupu.Tests.Components;

public class ContainerTests
{
    private readonly Fixture _fixture = new();
    private readonly Container _component = new();

    #region Set Split Direction

    [Fact]
    public void SetSplitDirection()
    {
        var value = _fixture.Create<Direction>();
        _component.SetSplitDirection(value)
            .SplitDirection.Should().Be(value);
    }

    [Fact]
    public void SetSplitDirectionWithBindableProperty()
    {
        var value = _fixture.Create<Direction>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetSplitDirection(ViewModel.DirectionProperty)
            .SetSplitDirection(value);

        viewModel.Direction.Should().Be(value);

        value = _fixture.Create<Direction>();
        viewModel.Direction = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Direction));
        _component.SplitDirection.Should().Be(value);
    }

    [Fact]
    public void SetSplitDirectionWithExpression()
    {
        var value = _fixture.Create<Direction>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetSplitDirection<ViewModel>(vm => vm.Direction)
            .SetSplitDirection(value);

        viewModel.Direction.Should().Be(value);

        value = _fixture.Create<Direction>();
        viewModel.Direction = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Direction));
        _component.SplitDirection.Should().Be(value);
    }

    [Fact]
    public void SetSplitDirectionWithDelegate()
    {
        var value = _fixture.Create<Direction>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetSplitDirection<ViewModel>(
                nameof(ViewModel.Direction),
                vm => vm.Direction,
                (vm, val) => vm.Direction = val)
            .SetSplitDirection(value);

        viewModel.Direction.Should().Be(value);

        value = _fixture.Create<Direction>();
        viewModel.Direction = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Direction));
        _component.SplitDirection.Should().Be(value);
    }

    [Fact]
    public void SetTrimWithObjectDelegate()
    {
        var value = _fixture.Create<Direction>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetSplitDirection(
                nameof(ViewModel.Direction),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.Direction;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.Direction = val;
                })
            .SetSplitDirection(value);

        viewModel.Direction.Should().Be(value);

        value = _fixture.Create<Direction>();
        viewModel.Direction = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Direction));
        _component.SplitDirection.Should().Be(value);
    }

    #endregion

    #region Set Style

    [Fact]
    public void SetStyle()
    {
        var style = new Style();
        _component.SetStyle(style)
            .Style.Should().Be(style);
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

    #region Set Expand to fill

    [Fact]
    public void SetExpandToFill()
    {
        var value = _fixture.Create<bool>();
        _component.SetExpandToFill(value)
            .ExpandToFill.Should().Be(value);
    }

    [Fact]
    public void EnableExpandToFill()
    {
        _component.EnableExpandToFill()
            .ExpandToFill.Should().BeTrue();
    }

    [Fact]
    public void DisableExpandToFill()
    {
        _component.DisableExpandToFill()
            .ExpandToFill.Should().BeFalse();
    }

    [Fact]
    public void SetExpandToFillWithBindableProperty()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetExpandToFill(ViewModel.ExpandToFillProperty)
            .SetExpandToFill(value);

        viewModel.ExpandToFill.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.ExpandToFill = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.ExpandToFill));
        _component.ExpandToFill.Should().Be(value);
    }

    [Fact]
    public void SetExpandToFillWithExpression()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetExpandToFill<ViewModel>(vm => vm.ExpandToFill)
            .SetExpandToFill(value);

        viewModel.ExpandToFill.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.ExpandToFill = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.ExpandToFill));
        _component.ExpandToFill.Should().Be(value);
    }

    [Fact]
    public void SetExpandToFillWithDelegate()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetExpandToFill<ViewModel>(
                nameof(ViewModel.ExpandToFill),
                vm => vm.ExpandToFill,
                (vm, val) => vm.ExpandToFill = val)
            .SetExpandToFill(value);

        viewModel.ExpandToFill.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.ExpandToFill = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.ExpandToFill));
        _component.ExpandToFill.Should().Be(value);
    }

    [Fact]
    public void SetExpandToFillWithObjectDelegate()
    {
        var value = _fixture.Create<bool>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetExpandToFill(
                nameof(ViewModel.ExpandToFill),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.ExpandToFill;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.ExpandToFill = val;
                })
            .SetExpandToFill(value);

        viewModel.ExpandToFill.Should().Be(value);

        value = _fixture.Create<bool>();
        viewModel.ExpandToFill = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.ExpandToFill));
        _component.ExpandToFill.Should().Be(value);
    }

    #endregion

    #region Render

    [Fact]
    public void Render()
    {
        var renderContext = Substitute.For<IRenderContext>();

        var area = new Rect(0, 0, 100, 100);
        renderContext.Area.Returns(area);
        renderContext.Buffer.Returns(new Buffer(area, new Cell()));

        _component
            .SetStyle(new() { Margin = new Margin(1, 1), Borders = Borders.All })
            .Add(Constraints.Percentage(100), new Container())
            .Render(renderContext);

        _ = renderContext.Received().Area;
    }

    #endregion

    public class ViewModel : INotifyPropertyChanged
    {
        public Direction Direction { get; set; } = Direction.Horizontal;

        public Style Style { get; set; } = new();

        public bool ExpandToFill { get; set; }

        public static IBindableProperty<ViewModel, Direction> DirectionProperty { get; } =
            new BindableProperty<ViewModel, Direction>(nameof(Direction),
                viewModel => viewModel.Direction,
                (viewModel, value) => viewModel.Direction = value);

        public static IBindableProperty<ViewModel, Style> StyleProperty { get; } =
            new BindableProperty<ViewModel, Style>(nameof(Style),
                viewModel => viewModel.Style,
                (viewModel, value) => viewModel.Style = value);

        public static IBindableProperty<ViewModel, bool> ExpandToFillProperty { get; } =
            new BindableProperty<ViewModel, bool>(nameof(ExpandToFill),
                viewModel => viewModel.ExpandToFill,
                (viewModel, value) => viewModel.ExpandToFill = value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
