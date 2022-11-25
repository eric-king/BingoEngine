namespace BingoEngine.Core.Patterns;

public class BlackoutPattern : BasePattern
{
    public override string PatternName => "Blackout";

    protected override List<WinCondition> BuildWinConditions()
    {
        return new List<WinCondition>
        {
            new WinCondition("Entire grid",
            """
            ■■■■■
            ■■■■■
            ■■□■■
            ■■■■■
            ■■■■■
            """),
        };
    }
}
