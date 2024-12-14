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

        var isExample = robots.Count() <= 12;
        var areaWidth = isExample ? 11 : AreaWidth;
        var areaHeight = isExample ? 7 : AreaHeight;

        for (int t = 0; t < 100; t++)
        {
            foreach (var robot in robots)
                robot.Move(areaWidth, areaHeight);
        }

        var groups = robots.GroupBy(r => r.Quadrant(areaWidth, areaHeight));

        return groups.Where(g => g.Key >= 0).Aggregate(1, (total, g) => total * g.Count()).ToString();
    }

    // This one isn't really solvable the "normal" way...
    // Output the state at each tick, look for patterns,
    // find a cycle that looks different from the rest.
    public string SolvePartTwo(string input)
    {
        var robots = SetUpRobots(input);
        const int answer = 7623;

        for (int t = 0; t < answer; t++)
        {
            foreach (var robot in robots)
                robot.Move(AreaWidth, AreaHeight);
        }

        if (ShouldPrint)
        {
            Print(robots);
        }

        return answer.ToString();
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

    private class Robot(int X, int Y, int Vx, int Vy)
    {
        public bool IsAt(int x, int y) => X == x && Y == y;

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
