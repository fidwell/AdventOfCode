namespace AdventOfCode.Core.PuzzleSolvers._2022;

public class Puzzle04Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        input.Split(Environment.NewLine)
            .Select(l => l.Split(','))
            .Select(p => p.Select(ParseRange))
            .Count(p => Contains(p.First(), p.Last()))
            .ToString();

    public string SolvePartTwo(string input) =>
        input.Split(Environment.NewLine)
            .Select(l => l.Split(','))
            .Select(p => p.Select(ParseRange))
            .Count(p => Overlaps(p.First(), p.Last()))
            .ToString();

    private static Range ParseRange(string input)
    {
        var ends = input.Split('-');
        var start = int.Parse(ends.First());
        var end = int.Parse(ends.Last());
        return new Range(start, end);
    }

    private static bool Contains(Range a, Range b) =>
        (a.Start.Value <= b.Start.Value && a.End.Value >= b.End.Value) ||
        (b.Start.Value <= a.Start.Value && b.End.Value >= a.End.Value);

    private static bool Overlaps(Range a, Range b) =>
        (a.Start.Value <= b.Start.Value && a.End.Value >= b.Start.Value) ||
        (b.Start.Value <= a.Start.Value && b.End.Value >= a.Start.Value);
}
