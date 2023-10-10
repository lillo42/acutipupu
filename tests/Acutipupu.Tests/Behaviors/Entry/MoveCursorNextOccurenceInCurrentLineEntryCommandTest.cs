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

public class MoveCursorNextOccurenceInCurrentLineEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component = new();


    [Theory]
    [InlineData(SingleLineText, 0, 0, "m", 1, false, 4)]
    [InlineData(SingleLineText, 0, 0, "m", 2, false, 10)]
    [InlineData(SingleLineText, 0, 0, "m", 1, true, 3)]
    [InlineData(SingleLineText, 0, 0, "m", 2, true, 9)]
    [InlineData(SingleLineText, 0, 0, "z", 1, true, 0)]
    [InlineData(ChineseSingleLineText, 0, 0, "分", 1, false, 3)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        string searchSymbol, int numberOfOccurence, bool beforeTheOccurence, int expectedColumn)
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        var command = new MoveCursorNextOccurenceInCurrentLineEntryCommand(accessor, options, beforeTheOccurence);
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await command.ExecuteAsync(numberOfOccurence.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char("F"), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char(searchSymbol), KeyModifiers.None, KeyEventKind.Press,
                KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }

    private class
        MoveCursorNextOccurenceInCurrentLineEntryCommand : Acutipupu.Behaviors.Entry.MoveCursorNextOccurenceInCurrentLineEntryCommand
    {
        public MoveCursorNextOccurenceInCurrentLineEntryCommand(IComponentAccessor accessor,
            IOptionsMonitor<AcutipupuAppOptions> options,
            bool before)
            : base(accessor, options)
        {
            BeforeTheOccurence = before;
        }

        protected override KeyBindingCollection GetKey(EntryKeyMap map)
        {
            throw new NotImplementedException();
        }
    }
}
