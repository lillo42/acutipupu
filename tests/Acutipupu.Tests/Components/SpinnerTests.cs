using System.ComponentModel;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using AutoFixture;
using Boto.Styles;
using FluentAssertions;
using Style = Acutipupu.Components.Style;

namespace Acutipupu.Tests.Components;

public class SpinnerTests
{
    private readonly Fixture _fixture = new();
    private readonly Spinner _component = new();

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

    #region Set Configuration

    [Fact]
    public void SetConfiguration()
    {
        var value = SpinnerConfiguration.Ellipsis;
        _component.SetConfiguration(value)
            .Configuration.Should().Be(value);
    }

    [Fact]
    public void SetConfigurationWithBindableProperty()
    {
        var value = SpinnerConfiguration.Ellipsis;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetConfiguration(ViewModel.ConfigurationProperty)
            .SetConfiguration(value);

        viewModel.Configuration.Should().Be(value);

        value = SpinnerConfiguration.Globe;
        viewModel.Configuration = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Configuration));
        _component.Configuration.Should().Be(value);
    }

    [Fact]
    public void SetConfigurationWithExpression()
    {
        var value = SpinnerConfiguration.Ellipsis;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetConfiguration<ViewModel>(vm => vm.Configuration)
            .SetConfiguration(value);

        viewModel.Configuration.Should().Be(value);

        value = SpinnerConfiguration.Globe;
        viewModel.Configuration = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Configuration));
        _component.Configuration.Should().Be(value);
    }

    [Fact]
    public void SetConfigurationWithDelegate()
    {
        var value = SpinnerConfiguration.Ellipsis;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetConfiguration<ViewModel>(
                nameof(ViewModel.Configuration),
                vm => vm.Configuration,
                (vm, val) => vm.Configuration = val)
            .SetConfiguration(value);

        viewModel.Configuration.Should().Be(value);

        value = SpinnerConfiguration.Globe;
        viewModel.Configuration = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Configuration));
        _component.Configuration.Should().Be(value);
    }

    [Fact]
    public void SetConfigurationWithObjectDelegate()
    {
        var value = SpinnerConfiguration.Ellipsis;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetConfiguration(
                nameof(ViewModel.Configuration),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.Configuration;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.Configuration = val;
                })
            .SetConfiguration(value);

        viewModel.Configuration.Should().Be(value);

        value = SpinnerConfiguration.Globe;
        viewModel.Configuration = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.Configuration));
        _component.Configuration.Should().Be(value);
    }

    #endregion

    #region Set CurrentFrame

    [Fact]
    public void SetCurrentFrame()
    {
        var value = _fixture.Create<string>();
        _component.SetCurrentFrame(value)
            .CurrentFrame.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameWithBindableProperty()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrame(ViewModel.CurrentFrameProperty)
            .SetCurrentFrame(value);

        viewModel.CurrentFrame.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.CurrentFrame = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrame));
        _component.CurrentFrame.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameWithExpression()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrame<ViewModel>(vm => vm.CurrentFrame)
            .SetCurrentFrame(value);

        viewModel.CurrentFrame.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.CurrentFrame = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrame));
        _component.CurrentFrame.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameWithDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrame<ViewModel>(
                nameof(ViewModel.CurrentFrame),
                vm => vm.CurrentFrame,
                (vm, val) => vm.CurrentFrame = val)
            .SetCurrentFrame(value);

        viewModel.CurrentFrame.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.CurrentFrame = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrame));
        _component.CurrentFrame.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameWithObjectDelegate()
    {
        var value = _fixture.Create<string>();
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrame(
                nameof(ViewModel.CurrentFrame),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.CurrentFrame;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.CurrentFrame = val;
                })
            .SetCurrentFrame(value);

        viewModel.CurrentFrame.Should().Be(value);

        value = _fixture.Create<string>();
        viewModel.CurrentFrame = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrame));
        _component.CurrentFrame.Should().Be(value);
    }

    #endregion

    #region Set CurrentFrameIndex

    [Fact]
    public void SetCurrentFrameIndex()
    {
        const int Value = 1;
        _component.SetCurrentFrameIndex(Value)
            .CurrentFrameIndex.Should().Be(Value);
    }

    [Fact]
    public void SetCurrentFrameIndexWithBindableProperty()
    {
        var value = 1;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrameIndex(ViewModel.CurrentFrameIndexProperty)
            .SetCurrentFrameIndex(value);

        viewModel.CurrentFrameIndex.Should().Be(value);

        value = 2;
        viewModel.CurrentFrameIndex = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrameIndex));
        _component.CurrentFrameIndex.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameIndexWithExpression()
    {
        var value = 1;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrameIndex<ViewModel>(vm => vm.CurrentFrameIndex)
            .SetCurrentFrameIndex(value);

        viewModel.CurrentFrameIndex.Should().Be(value);

        value = 2;
        viewModel.CurrentFrameIndex = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrameIndex));
        _component.CurrentFrameIndex.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameIndexWithDelegate()
    {
        var value = 1;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrameIndex<ViewModel>(
                nameof(ViewModel.CurrentFrameIndex),
                vm => vm.CurrentFrameIndex,
                (vm, val) => vm.CurrentFrameIndex = val)
            .SetCurrentFrameIndex(value);

        viewModel.CurrentFrameIndex.Should().Be(value);

        value = 2;
        viewModel.CurrentFrameIndex = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrameIndex));
        _component.CurrentFrameIndex.Should().Be(value);
    }

    [Fact]
    public void SetCurrentFrameIndexWithObjectDelegate()
    {
        var value = 1;
        var viewModel = new ViewModel();

        _component.SetBindingContext(viewModel)
            .SetCurrentFrameIndex(
                nameof(ViewModel.CurrentFrameIndex),
                vm =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    return labelViewModel.CurrentFrameIndex;
                },
                (vm, val) =>
                {
                    var labelViewModel = vm.Should().BeOfType<ViewModel>().Subject;
                    labelViewModel.CurrentFrameIndex = val;
                })
            .SetCurrentFrameIndex(value);

        viewModel.CurrentFrameIndex.Should().Be(value);

        value = 2;
        viewModel.CurrentFrameIndex = value;
        viewModel.OnPropertyChanged(nameof(ViewModel.CurrentFrameIndex));
        _component.CurrentFrameIndex.Should().Be(value);
    }

    #endregion

    [Fact]
    public async Task Start()
    {
        var counter = 0;
        _component
            .SetBindingContext(new ViewModel())
            .SetConfiguration(SpinnerConfiguration.Globe)
            .SetCurrentFrame(string.Empty, setter: (_, _) => counter++);

        _component.Start();

        await Task.Delay(1000);
        
        _component.Stop();
        
        counter.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Stop()
    {
        _component.Start();
        _component.Stop();
        _component.CurrentFrameIndex.Should().Be(0);
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public Style Style { get; set; } = new();

        public SpinnerConfiguration Configuration { get; set; } = SpinnerConfiguration.Line;

        public string? CurrentFrame { get; set; }

        public int CurrentFrameIndex { get; set; }

        public static IBindableProperty<ViewModel, Style> StyleProperty { get; } =
            new BindableProperty<ViewModel, Style>(nameof(Style),
                viewModel => viewModel.Style,
                (viewModel, value) => viewModel.Style = value);

        public static IBindableProperty<ViewModel, SpinnerConfiguration> ConfigurationProperty { get; } =
            new BindableProperty<ViewModel, SpinnerConfiguration>(nameof(Configuration),
                viewModel => viewModel.Configuration,
                (viewModel, value) => viewModel.Configuration = value);

        public static IBindableProperty<ViewModel, string?> CurrentFrameProperty { get; } =
            new BindableProperty<ViewModel, string?>(nameof(CurrentFrame),
                viewModel => viewModel.CurrentFrame,
                (viewModel, value) => viewModel.CurrentFrame = value);

        public static IBindableProperty<ViewModel, int> CurrentFrameIndexProperty { get; } =
            new BindableProperty<ViewModel, int>(nameof(CurrentFrameIndex),
                viewModel => viewModel.CurrentFrameIndex,
                (viewModel, value) => viewModel.CurrentFrameIndex = value);

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
