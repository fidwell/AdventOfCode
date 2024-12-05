using AdventOfCode.Solvers._2024;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2024 : SolutionVerifier
{
    public SolutionVerifier2024() : base(2024) { }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "11", DisplayName = "2024.01.1-example")]
    [DataRow(1, false, "2375403", DisplayName = "2024.01.1-problem")]
    [DataRow(2, true, "31", DisplayName = "2024.01.2-example")]
    [DataRow(2, false, "23082277", DisplayName = "2024.01.2-problem")]
    public void Solve_2024_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "2", DisplayName = "2024.02.1-example")]
    [DataRow(1, false, "432", DisplayName = "2024.02.1-problem")]
    [DataRow(2, true, "4", DisplayName = "2024.02.2-example")]
    [DataRow(2, false, "488", DisplayName = "2024.02.2-problem")]
    public void Solve_2024_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "161", DisplayName = "2024.03.1-example")]
    [DataRow(1, false, "173785482", DisplayName = "2024.03.1-problem")]
    [DataRow(2, true, "48", DisplayName = "2024.03.2-example")]
    [DataRow(2, false, "83158140", DisplayName = "2024.03.2-problem")]
    public void Solve_2024_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "18", DisplayName = "2024.04.1-example")]
    [DataRow(1, false, "2524", DisplayName = "2024.04.1-problem")]
    [DataRow(2, true, "9", DisplayName = "2024.04.2-example")]
    [DataRow(2, false, "1873", DisplayName = "2024.04.2-problem")]
    public void Solve_2024_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "143", DisplayName = "2024.05.1-example")]
    [DataRow(1, false, "5108", DisplayName = "2024.05.1-problem")]
    [DataRow(2, true, "123", DisplayName = "2024.05.2-example")]
    [DataRow(2, false, "7380", DisplayName = "2024.05.2-problem")]
    public void Solve_2024_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);
}
