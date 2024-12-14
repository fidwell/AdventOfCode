using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle14Solver : IPuzzleSolver
{
    const int AreaWidth = 101;
    const int AreaHeight = 103;

    // Disabled for benchmarking.
    // Toggle on if you want to see the output! 
    const bool ShouldPrint = false;

    public string SolvePartOne(string input)
    {
        var robots = SetUpRobots(input);

        var isExample = robots.Count <= 12;
        var areaWidth = isExample ? 11 : AreaWidth;
        var areaHeight = isExample ? 7 : AreaHeight;

        foreach (var robot in robots)
            robot.Move(areaWidth, areaHeight, 100);

        return robots.GroupBy(r => r.Quadrant(areaWidth, areaHeight))
            .Where(g => g.Key >= 0).Aggregate(1, (total, g) => total * g.Count()).ToString();
    }

    // This one isn't really solvable the "normal" way.
    // I first did it by output the state at each tick,
    // looking for outliers, and finding a cycle that had
    // suspicious patterns. I could then reduce my search
    // to just ticks that fit the pattern (in my case,
    // (t + 48) % 101 == 0).
    //
    // Another way (which works but isn't implied in the
    // puzzle) is to check to see if all robots don't
    // overlap. I didn't solve it that way originally,
    // but I've done it here anyway just so show
    // something programmatic.
    public string SolvePartTwo(string input)
    {
        var robots = SetUpRobots(input);

        // All robots will be back in their
        // original location after some number
        // of iterations.
        var maximumAnswer = AreaHeight * AreaWidth;

        var ticks = 0;
        while (ticks < maximumAnswer)
        {
            foreach (var robot in robots)
                robot.Move(AreaWidth, AreaHeight, 1);
            ticks++;
            if (robots.Select(r => (r.X, r.Y)).Distinct().Count() == robots.Count)
            {
                if (ShouldPrint)
                    Print(robots);

                return ticks.ToString();
            }
        }

        throw new Exception("No pattern found");
    }

    private static List<Robot> SetUpRobots(string input) =>
        input.SplitByNewline().Select(l =>
        {
            var matches = Regexes.Integer().Matches(l).Select(m => int.Parse(m.Value)).ToArray();
            return new Robot(matches[0], matches[1], matches[2], matches[3]);
        }).ToList();

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
    }

    private class Robot(int x, int y, int Vx, int Vy)
    {
        public int X { get; private set; } = x;
        public int Y { get; private set; } = y;

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
