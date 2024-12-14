using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle13Solver : IPuzzleSolver
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
            var buttonA = Regexes.Integer().Matches(lines[i]);
            var buttonAX = int.Parse(buttonA[0].Value);
            var buttonAY = int.Parse(buttonA[1].Value);

            var buttonB = Regexes.Integer().Matches(lines[i + 1]);
            var buttonBX = int.Parse(buttonB[0].Value);
            var buttonBY = int.Parse(buttonB[1].Value);

            var prize = Regexes.Integer().Matches(lines[i + 2]);
            double prizeX = ulong.Parse(prize[0].Value) + (isBig ? conversionError : 0);
            double prizeY = ulong.Parse(prize[1].Value) + (isBig ? conversionError : 0);

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
}
