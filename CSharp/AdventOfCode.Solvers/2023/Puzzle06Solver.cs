using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var data = input.Split(Environment.NewLine);
        var times = data[0].Split(": ")[1].SplitAndTrim(' ');
        var records = data[1].Split(": ")[1].SplitAndTrim(' ');
        return times
            .Select((t, i) => SolveRace(int.Parse(t), int.Parse(records[i])))
            .Aggregate((a, b) => a * b)
            .ToString();
    }

    public string SolvePartTwo(string input)
    {
        var data = input.Split(Environment.NewLine);
        var time = data[0].Split(": ")[1].Replace(" ", "");
        var record = data[1].Split(": ")[1].Replace(" ", "");
        return SolveRace(long.Parse(time), long.Parse(record)).ToString();
    }

    private static int SolveRace(long time, long record) =>
        QuadraticSolutionDiff(1, -time, record) - 1;

    private static int QuadraticSolutionDiff(long a, long b, long c)
    {
        var pos = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
        var neg = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

        var from = pos < neg ? Math.Floor(pos) : Math.Floor(neg);
        var to = neg < pos ? Math.Ceiling(pos) : Math.Ceiling(neg);

        return CountIntegersInRange(from, to);
    }

    private static int CountIntegersInRange(double from, double to)
        => (int)Math.Abs(to - from);
}
