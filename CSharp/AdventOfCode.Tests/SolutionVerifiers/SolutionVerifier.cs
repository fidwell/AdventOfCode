using AdventOfCode.Core.PuzzleSolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

public abstract class SolutionVerifier
{
    protected const int Timeout = 3000;

    protected static void Solve(IPuzzleSolver solver, int year, int puzzle, int part, bool useSample, string expected)
    {
        if (DateTime.Today < new DateTime(2023, 12, puzzle))
            return;

        var input = DataReader.GetData(year, puzzle, part, useSample);
        var result = part == 1
            ? solver.SolvePartOne(input)
            : solver.SolvePartTwo(input);
        Assert.AreEqual(expected, result);
    }
}
