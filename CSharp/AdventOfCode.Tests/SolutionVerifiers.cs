using AdventOfCode.Core.PuzzleSolvers;
using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests;

[TestClass]
public class SolutionVerifiers
{
    const int Timeout = 3000;

    private readonly IPuzzleSolver[] _solvers;

    public SolutionVerifiers()
    {
        _solvers = [
            new Puzzle01Solver(),
            //new Puzzle02Solver(),
            //new Puzzle03Solver(),
            //new Puzzle04Solver(),
            //new Puzzle05Solver(),
            //new Puzzle06Solver(),
            //new Puzzle07Solver(),
            //new Puzzle08Solver(),
            //new Puzzle09Solver(),
            //new Puzzle10Solver(),
            //new Puzzle11Solver(),
            //new Puzzle12Solver(),
            //new Puzzle13Solver(),
            //new Puzzle14Solver(),
            //new Puzzle15Solver(),
            //new Puzzle16Solver(),
            //new Puzzle17Solver(),
            //new Puzzle18Solver(),
            //new Puzzle19Solver(),
            //new Puzzle20Solver(),
            //new Puzzle21Solver(),
            //new Puzzle22Solver(),
            //new Puzzle23Solver(),
            //new Puzzle24Solver(),
            //new Puzzle25Solver()
        ];
    }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, 1, true, "142")]
    [DataRow(1, 1, false, "54632")]
    [DataRow(1, 2, true, "281")]
    [DataRow(1, 2, false, "54019")]
    public void Solve(int puzzle, int part, bool useSample, string expected)
    {
        if (DateTime.Today < new DateTime(2023, 12, puzzle))
            return;

        var input = DataReader.GetData(puzzle, part, useSample);
        var solver = _solvers[puzzle - 1];
        var result = part == 1
            ? solver.SolvePartOne(input)
            : solver.SolvePartTwo(input);
        Assert.AreEqual(expected, result);
    }

    /*

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "8")]
    [DataRow(false, "2348")]
    public void Puzzle02_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle02Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "2286")]
    [DataRow(false, "76008")]
    public void Puzzle02_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle02Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "4361")]
    [DataRow(false, "540131")]
    public void Puzzle03_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle03Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "467835")]
    [DataRow(false, "86879020")]
    public void Puzzle03_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle03Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "13")]
    [DataRow(false, "26218")]
    public void Puzzle04_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle04Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "30")]
    [DataRow(false, "9997537")]
    public void Puzzle04_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle04Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "35")]
    [DataRow(false, "323142486")]
    public void Puzzle05_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle05Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "46")]
    [DataRow(false, "79874951")]
    public void Puzzle05_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle05Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "288")]
    [DataRow(false, "512295")]
    public void Puzzle06_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle06Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "71503")]
    [DataRow(false, "36530883")]
    public void Puzzle06_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle06Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "6440")]
    [DataRow(false, "254024898")]
    public void Puzzle07_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle07Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "5905")]
    [DataRow(false, "254115617")]
    public void Puzzle07_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle07Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "6")]
    [DataRow(false, "12737")]
    public void Puzzle08_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle08Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "6")]
    [DataRow(false, "9064949303801")]
    public void Puzzle08_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle08Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "114")]
    [DataRow(false, "1992273652")]
    public void Puzzle09_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle09Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "2")]
    [DataRow(false, "1012")]
    public void Puzzle09_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle09Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "8")]
    [DataRow(false, "6773")]
    public void Puzzle10_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle10Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "10")]
    [DataRow(false, "493")]
    public void Puzzle10_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle10Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "374")]
    [DataRow(false, "9599070")]
    public void Puzzle11_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle11Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "82000210")]
    [DataRow(false, "842645913794")]
    public void Puzzle11_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle11Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "21")]
    [DataRow(false, "7017")]
    public void Puzzle12_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle12Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "525152")]
    [DataRow(false, "527570479489")]
    public void Puzzle12_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle12Solver().SolvePartTwo(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "405")]
    [DataRow(false, "26957")]
    public void Puzzle13_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle13Solver().SolvePartOne(useSample));

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(true, "400")]
    [DataRow(false, "42695")]
    public void Puzzle13_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle13Solver().SolvePartTwo(useSample));

    */
}
