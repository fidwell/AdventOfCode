using AdventOfCode.Solvers._2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2015 : SolutionVerifier
{
    public SolutionVerifier2015() : base(2015) { }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "232", DisplayName = "2015.01.1-problem")]
    [DataRow(2, false, "1783", DisplayName = "2015.01.2-problem")]
    public void Solve_2015_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "1588178", DisplayName = "2015.02.1-problem")]
    [DataRow(2, false, "3783758", DisplayName = "2015.02.2-problem")]
    public void Solve_2015_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "2565", DisplayName = "2015.03.1-problem")]
    [DataRow(2, false, "2639", DisplayName = "2015.03.2-problem")]
    public void Solve_2015_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(5000)]
    [DataRow(1, false, "282749", DisplayName = "2015.04.1-problem")]
    [DataRow(2, false, "9962624", DisplayName = "2015.04.2-problem")]
    public void Solve_2015_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(2, true, "2", DisplayName = "2015.05.2-example")]
    [DataRow(1, false, "238", DisplayName = "2015.05.1-problem")]
    [DataRow(2, false, "69", DisplayName = "2015.05.2-problem")]
    public void Solve_2015_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "569999", DisplayName = "2015.06.1-problem")]
    [DataRow(2, false, "17836115", DisplayName = "2015.06.2-problem")]
    public void Solve_2015_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "16076", DisplayName = "2015.07.1-problem")]
    [DataRow(2, false, "2797", DisplayName = "2015.07.2-problem")]
    public void Solve_2015_07(int part, bool useExample, string expected) =>
        Solve(new Puzzle07Solver(), 7, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "12", DisplayName = "2015.08.1-example")]
    [DataRow(1, false, "1342", DisplayName = "2015.08.1-problem")]
    [DataRow(2, true, "19", DisplayName = "2015.08.2-example")]
    [DataRow(2, false, "2074", DisplayName = "2015.08.2-problem")]
    public void Solve_2015_08(int part, bool useExample, string expected) =>
        Solve(new Puzzle08Solver(), 8, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "605", DisplayName = "2015.09.1-example")]
    [DataRow(1, false, "141", DisplayName = "2015.09.1-problem")]
    [DataRow(2, true, "982", DisplayName = "2015.09.2-example")]
    [DataRow(2, false, "736", DisplayName = "2015.09.2-problem")]
    public void Solve_2015_09(int part, bool useExample, string expected) =>
        Solve(new Puzzle09Solver(), 9, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "360154", DisplayName = "2015.10.1-problem")]
    [DataRow(2, false, "5103798", DisplayName = "2015.10.2-problem")]
    public void Solve_2015_10(int part, bool useExample, string expected) =>
        Solve(new Puzzle10Solver(), 10, part, useExample, expected);
}
