﻿using AdventOfCode.Core.PuzzleSolvers._2022;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2022 : SolutionVerifier
{
    public SolutionVerifier2022() : base(2022) { }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "24000", DisplayName = "2022.01.1-e")]
    [DataRow(1, false, "71471", DisplayName = "2022.01.1-r")]
    [DataRow(2, true, "45000", DisplayName = "2022.01.2-e")]
    [DataRow(2, false, "211189", DisplayName = "2022.01.2-r")]
    public void Solve_2022_01(int part, bool useExample, string expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "15", DisplayName = "2022.02.1-e")]
    [DataRow(1, false, "11841", DisplayName = "2022.02.1-r")]
    [DataRow(2, true, "12", DisplayName = "2022.02.2-e")]
    [DataRow(2, false, "13022", DisplayName = "2022.02.2-r")]
    public void Solve_2022_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "157", DisplayName = "2022.03.1-e")]
    [DataRow(1, false, "7553", DisplayName = "2022.03.1-r")]
    [DataRow(2, true, "70", DisplayName = "2022.03.2-e")]
    [DataRow(2, false, "2758", DisplayName = "2022.03.2-r")]
    public void Solve_2022_03(int part, bool useExample, string expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "2", DisplayName = "2022.04.1-e")]
    [DataRow(1, false, "536", DisplayName = "2022.04.1-r")]
    [DataRow(2, true, "4", DisplayName = "2022.04.2-e")]
    [DataRow(2, false, "845", DisplayName = "2022.04.2-r")]
    public void Solve_2022_04(int part, bool useExample, string expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "CMZ", DisplayName = "2022.05.1-e")]
    [DataRow(1, false, "CVCWCRTVQ", DisplayName = "2022.05.1-r")]
    [DataRow(2, true, "MCD", DisplayName = "2022.05.2-e")]
    [DataRow(2, false, "CNSCZWLVT", DisplayName = "2022.05.2-r")]
    public void Solve_2022_05(int part, bool useExample, string expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, true, "7", DisplayName = "2022.06.1-e")]
    [DataRow(1, false, "1623", DisplayName = "2022.06.1-r")]
    [DataRow(2, true, "19", DisplayName = "2022.06.2-e")]
    [DataRow(2, false, "3774", DisplayName = "2022.06.2-r")]
    public void Solve_2022_06(int part, bool useExample, string expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);
}
