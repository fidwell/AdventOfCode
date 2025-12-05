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
                ranges.Add(RangeLongExtensions.Parse(line, isInclusive: true));
            }
            else if (line.Length > 0)
            {
                ingredients.Add(long.Parse(line));
            }
        }

        return (ranges, ingredients);
    }
}
