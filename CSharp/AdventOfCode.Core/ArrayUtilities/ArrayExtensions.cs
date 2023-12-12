namespace AdventOfCode.Core.ArrayUtilities;

public static class ArrayExtensions
{
    /// <summary>
    /// Given a collection of items, pair each item.
    /// </summary>
    /// <typeparam name="T">The type of the collection data.</typeparam>
    /// <param name="input">The collection to find pairs in.</param>
    /// <returns>All unique pairs in the given collection.</returns>
    public static IEnumerable<T[]> Pairs<T>(T[] input)
    {
        for (var i = 0; i < input.Length - 1; i++)
        {
            for (var j = i + 1; j < input.Length; j++)
            {
                yield return new T[] { input[i], input[j] };
            }
        }
    }
}
