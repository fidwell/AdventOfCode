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

    private record struct Tile(int Id, Coord Coord);

    //public object SolvePartTwoAlternate(string input)
    public override object SolvePartTwo(string input)
    {
        // Step 1: Parse data

        var regex = new Regex(@"(\d+),(\d+)");
        var redTiles = input.SplitByNewline().Select((l, i) =>
        {
            var match = regex.Match(l);
            return new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }).ToArray();

        // Find lines
        var lines = new List<List<Coord>>();
        for (var i = 0; i < redTiles.Length; i++)
        {
            var nextI = i == redTiles.Length - 1 ? 0 : i + 1;

            var here = redTiles[i];
            var next = redTiles[nextI];

            var line = new List<Coord>();

            if (here.Item1 == next.Item1)
            {
                var hereY = Math.Min(here.Item2, next.Item2);
                var nextY = Math.Max(here.Item2, next.Item2);
                // Same x; going up or down
                for (var y = hereY; y <= nextY; y++)
                {
                    line.Add((here.Item1, y));
                }
            }
            else
            {
                var hereX = Math.Min(here.Item1, next.Item1);
                var nextX = Math.Max(here.Item1, next.Item1);
                // Same y; going left or right
                for (var x = hereX; x <= nextX; x++)
                {
                    line.Add((x, here.Item2));
                }
            }
            lines.Add(line);
        }

        // Iterate over all rectangles
        var largestSize = 0L;
        for (var i = 0; i < redTiles.Length - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Length; j++)
            {
                var size = Math.Abs((redTiles[i].Item1 - redTiles[j].Item1 + 1) *
                    (redTiles[i].Item2 - redTiles[j].Item2 + 1));
                //Console.Write($"Checking {redTiles[i]} to {redTiles[j]} (size {size})...");

                if (size < largestSize)
                {
                    //Console.WriteLine("too small");
                    continue;
                }

                var isValid = !lines.Any(l => Intersect(redTiles[i], redTiles[j], l));
                if (isValid)
                {
                    Console.WriteLine(size);
                    largestSize = size;
                }
            }
        }

        return largestSize;
    }

    public static bool Intersect(Coord item1, Coord item2, List<Coord> line)
    {
        var minX = Math.Min(item1.Item1, item2.Item1);
        var maxX = Math.Max(item1.Item1, item2.Item1);
        var minY = Math.Min(item1.Item2, item2.Item2);
        var maxY = Math.Max(item1.Item2, item2.Item2);
        return line.Any(l => l.Item1 > minX && l.Item1 < maxX && l.Item2 > minY && l.Item2 < maxY);
    }

    //public override object SolvePartTwo(string input)
    public object SolvePartTwoAlternate(string input)
    {
        // Step 1: Parse data

        var regex = new Regex(@"(\d+),(\d+)");
        var redTiles = input.SplitByNewline().Select((l, i) =>
        {
            var match = regex.Match(l);
            return new Tile(i, new Coord(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
        }).ToArray();

        // Step 1.5: Squish data
        var originalData = redTiles.Select(t => t.Coord).ToArray();

        Console.WriteLine("Squishing xs...");
        for (var x = 0; x < 99999; x++)
        {
            var furtherTiles = redTiles.Where(t => t.Coord.Item1 > x);
            if (!furtherTiles.Any())
                break;

            var unchangedTiles = redTiles.Where(t => t.Coord.Item1 <= x);
            var minX = furtherTiles.Min(t => t.Coord.Item1);
            var diff = minX - x;
            redTiles = [.. unchangedTiles, .. furtherTiles.Select(t => new Tile(t.Id, new Coord(t.Coord.Item1 - diff, t.Coord.Item2)))];
        }
        Console.WriteLine("Squishing ys...");
        for (var y = 0; y < 99999; y++)
        {
            var furtherTiles = redTiles.Where(t => t.Coord.Item2 > y);
            if (!furtherTiles.Any())
                break;

            var unchangedTiles = redTiles.Where(t => t.Coord.Item2 <= y);
            var minY = furtherTiles.Min(t => t.Coord.Item2);
            var diff = minY - y;
            redTiles = [.. unchangedTiles, .. furtherTiles.Select(t => new Tile(t.Id, new Coord(t.Coord.Item1, t.Coord.Item2 - diff)))];
        }

        redTiles = [.. redTiles.OrderBy(t => t.Id)];

        // Step 2: Find loop

        var maxX = redTiles.Max(t => t.Coord.Item1);
        var maxY = redTiles.Max(t => t.Coord.Item2);

        var coloredTiles = new HashSet<Coord>();

        for (var i = 0; i < redTiles.Length; i++)
        {
            var here = redTiles[i];
            coloredTiles.Add(here.Coord);

            var next = i == redTiles.Length - 1
                ? redTiles[0]
                : redTiles[i + 1];
            if (here.Coord.Item1 == next.Coord.Item1)
            {
                var hereY = Math.Min(here.Coord.Item2, next.Coord.Item2);
                var nextY = Math.Max(here.Coord.Item2, next.Coord.Item2);
                // Same x; going up or down
                for (var y = hereY; y <= nextY; y++)
                {
                    coloredTiles.Add((here.Coord.Item1, y));
                }
            }
            else
            {
                var hereX = Math.Min(here.Coord.Item1, next.Coord.Item1);
                var nextX = Math.Max(here.Coord.Item1, next.Coord.Item1);
                // Same y; going left or right
                for (var x = hereX; x <= nextX; x++)
                {
                    coloredTiles.Add((x, here.Coord.Item2));
                }
            }
        }

        // Step 3: Find largest valid rectangle

        var largestSize = 0L;

        for (var i = 0; i < redTiles.Length - 1; i++)
        {
            for (var j = i + 1; j < redTiles.Length; j++)
            {
                var size = Math.Abs((originalData[i].Item1 - originalData[j].Item1 + 1) *
                    (originalData[i].Item2 - originalData[j].Item2 + 1));

                //Console.Write($"Checking {originalData[i]} to {originalData[j]} (size {size})...");

                if (size < largestSize)
                {
                    //Console.WriteLine("too small");
                    continue;
                }

                var isValid = AreAllCoordinatesInsidePolygon(coloredTiles, redTiles[i].Coord, redTiles[j].Coord, maxX + 1);
                //Console.WriteLine(isValid);

                if (isValid)
                {
                    Console.WriteLine(size);
                    largestSize = size;
                }
            }
            var percentDone = i / (redTiles.Length - 1.0);
            Console.WriteLine($"{percentDone:P} done");
        }

        // Print design
        if (ShouldPrint)
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

        // 2147475684 is TOO HIGH!
        // 1516863759 is wrong
        //  169172640 is TOO LOW
        return largestSize;
    }

    private bool AreAllCoordinatesInsidePolygon(HashSet<Coord> vertices, Coord item1, Coord item2, int maxX)
    {
        return CoordinatesOfRectangleDefinedBy(item1, item2).All(c => IsInsidePolygon(vertices, c, maxX));
        //var v = VerticesOfRectangleDefinedBy(item1, item2);
        //return v.All(c => IsInsidePolygon(vertices, c, maxX));
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
            if (vertices.Contains((xr, pointToTest.Item2)) &&
                vertices.Contains((xr - 1, pointToTest.Item2)))
            {
                // Coincides with an edge
                boundariesHit--;
                while (vertices.Contains((xr, pointToTest.Item2)))
                    xr++;
            }
            else if (vertices.Contains((xr, pointToTest.Item2)))
            {
                // Crossing an edge
                boundariesHit++;
            }
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
