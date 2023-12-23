using AdventOfCode.Core.PuzzleSolvers._2022;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2022 : SolutionVerifier
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "24000", DisplayName = "part 1 example")]
    [DataRow(1, false, "71471", DisplayName = "part 1 real")]
    [DataRow(2, true, "45000", DisplayName = "part 2 example")]
    [DataRow(2, false, "211189", DisplayName = "part 2 real")]
    public void Solve_2022_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 2022, 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "15", DisplayName = "part 1 example")]
    [DataRow(1, false, "11841", DisplayName = "part 1 real")]
    [DataRow(2, true, "12", DisplayName = "part 2 example")]
    [DataRow(2, false, "13022", DisplayName = "part 2 real")]
    public void Solve_2022_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2022, 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "157", DisplayName = "part 1 example")]
    [DataRow(1, false, "7553", DisplayName = "part 1 real")]
    [DataRow(2, true, "70", DisplayName = "part 2 example")]
    [DataRow(2, false, "2758", DisplayName = "part 2 real")]
    public void Solve_2022_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 2022, 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "2", DisplayName = "part 1 example")]
    [DataRow(1, false, "536", DisplayName = "part 1 real")]
    [DataRow(2, true, "4", DisplayName = "part 2 example")]
    [DataRow(2, false, "845", DisplayName = "part 2 real")]
    public void Solve_2022_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 2022, 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "CMZ", DisplayName = "part 1 example")]
    [DataRow(1, false, "CVCWCRTVQ", DisplayName = "part 1 real")]
    [DataRow(2, true, "MCD", DisplayName = "part 2 example")]
    [DataRow(2, false, "CNSCZWLVT", DisplayName = "part 2 real")]
    public void Solve_2022_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 2022, 5, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "7", DisplayName = "part 1 example")]
    [DataRow(1, false, "1623", DisplayName = "part 1 real")]
    [DataRow(2, true, "19", DisplayName = "part 2 example")]
    [DataRow(2, false, "3774", DisplayName = "part 2 real")]
    public void Solve_2022_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 2022, 6, part, useExample, expected);
}
