﻿using AdventOfCode.Solvers._2016;
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
    [DataRow(1, true, "1985", DisplayName = "2016.01.1-example")]
    [DataRow(1, false, "84452", DisplayName = "2016.01.1-problem")]
    [DataRow(2, true, "5DB3", DisplayName = "2016.01.2-example")]
    [DataRow(2, false, "D65C3", DisplayName = "2016.01.2-problem")]
    public void Solve_2016_02(int part, bool useExample, string expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);
}
