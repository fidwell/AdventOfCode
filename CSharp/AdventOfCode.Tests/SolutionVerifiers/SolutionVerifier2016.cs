using AdventOfCode.Solvers._2016;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2016() : SolutionVerifier(2016)
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "353", DisplayName = "2016.01.1-problem")]
    [DataRow(2, false, "152", DisplayName = "2016.01.2-problem")]
    public void Solve_2016_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "1985", DisplayName = "2016.02.1-example")]
    [DataRow(1, false, "84452", DisplayName = "2016.02.1-problem")]
    [DataRow(2, true, "5DB3", DisplayName = "2016.02.2-example")]
    [DataRow(2, false, "D65C3", DisplayName = "2016.02.2-problem")]
    public void Solve_2016_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "862", DisplayName = "2016.03.1-problem")]
    [DataRow(2, false, "1577", DisplayName = "2016.03.2-problem")]
    public void Solve_2016_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "1514", DisplayName = "2016.04.1-example")]
    [DataRow(1, false, "245102", DisplayName = "2016.04.1-problem")]
    [DataRow(2, false, "324", DisplayName = "2016.04.2-problem")]
    public void Solve_2016_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "18f47a30", DisplayName = "2016.05.1-example")]
    [DataRow(1, false, "1a3099aa", DisplayName = "2016.05.1-problem")]
    [DataRow(2, true, "05ace8e3", DisplayName = "2016.05.2-example")]
    [DataRow(2, false, "694190cd", DisplayName = "2016.05.2-problem")]
    public void Solve_2016_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "easter", DisplayName = "2016.06.1-example")]
    [DataRow(1, false, "kqsdmzft", DisplayName = "2016.06.1-problem")]
    [DataRow(2, true, "advent", DisplayName = "2016.06.2-example")]
    [DataRow(2, false, "tpooccyo", DisplayName = "2016.06.2-problem")]
    public void Solve_2016_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);
}
