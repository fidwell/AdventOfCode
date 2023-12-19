using AdventOfCode.Core.PuzzleSolvers;
using AdventOfCode.Core.PuzzleSolvers._2022;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2022 : SolutionVerifier
{
    private readonly IPuzzleSolver[] _solvers;

    public SolutionVerifier2022()
    {
        _solvers = [
            new Puzzle01Solver(),
            new Puzzle02Solver(),
        ];
    }

    [DataTestMethod, Timeout(Timeout)]
    [DataRow(1, 1, true, "24000", DisplayName = "2022.01.1-s")]
    [DataRow(1, 1, false, "71471", DisplayName = "2022.01.1")]
    [DataRow(1, 2, true, "45000", DisplayName = "2022.01.2-s")]
    [DataRow(1, 2, false, "211189", DisplayName = "2022.01.2")]

    [DataRow(2, 1, true, "15", DisplayName = "2022.02.1-s")]
    [DataRow(2, 1, false, "11841", DisplayName = "2022.02.1")]
    [DataRow(2, 2, true, "12", DisplayName = "2022.02.2-s")]
    [DataRow(2, 2, false, "13022", DisplayName = "2022.02.2")]
    public void Solve(int puzzle, int part, bool useSample, string expected) =>
        Solve(_solvers[puzzle - 1], 2022, puzzle, part, useSample, expected);
}
