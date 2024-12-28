using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, 3);
    public override string SolvePartTwo(string input) => Solve(input, 4);

    private static string Solve(string input, int compartments)
    {
        var weights = input.SplitByNewline().Select(int.Parse).ToArray();
        var groupTargetWeight = weights.Sum() / compartments;

        // https://en.wikipedia.org/wiki/Subset_sum_problem
        var subsets = BacktrackingVersion(weights, groupTargetWeight).ToList();
        var minSize = subsets.Select(s => s.Length).Min();
        var minimumSubsets = subsets.Where(s => s.Length == minSize);
        var entanglements = minimumSubsets.Select(QuantumEntanglement);
        var minEntanglement = entanglements.Min();
        return minEntanglement.ToString();
    }

    private static List<int[]> BacktrackingVersion(int[] values, int target)
    {
        _solutions.Clear();
        SmallestSubsetSumBacktracting(values, 0, 0, target, []);
        return _solutions;
    }

    private static readonly List<int[]> _solutions = [];

    private static void SmallestSubsetSumBacktracting(int[] values, int sumSoFar, int startingIndex, int targetSum, int[] listSoFar)
    {
        // Already too long
        if (_solutions.Count > 0 && listSoFar.Length > _solutions[0].Length)
            return;

        if (targetSum == sumSoFar)
        {
            if (_solutions.Count == 0)
            {
                _solutions.Add(listSoFar);
            }
            else if (_solutions[0].Length > listSoFar.Length)
            {
                _solutions.Clear();
                _solutions.Add(listSoFar);
            }

            if (startingIndex < values.Length)
            {
                var value = values[startingIndex - 1];
                SmallestSubsetSumBacktracting(values, sumSoFar - value, startingIndex, targetSum, [.. listSoFar, value]);
            }
        }
        else
        {
            for (int i = startingIndex; i < values.Length; i++)
            {
                var value = values[i];
                SmallestSubsetSumBacktracting(values, sumSoFar + value, i + 1, targetSum, [.. listSoFar, value]);
            }
        }
    }

    private static ulong QuantumEntanglement(IEnumerable<int> input) => input.Aggregate(1UL, (product, value) => product * (ulong)value);
}
