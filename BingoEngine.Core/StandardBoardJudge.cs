using BingoEngine.Core.Boards;

namespace BingoEngine.Core;

public static class StandardBoardJudge
{
    /// <summary>
    /// Reconstitutes a standard Bingo Board and its values based on the combination of Session Code and 
    /// Board Code, then checks the Board based on the Win Conditions of the provided Bingo Pattern and the 
    /// Bingo Numbers called in this session.
    /// </summary>
    /// <param name="sessionCode">The code for the current game session</param>
    /// <param name="boardCode">The code for the board being evaluated</param>
    /// <param name="pattern">The game pattern used for this game</param>
    /// <param name="numbersCalled">The Bingo numbers that have been called during this game session</param>
    /// <returns>The first fulfilled Win Condition. If no Win Condition is fullfilled, returns null</returns>
    public static WinCondition? Evaluate(string sessionCode, string boardCode, IBingoPattern pattern, int[] numbersCalled)
    {
        // reconstitute the board with all of its values based on the board code 
        StandardBoard board = new(sessionCode, boardCode);

        // boards may match more than one win condition
        // but we only need the first one
        return pattern.WinConditions.FirstOrDefault(condition =>
        {
            // if ALL of the cells in the win condition had their
            // number called, then we have a winner
            return condition.Cells.All(cell =>
            {
                // get the value of the win condition cell in the board
                string boardCellValue = board.Grid[cell.RowIndex].Cells[cell.ColIndex].Value;

                // check to see if it was called
                return numbersCalled.Contains(int.Parse(boardCellValue));
            });
        });
    }
}
