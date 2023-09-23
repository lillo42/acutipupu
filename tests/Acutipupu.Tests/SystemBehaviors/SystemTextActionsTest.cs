namespace Acutipupu.Tests.SystemBehaviors;

public class SystemTextActionsTest
{
    /*
    [Theory]
    [InlineData(SimpleText, 0, 0, 1, 1)]
    [InlineData(SimpleText, 0, 0, 5, 5)]
    [InlineData(SimpleText, 0, 0, 10, 10)]
    [InlineData(SimpleText, 0, 0, 11, 10)]
    [InlineData(SimpleText, 0, 3, 5, 8)]
    [InlineData(MultiLineText, 1, 995, 5, 995)]
    [InlineData(MultiLineText, 2, 1007, 300, 1307)]
    public void Right(string text, int textPositionRow, int textIndex, int moveColumns, int expectedTextIndex)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textPositionRow, textIndex);

        actions.Right(moveColumns);

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
    }

    [Theory]
    [InlineData(SimpleText, 0, 0, 1, 0)]
    [InlineData(SimpleText, 0, 1, 1, 0)]
    [InlineData(SimpleText, 0, 8, 5, 3)]
    [InlineData(SimpleText, 0, 10, 10, 0)]
    [InlineData(SimpleText, 0, 10, 11, 0)]
    [InlineData(MultiLineText, 1, 995, 5, 995)]
    [InlineData(MultiLineText, 2, 1300, 300, 1000)]
    public void Left(string text, int textPositionRow, int textIndex, int moveColumns, int expectedTextIndex)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textPositionRow, textIndex);

        actions.Left(moveColumns);

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
    }

    [Theory]
    [InlineData(SimpleText, 0, 0, LastColumnType.AtColumn, 1, 0, 0)]
    [InlineData(SimpleText, 0, 1, LastColumnType.AtColumn, 10, 1, 0)]
    [InlineData(MultiLineText, 1, 995, LastColumnType.AtColumn, 1, 0, 0)]
    [InlineData(MultiLineText, 2, 2103, LastColumnType.AtColumn, 1, 995, 1)]
    [InlineData(MultiLineText, 2, 2103, LastColumnType.AtColumn, 2, 993, 0)]
    [InlineData(MultiLineText, 2, 1000, LastColumnType.AtColumn, 2, 3, 0)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.AtColumn, 2, 1849, 2)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.AtColumn, 4, 852, 0)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.EndOfLine, 2, 2103, 2)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.EndOfLine, 4, 993, 0)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.BeginningOfLine, 2, 993, 0)]
    [InlineData(MultiLineText, 4, 2959, LastColumnType.BeginningOfLine, 4, 0, 0)]
    public void Up(string text, int textRow, int textIndex, LastColumnType lastColumnType, int moveRows,
        int expectedTextIndex, int expectedRow)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textRow, textIndex);
        systemText.State.LastColumnPosition = lastColumnType switch
        {
            LastColumnType.BeginningOfLine => BeginningOfLinePosition.Instance,
            LastColumnType.EndOfLine => EndOfLinePosition.Instance,
            LastColumnType.AtColumn => new AtColumnPosition(textIndex - systemText.State.Indexes[textRow].StartAt),
            _ => throw new ArgumentOutOfRangeException(nameof(lastColumnType), lastColumnType, null)
        };

        actions.Up(moveRows);

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
        systemText.State.CursorTextPosition.Row.Should().Be(expectedRow);
    }

    [Theory]
    [InlineData(SimpleText, 0, 0, LastColumnType.AtColumn, 1, 0, 0)]
    [InlineData(SimpleText, 0, 1, LastColumnType.AtColumn, 10, 1, 0)]
    [InlineData(MultiLineText, 0, 0, LastColumnType.AtColumn, 1, 995, 1)]
    [InlineData(MultiLineText, 0, 0, LastColumnType.AtColumn, 2, 997, 2)]
    [InlineData(MultiLineText, 0, 0, LastColumnType.BeginningOfLine, 1, 995, 1)]
    [InlineData(MultiLineText, 0, 0, LastColumnType.BeginningOfLine, 2, 997, 2)]
    [InlineData(MultiLineText, 0, 993, LastColumnType.AtColumn, 1, 995, 1)]
    [InlineData(MultiLineText, 0, 993, LastColumnType.AtColumn, 2, 1990, 2)]
    [InlineData(MultiLineText, 0, 993, LastColumnType.EndOfLine, 1, 995, 1)]
    [InlineData(MultiLineText, 0, 993, LastColumnType.EndOfLine, 2, 2103, 2)]
    public void Down(string text, int textRow, int textIndex, LastColumnType lastColumnType, int moveRows,
        int expectedTextIndex, int expectedRow)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textRow, textIndex);
        systemText.State.LastColumnPosition = lastColumnType switch
        {
            LastColumnType.BeginningOfLine => BeginningOfLinePosition.Instance,
            LastColumnType.EndOfLine => EndOfLinePosition.Instance,
            LastColumnType.AtColumn => new AtColumnPosition(textIndex - systemText.State.Indexes[textRow].StartAt),
            _ => throw new ArgumentOutOfRangeException(nameof(lastColumnType), lastColumnType, null)
        };

        actions.Down(moveRows);

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
        systemText.State.CursorTextPosition.Row.Should().Be(expectedRow);
    }

    [Theory]
    [InlineData(SimpleText, 0, 0, 0)]
    [InlineData(SimpleText, 0, 1, 0)]
    [InlineData(SimpleText, 0, 10, 0)]
    [InlineData(MultiLineText, 0, 0, 0)]
    [InlineData(MultiLineText, 0, 1, 0)]
    [InlineData(MultiLineText, 1, 995, 995)]
    [InlineData(MultiLineText, 2, 2000, 997)]
    public void BeginningOfLine(string text, int textRow, int textIndex, int expectedTextIndex)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textRow, textIndex);

        actions.BeginningOfLine();

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
        systemText.State.LastColumnPosition.Should().BeOfType<BeginningOfLinePosition>();
    }

    [Theory]
    [InlineData(SimpleText, 0, 0, 10)]
    [InlineData(SimpleText, 0, 1, 10)]
    [InlineData(SimpleText, 0, 10, 10)]
    [InlineData(MultiLineText, 0, 0, 993)]
    [InlineData(MultiLineText, 0, 1, 933)]
    [InlineData(MultiLineText, 1, 995, 995)]
    [InlineData(MultiLineText, 2, 2000, 2103)]
    public void EndOfLine(string text, int textRow, int textIndex, int expectedTextIndex)
    {
        var systemText = new DefaultSystemText { Text = text };
        var actions = new SystemTextActions(systemText);
        systemText.State.CursorTextPosition = (textRow, textIndex);

        actions.EndOfLine();

        systemText.State.CursorTextPosition.Index.Should().Be(expectedTextIndex);
        systemText.State.LastColumnPosition.Should().BeOfType<EndOfLinePosition>();
    }


    private const string SimpleText = "Simple text";

    private const string MultiLineText =
        """
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio tempor orci dapibus ultrices in. Convallis aenean et tortor at risus viverra. Sed odio morbi quis commodo odio aenean sed adipiscing diam. Tincidunt ornare massa eget egestas purus viverra. Pretium vulputate sapien nec sagittis. Pellentesque sit amet porttitor eget dolor. Sit amet commodo nulla facilisi nullam vehicula. Ullamcorper dignissim cras tincidunt lobortis feugiat vivamus. Feugiat sed lectus vestibulum mattis ullamcorper velit sed ullamcorper. Gravida quis blandit turpis cursus in hac habitasse platea. Commodo ullamcorper a lacus vestibulum sed arcu non. Congue eu consequat ac felis donec et odio. Vel facilisis volutpat est velit egestas dui. Et leo duis ut diam quam nulla porttitor. Consequat semper viverra nam libero justo laoreet sit amet cursus. Dui faucibus in ornare quam viverra orci sagittis eu volutpat. Pulvinar mattis nunc sed blandit.

        Sed libero enim sed faucibus turpis. Ac turpis egestas sed tempus urna. Tellus mauris a diam maecenas sed enim ut sem. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae proin. Mollis aliquam ut porttitor leo a diam. Quisque egestas diam in arcu cursus. Adipiscing enim eu turpis egestas pretium aenean pharetra magna ac. Nibh tellus molestie nunc non blandit. Pellentesque pulvinar pellentesque habitant morbi tristique senectus et. Venenatis lectus magna fringilla urna porttitor rhoncus dolor. Turpis nunc eget lorem dolor sed viverra ipsum nunc. Sit amet nisl purus in mollis nunc. Eget sit amet tellus cras adipiscing enim eu. Tortor consequat id porta nibh venenatis cras sed felis. Facilisi etiam dignissim diam quis enim lobortis scelerisque. Donec adipiscing tristique risus nec feugiat in fermentum posuere urna. Dictumst vestibulum rhoncus est pellentesque elit ullamcorper dignissim cras tincidunt. Ac tincidunt vitae semper quis lectus nulla at volutpat. Amet consectetur adipiscing elit pellentesque habitant morbi tristique senectus et. Ut etiam sit amet nisl purus in mollis.

        Nunc sed blandit libero volutpat. Purus in massa tempor nec feugiat nisl pretium. Interdum consectetur libero id faucibus nisl tincidunt. Egestas erat imperdiet sed euismod nisi porta lorem. Turpis in eu mi bibendum neque egestas congue. Mi tempus imperdiet nulla malesuada pellentesque elit. Hac habitasse platea dictumst quisque sagittis purus sit. Dictum fusce ut placerat orci. Viverra maecenas accumsan lacus vel facilisis volutpat. Purus sit amet luctus venenatis lectus magna. Habitasse platea dictumst quisque sagittis purus sit amet volutpat consequat. At tellus at urna condimentum mattis pellentesque id nibh. Iaculis nunc sed augue lacus viverra vitae congue eu. Id interdum velit laoreet id donec ultrices tincidunt arcu. Suspendisse ultrices gravida dictum fusce ut placerat orci nulla pellentesque. Rhoncus dolor purus non enim praesent.
        """;

    private class DefaultSystemText : ISystemText
    {
        public bool IsActive { get; set; }

        public CursorPosition CursorPosition { get; set; }

        public Action<IRenderContext>? OnRender { get; set; }

        // public Action<Frame, bool> OnRender { get; set; } = (_, _) => { };
        public (int Top, int Left) Screen { get; set; }
        public SystemTextState State { get; set; } = new();

        public string Text { get; set; } = string.Empty;
    }

    public enum LastColumnType
    {
        BeginningOfLine,
        EndOfLine,
        AtColumn
    }
    */
}
