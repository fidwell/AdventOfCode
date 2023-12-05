namespace AdventOfCode.Core.ArrayUtilities;

internal static class ArrayChunker
{
    /// <summary>
    /// Splits a string array into "chunks" of arrays, where the
    /// original array is separated by some empty entries.
    /// </summary>
    /// <param name="array">An array of strings, some of which are blank.</param>
    /// <returns>A list of sub-arrays.</returns>
    public static IEnumerable<string[]> Chunk(string[] array)
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
}
