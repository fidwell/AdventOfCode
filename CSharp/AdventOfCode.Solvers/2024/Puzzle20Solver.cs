using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle20Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var maze = new CharacterMatrix(input);
        var isExample = maze.Width == 15;
        return Solve(maze, isExample ? 1 : 100, 2);
    }

    public override string SolvePartTwo(string input)
    {
        var maze = new CharacterMatrix(input);
        var isExample = maze.Width == 15;
        return Solve(maze, isExample ? 50 : 100, 20);
    }

    public string Solve(CharacterMatrix maze, int targetSavings, int cheatMax)
    {
        var stepCosts = CalculateStepCosts(maze);
        var end = maze.SingleMatch('E');
        var totalSteps = stepCosts[end.Item1, end.Item2];

        if (ShouldPrint)
        {
            Console.WriteLine($"Without cheating, solved in {totalSteps} steps");
        }

        var cheatsThatWouldSaveAtLeastXSteps = 0;
        var savingsDict = new Dictionary<int, int>();

        // Find cheat start point
        for (var y = 1; y < maze.Height - 1; y++)
        {
            for (var x = 1; x < maze.Width - 1; x++)
            {
                if (maze.CharAt((x, y)) == '#')
                    continue;

                var cheatStart = (x, y);

                // Find cheat end point
                for (var y0 = y - cheatMax - 5; y0 < y + cheatMax + 5; y0++)
                {
                    for (var x0 = x - cheatMax - 5; x0 < x + cheatMax + 5; x0++)
                    {
                        if (x0 < 0 || y0 < 0 || x0 >= maze.Width - 1 || y0 >= maze.Height - 1)
                            continue;

                        if (maze.CharAt((x0, y0)) == '#')
                            continue;

                        var cheatEnd = (x0, y0);
                        var manhattanDistance = MathExtensions.ManhattanDistance(cheatStart, cheatEnd);
                        if (manhattanDistance > cheatMax)
                            continue;

                        var costAtStart = stepCosts[cheatStart.x, cheatStart.y];
                        var costAtEnd = stepCosts[cheatEnd.x0, cheatEnd.y0];
                        var difference = costAtEnd - costAtStart;
                        var totalGain = difference - manhattanDistance;

                        if (totalGain >= targetSavings)
                        {
                            if (savingsDict.ContainsKey(totalGain))
                            {
                                savingsDict[totalGain]++;
                            }
                            else
                            {
                                savingsDict[totalGain] = 1;
                            }
                            cheatsThatWouldSaveAtLeastXSteps++;
                        }
                    }
                }
            }
        }

        foreach (var entry in savingsDict.OrderBy(d => d.Key))
        {
            Console.WriteLine($"There are {entry.Value} cheats that save {entry.Key} picoseconds.");
        }

        return cheatsThatWouldSaveAtLeastXSteps.ToString();
    }

    private static int[,] CalculateStepCosts(CharacterMatrix maze)
    {
        var start = maze.SingleMatch('S');
        var end = maze.SingleMatch('E');
        var current = start;
        var currentCost = 0;

        var result = new int[maze.Width, maze.Height];
        while (current != end)
        {
            var next = maze.CoordinatesOfNeighbors(current, allEight: false)
                .Single(n => maze.CharAt(n) != '#' && result[n.Item1, n.Item2] == 0 && n != start);
            result[next.Item1, next.Item2] = ++currentCost;
            current = next;
        }
        return result;
    }
}
