using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public partial class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();

        long testAreaFrom = 7;
        long testAreaTo = 27;
        if (lines[0].Length > 30)
        {
            testAreaFrom = 200000000000000;
            testAreaTo = 400000000000000;
        }

        var hailstones = lines.Select(l =>
        {
            var portions = l.SplitAndTrim('@');
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

    public override string SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline();
        var hailstones = lines
            .Take(3)
            .Select(l =>
            {
                var portions = l.SplitAndTrim('@');
                return new Ray3d(new Point3d(portions[0]), new Point3d(portions[1]));
            }).ToList();

        var (velocity, position) = Solve2(hailstones, lines.Count() < 10 ? 3 : 275);
        var stone = new Ray3d(position, velocity);
        return (stone.Position0.X + stone.Position0.Y + stone.Position0.Z).ToString();
    }

    private static (Point3d, Point3d) Solve2(List<Ray3d> hailstones, int velocityRange)
    {
        // The rock stands still, and each hailstone's
        // velocity is adjusted by what the rock's
        // velocity would have been.

        for (var x = -velocityRange; x < velocityRange; x++)
        {
            for (var y = -velocityRange; y < velocityRange; y++)
            {
                for (var z = -velocityRange; z < velocityRange; z++)
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
