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

public class MoveCursorLeftEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly MoveCursorLeftEntryCommand _command;

    public MoveCursorLeftEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 0, 1, 0, 0)]
    [InlineData(SingleLineText, 0, 10, 1, 0, 9)]
    [InlineData(SingleLineText, 0, 5, 3, 0, 2)]
    [InlineData(SingleLineText, 0, 10, 100, 0, 0)]
    [InlineData(CyrillicSingleLineText, 0, 6, 1, 0, 5)]
    [InlineData(GreekSingleLineText, 0, 7, 1, 0, 6)]
    [InlineData(ChineseSingleLineText, 0, 8, 1, 0, 7)]
    [InlineData(KoreanSingleLineText, 0, 9, 1, 0, 8)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        int movement, int expectedRow, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await _command.ExecuteAsync(movement.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
