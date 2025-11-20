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

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "easter", DisplayName = "2016.06.1-example")]
    [DataRow(1, false, "kqsdmzft", DisplayName = "2016.06.1-problem")]
    [DataRow(2, true, "advent", DisplayName = "2016.06.2-example")]
    [DataRow(2, false, "tpooccyo", DisplayName = "2016.06.2-problem")]
    public void Solve_2016_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "2", DisplayName = "2016.07.1-example")]
    [DataRow(1, false, "105", DisplayName = "2016.07.1-problem")]
    [DataRow(2, true, "3", DisplayName = "2016.07.2-example")]
    [DataRow(2, false, "258", DisplayName = "2016.07.2-problem")]
    public void Solve_2016_07(int part, bool useExample, string expected) =>
        Solve(new Puzzle07Solver(), 7, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "6", DisplayName = "2016.08.1-example")]
    [DataRow(1, false, "116", DisplayName = "2016.08.1-problem")]
    [DataRow(2, false, "UPOJFLBCEZ", DisplayName = "2016.08.2-problem")]
    public void Solve_2016_08(int part, bool useExample, string expected) =>
        Solve(new Puzzle08Solver(), 8, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "18", DisplayName = "2016.09.1-example")]
    [DataRow(1, false, "183269", DisplayName = "2016.09.1-problem")]
    [DataRow(2, true, "241920", DisplayName = "2016.09.2-example")]
    [DataRow(2, false, "11317278863", DisplayName = "2016.09.2-problem")]
    public void Solve_2016_09(int part, bool useExample, string expected) =>
        Solve(new Puzzle09Solver(), 9, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "2", DisplayName = "2016.10.1-example")]
    [DataRow(1, false, "47", DisplayName = "2016.10.1-problem")]
    [DataRow(2, true, "30", DisplayName = "2016.10.2-example")]
    [DataRow(2, false, "2666", DisplayName = "2016.10.2-problem")]
    public void Solve_2016_10(int part, bool useExample, string expected) =>
        Solve(new Puzzle10Solver(), 10, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "11", DisplayName = "2016.11.1-example")]
    [DataRow(1, false, "33", DisplayName = "2016.11.1-problem")]
    [DataRow(2, false, "57", DisplayName = "2016.11.2-problem")]
    public void Solve_2016_11(int part, bool useExample, string expected) =>
        Solve(new Puzzle11Solver(), 11, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "42", DisplayName = "2016.12.1-example")]
    [DataRow(1, false, "318117", DisplayName = "2016.12.1-problem")]
    [DataRow(2, false, "9227771", DisplayName = "2016.12.2-problem")]
    public void Solve_2016_12(int part, bool useExample, string expected) =>
        Solve(new Puzzle12Solver(), 12, part, useExample, expected);
}
