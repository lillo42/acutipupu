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

public class MoveCursorWordBackwardEntryCommandTest : EntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component = new();

    [Theory]
    [InlineData(SingleLineText, 0, 0, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 1, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 2, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 3, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 4, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 5, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 6, 1, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 7, 1, false, true, 0, 6)]
    [InlineData(SingleLineText, 0, 8, 1, false, true, 0, 6)]
    [InlineData(SingleLineText, 0, 9, 1, false, true, 0, 6)]
    [InlineData(SingleLineText, 0, 10, 1, false, true, 0, 6)]
    [InlineData(SingleLineText, 0, 10, 2, false, true, 0, 0)]
    [InlineData(SingleLineText, 0, 0, 1, false, false, 0, 0)]
    [InlineData(SingleLineText, 0, 1, 1, false, false, 0, 0)]
    [InlineData(SingleLineText, 0, 2, 1, false, false, 0, 0)]
    [InlineData(SingleLineText, 0, 3, 1, false, false, 0, 0)]
    [InlineData(SingleLineText, 0, 4, 1, false, false, 0, 0)]
    [InlineData(SingleLineText, 0, 5, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 6, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 7, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 8, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 9, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 10, 1, false, false, 0, 4)]
    [InlineData(SingleLineText, 0, 10, 2, false, false, 0, 0)]
    [InlineData("    " + SingleLineText, 0, 6, 1, false, true, 0, 4)]
    [InlineData("    " + SingleLineText, 0, 10, 1, false, false, 0, 8)]
    [InlineData("    " + SingleLineText, 0, 6, 2, false, true, 0, 0)]
    [InlineData("    " + SingleLineText, 0, 10, 2, false, false, 0, 0)]
    [InlineData(CyrillicSingleLineText, 0, 6, 1, false, true, 0, 0)]
    [InlineData(CyrillicSingleLineText, 0, 6, 1, false, false, 0, 4)]
    [InlineData(GreekSingleLineText, 0, 6, 1, false, true, 0, 0)]
    [InlineData(GreekSingleLineText, 0, 6, 1, false, false, 0, 4)]
    [InlineData(ChineseSingleLineText, 0, 20, 1, false, true, 0, 0)]
    [InlineData(ChineseSingleLineText, 0, 20, 1, false, false, 0, 19)]
    [InlineData(ChineseSingleLineText, 0, 22, 1, true, true, 0, 0)]
    [InlineData(KoreanSingleLineText, 0, 4, 1, false, true, 0, 0)]
    [InlineData(KoreanSingleLineText, 0, 4, 1, false, false, 0, 2)]
    [InlineData(MultiLineWindows, 1, 0, 1, false, true, 0, 6)]
    [InlineData(MultiLineWindows, 1, 0, 1, false, false, 0, 10)]
    [InlineData(MultiLineUnix, 1, 0, 1, false, true, 0, 6)]
    [InlineData(MultiLineUnix, 1, 0, 1, false, false, 0, 10)]
    [InlineData(MultiLineUnix, 1, 9, 1, false, true, 1, 6)]
    [InlineData(MultiLineUnix, 1, 9, 1, false, false, 1, 8)]
    [InlineData(MultiLineEndWithNewLine, 1, 0, 1, false, true, 0, 6)]
    [InlineData(MultiLineEndWithNewLine, 1, 0, 1, false, false, 0, 10)]
    [InlineData(MultiLineStartWithNewLine, 1, 0, 1, false, true, 0, 0)]
    [InlineData(MultiLineStartWithNewLine, 1, 0, 1, false, false, 0, 0)]
    [InlineData(MultiLine, 2, 0, 4, false, true, 0, 6)]
    [InlineData(MultiLine, 2, 0, 4, false, false, 0, 10)]
    [InlineData(MultiLineWithMultiNewLineSeq, 2, 0, 1, false, true, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 2, 0, 1, false, false, 1, 0)]
    [InlineData(MultiLineWithMultiNewLineSeq, 2, 0, 2, false, true, 0, 6)]
    [InlineData(MultiLineWithMultiNewLineSeq, 2, 0, 2, false, false, 0, 10)]
    [InlineData("Lorem, ipsum", 0, 7, 1, false, true, 0, 5)]
    [InlineData("Lorem, ipsum", 0, 7, 1, true, true, 0, 0)]
    [InlineData("Lorem, ipsum", 0, 7, 1, false, false, 0, 5)]
    [InlineData("Lorem, ipsum", 0, 7, 2, false, false, 0, 4)]
    [InlineData("Lorem, ipsum", 0, 7, 2, true, false, 0, 0)]
    public async Task OnExecute(string text, int cursorPositionRow, int cursorPositionColumn,
        int numberOfWord, bool ignorePunctuation, bool startOfWord,
        int expectedRow, int expectedColumn)
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        var command = new MoveCursorWordBackwardEntryCommand(accessor, options, ignorePunctuation, startOfWord);
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

    private class MoveCursorWordBackwardEntryCommand : Acutipupu.Behaviors.Entry.MoveCursorWordBackwardEntryCommand
    {
        public MoveCursorWordBackwardEntryCommand(IComponentAccessor accessor,
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
