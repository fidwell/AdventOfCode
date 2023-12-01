using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle01Solver(int part) : IPuzzleSolver
{
    public string Solve(bool useSample = false)
        => DataReader
            .GetData(1, part, useSample)
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => GetFirstAndLastDigits(l, part == 2))
            .Select(pair => int.Parse($"{pair.Item1}{pair.Item2}"))
            .Sum()
            .ToString();

    private static (int, int) GetFirstAndLastDigits(string input, bool allowWords)
    {
        var digits = DigitFinder.FindDigits(input, allowWords);
        return (digits.First(), digits.Last());
    }
}
