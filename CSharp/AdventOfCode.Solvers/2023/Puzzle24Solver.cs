using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public partial class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();

        var isExample = lines[0].Length <= 30;
        var testAreaFrom = isExample ? 7 : 200000000000000L;
        var testAreaTo = isExample ? 27 : 400000000000000L;

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
            .Select(l =>
            {
                var portions = l.SplitAndTrim('@');
                return new Ray3d(new Point3d(portions[0]), new Point3d(portions[1]));
            }).ToList();

        var isExample = lines[0].Length <= 30;
        var maxVelocity = isExample ? 5 : 1000;

        var (velocity, position) = Solve2(hailstones, maxVelocity);
        var stone = new Ray3d(position, velocity);
        return (stone.Position0.X + stone.Position0.Y + stone.Position0.Z).ToString();
    }

    private static (Point3d, Point3d) Solve2(List<Ray3d> hailstones, int maxVelocity)
    {
        // if two hailstones are moving in the same direction with the same velocity,
        // then the rock can only be moving a certain number of integer velocities to
        // hit them both. The actual velocity must satisfy the equation
        // (DistanceDifference % (RockVelocity - HailVelocity) = 0) and there are only
        // so many that will. Do that for every pair in every direction and you have
        // the velocity of your rock.
        // https://www.reddit.com/comments/18pnycy/_/keqf8uq/

        IEnumerable<int> possibleXs = [];
        var groupX = hailstones.GroupBy(h => h.Velocity.X).Where(g => g.Count() > 1);
        if (groupX.Any())
        {
            var hailstonesWithCommonXvelocity = groupX.First().ToList();
            var distanceDifferenceX = Math.Abs(hailstonesWithCommonXvelocity[0].Position0.X - hailstonesWithCommonXvelocity[1].Position0.X);
            possibleXs = Enumerable.Range(-maxVelocity, maxVelocity * 2 + 1)
                .Where(i => distanceDifferenceX % (i - hailstonesWithCommonXvelocity[0].Velocity.X) == 0);
        }

        IEnumerable<int> possibleYs = [];
        var groupY = hailstones.GroupBy(h => h.Velocity.Y).Where(g => g.Count() > 1);
        if (groupY.Any())
        {
            var hailstonesWithCommonYvelocity = groupY.First().ToList();
            var distanceDifferenceY = Math.Abs(hailstonesWithCommonYvelocity[0].Position0.Y - hailstonesWithCommonYvelocity[1].Position0.Y);
            possibleYs = Enumerable.Range(-maxVelocity, maxVelocity * 2 + 1)
                .Where(i => distanceDifferenceY % (i - hailstonesWithCommonYvelocity[0].Velocity.Y) == 0);
        }

        IEnumerable<int> possibleZs = [];
        var groupZ = hailstones.GroupBy(h => h.Velocity.Z).Where(g => g.Count() > 1);
        if (groupZ.Any())
        {
            var hailstonesWithCommonZvelocity = groupZ.First().ToList();
            var distanceDifferenceZ = Math.Abs(hailstonesWithCommonZvelocity[0].Position0.Z - hailstonesWithCommonZvelocity[1].Position0.Z);
            possibleZs = Enumerable.Range(-maxVelocity, maxVelocity * 2 + 1)
                .Where(i => distanceDifferenceZ % (i - hailstonesWithCommonZvelocity[0].Velocity.Z) == 0);
        }

        foreach (var x in possibleXs)
        {
            foreach (var y in possibleYs)
            {
                foreach (var z in possibleZs)
                {
                    var rockVelocity = new Point3d(x, y, z);
                    var adjustedHailstones = hailstones.Select(h => h - rockVelocity).ToList();

                    var (collision0with1, t1) = adjustedHailstones[0].Collision3d(adjustedHailstones[1]);
                    var (collision0with2, t2) = adjustedHailstones[0].Collision3d(adjustedHailstones[2]);

                    if (collision0with1 is not null &&
                        collision0with2 is not null &&
                        collision0with1.Equals(collision0with2))
                        return (rockVelocity, collision0with1.Value);
                }
            }
        }

        throw new Exception("No point found");
    }
}
