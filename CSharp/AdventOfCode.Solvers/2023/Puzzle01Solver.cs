using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle01Solver() : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);

    public override string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool allowWords)
        => input.SplitByNewline()
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => GetFirstAndLastDigits(l, allowWords))
            .Select(pair => pair.Item1 * 10 + pair.Item2)
            .Sum()
            .ToString();

    private static (int, int) GetFirstAndLastDigits(string input, bool allowWords)
    {
        if (allowWords)
        {
            foreach (var word in Replacers)
            {
                input = input.Replace(word.Key, word.Value);
            }
        }

        var matches = Regexes.Digit().Matches(input);
        return (int.Parse(matches.First().Value), int.Parse(matches.Last().Value));
    }

    private static readonly Dictionary<string, string> Replacers = new()
    {
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "t3e" },
        { "four", "f4r" },
        { "five", "f5" },
        { "six", "s6x" },
        { "seven", "s7n" },
        { "eight", "e8t" },
        { "nine", "n9e" }
    };
}
