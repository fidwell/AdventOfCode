using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle01Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input, false);
    public override object SolvePartTwo(string input) => Solve(input, true);

    public static int Solve(string input, bool countPasses)
    {
        var amounts = input
            .Replace("L", "-")
            .Replace("R", "")
            .SplitByNewline()
            .Select(int.Parse);
        var zeros = 0;
        var dial = 50;
        foreach (var amount in amounts)
        {
            var isGoingUp = amount > 0;
            var passes = Math.Abs(amount) / 100;
            var effectiveAmount = amount % 100;

            if (!isGoingUp && dial == 0)
                dial = 100;

            dial += effectiveAmount;

            if (isGoingUp && dial == 100)
                dial = 0;

            if (dial > 99)
            {
                dial -= 100;
                passes++;
            }
            else if (dial < 0)
            {
                dial += 100;
                passes++;
            }

            if (dial == 0)
                zeros++;

            if (countPasses)
                zeros += passes;
        }
        return zeros;
    }
}
