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

public class MoveCursorWordForwardEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component = new();

    [Theory]
    [InlineData(SingleLineText, 0, 0, 1, false, true, 0, 6)]
    [InlineData(SingleLineText, 0, 0, 2, false, true, 0, 10)]
    [InlineData(SingleLineText, 0, 0, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 0, 2, false, false, 0, 10)]
    [InlineData(CyrillicSingleLineText, 0, 0, 1, false, true, 0, 6)]
    [InlineData(CyrillicSingleLineText, 0, 0, 1, false, false, 0, 4)]
    [InlineData(GreekSingleLineText, 0, 0, 1, false, true, 0, 6)]
    [InlineData(GreekSingleLineText, 0, 0, 1, false, false, 0, 4)]
    [InlineData(ChineseSingleLineText, 0, 0, 1, false, true, 0, 20)]
    [InlineData(ChineseSingleLineText, 0, 0, 1, true, true, 0, 22)]
    [InlineData(ChineseSingleLineText, 0, 0, 1, false, false, 0, 19)]
    [InlineData(ChineseSingleLineText, 0, 0, 1, true, false, 0, 20)]
    [InlineData(KoreanSingleLineText, 0, 0, 1, false, true, 0, 4)]
    [InlineData(KoreanSingleLineText, 0, 0, 1, false, false, 0, 2)]
    [InlineData(MultiLineWindows, 0, 0, 2, false, true, 1, 0)]
    [InlineData(MultiLineWindows, 0, 7, 1, false, true, 1, 0)]
    [InlineData(MultiLineWindows, 0, 10, 1, false, false, 1, 4)]
    [InlineData(MultiLineUnix, 0, 0, 2, false, true, 1, 0)]
    [InlineData(MultiLineUnix, 0, 7, 1, false, true, 1, 0)]
    [InlineData(MultiLineUnix, 0, 10, 1, false, false, 1, 4)]
    [InlineData(MultiLineEndWithNewLine, 0, 0, 2, false, true, 1, 0)]
    [InlineData(MultiLineEndWithNewLine, 0, 0, 3, false, true, 1, 0)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, 1, false, true, 1, 0)]
    [InlineData(MultiLineStartWithNewLine, 0, 0, 1, false, false, 1, 4)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 7, 1, false, true, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 0, 7, 2, false, false, 1, 0)]
    [InlineData("Lorem, ipsum", 0, 0, 1, false, true, 0, 5)]
    [InlineData("Lorem, ipsum", 0, 0, 2, false, true, 0, 7)]
    [InlineData("Lorem, ipsum", 0, 0, 1, true, true, 0, 7)]
    [InlineData("Lorem, ipsum", 0, 0, 1, false, false, 0, 4)]
    [InlineData("Lorem, ipsum", 0, 0, 1, true, false, 0, 5)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        int numberOfWord, bool ignorePunctuation, bool startOfWord,
        int expectedRow, int expectedColumn)
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        var command = new MoveCursorWordForwardEntryCommand(accessor, options, ignorePunctuation, startOfWord);
        _component
            .SetText(text)
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetCursorPosition(new CursorPosition(cursorPositionRow, cursorPositionColumn))
            .SetLastColumnPosition(new AtColumnPosition(cursorPositionColumn));

        await command.ExecuteAsync(numberOfWord.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should().Be(new CursorPosition(expectedRow, expectedColumn));
    }

    private class MoveCursorWordForwardEntryCommand : Acutipupu.Behaviors.Entry.MoveCursorWordForwardEntryCommand
    {
        public MoveCursorWordForwardEntryCommand(IComponentAccessor accessor,
            IOptionsMonitor<AcutipupuAppOptions> options,
            bool ignorePunctuation, bool startOfWord)
            : base(accessor, options)
        {
            IgnorePunctuation = ignorePunctuation;
            StartOfWord = startOfWord;
        }

        protected override KeyBindingCollection GetKey(EntryKeyMap map)
        {
            throw new NotImplementedException();
        }
    }
}
