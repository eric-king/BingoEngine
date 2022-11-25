namespace BingoEngine.Core.Patterns;

public class FourCornersPattern : BasePattern
{
    public override string PatternName => "Four Corners";

    protected override List<WinCondition> BuildWinConditions()
    {
        return new List<WinCondition>
        {
            new WinCondition("Four corner cells of the grid",
            """
            ■□□□■
            □□□□□
            □□□□□
            □□□□□
            ■□□□■
            """),
        };
    }
}
