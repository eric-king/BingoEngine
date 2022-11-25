namespace BingoEngine.Core.Patterns;

public class PostageStampPattern : BasePattern
{
    public override string PatternName => "Postage Stamp";

    protected override List<WinCondition> BuildWinConditions()
    {
        return new List<WinCondition>
        {
            new WinCondition("4 cells at the top right corner",
            """
            □□□■■
            □□□■■
            □□□□□
            □□□□□
            □□□□□
            """),
        };
    }
}
