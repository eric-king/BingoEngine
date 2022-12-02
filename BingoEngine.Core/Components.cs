using System.Text;

namespace BingoEngine.Core;

public record BoardCell(string Value);
public record BoardRow(BoardCell[] Cells);
public record PatternCell(int ColIndex, int RowIndex);

public class WinCondition 
{
	public PatternCell[] Cells { get; init; }
    public string Description { get; init; }

    public WinCondition(string description, string pictogram)
    {
        Cells = BuildPatternCells(pictogram);
        Description = description;
    }

    public string ToPictogram() 
	{
        StringBuilder stringBuilder = new();
        for (int rowIndex = 0; rowIndex < Constants.STANDARD_ROW_COUNT; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Constants.STANDARD_COL_COUNT; colIndex++)
            {
                PatternCell cellToCheck = new(colIndex, rowIndex);
                bool isPartOfWinCondition = Cells.Any(cell => cell == cellToCheck);
                stringBuilder.Append(isPartOfWinCondition ? Constants.DARK_SQUARE : Constants.LIGHT_SQUARE);
            }
            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString().TrimEnd();
    }

    private static PatternCell[] BuildPatternCells(string pictogram)
    {
        List<PatternCell> patternCells = new();
        var rows = pictogram.Split(Environment.NewLine);
        for (int rowIndex = 0; rowIndex < Constants.STANDARD_ROW_COUNT; rowIndex++)
        {
            for (int colIndex = 0; colIndex < Constants.STANDARD_COL_COUNT; colIndex++)
            {
                if (rows[rowIndex][colIndex] == Constants.DARK_SQUARE)
                {
                    patternCells.Add(new PatternCell(colIndex, rowIndex));
                }
            }
        }
        return patternCells.ToArray();
    }
}
