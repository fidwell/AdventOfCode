using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle09Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var redTiles = ParseData(input);
        var largestSize = 0L;

        for (var i = 0; i < redTiles.Length - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Length; j++)
            {
                var width = Math.Abs(redTiles[i].Item1 - redTiles[j].Item1) + 1;
                var height = Math.Abs(redTiles[i].Item2 - redTiles[j].Item2) + 1;
                var size = (long)width * height;
                if (size > largestSize)
                    largestSize = size;
            }
        }
        return largestSize;
    }

    public override object SolvePartTwo(string input)
    {
        // Step 1: Parse data
        var cornerCoordinates = ParseData(input);

        // Step 2: Find edges
        var expandedEdges = new List<List<Coord>>();
        var edgeDefinitions = new List<(Coord, Coord)>();

        var allTiles = new HashSet<Coord>(cornerCoordinates);

        for (var i = 0; i < cornerCoordinates.Length; i++)
        {
            var nextI = i == cornerCoordinates.Length - 1 ? 0 : i + 1;

            var here = cornerCoordinates[i];
            var next = cornerCoordinates[nextI];

            // Precompute "ideal" edge (lower coordinates first)
            var minX = Math.Min(here.Item1, next.Item1);
            var maxX = Math.Max(here.Item1, next.Item1);
            var minY = Math.Min(here.Item2, next.Item2);
            var maxY = Math.Max(here.Item2, next.Item2);
            edgeDefinitions.Add(((minX, minY), (maxX, maxY)));

            // Add to list of all tiles in set
            if (minX == maxX)
            {
                for (var y = minY + 1; y < maxY; y++)
                {
                    allTiles.Add((minX, y));
                }
            }
            else
            {
                for (var x = minX + 1; x < maxX; x++)
                {
                    allTiles.Add((x, minY));
                }
            }
        }

        // Step 3: Iterate over all rectangles
        var biggestX = cornerCoordinates.Max(t => t.Item1);
        var largestArea = 0L;
        for (var i = 0; i < cornerCoordinates.Length - 1; i++)
        {
            for (var j = i + 1; j < cornerCoordinates.Length; j++)
            {
                var item1 = cornerCoordinates[i];
                var item2 = cornerCoordinates[j];
                var minX = Math.Min(item1.Item1, item2.Item1);
                var maxX = Math.Max(item1.Item1, item2.Item1);
                var minY = Math.Min(item1.Item2, item2.Item2);
                var maxY = Math.Max(item1.Item2, item2.Item2);

                var rectArea = (long)(maxX - minX + 1) * (maxY - minY + 1);
                if (rectArea < largestArea)
                    continue;

                var center = ((minX + maxX) / 2, (maxY + minY) / 2);
                var anyIntersect = edgeDefinitions.Any(e => DoesEdgeIntersectWithRectangle(item1, item2, e));
                if (anyIntersect)
                    continue;

                var centerIsInsidePolygon = IsInsidePolygon(allTiles, center, biggestX);
                if (centerIsInsidePolygon)
                {
                    largestArea = rectArea;
                }
            }
        }
        return largestArea;
    }

    private static Coord[] ParseData(string input) =>
        [.. input.SplitByNewline().Select((l, i) =>
        {
            var values = l.Split(',').Select(int.Parse).ToArray();
            return new Coord(values[0], values[1]);
        })];

    private static bool DoesEdgeIntersectWithRectangle(Coord item1, Coord item2, (Coord, Coord) edge)
    {
        var minX = Math.Min(item1.Item1, item2.Item1);
        var maxX = Math.Max(item1.Item1, item2.Item1);
        var minY = Math.Min(item1.Item2, item2.Item2);
        var maxY = Math.Max(item1.Item2, item2.Item2);

        return edge.Item1.Item2 == edge.Item2.Item2
            ? edge.Item1.Item2 > minY && edge.Item1.Item2 < maxY && edge.Item2.Item1 > minX && edge.Item1.Item1 < maxX
            : edge.Item1.Item1 > minX && edge.Item1.Item1 < maxX && edge.Item2.Item2 > minY && edge.Item1.Item2 < maxY;
    }

    private static bool IsInsidePolygon(HashSet<Coord> boundaryPoints, Coord pointToTest, int maxX)
    {
        if (boundaryPoints.Contains(pointToTest))
            return true;

        // Raycast to find if it's in the polygon
        var boundariesHit = 0;
        var xr = pointToTest.Item1;
        while (xr <= maxX + 1)
        {
            var currentPoint = (xr, pointToTest.Item2);

            if (boundaryPoints.Contains(currentPoint))
            {
                // Check if this is part of a horizontal edge
                if (boundaryPoints.Contains((xr - 1, pointToTest.Item2)) ||
                    boundaryPoints.Contains((xr + 1, pointToTest.Item2)))
                {
                    // Skip the entire edge
                    while (xr <= maxX + 1 && boundaryPoints.Contains((xr, pointToTest.Item2)))
                    {
                        xr++;
                    }
                    continue;
                }

                // Vertical edge
                boundariesHit++;
            }

            xr++;
        }

        return boundariesHit % 2 == 1;
    }
}
