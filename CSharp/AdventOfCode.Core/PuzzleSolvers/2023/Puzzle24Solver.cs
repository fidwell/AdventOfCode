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

    public string SolvePartTwo(string input) => throw new NotImplementedException();
}
