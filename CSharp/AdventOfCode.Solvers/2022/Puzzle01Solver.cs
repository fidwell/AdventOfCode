using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle01Solver() : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input
            .SplitByNewline(StringSplitOptions.None)
            .Chunk()
            .Max(g => g.Sum(int.Parse))
            .ToString();

    public override string SolvePartTwo(string input) =>
        input
            .SplitByNewline(StringSplitOptions.None)
            .Chunk()
            .Select(g => g.Sum(int.Parse))
            .OrderByDescending(s => s)
            .Take(3)
            .Sum()
            .ToString();
}
