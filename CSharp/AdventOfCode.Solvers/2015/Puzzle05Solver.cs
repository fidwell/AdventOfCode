using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Count(s =>
                HasAtLeastThreeVowels(s) &&
                HasAtLeastOneDouble(s) &&
                HasNoForbiddenStrings(s))
            .ToString();

    public string SolvePartTwo(string input) =>
        input.SplitByNewline()
            .Count(s =>
                HasAtLeastTwoIdenticalPairs(s) &&
                HasSandwich(s))
            .ToString();

    private static bool HasAtLeastThreeVowels(string input) =>
        Vowels().Matches(input).Count >= 3;

    private static bool HasAtLeastOneDouble(string input) =>
        DoubleLetter().IsMatch(input);

    private static bool HasAtLeastTwoIdenticalPairs(string input)
    {
        // Naive solution. Make a list of each pair and its start index.
        // Then iterate through each pair, see if they are the same,
        // unless their indices are off by only one.
        // I'm sure a more elegant solution exists.

        var pairs = new List<(int, char, char)>();
        for (var i = 0; i < input.Length - 1; i++)
        {
            pairs.Add((i, input[i], input[i + 1]));
        }

        for (var i = 0; i < pairs.Count - 1; i++)
        {
            var source = pairs[i];
            for (var j = i + 1; j < pairs.Count; j++)
            {
                var target = pairs[j];
                if (source.Item2 == target.Item2 && source.Item3 == target.Item3
                    && source.Item1 != target.Item1 - 1)
                    return true;
            }
        }

        return false;
    }

    private static bool HasNoForbiddenStrings(string input) =>
        !ForbiddenStrings().IsMatch(input);

    private static bool HasSandwich(string input) =>
        Sandwich().IsMatch(input);

    [GeneratedRegex(@"(a|e|i|o|u)")]
    private static partial Regex Vowels();

    [GeneratedRegex(@"(\w)\1")]
    private static partial Regex DoubleLetter();

    [GeneratedRegex(@"(ab|cd|pq|xy)")]
    private static partial Regex ForbiddenStrings();

    [GeneratedRegex(@"(\w)\w\1")]
    private static partial Regex Sandwich();
}
