namespace AdventOfCode.Core.ArrayUtilities;

public static class ArrayExtensions
{
    /// <summary>
    /// Splits a string array into "chunks" of arrays, where the
    /// original array is separated by some empty entries.
    /// </summary>
    /// <param name="array">An array of strings, some of which are blank.</param>
    /// <returns>A list of sub-arrays.</returns>
    public static IEnumerable<string[]> Chunk(this string[] array)
    {
        var emptyIndex = Array.FindIndex(array, string.IsNullOrEmpty);

        if (emptyIndex == -1)
        {
            yield return array.ToArray();
        }
        else
        {
            yield return array.Take(emptyIndex).ToArray();

            foreach (var subArray in Chunk(array.Skip(emptyIndex + 1).ToArray()))
            {
                yield return subArray;
            }
        }
    }

    /// <summary>
    /// Given a collection of items, pair each item.
    /// </summary>
    /// <typeparam name="T">The type of the collection data.</typeparam>
    /// <param name="input">The collection to find pairs in.</param>
    /// <returns>All unique pairs in the given collection.</returns>
    public static IEnumerable<T[]> Pairs<T>(this T[] input)
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
