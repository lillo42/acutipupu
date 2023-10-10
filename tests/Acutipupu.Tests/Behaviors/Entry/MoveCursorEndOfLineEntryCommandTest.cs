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

public class MoveCursorEndOfLineEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly MoveCursorEndOfLineEntryCommand _command;

    public MoveCursorEndOfLineEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 11)]
    [InlineData(SingleLineText, 0, 3, 11)]
    [InlineData(SingleLineText, 0, 10, 11)]
    [InlineData(CyrillicSingleLineText, 0, 5, 21)]
    [InlineData(GreekSingleLineText, 0, 6, 21)]
    [InlineData(ChineseSingleLineText, 0, 7, 35)]
    [InlineData(KoreanSingleLineText, 0, 8, 11)]
    [InlineData(MultiLineStartWithNewLine, 1, 2, 26)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, 0)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await _command.ExecuteAsync(ImmutableList<KeyMessage>.Empty);

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }
}
