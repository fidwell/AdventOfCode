namespace AdventOfCode.Core.MathUtilities;

public static class SubsetSum
{
    public static IEnumerable<int[]> FindSubsetsThatSumTo(int[] values, int target)
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
            var subsetAnswer = FindSubsetsThatSumTo(newSubset, newTarget);
            foreach (var answer in subsetAnswer)
            {
                // If so, reconstruct the list and return it
                yield return answer.Concat([testValue]).ToArray();
            }
        }
    }
}
