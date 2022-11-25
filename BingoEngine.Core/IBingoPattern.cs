namespace BingoEngine.Core;

public interface IBingoPattern
{
    string PatternName { get; }
    List<WinCondition> WinConditions { get; init; }
}