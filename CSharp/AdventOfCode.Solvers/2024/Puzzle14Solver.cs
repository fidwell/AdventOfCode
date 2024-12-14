using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle14Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var robots = input.SplitByNewline().Select(l =>
        {
            var matches = Regexes.Integer().Matches(l).Select(m => int.Parse(m.Value)).ToArray();
            return new Robot(matches[0], matches[1], matches[2], matches[3]);
        }).ToList();

        var isExample = robots.Count <= 12;
        var areaWidth = isExample ? 11 : 101;
        var areaHeight = isExample ? 7 : 103;
        const int ticks = 100;

        for (int t = 0; t < ticks; t++)
        {
            foreach (var robot in robots)
                robot.Move(areaWidth, areaHeight);
        }

        var groups = robots.GroupBy(r => r.Quadrant(areaWidth, areaHeight));

        return groups.Where(g => g.Key >= 0).Aggregate(1, (total, g) => total * g.Count()).ToString();
    }

    public string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private class Robot(int X, int Y, int Vx, int Vy)
    {
        public void Move(int areaWidth, int areaHeight)
        {
            X += Vx;
            Y += Vy;

            if (X < 0) X += areaWidth;
            if (X >= areaWidth) X -= areaWidth;
            if (Y < 0) Y += areaHeight;
            if (Y >= areaHeight) Y -= areaHeight;
        }

        public int Quadrant(int areaWidth, int areaHeight)
        {
            // discount robots in the middle
            if (X == areaWidth / 2 || Y == areaHeight / 2)
                return -1;

            var isOnLeft = X < areaWidth / 2;
            var isOnTop = Y < areaHeight / 2;
            return (isOnLeft ? 0 : 1) + (isOnTop ? 0 : 2); // sort of a bitmask
        }
    }
}
