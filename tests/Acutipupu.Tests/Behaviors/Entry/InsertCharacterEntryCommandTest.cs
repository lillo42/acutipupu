using System.Collections.Immutable;
using Acutipupu.Behaviors.Entry;
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

public class InsertCharacterEntryCommandTest
{
    private readonly Acutipupu.Components.Entry _component;
    private readonly InsertTest _command;

    public InsertCharacterEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();

        _component = new Acutipupu.Components.Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, options);
    }

    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return new object[] { string.Empty, new CursorPosition(0, 0), "L", "L" };
            yield return new object[] { "L", new CursorPosition(0, 1), "o", "Lo" };
            yield return new object[] { "Lr", new CursorPosition(0, 1), "o", "Lor" };
            yield return new object[] { "or", new CursorPosition(0, 0), "L", "Lor" };
            yield return new object[] { "Lorm", new CursorPosition(0, 3), "e", "Lorem" };
            yield return new object[] { "Lore", new CursorPosition(0, 4), "m", "Lorem" };
            yield return new object[] { "Lorem\n", new CursorPosition(1, 0), "I", "Lorem\nI" };
            yield return new object[] { "Lorem\nI", new CursorPosition(1, 1), "p", "Lorem\nIp" };
            yield return new object[] { "Lorem\np", new CursorPosition(1, 0), "I", "Lorem\nIp" };
        }
    }

    [Theory]
    [MemberData(nameof(Data))]
    public async Task OnExecute(string text, CursorPosition cursorPosition,
        string textToBeInserted,
        string expectedText)
    {
        _component
            .SetScreenArea(new Rect(0, 0, 100, 100))
            .SetText(text)
            .SetCursorPosition(cursorPosition);

        await _command.ExecuteAsync(ImmutableList<KeyMessage>.Empty
            .Add(new KeyMessage(new KeyCode.CharKeyCode(textToBeInserted), KeyModifiers.None, KeyEventKind.Press,
                KeyEventState.None)));

        _component.Text.Should().Be(expectedText);
        _component.CursorPosition.Should().Be(cursorPosition with { Column = cursorPosition.Column + 1 });
    }

    public class InsertTest : InsertCharacterEntryCommand
    {
        public InsertTest(IComponentAccessor accessor, IOptionsMonitor<AcutipupuAppOptions> options) : base(accessor,
            options)
        {
        }

        protected override KeyBindingCollection GetKey(EntryKeyMap map)
        {
            throw new Exception();
        }
    }
}
