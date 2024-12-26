using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle01Solver() : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input
            .Chunk()
            .Max(g => g.Sum(int.Parse))
            .ToString();

    public override string SolvePartTwo(string input) =>
        input
            .Chunk()
            .Select(g => g.Sum(int.Parse))
            .OrderByDescending(s => s)
            .Take(3)
            .Sum()
            .ToString();
}
