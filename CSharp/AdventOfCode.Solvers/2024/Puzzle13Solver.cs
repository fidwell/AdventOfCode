using System.Text.RegularExpressions;
using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public partial class Puzzle13Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, false);
    public string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool isBig)
    {
        var lines = input.SplitByNewline();
        ulong total = 0;
        const ulong conversionError = 10000000000000;

        for (var i = 0; i < lines.Length; i += 3)
        {
            var buttonA = ButtonPattern().Match(lines[i]);
            var buttonAX = int.Parse(buttonA.Groups[1].Value);
            var buttonAY = int.Parse(buttonA.Groups[2].Value);

            var buttonB = ButtonPattern().Match(lines[i + 1]);
            var buttonBX = int.Parse(buttonB.Groups[1].Value);
            var buttonBY = int.Parse(buttonB.Groups[2].Value);

            var prize = PrizePatern().Match(lines[i + 2]);
            double prizeX = ulong.Parse(prize.Groups[1].Value) + (isBig ? conversionError : 0);
            double prizeY = ulong.Parse(prize.Groups[2].Value) + (isBig ? conversionError : 0);

            // buttonAX * x + buttonBX * x = prizeX
            // buttonAY * y + buttonBY * y = prizeY
            var (tokensA, tokensB) = MathExtensions.SolveSystemOfEquations(buttonAX, buttonAY, buttonBX, buttonBY, prizeX, prizeY);

            if (MathExtensions.IsWholeNumber(tokensA) && MathExtensions.IsWholeNumber(tokensB))
            {
                total += (ulong)tokensA * 3 + (ulong)tokensB;
            }
        }

        return total.ToString();
    }

    [GeneratedRegex(@"Button \w: X\+(\d+), Y\+(\d+)")]
    private static partial Regex ButtonPattern();

    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex PrizePatern();
}
