using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var weights = input.SplitByNewline().Select(int.Parse).ToArray();
        var groupTargetWeight = weights.Sum() / 3;

        // https://en.wikipedia.org/wiki/Subset_sum_problem
        var subsets = SubsetSum(weights, groupTargetWeight).ToList();
        var minSize = subsets.Select(s => s.Length).Min();
        var minimumSubsets = subsets.Where(s => s.Length == minSize);
        var entanglements = minimumSubsets.Select(QuantumEntanglement);
        var minEntanglement = entanglements.Min();
        return minEntanglement.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<int[]> SubsetSum(int[] values, int target)
    {
        foreach (var v in values.Where(v => v == target))
        {
            yield return [v];
        }

        if (values.All(v => v > target))
        {
            // No solutions
            yield break;
        }

        for (var i = 0; i < values.Length; i++)
        {
            // Remove item values[i] from the list.
            // Can the subset sum to the target value?

            var testValue = values[i];

            if (testValue > target)
                continue;

            var newSubset = values.Where((_, i0) => i0 != i).ToArray();
            var newTarget = target - testValue;
            var subsetAnswer = SubsetSum(newSubset, newTarget);
            foreach (var answer in subsetAnswer)
            {
                // If so, reconstruct the list and return it
                yield return answer.Concat([testValue]).ToArray();
            }
        }
    }

    private static int QuantumEntanglement(IEnumerable<int> input) => input.Aggregate(1, (product, value) => product * value);
}
