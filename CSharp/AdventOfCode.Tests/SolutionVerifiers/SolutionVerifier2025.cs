using AdventOfCode.Solvers._2025;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2025() : SolutionVerifier(2025)
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "3", DisplayName = "2025.01.1-example")]
    [DataRow(1, false, "1132", DisplayName = "2025.01.1-problem")]
    [DataRow(2, true, "6", DisplayName = "2025.01.2-example")]
    [DataRow(2, false, "6623", DisplayName = "2025.01.2-problem")]
    public void Solve_2025_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "1227775554", DisplayName = "2025.02.1-example")]
    [DataRow(1, false, "31000881061", DisplayName = "2025.02.1-problem")]
    [DataRow(2, true, "4174379265", DisplayName = "2025.02.2-example")]
    [DataRow(2, false, "46769308485", DisplayName = "2025.02.2-problem")]
    public void Solve_2025_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "357", DisplayName = "2025.03.1-example")]
    [DataRow(1, false, "16858", DisplayName = "2025.03.1-problem")]
    [DataRow(2, true, "3121910778619", DisplayName = "2025.03.2-example")]
    [DataRow(2, false, "167549941654721", DisplayName = "2025.03.2-problem")]
    public void Solve_2025_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "13", DisplayName = "2025.04.1-example")]
    [DataRow(1, false, "1505", DisplayName = "2025.04.1-problem")]
    [DataRow(2, true, "43", DisplayName = "2025.04.2-example")]
    [DataRow(2, false, "9182", DisplayName = "2025.04.2-problem")]
    public void Solve_2025_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);
}
