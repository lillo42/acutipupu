using System.Collections.Immutable;
using Acutipupu.Bindings;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Layouts;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tutu.Events;

namespace Acutipupu.Tests.Behaviors.Entry;

public class MoveCursorPreviousOccurenceInCurrentLineEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component = new();


    [Theory]
    [InlineData(SingleLineText, 0, 10, "m", 1, false, 4)]
    [InlineData(SingleLineText, 0, 10, "m", 1, true, 5)]
    [InlineData(SingleLineText, 0, 10, "z", 1, true, 10)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        string searchSymbol, int numberOfOccurence, bool afterTheOccurence, int expectedColumn)
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        var command = new MoveCursorPreviousOccurenceInCurrentLineEntryCommand(accessor, options, afterTheOccurence);
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await command.ExecuteAsync(numberOfOccurence.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char("f"), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char(searchSymbol), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }

    private class
        MoveCursorPreviousOccurenceInCurrentLineEntryCommand : Acutipupu.Behaviors.Entry.MoveCursorPreviousOccurenceInCurrentLineEntryCommand
    {
        public MoveCursorPreviousOccurenceInCurrentLineEntryCommand(IComponentAccessor accessor,
            IOptionsMonitor<AcutipupuAppOptions> options,
            bool after)
            : base(accessor, options)
        {
            AfterTheOccurence = after;
        }

        protected override KeyBindingCollection GetKey(EntryKeyMap map)
        {
            throw new NotImplementedException();
        }
    }
}
