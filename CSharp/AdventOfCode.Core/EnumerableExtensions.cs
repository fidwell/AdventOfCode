namespace AdventOfCode.Core;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> AllPermutations<T>(this List<T> options)
    {
        if (options.Count == 1)
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
}
