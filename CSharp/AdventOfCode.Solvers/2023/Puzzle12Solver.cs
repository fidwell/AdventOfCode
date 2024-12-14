using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle12Solver : IPuzzleSolver
{
    private readonly Dictionary<string, long> _cache = [];

    public string SolvePartOne(string input) =>
        input
        .SplitByNewline()
        .Select(l => PossibleArrangementCount(l, 1))
        .Sum()
        .ToString();

    public string SolvePartTwo(string input) =>
        input
        .SplitByNewline()
        .Select(l => PossibleArrangementCount(l, 5))
        .Sum()
        .ToString();

    private long PossibleArrangementCount(string input, int repeat)
    {
        var data = input.Split(" ");
        var asString = $".{data[0].Repeat(repeat, "?")}.";
        var groupCounts = data[1].Repeat(repeat, ",").Split(",").Select(int.Parse).ToArray();
        return CachedResult(asString, groupCounts);
    }

    private long CachedResult(string input, int[] groupCounts)
    {
        var key = $"{input}|{string.Join(",", groupCounts)}";
        if (_cache.TryGetValue(key, out var existing))
            return existing;

        var value = CalculateResult(input, groupCounts);
        _cache[key] = value;
        return value;
    }

    private long CalculateResult(string input, int[] groupCounts)
    {
        if (input.Length == 0)
            return groupCounts.Length == 0 ? 1 : 0;

        if (groupCounts.Length == 0)
            return input.Contains('#') ? 0 : 1;

        if (input.Length < groupCounts.Sum() + groupCounts.Length - 1)
            return 0;

        if (input[0] == '.')
            return CachedResult(input.Substring(1), groupCounts);

        if (input[0] == '#')
        {
            var thisGroupCount = groupCounts[0];

            // Current group is too small
            if (input.Substring(0, thisGroupCount).Contains('.'))
                return 0;

            // Current group is too big (all # or ?)
            if (thisGroupCount < input.Length && input[thisGroupCount] == '#')
                return 0;

            return CachedResult(input.Substring(groupCounts[0] + 1), groupCounts.Skip(1).ToArray());
        }

        var ifPound = CachedResult($"#{input.Substring(1)}", groupCounts);
        var ifPeriod = CachedResult($".{input.Substring(1)}", groupCounts);
        return ifPound + ifPeriod;
    }
}
