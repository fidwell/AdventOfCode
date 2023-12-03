using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests;

[TestClass]
public class SolutionVerifiers
{
    [DataTestMethod]
    [DataRow(true, "142")]
    [DataRow(false, "54632")]
    public void Puzzle01_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle01Solver().SolvePartOne(useSample));

    [DataTestMethod]
    [DataRow(true, "281")]
    [DataRow(false, "54019")]
    public void Puzzle01_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle01Solver().SolvePartTwo(useSample));

    [DataTestMethod]
    [DataRow(true, "8")]
    [DataRow(false, "2348")]
    public void Puzzle02_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle02Solver().SolvePartOne(useSample));

    [DataTestMethod]
    [DataRow(true, "2286")]
    [DataRow(false, "76008")]
    public void Puzzle02_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle02Solver().SolvePartTwo(useSample));

    [DataTestMethod]
    [DataRow(true, "4361")]
    [DataRow(false, "540131")]
    public void Puzzle03_Part1(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle03Solver().SolvePartOne(useSample));

    [DataTestMethod]
    [DataRow(true, "467835")]
    [DataRow(false, "86879020")]
    public void Puzzle03_Part2(bool useSample, string expected)
        => Assert.AreEqual(expected, new Puzzle03Solver().SolvePartTwo(useSample));
}
