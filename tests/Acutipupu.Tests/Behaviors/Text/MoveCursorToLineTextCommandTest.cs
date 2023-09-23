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

public class MoveCursorToLineTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorToLineTextCommand _command;

    public MoveCursorToLineTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 0, 0, 0)]
    [InlineData(SingleLineText, 0, 10, 1, 0, 0)]
    [InlineData(CyrillicSingleLineText, 0, 5, 0, 0, 0)]
    [InlineData(GreekSingleLineText, 0, 6, 0, 0, 0)]
    [InlineData(ChineseSingleLineText, 0, 7, 0, 0, 0)]
    [InlineData(KoreanSingleLineText, 0, 8, 0, 0, 0)]
    [InlineData(MultiLineWindows, 0, 3, 1, 1, 0)]
    [InlineData(MultiLineUnix, 0, 3, 1, 1, 0)]
    [InlineData(MultiLineEndWithNewLine, 0, 2, 1, 1, 0)]
    [InlineData(MultiLineStartWithNewLine, 1, 2, 0, 0, 0)]
    [InlineData(MultiLine, 0, 4, 2, 2, 0)]
    [InlineData(SingleLineText + " \n    " + SingleLineText, 0, 2, 1, 1, 4)]
    [InlineData("    " + MultiLine, 1, 2, 0, 0, 4)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, int line, int expectedRow,
        int expectedColumn)
    {
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await _command.ExecuteAsync(line.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
