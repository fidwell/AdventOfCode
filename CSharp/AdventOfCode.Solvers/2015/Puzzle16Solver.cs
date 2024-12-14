using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle16Solver : IPuzzleSolver
{
    private static readonly Dictionary<string, int> Analysis = new()
    {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2 },
        { "pomeranians", 3 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "goldfish", 5 },
        { "trees", 3 },
        { "cars", 2 },
        { "perfumes", 1 }
    };

    public string SolvePartOne(string input) => input.SplitByNewline()
        .Select(MapAunt)
        .Single(a => IsMatch(a.Item2, false)).Item1.ToString();

    public string SolvePartTwo(string input) => input.SplitByNewline()
        .Select(MapAunt)
        .Single(a => IsMatch(a.Item2, true)).Item1.ToString();

    private static (int, Dictionary<string, int>) MapAunt(string input)
    {
        var matches = EntryDefinition().Match(input);
        var id = int.Parse(matches.Groups[1].Value);
        var result = new Dictionary<string, int>();

        for (var i = 2; i < 7; i += 2)
        {
            var property = matches.Groups[i].Value;
            var value = int.Parse(matches.Groups[i + 1].Value);
            result.Add(property, value);
        }

        return (id, result);
    }

    private static bool IsMatch(Dictionary<string, int> aunt, bool isRangedMatch)
    {
        foreach (var key in aunt.Keys)
        {
            switch (key)
            {
                case "cats" when isRangedMatch:
                case "trees" when isRangedMatch:
                    if (aunt[key] <= Analysis[key])
                        return false;
                    break;
                case "pomeranians" when isRangedMatch:
                case "goldfish" when isRangedMatch:
                    if (aunt[key] >= Analysis[key])
                        return false;
                    break;
                default:
                    if (Analysis[key] != aunt[key])
                        return false;
                    break;
            }
        }
        return true;
    }

    [GeneratedRegex(@"Sue (\d+): ([a-z]+): (\d+), ([a-z]+): (\d+), ([a-z]+): (\d+)")]
    private static partial Regex EntryDefinition();
}
