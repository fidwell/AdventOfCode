using System.Diagnostics;
using AdventOfCode.Core.MathUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public partial class Puzzle24Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var testArea = lines.First().Split(" ").Select(long.Parse);
        var testAreaFrom = testArea.First();
        var testAreaTo = testArea.Last();

        var hailstones = lines.Skip(1).Select(l =>
        {
            var portions = l.Split('@', StringSplitOptions.TrimEntries);
            return new Ray3d(new Point3d(portions[0]), new Point3d(portions[1]));
        }).ToList();

        var count = 0;
        for (var i = 0; i < hailstones.Count - 1; i++)
        {
            for (var j = i + 1; j < hailstones.Count; j++)
            {
                count += hailstones[i].CollidesWithInXy(hailstones[j], testAreaFrom, testAreaTo) ? 1 : 0;
            }
        }

        return count.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var hailstones = lines
            .Skip(1)
            .Take(3)
            .Select(l =>
            {
                var portions = l.Split('@', StringSplitOptions.TrimEntries);
                return new Ray3d(new Point3d(portions[0]), new Point3d(portions[1]));
            }).ToList();

        var (velocity, position) = Solve2(hailstones);
        var stone = new Ray3d(position, velocity);

        Trace.WriteLine($"Stone is position {position.X},{position.Y},{position.Z} velocity {velocity.X},{velocity.Y},{velocity.Z}");

        for (var i = 0; i < hailstones.Count; i++)
        {
            var (collision, t) = hailstones[i].Collision3d(stone);
            if (collision is not null)
                Trace.WriteLine($"Hailstone {i} collides with stone at position {collision.X:0},{collision.Y:0},{collision.Z:0} and time {t}");
            else
                Trace.WriteLine($"No collision found for hailstone {i}");
        }

        return (stone.Position0.X + stone.Position0.Y + stone.Position0.Z).ToString();
    }

    private (Point3d, Point3d) Solve2(List<Ray3d> hailstones)
    {
        // The rock stands still, and each hailstone's
        // velocity is adjusted by what the rock's
        // velocity would have been.

        const int velocityMin = -300;
        const int velocityMax = 300;
        for (var x = velocityMin; x < velocityMax; x++)
        {
            for (var y = velocityMin; y < velocityMax; y++)
            {
                for (var z = velocityMin; z < velocityMax; z++)
                {
                    var rockVelocity = new Point3d(x, y, z);
                    var adjustedHailstones = hailstones.Select(h => h.Minus(rockVelocity)).ToList();

                    var (collision0with1, t1) = adjustedHailstones[0].Collision3d(adjustedHailstones[1]);
                    var (collision0with2, t2) = adjustedHailstones[0].Collision3d(adjustedHailstones[2]);

                    if (collision0with1 is not null &&
                        collision0with2 is not null &&
                        collision0with1.Equals(collision0with2))
                        return (rockVelocity, collision0with1);
                }
            }
        }

        throw new Exception("No point found");
    }
}
