using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle03Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Select(r => (r.Substring(0, r.Length / 2), r.Substring(r.Length / 2)))
            .Select(r => r.Item1.Intersect(r.Item2).Single())
            .Select(ValueOf)
            .Sum()
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline()
            .Select((v, i) => new { i, v })
            .GroupBy(x => x.i / 3)
            .Select(g => g.Select(r => r.v.ToCharArray()).Aggregate((a, b) => a.Intersect(b).ToArray()).Single())
            .Select(ValueOf)
            .Sum()
            .ToString();

    private int ValueOf(char c) => c >= 'a' ? 1 + c - 'a' : 27 + c - 'A';
}
