namespace BingoEngine.Core.Patterns;

public class StandardPattern : BasePattern
{
    public override string PatternName => "Standard Bingo";

    protected override List<WinCondition> BuildWinConditions()
    {
        return new List<WinCondition>
        {
            new WinCondition("Entire B column",
            """
            ■□□□□
            ■□□□□
            ■□□□□
            ■□□□□
            ■□□□□
            """),

            new WinCondition("Entire I column",
            """
            □■□□□
            □■□□□
            □■□□□
            □■□□□
            □■□□□
            """),

            new WinCondition("Entire N column",
            """
            □□■□□
            □□■□□
            □□□□□
            □□■□□
            □□■□□
            """),

            new WinCondition("Entire G column",
            """
            □□□■□
            □□□■□
            □□□■□
            □□□■□
            □□□■□
            """),

            new WinCondition("Entire O column",
            """
            □□□□■
            □□□□■
            □□□□■
            □□□□■
            □□□□■
            """),

            new WinCondition("Entire first row",
            """
            ■■■■■
            □□□□□
            □□□□□
            □□□□□
            □□□□□
            """),

            new WinCondition("Entire second row",
            """
            □□□□□
            ■■■■■
            □□□□□
            □□□□□
            □□□□□
            """),

            new WinCondition("Entire third row",
            """
            □□□□□
            □□□□□
            ■■□■■
            □□□□□
            □□□□□
            """),

            new WinCondition("Entire fourth row",
            """
            □□□□□
            □□□□□
            □□□□□
            ■■■■■
            □□□□□
            """),

            new WinCondition("Entire fifth row",
            """
            □□□□□
            □□□□□
            □□□□□
            □□□□□
            ■■■■■
            """),

            new WinCondition("Diagonal top left to bottom right",
            """
            ■□□□□
            □■□□□
            □□□□□
            □□□■□
            □□□□■
            """),

            new WinCondition("Diagonal top right to bottom left",
            """
            □□□□■
            □□□■□
            □□□□□
            □■□□□
            ■□□□□
            """)
        };
    }
}
