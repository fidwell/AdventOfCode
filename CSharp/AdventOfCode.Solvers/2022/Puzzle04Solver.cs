using AdventOfCode.Core.Ranges;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Select(l => l.Split(','))
            .Select(p => p.Select(x => RangeExtensions.Parse(x)))
            .Count(p => p.First().ContainsInclusive(p.Last()))
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline()
            .Select(l => l.Split(','))
            .Select(p => p.Select(x => RangeExtensions.Parse(x)))
            .Count(p => p.First().OverlapsInclusive(p.Last()))
            .ToString();
}
