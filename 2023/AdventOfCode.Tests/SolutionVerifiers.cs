using AdventOfCode.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests;

[TestClass]
public class SolutionVerifiers
{
    [TestMethod]
    public void Puzzle01Sample()
        => Assert.AreEqual("142", new Puzzle01Solver().Solve(useSample: true));

    [TestMethod]
    public void Puzzle01()
        => Assert.AreEqual("54632", new Puzzle01Solver().Solve());
}
