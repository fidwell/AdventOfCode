using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle01Solver() : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) => Solve(false, 1, useSample);
    public string SolvePartTwo(bool useSample = false) => Solve(true, 2, useSample);

    private string Solve(bool allowWords, int part, bool useSample = false)
        => DataReader
            .GetData(1, part, useSample)
            .Split(Environment.NewLine)
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
