using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle02Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        input.AsListOfIntArrays()
            .Count(IsSafe)
            .ToString();

    public string SolvePartTwo(string input) =>
        input.AsListOfIntArrays()
            .Count(r => IsSafe(r) || IsSafeIfDampened(r))
            .ToString();

    private bool IsSafe(int[] levels) => levels.IsSorted() && HasNoBigJumps(levels);

    private bool IsSafeIfDampened(int[] levels)
    {
        for (var i = 0; i < levels.Length; i++)
        {
            if (IsSafe(levels.CopyExcept(i)))
                return true;
        }

        return false;
    }

    private static bool HasNoBigJumps(int[] input) =>
        Differences(input).All(d => d >= 1 && d <= 3);

    private static int[] Differences(int[] input) =>
        input.Zip(input.Skip(1), (a, b) => a > b ? a - b : b - a).ToArray();
}
