using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core;

public static class TileMath
{
    /// <summary>
    /// Returns a list of coordinates in a CharacterMatrix where the values are in a continuous
    /// region that matches that of the starting location.
    /// </summary>
    /// <param name="matrix">The character matrix to search.</param>
    /// <param name="start">The coordinate from which to start the fill.</param>
    /// <returns>All coordinates in a continuous region from the start.</returns>
    public static List<(int, int)> FloodFill(CharacterMatrix matrix, (int, int) start)
    {
        var result = new HashSet<(int, int)>();
        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);
        var targetChar = matrix.CharAt(start);

        while (queue.Count != 0)
        {
            var coord = queue.Dequeue();
            if (result.Add(coord))
            {
                foreach (var neighbor in matrix.CoordinatesOfNeighbors(coord, allEight: false).Where(n => matrix.CharAt(n) == targetChar))
                    queue.Enqueue(neighbor);
            }
        }
        return [.. result.OrderBy(c => c)];
    }
}
