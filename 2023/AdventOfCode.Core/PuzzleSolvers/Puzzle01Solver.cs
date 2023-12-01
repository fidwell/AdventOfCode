using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers;

public class Puzzle01Solver(int part) : IPuzzleSolver
{
    public string Solve(bool useSample = false)
        => DataReader
            .GetData(1, part, useSample)
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(GetFirstAndLastDigits)
            .Select(pair => int.Parse($"{pair.Item1}{pair.Item2}"))
            .Sum()
            .ToString();

    private (int, int) GetFirstAndLastDigits(string input) => part switch
    {
        1 => GetFirstAndLastDigitsOne(input),
        2 => GetFirstAndLastDigitsTwo(input),
        _ => default,
    };

    private static (int, int) GetFirstAndLastDigitsOne(string input)
    {
        var digits = input.ToCharArray().Where(char.IsDigit);
        return (digits.First() - '0', digits.Last() - '0');
    }

    private static (int, int) GetFirstAndLastDigitsTwo(string input)
    {
        var digits = DigitWordFinder.FindWordDigits(input);
        return (digits.First(), digits.Last());
    }
}
