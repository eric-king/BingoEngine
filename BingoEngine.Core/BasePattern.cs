namespace BingoEngine.Core;

public abstract class BasePattern : IBingoPattern
{
    public abstract string PatternName { get; }
    public List<WinCondition> WinConditions { get; init; }

    public BasePattern()
    {
        WinConditions = BuildWinConditions();
    }

    protected abstract List<WinCondition> BuildWinConditions();
}
