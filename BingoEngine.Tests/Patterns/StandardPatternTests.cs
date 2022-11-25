using BingoEngine.Core;
using BingoEngine.Core.Patterns;

namespace BingoEngine.Tests.Patterns;

[TestClass]
public class StandardPatternTests
{
    // B       I       N       G       O
    // 13      17      43      59      73
    // 2       20      35      47      71
    // 5       23      39      57      62
    // 8       24      45      55      72
    // 4       29      42      54      65
    private const string TEST_BOARD_CODE = "l6TlfeUwXfr4fEsQibcZbHnrU4HGFxIebUNKiLCYtrrf89TXKsoujUzhjeSpE";
    private const string TEST_SESSION_CODE = "Test";
    private IBingoPattern testPattern = new StandardPattern();

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
        var numbersCalled = new int[] { 13, 2, 5, 8 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNull(winCondition);
    }

    [TestMethod]
    public void NoWinConditionWithIncorrectNumbersCalled()
    {
        var numbersCalled = new int[] { 13, 2, 5, 8, 6, 21, 36, 47, 72 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNull(winCondition);
    }

    [TestMethod]
    public void WinCondition01EntireBColumn()
    {
        var numbersCalled = new int[] { 13, 2, 5, 8, 4 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire B column", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition02EntireIColumn()
    {
        var numbersCalled = new int[] { 17, 20, 29, 24, 23 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire I column", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition03EntireNColumn()
    {
        var numbersCalled = new int[] { 9, 43, 42, 35, 39, 45 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire N column", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition04EntireGColumn()
    {
        var numbersCalled = new int[] { 9, 59, 47, 57, 55, 54 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire G column", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition05EntireOColumn()
    {
        var numbersCalled = new int[] { 9, 73, 71, 62, 72, 65 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire O column", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition06EntireFirstRow()
    {
        var numbersCalled = new int[] { 65, 13, 17, 43, 59, 73 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire first row", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition07EntireSecondRow()
    {
        var numbersCalled = new int[] { 65, 2, 20, 35, 47, 71 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire second row", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition08EntireThirdRow()
    {
        var numbersCalled = new int[] { 65, 5, 23, 39, 57, 62 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire third row", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition09EntireFourthRow()
    {
        var numbersCalled = new int[] { 65, 8, 24, 45, 55, 72 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire fourth row", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition10EntireFifthRow()
    {
        var numbersCalled = new int[] { 67, 4, 29, 42, 54, 65 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Entire fifth row", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition11DiagonalTopLeftBottomRight()
    {
        var numbersCalled = new int[] { 67, 13, 20, 39, 55, 65 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Diagonal top left to bottom right", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }

    [TestMethod]
    public void WinCondition12DiagonalTopRightBottomLeft()
    {
        var numbersCalled = new int[] { 67, 73, 47, 39, 24, 4 };
        var winCondition = StandardBoardJudge.Evaluate(TEST_SESSION_CODE, TEST_BOARD_CODE, testPattern, numbersCalled);
        Assert.IsNotNull(winCondition);
        Assert.AreEqual("Diagonal top right to bottom left", winCondition.Description);
        Console.WriteLine(winCondition.ToPictogram());
    }
}