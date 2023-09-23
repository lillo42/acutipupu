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

public class MoveCursorLastNonBlankCharacterInTheLineTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorLastNonBlankCharacterInTheLineTextCommand _command;

    public MoveCursorLastNonBlankCharacterInTheLineTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 10)]
    [InlineData(SingleLineText, 0, 3, 10)]
    [InlineData(SingleLineText, 0, 10, 10)]
    [InlineData(CyrillicSingleLineText, 0, 5, 20)]
    [InlineData(GreekSingleLineText, 0, 6, 20)]
    [InlineData(ChineseSingleLineText, 0, 7, 34)]
    [InlineData(KoreanSingleLineText, 0, 8, 10)]
    [InlineData(SingleLineText + "    ", 0, 0, 10)]
    [InlineData(SingleLineText + "    ", 0, 1, 10)]
    [InlineData(SingleLineText + "    ", 0, 10, 10)]
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
