using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle03Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, 2).ToString();
    public override string SolvePartTwo(string input) => Solve(input, 12).ToString();

    private static long Solve(string input, int length) =>
        input.SplitByNewline()
        .Sum(line => long.Parse(HighestValueIn([.. line.ToCharArray()], length)));

    private static char[] HighestValueIn(char[] values, int resultLength)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resultLength, nameof(resultLength));

        if (resultLength == 1)
            return [values.Max()];

        char maxDigit = '\0';
        var maxDigitIndex = -1;
        for (var i = 0; i <= values.Length - resultLength; i++)
        {
            if (values[i] > maxDigit)
            {
                maxDigit = values[i];
                maxDigitIndex = i;
            }
        }

        var subArray = values.Skip(maxDigitIndex + 1).ToArray();
        var bestFollowingValue = HighestValueIn(subArray, resultLength - 1);
        return [maxDigit, .. bestFollowingValue];
    }
}
