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
        return ranges.Simplify().Sum(r => r.Length).ToString();
    }

    private static (List<RangeLong>, List<long>) ParseInput(string input)
    {
        var ranges = new List<RangeLong>();
        var ingredients = new List<long>();

        foreach (var line in input.SplitByNewline())
        {
            if (line.Contains('-'))
            {
                var matches = Regexes.Range().Match(line);
                var min = long.Parse(matches.Groups[1].Value);
                var max = long.Parse(matches.Groups[2].Value);
                ranges.Add(RangeLong.ByBounds(min, max + 1));
            }
            else if (line.Length > 0)
            {
                ingredients.Add(long.Parse(line));
            }
        }

        return (ranges, ingredients);
    }
}
