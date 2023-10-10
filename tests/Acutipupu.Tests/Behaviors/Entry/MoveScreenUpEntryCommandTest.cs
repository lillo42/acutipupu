using System.Collections.Immutable;
using Acutipupu.Behaviors.Entry;
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

public class MoveScreenUpEntryCommandTest : EntryCommandTest
{
    private readonly MoveScreenUpEntryCommand _command;
    private readonly Acutipupu.Components.Entry _component = new();

    public MoveScreenUpEntryCommandTest()
    {
        var options = Substitute.For<IOptionsMonitor<AcutipupuAppOptions>>();
        var lastTextColumn = Substitute.For<ILastTextColumn>();
        lastTextColumn.LastColumn(Arg.Any<int>()).Returns(call => call.Arg<int>());

        _component = new Acutipupu.Components.Entry();
        var accessor = Substitute.For<IComponentAccessor>();
        accessor.Component.Returns(_component);

        _command = new(accessor, lastTextColumn, options);
    }


    private const string LoremIpsum = """
                                      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Pellentesque massa placerat duis ultricies lacus sed turpis tincidunt id. Vitae congue eu consequat ac felis donec et odio. Felis eget nunc lobortis mattis aliquam faucibus purus in massa. Sed felis eget velit aliquet sagittis id consectetur purus ut. Sed risus pretium quam vulputate dignissim. Viverra tellus in hac habitasse platea dictumst vestibulum rhoncus est. Et tortor consequat id porta nibh. Integer eget aliquet nibh praesent tristique. Purus faucibus ornare suspendisse sed. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae proin. Iaculis nunc sed augue lacus viverra vitae. Venenatis cras sed felis eget velit aliquet sagittis id consectetur. Porttitor lacus luctus accumsan tortor posuere ac ut. Tristique senectus et netus et malesuada.

                                      Pretium quam vulputate dignissim suspendisse in est ante in. Dignissim cras tincidunt lobortis feugiat vivamus at augue eget. Sem viverra aliquet eget sit amet tellus cras. Sit amet purus gravida quis blandit turpis. Tincidunt ornare massa eget egestas purus viverra accumsan in nisl. Placerat in egestas erat imperdiet sed euismod. Lorem dolor sed viverra ipsum. Eu volutpat odio facilisis mauris. Enim ut sem viverra aliquet eget sit. Vivamus arcu felis bibendum ut. Nunc sed id semper risus in hendrerit gravida rutrum. Id velit ut tortor pretium viverra suspendisse potenti nullam. Cras sed felis eget velit aliquet sagittis id. Consequat ac felis donec et odio. Tristique nulla aliquet enim tortor at. Aliquet bibendum enim facilisis gravida neque. Sed turpis tincidunt id aliquet risus feugiat. Eget est lorem ipsum dolor sit amet consectetur. Pellentesque pulvinar pellentesque habitant morbi tristique senectus. Nibh tellus molestie nunc non.

                                      Fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Eget nulla facilisi etiam dignissim diam quis enim lobortis scelerisque. Volutpat diam ut venenatis tellus in. Tristique magna sit amet purus gravida. Et tortor consequat id porta nibh venenatis cras. Vitae turpis massa sed elementum tempus. Fringilla est ullamcorper eget nulla facilisi etiam dignissim diam. Feugiat vivamus at augue eget arcu dictum. Faucibus in ornare quam viverra orci sagittis eu volutpat odio. Consectetur a erat nam at. Sem viverra aliquet eget sit amet tellus.

                                      Malesuada bibendum arcu vitae elementum curabitur. Nulla facilisi cras fermentum odio eu feugiat. Aliquam faucibus purus in massa. Dictum varius duis at consectetur lorem donec. At ultrices mi tempus imperdiet nulla. Fermentum posuere urna nec tincidunt praesent semper feugiat nibh. Enim nec dui nunc mattis enim ut tellus elementum sagittis. Lectus arcu bibendum at varius vel pharetra. Blandit volutpat maecenas volutpat blandit aliquam etiam erat velit scelerisque. Egestas pretium aenean pharetra magna ac placerat vestibulum lectus. Morbi blandit cursus risus at ultrices. Ante in nibh mauris cursus mattis molestie a iaculis at. Sed blandit libero volutpat sed cras ornare arcu.

                                      Amet nisl suscipit adipiscing bibendum est. Malesuada bibendum arcu vitae elementum curabitur vitae nunc. Ultrices mi tempus imperdiet nulla malesuada pellentesque elit. Auctor augue mauris augue neque. Vulputate eu scelerisque felis imperdiet proin fermentum leo vel. Amet consectetur adipiscing elit pellentesque habitant morbi tristique. Pharetra magna ac placerat vestibulum. Sodales ut eu sem integer vitae justo. Condimentum vitae sapien pellentesque habitant morbi. Purus in massa tempor nec feugiat nisl pretium fusce. Ornare arcu dui vivamus arcu felis bibendum ut tristique. Vitae tempus quam pellentesque nec nam.

                                      In est ante in nibh mauris cursus. Dolor purus non enim praesent elementum. Vitae et leo duis ut diam quam nulla porttitor massa. Arcu cursus euismod quis viverra. Dolor sed viverra ipsum nunc aliquet bibendum. Nulla facilisi cras fermentum odio eu. Suspendisse interdum consectetur libero id. Libero justo laoreet sit amet cursus sit amet dictum. Proin fermentum leo vel orci. Elit duis tristique sollicitudin nibh. Blandit libero volutpat sed cras ornare arcu. Metus vulputate eu scelerisque felis imperdiet. Ultricies leo integer malesuada nunc vel risus commodo viverra. Est ante in nibh mauris cursus mattis molestie a. Imperdiet dui accumsan sit amet nulla facilisi morbi. Odio ut enim blandit volutpat maecenas. Mattis enim ut tellus elementum sagittis vitae et leo. Amet dictum sit amet justo donec enim diam vulputate ut.

                                      Neque gravida in fermentum et sollicitudin ac. Venenatis tellus in metus vulputate eu scelerisque. Lectus mauris ultrices eros in cursus turpis massa. Vehicula ipsum a arcu cursus vitae congue mauris rhoncus aenean. Faucibus in ornare quam viverra orci. Gravida arcu ac tortor dignissim. Felis donec et odio pellentesque diam. Interdum velit laoreet id donec ultrices tincidunt arcu non. Non quam lacus suspendisse faucibus interdum. Aliquet porttitor lacus luctus accumsan tortor posuere ac ut. Vitae tempus quam pellentesque nec nam aliquam sem et tortor. Bibendum neque egestas congue quisque egestas diam in arcu. Adipiscing vitae proin sagittis nisl. A pellentesque sit amet porttitor eget dolor morbi. Massa sapien faucibus et molestie. Pellentesque habitant morbi tristique senectus. Purus viverra accumsan in nisl.

                                      Sagittis nisl rhoncus mattis rhoncus urna neque viverra. Non sodales neque sodales ut. Elementum tempus egestas sed sed. Ridiculus mus mauris vitae ultricies. Turpis massa sed elementum tempus egestas. Mus mauris vitae ultricies leo integer malesuada nunc vel risus. Tempus urna et pharetra pharetra massa. Id ornare arcu odio ut sem nulla pharetra diam. Morbi tristique senectus et netus et malesuada. Fermentum odio eu feugiat pretium. At lectus urna duis convallis convallis tellus id. Placerat vestibulum lectus mauris ultrices. Turpis egestas pretium aenean pharetra magna ac. Odio ut sem nulla pharetra diam. Sed sed risus pretium quam vulputate dignissim suspendisse in est. Mauris nunc congue nisi vitae suscipit tellus mauris a diam. Lacus vel facilisis volutpat est velit egestas dui id. Est ante in nibh mauris. Risus ultricies tristique nulla aliquet enim tortor at auctor. Eu non diam phasellus vestibulum lorem sed.

                                      Sed ullamcorper morbi tincidunt ornare massa eget egestas purus viverra. Arcu non odio euismod lacinia at quis. A erat nam at lectus urna duis convallis convallis. Eu feugiat pretium nibh ipsum consequat. Nunc lobortis mattis aliquam faucibus purus. Netus et malesuada fames ac. Ut pharetra sit amet aliquam id. A arcu cursus vitae congue mauris. Pretium viverra suspendisse potenti nullam ac tortor vitae purus. Mauris in aliquam sem fringilla ut morbi tincidunt. Habitant morbi tristique senectus et netus et malesuada fames ac. Tortor at risus viverra adipiscing at in tellus integer. Nullam vehicula ipsum a arcu cursus. Tortor pretium viverra suspendisse potenti nullam ac tortor vitae purus. Congue nisi vitae suscipit tellus mauris a. Bibendum neque egestas congue quisque egestas diam in arcu. Etiam erat velit scelerisque in dictum non consectetur a erat. Fermentum posuere urna nec tincidunt praesent semper feugiat nibh sed. Eu lobortis elementum nibh tellus.

                                      Lacus suspendisse faucibus interdum posuere lorem ipsum. Lectus vestibulum mattis ullamcorper velit sed ullamcorper morbi tincidunt ornare. Congue eu consequat ac felis donec et odio pellentesque. Viverra mauris in aliquam sem fringilla ut morbi. Turpis massa sed elementum tempus egestas sed. Facilisis gravida neque convallis a cras semper auctor. Sed viverra tellus in hac habitasse. Enim diam vulputate ut pharetra sit amet aliquam. Quisque id diam vel quam. Aliquam eleifend mi in nulla posuere sollicitudin aliquam ultrices. Turpis in eu mi bibendum neque. Amet purus gravida quis blandit turpis cursus in hac habitasse. Aliquam sem et tortor consequat id porta nibh. Tortor aliquam nulla facilisi cras fermentum odio eu feugiat pretium. Sed adipiscing diam donec adipiscing tristique risus nec feugiat in. A lacus vestibulum sed arcu non odio. Et tortor at risus viverra adipiscing.
                                      """;

    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return new object[] { };

