using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle09Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var regex = new Regex(@"(\d+),(\d+)");
        var redTiles = input.SplitByNewline().Select(l =>
        {
            var match = regex.Match(l);
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

        var regex = new Regex(@"(\d+),(\d+)");
        var redTiles = input.SplitByNewline().Select(l =>
        {
            var match = regex.Match(l);
            return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }).ToList();

        // Step 2: Find loop

        var maxX = redTiles.Max(t => t.Item1);
        var maxY = redTiles.Max(t => t.Item2);

        var coloredTiles = new HashSet<Coord>();

        for (var i = 0; i < redTiles.Count; i++)
        {
            var here = redTiles[i];
            coloredTiles.Add(here);

            var next = i == redTiles.Count - 1
                ? redTiles[0]
                : redTiles[i + 1];
            if (here.Item1 == next.Item1)
            {
                var hereY = Math.Min(here.Item2, next.Item2);
                var nextY = Math.Max(here.Item2, next.Item2);
                // Same x; going up or down
                for (var y = hereY; y <= nextY; y++)
                {
                    coloredTiles.Add((here.Item1, y));
                }
            }
            else
            {
                var hereX = Math.Min(here.Item1, next.Item1);
                var nextX = Math.Max(here.Item1, next.Item1);
                // Same y; going left or right
                for (var x = hereX; x <= nextX; x++)
                {
                    coloredTiles.Add((x, here.Item2));
                }
            }
        }

        var largestSize = 0L;

        for (var i = 0; i < redTiles.Count - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Count; j++)
            {
                var size = Math.Abs((redTiles[i].Item1 - redTiles[j].Item1 + 1) *
                    (redTiles[i].Item2 - redTiles[j].Item2 + 1));

                if (size < largestSize)
                    continue;

                Console.Write($"Checking {redTiles[i]} to {redTiles[j]} (size {size})...");
                var isValid = AreAllCoordinatesInsidePolygon(coloredTiles, redTiles[i], redTiles[j], maxX + 1);
                Console.WriteLine(isValid);

                if (isValid && size > largestSize)
                {
                    Console.WriteLine(size);
                    largestSize = size;
                }
            }
        }
        return largestSize;

        /*
        // Print floor
        if (maxY < 100)
        {
            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    Console.Write(coloredTiles.Contains((x, y)) ? 'O' : '.');
                }
                Console.WriteLine();
            }
        }

        return 0;
        */
    }

    private bool AreAllCoordinatesInsidePolygon(HashSet<Coord> vertices, Coord item1, Coord item2, int maxX)
    {
        //return CoordinatesOfRectangleDefinedBy(item1, item2).All(c => IsInsidePolygon(vertices, c, maxX));
        var v = VerticesOfRectangleDefinedBy(item1, item2);
        return v.All(c => IsInsidePolygon(vertices, c, maxX));
    }

    private static HashSet<Coord> VerticesOfRectangleDefinedBy(Coord item1, Coord item2) =>
        [(item1.Item1, item1.Item2),
         (item1.Item1, item2.Item2),
         (item2.Item1, item1.Item2),
         (item2.Item1, item2.Item2)];

    private static HashSet<Coord> CoordinatesOfRectangleDefinedBy(Coord item1, Coord item2)
    {
        var minX = Math.Min(item1.Item1, item2.Item1);
        var maxX = Math.Max(item1.Item1, item2.Item1);
        var minY = Math.Min(item1.Item2, item2.Item2);
        var maxY = Math.Max(item1.Item2, item2.Item2);
        var xs = Enumerable.Range(minX, maxX - minX + 1);
        var ys = Enumerable.Range(minY, maxY - minY + 1);
        var top = xs.Select(x => (x, minY));
        var bottom = xs.Select(x => (x, maxY));
        var left = ys.Select(y => (minX, y));
        var right = ys.Select(y => (maxX, y));
        return [.. top, .. bottom, .. left, .. right];
    }

    private readonly HashSet<Coord> KnownInside = [];
    private readonly HashSet<Coord> KnownOutside = [];


    // Doesn't work. Old solution was inefficient.
    private bool IsInsidePolygon(HashSet<Coord> vertices, Coord pointToTest, int maxX)
    {
        if (vertices.Contains(pointToTest))
            return true;
        if (KnownInside.Contains(pointToTest))
            return true;
        if (KnownOutside.Contains(pointToTest))
            return false;

        // Raycast to find if it's in polygon
        var boundariesHit = 0;
        for (var xr = pointToTest.Item1 + 1; xr <= maxX + 1; xr++)
        {
            if (vertices.Contains((xr, pointToTest.Item2)))
                boundariesHit++;
        }

        var result = boundariesHit % 2 == 1;
        if (result)
        {
            KnownInside.Add(pointToTest);
        }
        else
        {
            KnownOutside.Add(pointToTest);
        }
        return result;
    }
}
