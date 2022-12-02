using HashidsNet;

namespace BingoEngine.Core.Boards;

public class StandardBoard
{
    public string[] ColumnLabels { get; } = new[] { "B", "I", "N", "G", "O" };
    public List<BoardRow> Grid { get; init; }
    public string BoardCode { get; init; }

    /// <summary>
    /// Builds a standard 5-by-5 Bingo board with a randomly-generated grid 
    /// </summary>
    /// <param name="sessionCode"></param>
    public StandardBoard(string sessionCode)
    {
        Grid = BuildRandomGrid();
        BoardCode = BuildBoardCode(sessionCode);
    }

    /// <summary>
    /// Builds a standard 5-by-5 Bingo board with the grid values
    /// determined by the boardCode
    /// </summary>
    /// <param name="sessionCode"></param>
    /// <param name="boardCode"></param>
    public StandardBoard(string sessionCode, string boardCode)
    {
        Grid = ReconstituteGrid(sessionCode, boardCode);
        BoardCode = boardCode;
    }

    public static int Draw(Random random, List<int> previouslyGeneratedValues)
    {
        return GenerateDistinctRandomInRange(random, (1, 75), previouslyGeneratedValues);
    }

    private static int GenerateDistinctRandomInRange(Random random, (int minValue, int maxValue) range, List<int> previouslyGeneratedValues)
    {
        int newValue;
        do
        {
            newValue = random.Next(range.minValue, range.maxValue + 1);
        }
        // try again if we've already got that value
        while (previouslyGeneratedValues.Any(previousValue => newValue == previousValue));

        return newValue;
    }

    private string BuildBoardCode(string sessionCode)
    {
        int[] gridValues = Grid.SelectMany(row => row.Cells.Select(cell => int.Parse(cell.Value))).ToArray();
        Hashids hashids = new(sessionCode);
        string boardCode = hashids.Encode(gridValues);
        return boardCode;
    }

    private List<BoardRow> BuildRandomGrid()
    {
        // it's easiest to generate the grid column-by-column
        // since that's how the rules are written
        var columns = GenerateStandardBingoColumns();

        // but we display it row-by-row, so
        // we need to pivot from columns to rows
        var rows = TransformColumnsToRows(columns);

        return rows;
    }

    private string[,] GenerateStandardBingoColumns()
    {
        var columns = new string[Constants.STANDARD_COL_COUNT, Constants.STANDARD_ROW_COUNT];

        // The valid cell values are numbers between 1-75,
        // based on these column parameters
        var columnParams = new Dictionary<string, (int MinCellValue, int MaxCellValue)>
        {
            { "B", (01,15) },
            { "I", (16,30) },
            { "N", (31,45) },
            { "G", (46,60) },
            { "O", (61,75) }
        };

        var random = new Random();
        foreach (var columnParam in columnParams)
        {
            // make sure we're working with the correct column,
            // based on the column label
            var colIndex = Array.IndexOf(ColumnLabels, columnParam.Key);

            // cell values can't repeat, so we need to keep track
            // of the previously generated values in this column
            var previouslyGeneratedValues = new List<int>();

            // calculate each cells distinct value based on the column's
            // designated value bucket as described above
            for (int rowIndex = 0; rowIndex < Constants.STANDARD_ROW_COUNT; rowIndex++)
            {
                int cellValue = GenerateDistinctRandomInRange(random, columnParam.Value, previouslyGeneratedValues);
                columns[colIndex, rowIndex] = cellValue.ToString();
                previouslyGeneratedValues.Add(cellValue);
            }
        }

        return columns;
    }

    private static List<BoardRow> ReconstituteGrid(string sessionCode, string boardCode)
    {
        // We can use HashIds with the SessionCode and the Boardcode
        // to decode the cell values into an int[25]
        // and then loop through the array to reverse engineer the rows
        Hashids hashids = new(sessionCode);
        int[] gridValues = hashids.Decode(boardCode);

        List<BoardRow> rows = new();
        for (int rowIndex = 0; rowIndex < Constants.STANDARD_ROW_COUNT; rowIndex++)
        {
            var row = new BoardRow(new BoardCell[Constants.STANDARD_COL_COUNT]);
            for (int colIndex = 0; colIndex < Constants.STANDARD_COL_COUNT; colIndex++)
            {
                row.Cells[colIndex] = new BoardCell(gridValues[rowIndex * Constants.STANDARD_COL_COUNT + colIndex].ToString());
            }
            rows.Add(row);
        }
        return rows;
    }

    private static List<BoardRow> TransformColumnsToRows(string[,] columns)
    {
        var rows = new List<BoardRow>();

        for (int rowIndex = 0; rowIndex < Constants.STANDARD_ROW_COUNT; rowIndex++)
        {
            var boardRow = new BoardRow(new BoardCell[Constants.STANDARD_COL_COUNT]);
            for (int columnIndex = 0; columnIndex < Constants.STANDARD_COL_COUNT; columnIndex++)
            {
                boardRow.Cells[columnIndex] = new BoardCell(columns[columnIndex, rowIndex]);
            }
            rows.Add(boardRow);
        }

        return rows;
    }

}
