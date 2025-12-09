using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle09Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var redTiles = input.SplitByNewline().Select(l =>
        {
            var match = CoordinateRegex().Match(l);
            return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }).ToList();

        var largestSize = 0L;

        for (var i = 0; i < redTiles.Count - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
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
        var cornerCoordinates = input.SplitByNewline().Select((l, i) =>
        {
            var match = CoordinateRegex().Match(l);
            return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }).ToArray();

        // Step 2: Find edges
        var edges = new List<List<Coord>>();
        for (var i = 0; i < cornerCoordinates.Length; i++)
        {
            var nextI = i == cornerCoordinates.Length - 1 ? 0 : i + 1;

            var here = cornerCoordinates[i];
            var next = cornerCoordinates[nextI];

            var edge = new List<Coord>();

            if (here.Item1 == next.Item1)
            {
                var hereY = Math.Min(here.Item2, next.Item2);
                var nextY = Math.Max(here.Item2, next.Item2);
                // Same x; going up or down
                for (var y = hereY; y <= nextY; y++)
                {
                    edge.Add((here.Item1, y));
                }
            }
            else
            {
                var hereX = Math.Min(here.Item1, next.Item1);
                var nextX = Math.Max(here.Item1, next.Item1);
                // Same y; going left or right
                for (var x = hereX; x <= nextX; x++)
                {
                    edge.Add((x, here.Item2));
                }
            }
            edges.Add(edge);
        }
        var edgeCoordinates = edges.SelectMany(c => c).ToHashSet();
        var allTiles = new HashSet<Coord>([.. cornerCoordinates, .. edgeCoordinates]);

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

                // Skip skinny rectangles
                if (minX >= maxX - 1 || minY >= maxY - 1)
                    continue;

                var rectArea = (long)(maxX - minX + 1) * (maxY - minY + 1);
                if (rectArea < largestArea || rectArea < 169172640)
                    continue;

                var center = ((minX + maxX) / 2, (maxY + minY) / 2);
                var anyIntersect = edges.Any(l => DoesLineIntersectWithRectangle(item1, item2, l));
                if (anyIntersect)
                    continue;

                var centerIsInsidePolygon = IsInsidePolygon(allTiles, center, biggestX);
                if (centerIsInsidePolygon)
                {
                    largestArea = rectArea;
                    Console.WriteLine($"{item1},{item2}: {largestArea}");
                }
            }
        }
        return largestArea;
    }

    private static bool DoesLineIntersectWithRectangle(Coord item1, Coord item2, List<Coord> line)
    {
        var minX = Math.Min(item1.Item1, item2.Item1);
        var maxX = Math.Max(item1.Item1, item2.Item1);
        var minY = Math.Min(item1.Item2, item2.Item2);
        var maxY = Math.Max(item1.Item2, item2.Item2);
        return line.Any(l => l.Item1 > minX && l.Item1 < maxX && l.Item2 > minY && l.Item2 < maxY);
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

    [GeneratedRegex(@"(\d+),(\d+)")]
    private static partial Regex CoordinateRegex();
}
