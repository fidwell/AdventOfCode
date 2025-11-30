using AdventOfCode.Solvers._2025;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2025() : SolutionVerifier(2025)
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "", DisplayName = "2025.01.1-example")]
    [DataRow(1, false, "", DisplayName = "2025.01.1-problem")]
    [DataRow(2, true, "", DisplayName = "2025.01.2-example")]
    [DataRow(2, false, "", DisplayName = "2025.01.2-problem")]
    public void Solve_2025_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);
}
