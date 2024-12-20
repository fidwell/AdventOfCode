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

    [DataTestMethod, Timeout(LongTimeout)]
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

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "cqjxxyzz", DisplayName = "2015.11.1-problem")]
    [DataRow(2, false, "cqkaabcc", DisplayName = "2015.11.2-problem")]
    public void Solve_2015_11(int part, bool useExample, string expected) =>
        Solve(new Puzzle11Solver(), 11, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "111754", DisplayName = "2015.12.1-problem")]
    [DataRow(2, false, "65402", DisplayName = "2015.12.2-problem")]
    public void Solve_2015_12(int part, bool useExample, string expected) =>
        Solve(new Puzzle12Solver(), 12, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "330", DisplayName = "2015.13.1-example")]
    [DataRow(1, false, "733", DisplayName = "2015.13.1-problem")]
    [DataRow(2, false, "725", DisplayName = "2015.13.2-problem")]
    public void Solve_2015_13(int part, bool useExample, string expected) =>
        Solve(new Puzzle13Solver(), 13, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "2660", DisplayName = "2015.14.1-problem")]
    [DataRow(2, false, "1256", DisplayName = "2015.14.2-problem")]
    public void Solve_2015_14(int part, bool useExample, string expected) =>
        Solve(new Puzzle14Solver(), 14, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "62842880", DisplayName = "2015.15.1-example")]
    [DataRow(1, false, "222870", DisplayName = "2015.15.1-problem")]
    [DataRow(2, true, "57600000", DisplayName = "2015.15.2-example")]
    [DataRow(2, false, "117936", DisplayName = "2015.15.2-problem")]
    public void Solve_2015_15(int part, bool useExample, string expected) =>
        Solve(new Puzzle15Solver(), 15, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, false, "103", DisplayName = "2015.16.1-problem")]
    [DataRow(2, false, "405", DisplayName = "2015.16.2-problem")]
    public void Solve_2015_16(int part, bool useExample, string expected) =>
        Solve(new Puzzle16Solver(), 16, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "4", DisplayName = "2015.17.1-example")]
    [DataRow(1, false, "4372", DisplayName = "2015.17.1-problem")]
    [DataRow(2, true, "3", DisplayName = "2015.17.2-example")]
    [DataRow(2, false, "4", DisplayName = "2015.17.2-problem")]
    public void Solve_2015_17(int part, bool useExample, string expected) =>
        Solve(new Puzzle17Solver(), 17, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "4", DisplayName = "2015.18.1-example")]
    [DataRow(1, false, "768", DisplayName = "2015.18.1-problem")]
    [DataRow(2, true, "17", DisplayName = "2015.18.2-example")]
    [DataRow(2, false, "781", DisplayName = "2015.18.2-problem")]
    public void Solve_2015_18(int part, bool useExample, string expected) =>
        Solve(new Puzzle18Solver(), 18, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "7", DisplayName = "2015.19.1-example")]
    [DataRow(1, false, "509", DisplayName = "2015.19.1-problem")]
    [DataRow(2, true, "6", DisplayName = "2015.19.2-example")]
    [DataRow(2, false, "195", DisplayName = "2015.19.2-problem")]
    public void Solve_2015_19(int part, bool useExample, string expected) =>
        Solve(new Puzzle19Solver(), 19, part, useExample, expected);
}
