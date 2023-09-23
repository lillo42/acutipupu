using System.Collections.Immutable;
using Acutipupu.Behaviors.Text;
using Acutipupu.Bindings;
using Acutipupu.Components;
using Acutipupu.Components.Extensions;
using Acutipupu.Messages;
using Acutipupu.SystemBehaviors;
using Boto.Terminals;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Tutu.Events;

namespace Acutipupu.Tests.Behaviors.Text;

public class MoveCursorNextOccurenceInCurrentLineTextCommandTest : TextCommandTest
{
    private readonly Entry _component = new();


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

        var command = new MoveCursorNextOccurenceInCurrentLineTextCommand(accessor, options, beforeTheOccurence);
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await command.ExecuteAsync(numberOfOccurence.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char("F"), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char(searchSymbol), KeyModifiers.None, KeyEventKind.Press,
                KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }

    private class
        MoveCursorNextOccurenceInCurrentLineTextCommand : Acutipupu.Behaviors.Text.
            MoveCursorNextOccurenceInCurrentLineTextCommand
    {
        public MoveCursorNextOccurenceInCurrentLineTextCommand(IComponentAccessor accessor,
            IOptionsMonitor<AcutipupuAppOptions> options,
            bool before)
            : base(accessor, options)
        {
            BeforeTheOccurence = before;
        }

        protected override KeyBindingCollection GetKey(TextKeyMap map)
        {
            throw new NotImplementedException();
        }
    }
}
