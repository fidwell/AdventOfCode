using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.IntSpace;

public static class IntSpaceAlgorithms
{
    /// <summary>
    /// Returns a list of coordinates in a CharacterMatrix where the values are in a continuous
    /// region that matches that of the starting location.
    /// </summary>
    /// <param name="matrix">The character matrix to search.</param>
    /// <param name="start">The coordinate from which to start the fill.</param>
    /// <returns>All coordinates in a continuous region from the start.</returns>
    public static List<Coord2d> FloodFill(CharacterMatrix matrix, Coord2d start)
    {
        var result = new HashSet<Coord2d>();
        var queue = new Queue<Coord2d>();
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
        return [.. result];
    }
}
