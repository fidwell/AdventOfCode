using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle17Solver : IPuzzleSolver
{
    private static readonly List<List<int>> Combinations = [];

    public string SolvePartOne(string input)
    {
        Combinations.Clear();

        var values = input.SplitByNewline().Select(int.Parse).OrderByDescending(x => x).ToList();
        var isExample = values.Count <= 5;
        var target = isExample ? 25 : 150;

        return TimesYouCouldFillContainers(values, [], target).ToString();
    }

    // Originally discovered by outputting values from part 1,
    // pasting them into a spreadsheet, and simply counting
    // how many possible combinations were only 3 items long.
    // Here's how we can do it programmatically if we want.
    public string SolvePartTwo(string input)
    {
        _ = SolvePartOne(input);
        var minLength = Combinations.Min(c => c.Count);
        return Combinations.Count(c => c.Count == minLength).ToString();
    }

    private static int TimesYouCouldFillContainers(List<int> availableContainers, List<int> usedContainers, int target)
    {
        if (target == 0)
        {
            Combinations.Add(usedContainers);
            return 1;
        }

        if (availableContainers.All(c => c > target))
            return 0;

        var total = 0;
        for (var i = 0; i < availableContainers.Count; i++)
        {
            var thisAmount = availableContainers[i];

            if (thisAmount > target)
                continue;

            var newTarget = target - thisAmount;
            var otherContainers = availableContainers.Where((c, j) => i > j).ToList();
            total += TimesYouCouldFillContainers(otherContainers, [thisAmount, .. usedContainers], newTarget);
        }
        return total;
    }
}
