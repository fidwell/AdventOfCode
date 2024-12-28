using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var weights = input.SplitByNewline().Select(int.Parse).ToArray();
        var groupTargetWeight = weights.Sum() / 3;

        // https://en.wikipedia.org/wiki/Subset_sum_problem
        var subsets = SubsetSum.FindSubsetsThatSumTo(weights, groupTargetWeight).ToList();
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

    private static int QuantumEntanglement(IEnumerable<int> input) => input.Aggregate(1, (product, value) => product * value);
}
