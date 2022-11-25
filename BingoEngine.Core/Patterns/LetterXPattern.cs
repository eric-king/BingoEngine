namespace BingoEngine.Core.Patterns;

public class LetterXPattern : BasePattern
{
    public override string PatternName => "Letter X";

    protected override List<WinCondition> BuildWinConditions()
    {
        return new List<WinCondition>
        {
            new WinCondition("Both diagonals from corner to corner, forming the letter X",
            """
            ■□□□■
            □■□■□
            □□□□□
            □■□■□
            ■□□□■
            """),
        };
    }
}
