namespace AdventOfCode.Core;

public static class EnumerableExtensions
{
    public static IEnumerable<long> Range(long start, long count)
    {
        for (long i = start; i < count; i++)
        {
            yield return i;
        }
    }
}
