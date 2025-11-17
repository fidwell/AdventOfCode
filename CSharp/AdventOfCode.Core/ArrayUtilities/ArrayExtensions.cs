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

            foreach (var subArray in Chunk([.. array.Skip(emptyIndex + 1)]))
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

    public static IEnumerable<Coord> AllPoints(int xCount, int yCount) =>
        Enumerable.Range(0, yCount).SelectMany(y => Enumerable.Range(0, xCount).Select(x => (x, y)));

    /// <summary>
    /// Determines whether the elements in an array are in a sorted order,
    /// either ascending or descending.
    /// </summary>
    /// <param name="input">The array to analyze.</param>
    /// <returns>True if the list is sorted ascending or descending; false if not.</returns>
    public static bool IsSorted(this int[] input)
    {
        var sorted = input.OrderBy(a => a);
        return Enumerable.SequenceEqual(input, sorted) ||
               Enumerable.SequenceEqual(input, sorted.Reverse());
    }

    /// <summary>
    /// Returns a hard copy of an array, except for the element at a specific index.
    /// </summary>
    /// <typeparam name="T">The data type of the array.</typeparam>
    /// <param name="input">The array to copy.</param>
    /// <param name="index">The index to omit.</param>
    /// <returns>A copy of the array.</returns>
    public static T[] CopyExcept<T>(this T[] input, int index)
    {
        if (index < 0 || index >= input.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        var copy = new T[input.Length - 1];
        for (int i = 0, j = 0; i < copy.Length; i++, j++)
        {
            if (i == index)
            {
                j++;
            }
            copy[i] = input[j];
        }
        return copy;
    }
}
