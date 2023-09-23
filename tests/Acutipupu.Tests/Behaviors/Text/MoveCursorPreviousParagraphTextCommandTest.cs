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

public class MoveCursorPreviousParagraphTextCommandTest : TextCommandTest
{
    private readonly Entry _component;
    private readonly MoveCursorPreviousParagraphTextCommand _command;

    public MoveCursorPreviousParagraphTextCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    [Theory]
    [InlineData(SingleLineText, 0, 6, 1, 0, 0)]
    [InlineData(SingleLineText + "\n" + SingleLineText, 1, 0, 1, 0, 0)]
    [InlineData(SingleLineText + "\n\n" + SingleLineText, 2, 0, 1, 1, 0)]
    [InlineData(SingleLineText + "\n\n" + SingleLineText, 1, 0, 2, 0, 0)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        int numberOfParagraph, int expectedRow, int expectedColumn)
    {
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await _command.ExecuteAsync(numberOfParagraph.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
