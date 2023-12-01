using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests;

[TestClass]
public class SolutionVerifiers
{
    [TestMethod]
    public void Puzzle01_Part1_Sample()
        => Assert.AreEqual("142", new Puzzle01Solver(1).Solve(useSample: true));

    [TestMethod]
    public void Puzzle01_Part1()
        => Assert.AreEqual("54632", new Puzzle01Solver(1).Solve());

    [TestMethod]
    public void Puzzle01_Part2_Sample()
        => Assert.AreEqual("281", new Puzzle01Solver(2).Solve(useSample: true));

    [TestMethod]
    public void Puzzle01_Part2()
        => Assert.AreEqual("54019", new Puzzle01Solver(2).Solve());
}
