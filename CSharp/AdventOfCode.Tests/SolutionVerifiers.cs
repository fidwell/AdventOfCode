using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests;

[TestClass]
public class SolutionVerifiers
{
    [TestMethod]
    public void Puzzle01_Part1_Sample()
        => Assert.AreEqual("142", new Puzzle01Solver().SolvePartOne(useSample: true));

    [TestMethod]
    public void Puzzle01_Part1()
        => Assert.AreEqual("54632", new Puzzle01Solver().SolvePartOne());

    [TestMethod]
    public void Puzzle01_Part2_Sample()
        => Assert.AreEqual("281", new Puzzle01Solver().SolvePartTwo(useSample: true));

    [TestMethod]
    public void Puzzle01_Part2()
        => Assert.AreEqual("54019", new Puzzle01Solver().SolvePartTwo());


    [TestMethod]
    public void Puzzle02_Part1_Sample()
        => Assert.AreEqual("8", new Puzzle02Solver().SolvePartOne(useSample: true));

    [TestMethod]
    public void Puzzle02_Part1()
        => Assert.AreEqual("2348", new Puzzle02Solver().SolvePartOne());

    [TestMethod]
    public void Puzzle02_Part2_Sample()
        => Assert.AreEqual("2286", new Puzzle02Solver().SolvePartTwo(useSample: true));

    [TestMethod]
    public void Puzzle02_Part2()
        => Assert.AreEqual("76008", new Puzzle02Solver().SolvePartTwo());


    [TestMethod]
    public void Puzzle03_Part1_Sample()
        => Assert.AreEqual("4361", new Puzzle03Solver().SolvePartOne(useSample: true));

    [TestMethod]
    public void Puzzle03_Part1()
        => Assert.AreEqual("540131", new Puzzle03Solver().SolvePartOne());

    [TestMethod]
    public void Puzzle03_Part2_Sample()
        => Assert.AreEqual("467835", new Puzzle03Solver().SolvePartTwo(useSample: true));

    [TestMethod]
    public void Puzzle03_Part2()
        => Assert.AreEqual("86879020", new Puzzle03Solver().SolvePartTwo());
}
