using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle02Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var presents = input.SplitByNewline()
            .Select(line => line.ParseInts());
        var sum = 0;
        foreach (var present in presents)
        {
            var side1 = present[0] * present[1];
            var side2 = present[1] * present[2];
            var side3 = present[2] * present[0];
            var slack = Math.Min(Math.Min(side1, side2), side3);
            var total = 2 * side1 + 2 * side2 + 2 * side3 + slack;
            sum += total;
        }
        return sum.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var presents = input.SplitByNewline()
            .Select(line => line.ParseInts());
        var sum = 0;
        foreach (var present in presents)
        {
            var side1 = 2 * present[0] + 2 * present[1];
            var side2 = 2 * present[1] + 2 * present[2];
            var side3 = 2 * present[2] + 2 * present[0];
            var minLength = Math.Min(Math.Min(side1, side2), side3);
            var volume = present[0] * present[1] * present[2];
            var total = minLength + volume;
            sum += total;
        }
        return sum.ToString();
    }
}
