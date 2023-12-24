using System.Diagnostics;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle24Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var testArea = lines.First().Split(" ").Select(long.Parse);
        var testAreaFrom = testArea.First();
        var testAreaTo = testArea.Last();

        var hailstones = lines.Skip(1).Select(l => new Hailstone(l)).ToList();

        var count = 0L;
        for (var i = 0; i < hailstones.Count - 1; i++)
        {
            for (var j = i + 1; j < hailstones.Count; j++)
            {
                count += hailstones[i].MightCollideWith2d(hailstones[j], testAreaFrom, testAreaTo) ? 1 : 0;
            }
        }

        return count.ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private class Hailstone
    {
        public Point3d Position0;
        public Point3d Velocity;

        public double SlopeXy;

        public Hailstone(string input)
        {
            var portions = input.Split('@', StringSplitOptions.TrimEntries);
            Position0 = new Point3d(portions[0]);
            Velocity = new Point3d(portions[1]);

            // y - y1 = m (x - x1)
            // y = m (x - x1) + y1

            SlopeXy = (double)Velocity.Y / Velocity.X;
        }

        public bool MightCollideWith2d(Hailstone other, long testAreaFrom, long testAreaTo)
        {
            // Parallel; will never intersect
            if (SlopeXy == other.SlopeXy)
                return false;

            // Find point of intersection
            var x = (SlopeXy * Position0.X - other.SlopeXy * other.Position0.X - Position0.Y + other.Position0.Y) / (SlopeXy - other.SlopeXy);
            var y = SlopeXy * (x - Position0.X) + Position0.Y;

            // Also ensure that the point is in the future for both lines
            var isThisInFuture = (x - Position0.X) / Velocity.X > 0;
            var isOtherInFuture = (x - other.Position0.X) / other.Velocity.X > 0;

            if (!isThisInFuture || !isOtherInFuture)
                return false;

            return x >= testAreaFrom && y >= testAreaFrom && x <= testAreaTo && y <= testAreaTo;
        }
    }

    private class Point3d
    {
        public readonly long X;
        public readonly long Y;
        public readonly long Z;

        public Point3d(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3d(string input)
        {
            var portions = input.Split(',', StringSplitOptions.TrimEntries).Select(long.Parse).ToArray();
            X = portions[0];
            Y = portions[1];
            Z = portions[2];
        }
    }
}
