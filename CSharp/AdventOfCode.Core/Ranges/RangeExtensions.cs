using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.Ranges;

public static class RangeExtensions
{
    public static Range Parse(string input, bool isInclusive = false)
    {
        var matches = Regexes.Range().Match(input);
        var start = int.Parse(matches.Groups[1].Value);
        var end = int.Parse(matches.Groups[2].Value);
        return new Range(start, end + (isInclusive ? 1 : 0));
    }

    public static int Length(this Range range) => range.End.Value - range.Start.Value;

    public static bool ContainsInclusive(this Range a, Range b) =>
        (a.Start.Value <= b.Start.Value && a.End.Value >= b.End.Value) ||
        (b.Start.Value <= a.Start.Value && b.End.Value >= a.End.Value);

    public static bool OverlapsInclusive(this Range a, Range b) =>
        (a.Start.Value <= b.Start.Value && a.End.Value >= b.Start.Value) ||
        (b.Start.Value <= a.Start.Value && b.End.Value >= a.Start.Value);
}
