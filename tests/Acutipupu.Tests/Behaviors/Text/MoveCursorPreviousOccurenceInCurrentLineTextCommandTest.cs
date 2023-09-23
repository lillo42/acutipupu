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

public class MoveCursorPreviousOccurenceInCurrentLineTextCommandTest : TextCommandTest
{
    private readonly Entry _component = new();


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

        var command = new MoveCursorPreviousOccurenceInCurrentLineTextCommand(accessor, options, afterTheOccurence);
        _component
            .SetText(text)
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtCursorPosition(cursorPositionColumn));

        await command.ExecuteAsync(numberOfOccurence.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char("f"), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .Append(new KeyMessage(KeyCode.Char(searchSymbol), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(cursorPositionRow, expectedColumn));
    }

    private class
        MoveCursorPreviousOccurenceInCurrentLineTextCommand : Acutipupu.Behaviors.Text.
            MoveCursorPreviousOccurenceInCurrentLineTextCommand
    {
        public MoveCursorPreviousOccurenceInCurrentLineTextCommand(IComponentAccessor accessor,
            IOptionsMonitor<AcutipupuAppOptions> options,
            bool after)
            : base(accessor, options)
        {
            AfterTheOccurence = after;
        }

        protected override KeyBindingCollection GetKey(TextKeyMap map)
        {
            throw new NotImplementedException();
        }
    }
}
