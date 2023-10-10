namespace Acutipupu.Tests.Behaviors.Entry;

public abstract class EntryCommandTest
{
    protected const string SingleLineText = "Lorem ipsum";
    protected const string MultiLineWindows = "Lorem ipsum\r\ndolor sit amet";
    protected const string MultiLineUnix = "Lorem ipsum\ndolor sit amet";
    protected const string MultiLineEndWithNewLine = "Lorem ipsum\n";
    protected const string MultiLineStartWithNewLine = "\nLorem ipsum dolor sit amet";
    protected const string MultiLineWithMultiNewLineSeq = "Lorem ipsum\n\n\ndolor sit amet";
    protected const string MultiLine = "Lorem ipsum\ndolor sit amet\nconsectetur adipiscing elit";
    protected const string MultiLineWithFirstLineLargerThanFirst = "dolor sit amet\nLorem ipsum";

    // Generated with https://generator.lorem-ipsum.info
    protected const string CyrillicSingleLineText = "Лорем ипсум долор сит";
    protected const string GreekSingleLineText = "Λορεμ ιπσθμ δολορ σιτ";
    protected const string ChineseSingleLineText = "上周映分分天幡及到能朝度野止己度気刈金無。 与載対嘆葉卓謙望色備著書。";
    protected const string KoreanSingleLineText = "이상은 기관과 것이다";
}
