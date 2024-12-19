using AdventOfCode.Core.Input;
using AdventOfCode.Core.Memoization;
using AdventOfCode.Solvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

public abstract class SolutionVerifier(int year)
{
    protected const int Timeout = 2000;
    protected const int LongTimeout = 5000;
    protected const int MaxTimeout = 15000;
    protected readonly int Year = year;

    protected void Solve(PuzzleSolver solver, int puzzle, int part, bool useExample, string expected)
    {
        MemoizationCache.Clear();
        var input = DataReader.GetData(Year, puzzle, part, useExample);
        var result = part == 1
            ? solver.SolvePartOne(input)
            : solver.SolvePartTwo(input);
        Assert.AreEqual(expected, result);
    }
}
