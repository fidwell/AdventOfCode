using AdventOfCode.Solvers._2025;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2025() : SolutionVerifier(2025)
{
    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 3, DisplayName = "2025.01.1-example")]
    [DataRow(1, false, 1132, DisplayName = "2025.01.1-problem")]
    [DataRow(2, true, 6, DisplayName = "2025.01.2-example")]
    [DataRow(2, false, 6623, DisplayName = "2025.01.2-problem")]
    public void Solve_2025_01(int part, bool useExample, object expected) =>
        Solve(new Puzzle01Solver(), 1, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 1227775554, DisplayName = "2025.02.1-example")]
    [DataRow(1, false, 31000881061, DisplayName = "2025.02.1-problem")]
    [DataRow(2, true, 4174379265, DisplayName = "2025.02.2-example")]
    [DataRow(2, false, 46769308485, DisplayName = "2025.02.2-problem")]
    public void Solve_2025_02(int part, bool useExample, object expected) =>
        Solve(new Puzzle02Solver(), 2, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 357, DisplayName = "2025.03.1-example")]
    [DataRow(1, false, 16858, DisplayName = "2025.03.1-problem")]
    [DataRow(2, true, 3121910778619, DisplayName = "2025.03.2-example")]
    [DataRow(2, false, 167549941654721, DisplayName = "2025.03.2-problem")]
    public void Solve_2025_03(int part, bool useExample, object expected) =>
        Solve(new Puzzle03Solver(), 3, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 13, DisplayName = "2025.04.1-example")]
    [DataRow(1, false, 1505, DisplayName = "2025.04.1-problem")]
    [DataRow(2, true, 43, DisplayName = "2025.04.2-example")]
    [DataRow(2, false, 9182, DisplayName = "2025.04.2-problem")]
    public void Solve_2025_04(int part, bool useExample, object expected) =>
        Solve(new Puzzle04Solver(), 4, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 3, DisplayName = "2025.05.1-example")]
    [DataRow(1, false, 874, DisplayName = "2025.05.1-problem")]
    [DataRow(2, true, 14, DisplayName = "2025.05.2-example")]
    [DataRow(2, false, 348548952146313, DisplayName = "2025.05.2-problem")]
    public void Solve_2025_05(int part, bool useExample, object expected) =>
        Solve(new Puzzle05Solver(), 5, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 4277556, DisplayName = "2025.06.1-example")]
    [DataRow(1, false, 6757749566978, DisplayName = "2025.06.1-problem")]
    [DataRow(2, true, 3263827, DisplayName = "2025.06.2-example")]
    [DataRow(2, false, 10603075273949, DisplayName = "2025.06.2-problem")]
    public void Solve_2025_06(int part, bool useExample, object expected) =>
        Solve(new Puzzle06Solver(), 6, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 21, DisplayName = "2025.07.1-example")]
    [DataRow(1, false, 1546, DisplayName = "2025.07.1-problem")]
    [DataRow(2, true, 40, DisplayName = "2025.07.2-example")]
    [DataRow(2, false, 13883459503480, DisplayName = "2025.07.2-problem")]
    public void Solve_2025_07(int part, bool useExample, object expected) =>
        Solve(new Puzzle07Solver(), 7, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 40, DisplayName = "2025.08.1-example")]
    [DataRow(1, false, 81536, DisplayName = "2025.08.1-problem")]
    [DataRow(2, true, 25272, DisplayName = "2025.08.2-example")]
    [DataRow(2, false, 7017750530, DisplayName = "2025.08.2-problem")]
    public void Solve_2025_08(int part, bool useExample, object expected) =>
        Solve(new Puzzle08Solver(), 8, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 50, DisplayName = "2025.09.1-example")]
    [DataRow(1, false, 4763509452, DisplayName = "2025.09.1-problem")]
    [DataRow(2, true, 24, DisplayName = "2025.09.2-example")]
    [DataRow(2, false, 1516897893, DisplayName = "2025.09.2-problem")]
    public void Solve_2025_09(int part, bool useExample, object expected) =>
        Solve(new Puzzle09Solver(), 9, part, useExample, expected);

    [TestMethod, Timeout(Timeout)]
    [DataRow(1, true, 7, DisplayName = "2025.10.1-example")]
    [DataRow(1, false, 477, DisplayName = "2025.10.1-problem")]
    [DataRow(2, true, 33, DisplayName = "2025.10.2-example")]
    [DataRow(2, false, 17970, DisplayName = "2025.10.2-problem")]
    public void Solve_2025_10(int part, bool useExample, object expected) =>
        Solve(new Puzzle10Solver(), 10, part, useExample, expected);
}
