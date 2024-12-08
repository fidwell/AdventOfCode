﻿using AdventOfCode.Solvers._2024;
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

    [DataTestMethod, Timeout(LongTimeout)]
    [DataRow(1, true, "41", DisplayName = "2024.06.1-example")]
    [DataRow(1, false, "4776", DisplayName = "2024.06.1-problem")]
    [DataRow(2, true, "6", DisplayName = "2024.06.2-example")]
    [DataRow(2, false, "1586", DisplayName = "2024.06.2-problem")]
    public void Solve_2024_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "3749", DisplayName = "2024.07.1-example")]
    [DataRow(1, false, "1038838357795", DisplayName = "2024.07.1-problem")]
    [DataRow(2, true, "11387", DisplayName = "2024.07.2-example")]
    [DataRow(2, false, "254136560217241", DisplayName = "2024.07.2-problem")]
    public void Solve_2024_07(int part, bool useExample, string expected) =>
        Solve(new Puzzle07Solver(), 7, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "14", DisplayName = "2024.08.1-example")]
    [DataRow(1, false, "265", DisplayName = "2024.08.1-problem")]
    [DataRow(2, true, "34", DisplayName = "2024.08.2-example")]
    [DataRow(2, false, "962", DisplayName = "2024.08.2-problem")]
    public void Solve_2024_08(int part, bool useExample, string expected) =>
        Solve(new Puzzle08Solver(), 8, part, useExample, expected);

    [DataTestMethod, Timeout(MaxTimeout)]
    [DataRow(1, true, "1928", DisplayName = "2024.09.1-example")]
    [DataRow(1, false, "6262891638328", DisplayName = "2024.09.1-problem")]
    [DataRow(2, true, "2858", DisplayName = "2024.09.2-example")]
    [DataRow(2, false, "6287317016845", DisplayName = "2024.09.2-problem")]
    public void Solve_2024_09(int part, bool useExample, string expected) =>
        Solve(new Puzzle09Solver(), 9, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "36", DisplayName = "2024.10.1-example")]
    [DataRow(1, false, "754", DisplayName = "2024.10.1-problem")]
    [DataRow(2, true, "81", DisplayName = "2024.10.2-example")]
    [DataRow(2, false, "1609", DisplayName = "2024.10.2-problem")]
    public void Solve_2024_10(int part, bool useExample, string expected) =>
        Solve(new Puzzle10Solver(), 10, part, useExample, expected);
}
