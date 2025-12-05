using System.Text.RegularExpressions;
using AdventOfCode.Core.Ranges;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle05Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        (List<RangeLong> ranges, List<long> ingredients) = ParseInput(input);
        return ingredients.Count(i => ranges.Any(r => r.ContainsInclusive(i))).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        (List<RangeLong> ranges, _) = ParseInput(input);

        var anythingChanged = false;
        do
        {
            anythingChanged = false;
            foreach (var range in ranges)
            {
                var firstOverlap = ranges.FirstOrDefault(r => r != range && r.OverlapsWith(range));
                if (firstOverlap != null)
                {
                    var newRange = range.MergeWith(firstOverlap);
                    var otherRanges = ranges.Where(r => r != range && r != firstOverlap);
                    ranges = [newRange, .. otherRanges];
                    anythingChanged = true;
                    break;
                }
            }

        } while (anythingChanged);

        return ranges.Sum(r => r.Length).ToString();
    }

    private static (List<RangeLong>, List<long>) ParseInput(string input)
    {
        var ranges = new List<RangeLong>();
        var ingredients = new List<long>();

        foreach (var line in input.SplitByNewline())
        {
            if (line.Contains('-'))
            {
                var matches = Range().Match(line);
                var min = long.Parse(matches.Groups[1].Value);
                var max = long.Parse(matches.Groups[2].Value);
                ranges.Add(new RangeLong(min, max - min + 1));
            }
            else if (line.Length > 0)
            {
                ingredients.Add(long.Parse(line));
            }
        }

        return (ranges, ingredients);
    }

    [GeneratedRegex(@"(\d+)\-(\d+)")]
    private static partial Regex Range();
}
