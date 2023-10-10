using System.Collections.Immutable;
using Acutipupu.Behaviors.Entry;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Layouts;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tutu.Events;

namespace Acutipupu.Tests.Behaviors.Entry;

public class MoveCursorUpEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly MoveCursorUpEntryCommand _command;

    public MoveCursorUpEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, false, 1, 0, 0)]
    [InlineData(CyrillicSingleLineText, 0, 1, false, 1, 0, 1)]
    [InlineData(GreekSingleLineText, 0, 2, false, 1, 0, 2)]
    [InlineData(ChineseSingleLineText, 0, 3, false, 1, 0, 3)]
    [InlineData(KoreanSingleLineText, 0, 4, false, 4, 0, 4)]
    [InlineData(MultiLineWindows, 1, 0, false, 1, 0, 0)]
    [InlineData(MultiLineWindows, 1, 10, true, 1, 0, 11)]
    [InlineData(MultiLineUnix, 1, 0, false, 1, 0, 0)]
    [InlineData(MultiLineUnix, 1, 13, true, 1, 0, 11)]
    [InlineData(MultiLineUnix, 1, 0, false, 3, 0, 0)]
    [InlineData(MultiLineUnix, 1, 13, true, 3, 0, 11)]
    [InlineData(MultiLineWithFirstLineLargerThanFirst, 1, 10, false, 1, 0, 10)]
    [InlineData(MultiLineWithFirstLineLargerThanFirst, 1, 10, true, 1, 0, 14)]
    [InlineData(MultiLineEndWithNewLine, 1, 0, false, 1, 0, 0)]
    [InlineData(MultiLineEndWithNewLine, 1, 0, true, 1, 0, 11)]
    [InlineData(MultiLineStartWithNewLine, 1, 0, false, 1, 0, 0)]
    [InlineData(MultiLineStartWithNewLine, 1, 3, false, 1, 0, 0)]
    [InlineData(MultiLineStartWithNewLine, 1, 13, true, 1, 0, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 3, 0, false, 1, 2, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 3, 0, false, 2, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 3, 0, false, 3, 0, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 3, 5, false, 3, 0, 5)]
    [InlineData(MultiLineWithMultiNewLineSeq, 3, 13, true, 3, 0, 11)]
    [InlineData(MultiLine, 2, 5, false, 5, 0, 5)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, bool isEndOfLine,
        int movement, int expectedRow, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(isEndOfLine ? -1 : cursorPositionColumn));

        await _command.ExecuteAsync(movement.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
