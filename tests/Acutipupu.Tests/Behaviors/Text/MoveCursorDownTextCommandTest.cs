using System.Collections.Immutable;
using Acutipupu.Behaviors.Text;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tutu.Events;

namespace Acutipupu.Tests.Behaviors.Text;

public class MoveCursorDownTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorDownTextCommand _command;

    public MoveCursorDownTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
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
    [InlineData(MultiLineWindows, 0, 0, false, 1, 1, 0)]
    [InlineData(MultiLineWindows, 0, 10, true, 1, 1, 14)]
    [InlineData(MultiLineUnix, 0, 0, false, 1, 1, 0)]
    [InlineData(MultiLineUnix, 0, 10, true, 1, 1, 14)]
    [InlineData(MultiLineUnix, 0, 0, false, 3, 1, 0)]
    [InlineData(MultiLineUnix, 0, 10, true, 3, 1, 14)]
    [InlineData(MultiLineEndWithNewLine, 0, 0, false, 1, 1, 0)]
    [InlineData(MultiLineEndWithNewLine, 0, 3, false, 1, 1, 0)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, false, 1, 1, 0)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, true, 1, 1, 26)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 0, false, 1, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 5, false, 1, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 5, false, 2, 2, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 5, false, 3, 3, 5)]
    [InlineData(MultiLine, 0, 5, false, 2, 2, 5)]
    [InlineData(MultiLine, 0, 10, true, 1, 1, 14)]
    [InlineData(MultiLine, 0, 10, true, 2, 2, 27)]
    [InlineData(MultiLineWithFirstLineLargerThanFirst, 0, 13, true, 2, 1, 11)]
    [InlineData(MultiLineWithFirstLineLargerThanFirst, 0, 7, false, 10, 1, 7)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, bool isEndOfLine,
        int movement, int expectedRow, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(isEndOfLine ? EndOfLine.Instance : new AtCursorPosition(cursorPositionColumn));

        await _command.ExecuteAsync(movement.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
