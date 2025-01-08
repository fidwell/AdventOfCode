using AdventOfCode.Solvers._2023;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2023() : SolutionVerifier(2023)
{
    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "142", DisplayName = "2023.01.1-example")]
    [DataRow(1, false, "54632", DisplayName = "2023.01.1-problem")]
    [DataRow(2, true, "281", DisplayName = "2023.01.2-example")]
    [DataRow(2, false, "54019", DisplayName = "2023.01.2-problem")]
    public void Solve_2023_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "8", DisplayName = "2023.02.1-example")]
    [DataRow(1, false, "2348", DisplayName = "2023.02.1-problem")]
    [DataRow(2, true, "2286", DisplayName = "2023.02.2-example")]
    [DataRow(2, false, "76008", DisplayName = "2023.02.2-problem")]
    public void Solve_2023_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "4361", DisplayName = "2023.03.1-example")]
    [DataRow(1, false, "540131", DisplayName = "2023.03.1-problem")]
    [DataRow(2, true, "467835", DisplayName = "2023.03.2-example")]
    [DataRow(2, false, "86879020", DisplayName = "2023.03.2-problem")]
    public void Solve_2023_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "13", DisplayName = "2023.04.1-example")]
    [DataRow(1, false, "26218", DisplayName = "2023.04.1-problem")]
    [DataRow(2, true, "30", DisplayName = "2023.04.2-example")]
    [DataRow(2, false, "9997537", DisplayName = "2023.04.2-problem")]
    public void Solve_2023_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "35", DisplayName = "2023.05.1-example")]
    [DataRow(1, false, "323142486", DisplayName = "2023.05.1-problem")]
    [DataRow(2, true, "46", DisplayName = "2023.05.2-example")]
    [DataRow(2, false, "79874951", DisplayName = "2023.05.2-problem")]
    public void Solve_2023_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "288", DisplayName = "2023.06.1-example")]
    [DataRow(1, false, "512295", DisplayName = "2023.06.1-problem")]
    [DataRow(2, true, "71503", DisplayName = "2023.06.2-example")]
    [DataRow(2, false, "36530883", DisplayName = "2023.06.2-problem")]
    public void Solve_2023_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "6440", DisplayName = "2023.07.1-example")]
    [DataRow(1, false, "254024898", DisplayName = "2023.07.1-problem")]
    [DataRow(2, true, "5905", DisplayName = "2023.07.2-example")]
    [DataRow(2, false, "254115617", DisplayName = "2023.07.2-problem")]
    public void Solve_2023_07(int part, bool useExample, string expected) =>
        Solve(new Puzzle07Solver(), 7, part, useExample, expected);

    [DataTestMethod, Timeout(LongTimeout)]
    [DataRow(1, true, "6", DisplayName = "2023.08.1-example")]
    [DataRow(1, false, "12737", DisplayName = "2023.08.1-problem")]
    [DataRow(2, true, "6", DisplayName = "2023.08.2-example")]
    [DataRow(2, false, "9064949303801", DisplayName = "2023.08.2-problem")]
    public void Solve_2023_08(int part, bool useExample, string expected) =>
        Solve(new Puzzle08Solver(), 8, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "114", DisplayName = "2023.09.1-example")]
    [DataRow(1, false, "1992273652", DisplayName = "2023.09.1-problem")]
    [DataRow(2, true, "2", DisplayName = "2023.09.2-example")]
    [DataRow(2, false, "1012", DisplayName = "2023.09.2-problem")]
    public void Solve_2023_09(int part, bool useExample, string expected) =>
        Solve(new Puzzle09Solver(), 9, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "8", DisplayName = "2023.10.1-example")]
    [DataRow(1, false, "6773", DisplayName = "2023.10.1-problem")]
    [DataRow(2, true, "10", DisplayName = "2023.10.2-example")]
    [DataRow(2, false, "493", DisplayName = "2023.10.2-problem")]
    public void Solve_2023_10(int part, bool useExample, string expected) =>
        Solve(new Puzzle10Solver(), 10, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "374", DisplayName = "2023.11.1-example")]
    [DataRow(1, false, "9599070", DisplayName = "2023.11.1-problem")]
    [DataRow(2, true, "82000210", DisplayName = "2023.11.2-example")]
    [DataRow(2, false, "842645913794", DisplayName = "2023.11.2-problem")]
    public void Solve_2023_11(int part, bool useExample, string expected) =>
        Solve(new Puzzle11Solver(), 11, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "21", DisplayName = "2023.12.1-example")]
    [DataRow(1, false, "7017", DisplayName = "2023.12.1-problem")]
    [DataRow(2, true, "525152", DisplayName = "2023.12.2-example")]
    [DataRow(2, false, "527570479489", DisplayName = "2023.12.2-problem")]
    public void Solve_2023_12(int part, bool useExample, string expected) =>
        Solve(new Puzzle12Solver(), 12, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "405", DisplayName = "2023.13.1-example")]
    [DataRow(1, false, "26957", DisplayName = "2023.13.1-problem")]
    [DataRow(2, true, "400", DisplayName = "2023.13.2-example")]
    [DataRow(2, false, "42695", DisplayName = "2023.13.2-problem")]
    public void Solve_2023_13(int part, bool useExample, string expected) =>
        Solve(new Puzzle13Solver(), 13, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "136", DisplayName = "2023.14.1-example")]
    [DataRow(1, false, "113525", DisplayName = "2023.14.1-problem")]
    [DataRow(2, true, "64", DisplayName = "2023.14.2-example")]
    [DataRow(2, false, "101292", DisplayName = "2023.14.2-problem")]
    public void Solve_2023_14(int part, bool useExample, string expected) =>
        Solve(new Puzzle14Solver(), 14, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "1320", DisplayName = "2023.15.1-example")]
    [DataRow(1, false, "516657", DisplayName = "2023.15.1-problem")]
    [DataRow(2, true, "145", DisplayName = "2023.15.2-example")]
    [DataRow(2, false, "210906", DisplayName = "2023.15.2-problem")]
    public void Solve_2023_15(int part, bool useExample, string expected) =>
        Solve(new Puzzle15Solver(), 15, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "46", DisplayName = "2023.16.1-example")]
    [DataRow(1, false, "7185", DisplayName = "2023.16.1-problem")]
    [DataRow(2, true, "51", DisplayName = "2023.16.2-example")]
    [DataRow(2, false, "7616", DisplayName = "2023.16.2-problem")]
    public void Solve_2023_16(int part, bool useExample, string expected) =>
        Solve(new Puzzle16Solver(), 16, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "102", DisplayName = "2023.17.1-example")]
    [DataRow(1, false, "785", DisplayName = "2023.17.1-problem")]
    [DataRow(2, true, "94", DisplayName = "2023.17.2-example")]
    [DataRow(2, false, "922", DisplayName = "2023.17.2-problem")]
    public void Solve_2023_17(int part, bool useExample, string expected) =>
        Solve(new Puzzle17Solver(), 17, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "62", DisplayName = "2023.18.1-example")]
    [DataRow(1, false, "58550", DisplayName = "2023.18.1-problem")]
    [DataRow(2, true, "952408144115", DisplayName = "2023.18.2-example")]
    [DataRow(2, false, "47452118468566", DisplayName = "2023.18.2-problem")]
    public void Solve_2023_18(int part, bool useExample, string expected) =>
        Solve(new Puzzle18Solver(), 18, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "19114", DisplayName = "2023.19.1-example")]
    [DataRow(1, false, "432434", DisplayName = "2023.19.1-problem")]
    [DataRow(2, true, "167409079868000", DisplayName = "2023.19.2-example")]
    [DataRow(2, false, "132557544578569", DisplayName = "2023.19.2-problem")]
    public void Solve_2023_19(int part, bool useExample, string expected) =>
        Solve(new Puzzle19Solver(), 19, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "11687500", DisplayName = "2023.20.1-example")]
    [DataRow(1, false, "684125385", DisplayName = "2023.20.1-problem")]
    [DataRow(2, true, "1", DisplayName = "2023.20.2-example")]
    [DataRow(2, false, "225872806380073", DisplayName = "2023.20.2-problem")]
    public void Solve_2023_20(int part, bool useExample, string expected) =>
        Solve(new Puzzle20Solver(), 20, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "16", DisplayName = "2023.21.1-example")]
    [DataRow(1, false, "3740", DisplayName = "2023.21.1-problem")]
    [DataRow(2, true, "6536", DisplayName = "2023.21.2-example")]
    [DataRow(2, false, "620962518745459", DisplayName = "2023.21.2-problem")]
    public void Solve_2023_21(int part, bool useExample, string expected) =>
        Solve(new Puzzle21Solver(), 21, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "5", DisplayName = "2023.22.1-example")]
    [DataRow(1, false, "437", DisplayName = "2023.22.1-problem")]
    [DataRow(2, true, "7", DisplayName = "2023.22.2-example")]
    [DataRow(2, false, "42561", DisplayName = "2023.22.2-problem")]
    public void Solve_2023_22(int part, bool useExample, string expected) =>
        Solve(new Puzzle22Solver(), 22, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "94", DisplayName = "2023.23.1-example")]
    [DataRow(1, false, "2134", DisplayName = "2023.23.1-problem")]
    [DataRow(2, true, "154", DisplayName = "2023.23.2-example")]
    [DataRow(2, false, "6298", DisplayName = "2023.23.2-problem")]
    public void Solve_2023_23(int part, bool useExample, string expected) =>
        Solve(new Puzzle23Solver(), 23, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "2", DisplayName = "2023.24.1-example")]
    [DataRow(1, false, "20847", DisplayName = "2023.24.1-problem")]
    [DataRow(2, true, "47", DisplayName = "2023.24.2-example")]
    [DataRow(2, false, "908621716620524", DisplayName = "2023.24.2-problem")]
    public void Solve_2023_24(int part, bool useExample, string expected) =>
        Solve(new Puzzle24Solver(), 24, part, useExample, expected);

    [DataTestMethod, Timeout(LongTimeout)]
    [DataRow(1, true, "54", DisplayName = "2023.25.1-example")]
    [DataRow(1, false, "546804", DisplayName = "2023.25.1-problem")]
    public void Solve_2023_25(int part, bool useExample, string expected) =>
        Solve(new Puzzle25Solver(), 25, part, useExample, expected);
}
