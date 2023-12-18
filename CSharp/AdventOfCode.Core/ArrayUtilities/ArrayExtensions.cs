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

    public static IEnumerable<(int, int)> AllPoints(int xCount, int yCount) =>
        Enumerable.Range(0, yCount).SelectMany(y => Enumerable.Range(0, xCount).Select(x => (x, y)));

    public static void FloodFill(
        bool[,] shapeArray,
        (int, int) start,
        List<(int, int)> result)
    {
        var width = shapeArray.GetLength(0);
        var height = shapeArray.GetLength(1);
        var visited = new HashSet<(int, int)>();
        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();

            if (point.Item1 < 0 || point.Item1 >= width ||
                point.Item2 < 0 || point.Item2 >= height ||
                visited.Contains(point) ||
                shapeArray[point.Item1, point.Item2])
                continue;

            visited.Add(point);
            result.Add(point);
            queue.Enqueue((point.Item1 + 1, point.Item2));
            queue.Enqueue((point.Item1 - 1, point.Item2));
            queue.Enqueue((point.Item1, point.Item2 + 1));
            queue.Enqueue((point.Item1, point.Item2 - 1));
        }
    }
}
