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

public class MoveCursorPreviousParagraphEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly MoveCursorPreviousParagraphEntryCommand _command;

    public MoveCursorPreviousParagraphEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
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
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await _command.ExecuteAsync(numberOfParagraph.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }
}
