using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle01Solver() : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, false);

    public string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool allowWords)
        => input.Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => GetFirstAndLastDigits(l, allowWords))
            .Select(pair => pair.Item1 * 10 + pair.Item2)
            .Sum()
            .ToString();

    private static (int, int) GetFirstAndLastDigits(string input, bool allowWords)
    {
        var digits = DigitFinder.FindDigits(input, allowWords);
        return (digits.First(), digits.Last());
    }
}
