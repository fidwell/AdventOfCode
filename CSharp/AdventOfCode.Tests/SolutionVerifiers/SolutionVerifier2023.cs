using AdventOfCode.Core.PuzzleSolvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2023 : SolutionVerifier
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "142", DisplayName = "part 1 example")]
    [DataRow(1, false, "54632", DisplayName = "part 1 real")]
    [DataRow(2, true, "281", DisplayName = "part 2 example")]
    [DataRow(2, false, "54019", DisplayName = "part 2 real")]
    public void Solve_2023_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 2023, 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "8", DisplayName = "part 1 example")]
    [DataRow(1, false, "2348", DisplayName = "part 1 real")]
    [DataRow(2, true, "2286", DisplayName = "part 2 example")]
    [DataRow(2, false, "76008", DisplayName = "part 2 real")]
    public void Solve_2023_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2023, 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "4361", DisplayName = "part 1 example")]
    [DataRow(1, false, "540131", DisplayName = "part 1 real")]
    [DataRow(2, true, "467835", DisplayName = "part 2 example")]
    [DataRow(2, false, "86879020", DisplayName = "part 2 real")]
    public void Solve_2023_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 2023, 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "13", DisplayName = "part 1 example")]
    [DataRow(1, false, "26218", DisplayName = "part 1 real")]
    [DataRow(2, true, "30", DisplayName = "part 2 example")]
    [DataRow(2, false, "9997537", DisplayName = "part 2 real")]
    public void Solve_2023_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 2023, 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "35", DisplayName = "part 1 example")]
    [DataRow(1, false, "323142486", DisplayName = "part 1 real")]
    [DataRow(2, true, "46", DisplayName = "part 2 example")]
    [DataRow(2, false, "79874951", DisplayName = "part 2 real")]
    public void Solve_2023_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 2023, 5, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "288", DisplayName = "part 1 example")]
    [DataRow(1, false, "512295", DisplayName = "part 1 real")]
    [DataRow(2, true, "71503", DisplayName = "part 2 example")]
    [DataRow(2, false, "36530883", DisplayName = "part 2 real")]
    public void Solve_2023_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 2023, 6, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "6440", DisplayName = "part 1 example")]
    [DataRow(1, false, "254024898", DisplayName = "part 1 real")]
    [DataRow(2, true, "5905", DisplayName = "part 2 example")]
    [DataRow(2, false, "254115617", DisplayName = "part 2 real")]
    public void Solve_2023_07(int part, bool useExample, string expected) =>
        Solve(new Puzzle07Solver(), 2023, 7, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "6", DisplayName = "part 1 example")]
    [DataRow(1, false, "12737", DisplayName = "part 1 real")]
    [DataRow(2, true, "6", DisplayName = "part 2 example")]
    [DataRow(2, false, "9064949303801", DisplayName = "part 2 real")]
    public void Solve_2023_08(int part, bool useExample, string expected) =>
        Solve(new Puzzle08Solver(), 2023, 8, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "114", DisplayName = "part 1 example")]
    [DataRow(1, false, "1992273652", DisplayName = "part 1 real")]
    [DataRow(2, true, "2", DisplayName = "part 2 example")]
    [DataRow(2, false, "1012", DisplayName = "part 2 real")]
    public void Solve_2023_09(int part, bool useExample, string expected) =>
        Solve(new Puzzle09Solver(), 2023, 9, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "8", DisplayName = "part 1 example")]
    [DataRow(1, false, "6773", DisplayName = "part 1 real")]
    [DataRow(2, true, "10", DisplayName = "part 2 example")]
    [DataRow(2, false, "493", DisplayName = "part 2 real")]
    public void Solve_2023_10(int part, bool useExample, string expected) =>
        Solve(new Puzzle10Solver(), 2023, 10, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "374", DisplayName = "part 1 example")]
    [DataRow(1, false, "9599070", DisplayName = "part 1 real")]
    [DataRow(2, true, "82000210", DisplayName = "part 2 example")]
    [DataRow(2, false, "842645913794", DisplayName = "part 2 real")]
    public void Solve_2023_11(int part, bool useExample, string expected) =>
        Solve(new Puzzle11Solver(), 2023, 11, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "21", DisplayName = "part 1 example")]
    [DataRow(1, false, "7017", DisplayName = "part 1 real")]
    [DataRow(2, true, "525152", DisplayName = "part 2 example")]
    [DataRow(2, false, "527570479489", DisplayName = "part 2 real")]
    public void Solve_2023_12(int part, bool useExample, string expected) =>
        Solve(new Puzzle12Solver(), 2023, 12, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "405", DisplayName = "part 1 example")]
    [DataRow(1, false, "26957", DisplayName = "part 1 real")]
    [DataRow(2, true, "400", DisplayName = "part 2 example")]
    [DataRow(2, false, "42695", DisplayName = "part 2 real")]
    public void Solve_2023_13(int part, bool useExample, string expected) =>
        Solve(new Puzzle13Solver(), 2023, 13, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "136", DisplayName = "part 1 example")]
    [DataRow(1, false, "113525", DisplayName = "part 1 real")]
    [DataRow(2, true, "64", DisplayName = "part 2 example")]
    [DataRow(2, false, "101292", DisplayName = "part 2 real")]
    public void Solve_2023_14(int part, bool useExample, string expected) =>
        Solve(new Puzzle14Solver(), 2023, 14, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "1320", DisplayName = "part 1 example")]
    [DataRow(1, false, "516657", DisplayName = "part 1 real")]
    [DataRow(2, true, "145", DisplayName = "part 2 example")]
    [DataRow(2, false, "210906", DisplayName = "part 2 real")]
    public void Solve_2023_15(int part, bool useExample, string expected) =>
        Solve(new Puzzle15Solver(), 2023, 15, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "46", DisplayName = "part 1 example")]
    [DataRow(1, false, "7185", DisplayName = "part 1 real")]
    [DataRow(2, true, "51", DisplayName = "part 2 example")]
    [DataRow(2, false, "7616", DisplayName = "part 2 real")]
    public void Solve_2023_16(int part, bool useExample, string expected) =>
        Solve(new Puzzle16Solver(), 2023, 16, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "102", DisplayName = "part 1 example")]
    [DataRow(1, false, "785", DisplayName = "part 1 real")]
    [DataRow(2, true, "94", DisplayName = "part 2 example")]
    [DataRow(2, false, "922", DisplayName = "part 2 real")]
    public void Solve_2023_17(int part, bool useExample, string expected) =>
        Solve(new Puzzle17Solver(), 2023, 17, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "62", DisplayName = "part 1 example")]
    [DataRow(1, false, "58550", DisplayName = "part 1 real")]
    [DataRow(2, true, "952408144115", DisplayName = "part 2 example")]
    [DataRow(2, false, "47452118468566", DisplayName = "part 2 real")]
    public void Solve_2023_18(int part, bool useExample, string expected) =>
        Solve(new Puzzle18Solver(), 2023, 18, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "19114", DisplayName = "part 1 example")]
    [DataRow(1, false, "432434", DisplayName = "part 1 real")]
    [DataRow(2, true, "167409079868000", DisplayName = "part 2 example")]
    [DataRow(2, false, "132557544578569", DisplayName = "part 2 real")]
    public void Solve_2023_19(int part, bool useExample, string expected) =>
        Solve(new Puzzle19Solver(), 2023, 19, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "11687500", DisplayName = "part 1 example")]
    [DataRow(1, false, "684125385", DisplayName = "part 1 real")]
    [DataRow(2, true, "1", DisplayName = "part 2 example")]
    [DataRow(2, false, "225872806380073", DisplayName = "part 2 real")]
    public void Solve_2023_20(int part, bool useExample, string expected) =>
        Solve(new Puzzle20Solver(), 2023, 20, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "16", DisplayName = "part 1 example")]
    [DataRow(1, false, "3740", DisplayName = "part 1 real")]
    [DataRow(2, true, "6536", DisplayName = "part 2 example")]
    [DataRow(2, false, "620962518745459", DisplayName = "part 2 real")]
    public void Solve_2023_21(int part, bool useExample, string expected) =>
        Solve(new Puzzle21Solver(), 2023, 21, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "5", DisplayName = "part 1 example")]
    [DataRow(1, false, "437", DisplayName = "part 1 real")]
    [DataRow(2, true, "7", DisplayName = "part 2 example")]
    [DataRow(2, false, "42561", DisplayName = "part 2 real")]
    public void Solve_2023_22(int part, bool useExample, string expected) =>
        Solve(new Puzzle22Solver(), 2023, 22, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "_", DisplayName = "part 1 example")]
    [DataRow(1, false, "_", DisplayName = "part 1 real")]
    [DataRow(2, true, "_", DisplayName = "part 2 example")]
    [DataRow(2, false, "_", DisplayName = "part 2 real")]
    public void Solve_2023_23(int part, bool useExample, string expected) =>
        Solve(new Puzzle23Solver(), 2023, 23, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "_", DisplayName = "part 1 example")]
    [DataRow(1, false, "_", DisplayName = "part 1 real")]
    [DataRow(2, true, "_", DisplayName = "part 2 example")]
    [DataRow(2, false, "_", DisplayName = "part 2 real")]
    public void Solve_2023_24(int part, bool useExample, string expected) =>
        Solve(new Puzzle24Solver(), 2023, 24, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "_", DisplayName = "part 1 example")]
    [DataRow(1, false, "_", DisplayName = "part 1 real")]
    [DataRow(2, true, "_", DisplayName = "part 2 example")]
    [DataRow(2, false, "_", DisplayName = "part 2 real")]
    public void Solve_2023_25(int part, bool useExample, string expected) =>
        Solve(new Puzzle25Solver(), 2023, 25, part, useExample, expected);
}
