using AdventOfCode.Core;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle06Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);
    public override string SolvePartTwo(string input) => Solve(input, false);

    private static string Solve(string input, bool getMostLikely)
    {
        var lines = input.SplitByNewline();
        var columns = Enumerable.Range(0, lines[0].Length)
            .Select(i => new string([.. lines.Select(l => l[i])]));
        return new string([.. columns.Select(g => BestCharIn(g, getMostLikely))]);
    }

    private static char BestCharIn(string input, bool getMostLikely)
        => EnumerableExtensions.GetWithBestValue(input.ToCharArray().GroupBy(c => c),
            g => g.Count(),
            getMostLikely ? (a, b) => a > b : (a, b) => b > a).Key;
}
