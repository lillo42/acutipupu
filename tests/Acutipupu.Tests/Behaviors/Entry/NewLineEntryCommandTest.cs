using System.Collections.Immutable;
using Acutipupu.Behaviors.Entry;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Acutipupu.Tests.Behaviors.Entry;

public class NewLineEntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly NewLineEntryCommand _command;

    public NewLineEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
        _component.NewLine = "\n";
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return new object[] { string.Empty, new CursorPosition(0, 0), "\n" };
            yield return new object[] { "L", new CursorPosition(0, 1), "L\n" };
            yield return new object[] { "Lr", new CursorPosition(0, 1), "L\nr" };
            yield return new object[] { "or", new CursorPosition(0, 0), "\nor" };
            yield return new object[] { "Lorm", new CursorPosition(0, 3), "Lor\nm" };
            yield return new object[] { "Lore", new CursorPosition(0, 4), "Lore\n" };
            yield return new object[] { "Lorem\n", new CursorPosition(1, 0), "Lorem\n\n" };
            yield return new object[] { "Lorem\nI", new CursorPosition(1, 1), "Lorem\nI\n" };
            yield return new object[] { "Lorem\np", new CursorPosition(1, 0), "Lorem\n\np" };
        }
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnExecute(string text, CursorPosition cursorPosition, string expectedText)
    {
        _component
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetText(text)
            .SetCursorPosition(cursorPosition);
        
        await _command.ExecuteAsync(ImmutableList<KeyMessage>.Empty);
        
        _component.Text.Should().Be(expectedText);
        _component.CursorPosition.Should().Be(new CursorPosition(cursorPosition.Row + 1, 0));
    }
}
