using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle01Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false).ToString();
    public override string SolvePartTwo(string input) => Solve(input, true).ToString();

    public static int Solve(string input, bool countPasses)
    {
        var directions = input.SplitByNewline();
        var zeros = 0;
        var dial = 50;
        foreach (var direction in directions)
        {
            var orientation = direction[0];
            var isGoingUp = orientation == 'R';
            var amount = int.Parse(direction[1..]) *
                (isGoingUp ? 1 : -1);

            var passes = Math.Abs(amount) / 100;
            var effectiveAmount = amount % 100;

            if (!isGoingUp && dial == 0)
                dial = 100;

            dial += effectiveAmount;

            if (isGoingUp && dial == 100)
                dial = 0;

            while (dial > 99)
            {
                dial -= 100;
                passes++;
            }
            while (dial < 0)
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
