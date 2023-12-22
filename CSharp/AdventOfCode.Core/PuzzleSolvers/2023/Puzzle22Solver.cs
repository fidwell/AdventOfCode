using System;
using System.Diagnostics;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle22Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var bricks = input.Split(Environment.NewLine)
            .Select((l, i) => new Brick(l, i))
            .OrderBy(b => b.Point1.Z)
            .ToList();

        var stableBrickIds = new HashSet<int>();
        while (stableBrickIds.Count < bricks.Count)
        {
            foreach (var brick in bricks)
            {
                if (stableBrickIds.Contains(brick.Id))
                    continue;

                if (brick.Point1.Z == 1 || brick.Point2.Z == 1)
                {
                    stableBrickIds.Add(brick.Id);
                    continue;
                }

                while (!IsSupported(brick, bricks))
                {
                    brick.Fall();
                }

                var supporters = Supporters(brick, bricks);
                if (supporters.Any(s => stableBrickIds.Contains(s.Id)))
                {
                    stableBrickIds.Add(brick.Id);
                }
            }
        }

        return bricks.Count(brick =>
        {
            var newBricks = bricks.Where(b => b != brick).ToList();
            return newBricks.All(b => IsSupported(b, newBricks));
        }).ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static bool IsSupported(Brick brick, List<Brick> bricks) =>
        brick.Point1.Z == 1 || brick.Point2.Z == 1 || bricks.Any(a => a != brick && Supports(a, brick));

    private static IEnumerable<Brick> Supporters(Brick brick, List<Brick> bricks) =>
        bricks.Where(a => a != brick && Supports(a, brick));


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

        public Brick(string input, int id)
        {
            Id = id;
            var portions = input.Split('~');
            Point1 = new Point3d(portions.First());
            Point2 = new Point3d(portions.Last());
        }

        public char IdChar => (char)('A' + Id);

        public Point3d[] AllCubes =>
            Enumerable.Range(Point1.X, Point2.X - Point1.X + 1).SelectMany(x =>
            Enumerable.Range(Point1.Y, Point2.Y - Point1.Y + 1).SelectMany(y =>
            Enumerable.Range(Point1.Z, Point2.Z - Point1.Z + 1).Select(z =>
                new Point3d(x, y, z)))).ToArray();

        public void Fall()
        {
            Point1.Z--;
            Point2.Z--;
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
