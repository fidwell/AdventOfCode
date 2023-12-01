using AdventOfCode.Data;

namespace AdventOfCode.Core;

public class Puzzle01Solver : IPuzzleSolver
{
    public string Solve(bool useSample = false)
        => DataReader
            .GetData(1, useSample)
            .Split(Environment.NewLine)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(GetFirstAndLastDigits)
            .Select(pair => int.Parse($"{pair.Item1}{pair.Item2}"))
            .Sum()
            .ToString();

    private (int, int) GetFirstAndLastDigits(string input)
    {
        var digits = input.ToCharArray().Where(char.IsDigit);
        return (digits.First() - '0', digits.Last() - '0');
    }
}
