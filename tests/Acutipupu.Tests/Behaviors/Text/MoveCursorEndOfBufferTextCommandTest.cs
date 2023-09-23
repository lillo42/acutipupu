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

public class MoveCursorEndOfBufferTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorEndOfBufferTextCommand _command;

    public MoveCursorEndOfBufferTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 0, 11)]
    [InlineData(SingleLineText, 0, 10, 0, 11)]
    [InlineData(CyrillicSingleLineText, 0, 5, 0, 21)]
    [InlineData(GreekSingleLineText, 0, 6, 0, 21)]
    [InlineData(ChineseSingleLineText, 0, 7, 0, 35)]
    [InlineData(KoreanSingleLineText, 0, 8, 0, 11)]
    [InlineData(MultiLineWindows, 0, 3, 1, 14)]
    [InlineData(MultiLineUnix, 0, 3, 1, 14)]
    [InlineData(MultiLineEndWithNewLine, 0, 2, 1, 0)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn, 
        int expectedRow, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await _command.ExecuteAsync(ImmutableList<KeyMessage>.Empty);

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
