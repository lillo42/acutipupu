using System.Collections.Immutable;
using Acutipupu.Behaviors.Text;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Acutipupu.Tests.Behaviors.Text;

public class MoveCursorFirstNonBlankCharacterInTheLineTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorFirstNonBlankCharacterInTheLineTextCommand _command;

    public MoveCursorFirstNonBlankCharacterInTheLineTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 0)]
    [InlineData(SingleLineText, 0, 3, 0)]
    [InlineData(SingleLineText, 0, 10, 0)]
    [InlineData(CyrillicSingleLineText, 0, 5, 0)]
    [InlineData(GreekSingleLineText, 0, 6, 0)]
    [InlineData(ChineseSingleLineText, 0, 7, 0)]
    [InlineData(KoreanSingleLineText, 0, 8, 0)]
    [InlineData(MultiLineStartWithNewLine, 1, 2, 0)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, 0)]
    [InlineData("    " + SingleLineText, 0, 10, 4)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await _command.ExecuteAsync(ImmutableList<KeyMessage>.Empty);

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }
}