            yield return new object[]
            {
                LoremIpsum, 21, new Rect(0, 15, 5, 5), new Margin(0, 0), new CursorPosition(15, 0),
                new Rect(0, 0, 5, 5), new CursorPosition(0, 0)
            };
        }
    }


    [Theory]
    [InlineData(LoremIpsum, 1,
        0, 10, 5, 5,
        0, 0,
        0, 0,
        0, 9, 9, 0)]
    [InlineData(LoremIpsum, 5,
        0, 10, 5, 5,
        0, 0,
        0, 0,
        0, 5,
        5, 0)]
    [InlineData(LoremIpsum, 3,
        0, 10, 5, 5,
        0, 0,
        7, 0,
        0, 7,
        7, 0)]
    [InlineData(LoremIpsum, 21,
        0, 18, 5, 5,
        0, 0,
        15, 0,
        0, 0,
        5, 0)]
    public async Task OnExecute(string text, int movement,
        int screenAreaX, int screenAreaY, int screenAreaWidth, int screenAreaHeight,
        int marginHorizontal, int marginVertical,
        int cursorPositionRow, int cursorPositionColumn,
        int expectedScreenAreaX, int expectedScreenAreaY,
        int expectedCursorPositionRow, int expectedCursorPositionColumn)
    {
        var screenArea = new Rect(screenAreaX, screenAreaY, screenAreaWidth, screenAreaHeight);
        var margin = new Margin(marginHorizontal, marginVertical);
        var cursorPosition = new CursorPosition(cursorPositionRow, cursorPositionColumn);

        _component
            .SetText(text)
            .SetCursorPosition(cursorPosition)
            .SetScreenArea(screenArea)
            .SetMargin(margin);

        await _command.ExecuteAsync(movement.ToString().ToCharArray()
            .Select(ch => new KeyMessage(KeyCode.Char(ch), KeyModifiers.None, KeyEventKind.Press, KeyEventState.None))
            .ToImmutableList());

        _component.CursorPosition.Should()
            .Be(new CursorPosition(expectedCursorPositionRow, expectedCursorPositionColumn));
        _component.ScreenArea.Should()
            .Be(new Rect(expectedScreenAreaX, expectedScreenAreaY, screenAreaWidth, screenAreaHeight));
    }
}
