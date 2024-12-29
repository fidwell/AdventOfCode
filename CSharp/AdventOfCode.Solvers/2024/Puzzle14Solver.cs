using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle14Solver : PuzzleSolver
{
    const int AreaWidth = 101;
    const int AreaHeight = 103;

    public override string SolvePartOne(string input)
    {
        var robots = SetUpRobots(input);

        var isExample = robots.Count <= 12;
        var areaWidth = isExample ? 11 : AreaWidth;
        var areaHeight = isExample ? 7 : AreaHeight;

        foreach (var robot in robots)
            robot.Move(areaWidth, areaHeight, 100);

        return robots.GroupBy(r => r.Quadrant(areaWidth, areaHeight))
            .Where(g => g.Key >= 0).Select(g => g.Count()).Product().ToString();
    }

    // This one isn't really solvable the "normal" way.
    // I first did it by output the state at each tick,
    // looking for outliers, and finding a cycle that had
    // suspicious patterns. I could then reduce my search
    // to just ticks that fit the pattern (in my case,
    // (t + 48) % 101 == 0).
    //
    // Another way is to consider that if the robots form
    // a picture, they'll likely be densely clustered, as
    // opposed to pseudo-randomly spread about the area.
    // So, find the time at which the variance of their
    // locations is "unnaturally" low.
    public override string SolvePartTwo(string input)
    {
        var robots = SetUpRobots(input);
        var startingVariance = Variance(robots);

        for (var tick = 1; tick < AreaHeight * AreaWidth; tick++)
        {
            foreach (var robot in robots)
                robot.Move(AreaWidth, AreaHeight, 1);

            if (Variance(robots) < startingVariance / 2)
            {
                if (ShouldPrint)
                {
                    Print(robots);
                }
                return tick.ToString();
            }
        }

        throw new Exception("Couldn't find a solution");
    }

    private static List<Robot> SetUpRobots(string input) =>
        input.SplitByNewline().Select(l =>
        {
            var matches = Regexes.Integer().Matches(l).Select(m => int.Parse(m.Value)).ToArray();
            return new Robot(matches[0], matches[1], matches[2], matches[3]);
        }).ToList();

    private static double Variance(IEnumerable<Robot> robots)
    {
        var xs = robots.Select(r => r.X);
        var xMean = xs.Average();
        var xVariance = xs.Average(x => (x - xMean) * (x - xMean));
        var ys = robots.Select(r => r.Y);
        var yMean = ys.Average();
        var yVariance = ys.Average(y => (y - yMean) * (y - yMean));
        return xVariance + yVariance;
    }

    private static void Print(IEnumerable<Robot> robots)
    {
        for (var y = 0; y < AreaHeight; y++)
        {
            for (var x = 0; x < AreaWidth; x++)
            {
                Console.Write(robots.Any(r => r.IsAt(x, y)) ? "X" : ".");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private class Robot(int x0, int y0, int Vx, int Vy)
    {
        public int X { get; private set; } = x0;
        public int Y { get; private set; } = y0;

        public bool IsAt(int x, int y) => X == x && Y == y;

        public void Move(int areaWidth, int areaHeight, int amount)
        {
            X = MathExtensions.Modulo(X + Vx * amount, areaWidth);
            Y = MathExtensions.Modulo(Y + Vy * amount, areaHeight);
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
