using BingoEngine.Core.Patterns;
using BingoEngine.Core;

namespace BingoEngine.Tests.Patterns;

[TestClass]
public class LetterXPatternTests
{
    // B       I       N       G       O
    // 13      17      43      59      73
    // 2       20      35      47      71
    // 5       23      39      57      62
    // 8       24      45      55      72
    // 4       29      42      54      65
    private const string TEST_BOARD_CODE = "l6TlfeUwXfr4fEsQibcZbHnrU4HGFxIebUNKiLCYtrrf89TXKsoujUzhjeSpE";
    private const string TEST_SESSION_CODE = "Test";
    private IBingoPattern testPattern = new LetterXPattern();

    [TestMethod]
    public void NoWinConditionWithNoNumbersCalled()
    {
        var numbersCalled = Array.Empty<int>();
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNull(winCondition);
    }

    [TestMethod]
    public void NoWinConditionWithInsufficientNumbersCalled()
    {
        var numbersCalled = new int[] { 13, 20, 55, 65, 4, 24, 47 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNull(winCondition);
    }

    [TestMethod]
    public void NoWinConditionWithIncorrectNumbersCalled()
    {
        var numbersCalled = new int[] { 14, 21, 56, 65, 4, 24, 47 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNull(winCondition);
    }

    [TestMethod]
    public void WinCondition01LetterX()
    {
        var numbersCalled = new int[] { 13, 20, 55, 65, 4, 24, 47, 73 };

        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Both diagonals from corner to corner, forming the letter X", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }
}
