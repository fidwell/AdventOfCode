using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading.Tasks.Sources;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle22Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var bricks = input.Split(Environment.NewLine)
            .Select((l, i) => new Brick(l, i))
            .OrderBy(b => b.Point1.Z)
            .ToList();

        // Not general, but this is my input
        const int maxX = 9;
        const int maxY = 9;
        var occupiedZs =
            Enumerable.Range(0, maxX).SelectMany(x =>
            Enumerable.Range(0, maxY).Select(y =>
                (x, y, 0)))
            .ToList();

        // Part 1. Fall
        foreach (var brick in bricks)
        {
            // Spaces under this cube are all elements in occupiedZs (oz) where
            // oz.X is in allcubes.x and oz.Y is in allcubes.y.
            // We can fall down to one more than the max Z in that list.
            var spacesUnder = occupiedZs.Where(oz =>
                brick.AllCubes.Select(c => c.X).Contains(oz.x) &&
                brick.AllCubes.Select(c => c.Y).Contains(oz.y));
            var maxZ = spacesUnder.Max(oz => oz.Item3);
            var minZOfThis = brick.Point1.Z; // should be enforced by brick constructor
            var fallAmount = minZOfThis - maxZ - 1;
            brick.Fall(fallAmount);

            foreach (var p in brick.AllCubes)
            {
                var occupiedZHere = occupiedZs.Single(oz => oz.x == p.X && oz.y == p.Y);
                occupiedZs.Remove(occupiedZHere);
                occupiedZs.Add((p.X, p.Y, Math.Max(occupiedZHere.Item3, p.Z)));
            }
        }

        // 2. Create dependency graph
        var dependencyGraph = new Dictionary<int, List<int>>();
        for (var i = 0; i < bricks.Count; i++)
        {
            var thisBrick = bricks[i];
            for (var j = i + 1; j < bricks.Count; j++)
            {
                var thatBrick = bricks[j];
                if (Supports(thisBrick, thatBrick))
                {
                    if (!dependencyGraph.ContainsKey(thisBrick.Id))
                    {
                        dependencyGraph.Add(thisBrick.Id, []);
                    }
                    dependencyGraph[thisBrick.Id].Add(thatBrick.Id);
                }
            }
        }

        return "";
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static bool Supports(Brick lower, Brick higher) =>
        lower.AllCubes.Any(lc => higher.AllCubes.Any(hc => 
            hc.X == lc.X &&
            hc.Y == lc.Y &&
            hc.Z == lc.Z + 1));

    private record Brick
    {
        public readonly int Id;
        public readonly Point3d Point1;
        public readonly Point3d Point2;
        public readonly Point3d[] AllCubes;

        public Brick(string input, int id)
        {
            Id = id;
            var portions = input.Split('~');
            var p1 = new Point3d(portions.First());
            var p2 = new Point3d(portions.Last());
            Point1 = p1.Z < p2.Z ? p1 : p2;
            Point2 = Point1 == p1 ? p2 : p1;
            AllCubes = GetAllPoints().ToArray();
        }

        public char IdChar => (char)('A' + Id);

        public void Fall(int amount)
        {
            Point1.Z -= amount;
            Point2.Z -= amount;
            foreach (var point in AllCubes)
            {
                point.Z -= amount;
            }
        }

        private IEnumerable<Point3d> GetAllPoints()
        {
            if (Point1.X != Point2.X)
            {
                var start = Point1.X < Point2.X ? Point1.X : Point2.X;
                var end = Point1.X > Point2.X ? Point1.X : Point2.X;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(x => new Point3d(x, Point1.Y, Point1.Z));
            }
            else if (Point1.Y != Point2.Y)
            {
                var start = Point1.Y < Point2.Y ? Point1.Y : Point2.Y;
                var end = Point1.Y > Point2.Y ? Point1.Y : Point2.Y;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(y => new Point3d(Point1.X, y, Point1.Z));
            }
            else if (Point1.Z != Point2.Z)
            {
                var start = Point1.Z < Point2.Z ? Point1.Z : Point2.Z;
                var end = Point1.Z > Point2.Z ? Point1.Z : Point2.Z;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(z => new Point3d(Point1.X, Point1.Y, z));
            }
            throw new NotImplementedException();
        }
    }

    private record Point3d
    {
        public readonly int X;
        public readonly int Y;
        public int Z;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3d(string input)
        {
            var portions = input.Split(',').Select(int.Parse).ToArray();
            X = portions[0];
            Y = portions[1];
            Z = portions[2];
        }
    }
}
