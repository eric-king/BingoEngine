using BingoEngine.Core;
using BingoEngine.Core.Boards;
using BingoEngine.Core.Patterns;
using System.Reflection;
using System.Text;

// set up the console so it can display the
// symbols in the board pictograph 
Console.OutputEncoding = Encoding.UTF8;

// set up the game session
var sessionCode = "CONSOLE_SESSION_CODE";
bool shouldContinue = true;
List<int> numbersCalled = new();
var random = new Random();

// generate a new random board and select the BINGO pattern
var board = new StandardBoard(sessionCode);
IBingoPattern[] patterns = GetPatterns();
IBingoPattern selectedPattern = SelectPattern();

// display the board for the user
Console.WriteLine();
Console.WriteLine("Generating random BINGO board:");
DisplayBoard(board, selectedPattern, numbersCalled);

Play();

void Play() 
{
    // the game loop
    do
    {
        Console.WriteLine();
        Console.WriteLine("Press D to draw a number, or any other key to quit");
        var choice = Console.ReadKey().KeyChar;

        Action action = choice switch
        {
            'D' or 'd' => DrawANumber,
            _ => Quit
        };

        action();
    }
    while (shouldContinue);

}

IBingoPattern[] GetPatterns()
{
    // dynamically parse all of the classes that implement 
    // the IBingoPattern interface
    var patternInterface = typeof(IBingoPattern);
    var assembly = Assembly.GetAssembly(patternInterface);

    // this shouldn't ever be null, but handled anyway
    if (assembly == null)
    {
        return Array.Empty<IBingoPattern>();
    }

    // exclude the BasePattern, which is an abstract class and cannot be instantiated
    Type[] types = assembly.GetTypes()
        .Where(x => x.GetInterfaces().Contains(patternInterface))
        .Where(x => !x.IsAbstract)
        .ToArray();

    // I don't know how to write this so that we can guarantee to the compiler
    // that the instances won't be null, so I null coalesce to a StandardPattern
    // instance even though I don't think that will ever happen
    var patterns = types.Select(x => Activator.CreateInstance(x) as IBingoPattern ?? new StandardPattern());

    return patterns.ToArray();
}

IBingoPattern SelectPattern()
{
    IBingoPattern selectedPattern = new StandardPattern();
    int selectedPatternIndex = 0;

    // let the player know what the options are
    DisplayPatternOptions();

    // continue asking until we've gotten a valid selection
    do
    {
        var choice = Console.ReadKey().KeyChar.ToString();

        if (int.TryParse(choice, out selectedPatternIndex))
        {
            if (selectedPatternIndex >= 1 && selectedPatternIndex < patterns.Length + 1)
            {
                selectedPattern = patterns[selectedPatternIndex - 1];
                break;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"'{choice}' is not a valid pattern choice. Please enter a number.");
    }
    while (selectedPattern is null);

    // let the player know how they can win
    DisplayWinConditions(selectedPattern);

    return selectedPattern;
}

void DrawANumber()
{
    Console.Clear();
    var value = StandardBoard.Draw(random, numbersCalled);
    var match = board.Grid.Any(x => x.Cells.Any(x => x.Value == value.ToString()));
    numbersCalled.Add(value);

    Console.WriteLine($"Number Called: {value}");
    Console.WriteLine(match ? "It's a match!" : "No match.");
    Console.WriteLine();
    DisplayBoard(board, selectedPattern, numbersCalled);
    Console.WriteLine();
    Console.WriteLine($"{numbersCalled.Count} numbers have been called.");

    CheckWinCondition();
}

void CheckWinCondition()
{
    var winCondition = StandardBoardJudge.Evaluate(sessionCode, board.BoardCode, selectedPattern, numbersCalled.ToArray());
    if (winCondition is not null)
    {
        Console.WriteLine("Congratulations! You've won! You reached BINGO based on this win condition:");
        Console.WriteLine(winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
        Console.WriteLine();
        Console.WriteLine("Press any key to quit.");
        Console.ReadKey();
        Quit();
    }
}

void Quit()
{
    shouldContinue = false;
}

void DisplayBoard(StandardBoard board, IBingoPattern pattern, List<int> numbersCalled)
{
    Console.WriteLine($"Using {pattern.PatternName} Pattern");
    Console.WriteLine();

    // separating all of the columns with a tab character for alignment
    Console.WriteLine(string.Join("\t", board.ColumnLabels));
    foreach (var row in board.Grid)
    {
        foreach (var cell in row.Cells)
        {
            // display called numbers in green, uncalled numbers in regular color
            if (numbersCalled.Contains(int.Parse(cell.Value)))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ResetColor();
            }
            Console.Write(cell.Value + "\t");
        }

        // make sure the color gets reset for the next row
        // or if the last row, for the lines that follow
        Console.ResetColor();
        Console.WriteLine();
    }
}

void DisplayPatternOptions()
{
    Console.WriteLine("Please choose one of these Bingo Patterns by number:");
    for (int i = 0; i < patterns.Length; i++)
    {
        // I want these displayed starting with '1' rather than '0'
        // since that's the way most non-programmers are expecting lists
        // to be numbered, hence the 'i + 1'
        Console.WriteLine($"{i + 1}: {patterns[i].PatternName}");
    }
}

void DisplayWinConditions(IBingoPattern selectedPattern)
{
    Console.WriteLine();
    Console.WriteLine($"We will be playing with the {selectedPattern.PatternName} Pattern.");
    Console.WriteLine("Here are all the possible ways to win:");
    foreach (var winCondition in selectedPattern.WinConditions)
    {
        Console.WriteLine(winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
        Console.WriteLine();
    }
}
