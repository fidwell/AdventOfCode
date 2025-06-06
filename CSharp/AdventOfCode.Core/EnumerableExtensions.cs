namespace AdventOfCode.Core;

public static class EnumerableExtensions
{
    public static IEnumerable<List<T>> AllPermutations<T>(this List<T> options)
    {
        if (options.Count <= 1)
        {
            yield return options;
        }
        else
        {
            for (var i = 0; i < options.Count; i++)
            {
                var first = options[i];
                var rest = options.Where((v, j) => j != i).ToList();
                foreach (var subpermutation in rest.AllPermutations())
                    yield return [first, .. subpermutation];
            }
        }
    }

    public static T GetWithBestValue<T>(IEnumerable<T> groups, Func<T, int> selector, Func<int, int, bool> isBetter)
    {
        T result = groups.First();
        var bestValue = selector(result);
        foreach (var group in groups.Skip(1))
        {
            var amount = selector(group);
            if (isBetter(amount, bestValue))
            {
                result = group;
                bestValue = amount;
            }
        }
        return result;
    }
}
