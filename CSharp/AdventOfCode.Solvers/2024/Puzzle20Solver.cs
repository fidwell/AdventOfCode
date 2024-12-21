using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle20Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var maze = new CharacterMatrix(input);
        return Solve(maze, maze.Width == 15 ? 1 : 100, 2);
    }

    public override string SolvePartTwo(string input)
    {
        var maze = new CharacterMatrix(input);
        return Solve(maze, maze.Width == 15 ? 50 : 100, 20);
    }

    public static string Solve(CharacterMatrix maze, int targetSavings, int cheatMax)
    {
        var stepCosts = CalculateStepCosts(maze);
        var cheatsThatWouldSaveAtLeastXSteps = 0;

        // Find cheat start point
        for (var y = 1; y < maze.Height - 1; y++)
        {
            for (var x = 1; x < maze.Width - 1; x++)
            {
                if (maze.CharAt((x, y)) == '#')
                    continue;

                var cheatStart = (x, y);

                // Find cheat end point
                for (var y0 = y - cheatMax; y0 <= y + cheatMax; y0++)
                {
                    for (var x0 = x - cheatMax; x0 <= x + cheatMax; x0++)
                    {
                        if (x0 < 0 || y0 < 0 || x0 >= maze.Width - 1 || y0 >= maze.Height - 1)
                            continue;

                        if (maze.CharAt((x0, y0)) == '#')
                            continue;

                        var cheatEnd = (x0, y0);
                        var manhattanDistance = cheatStart.ManhattanDistance(cheatEnd);
                        if (manhattanDistance > cheatMax)
                            continue;

                        var costAtStart = stepCosts[cheatStart.x, cheatStart.y];
                        var costAtEnd = stepCosts[cheatEnd.x0, cheatEnd.y0];
                        var difference = costAtEnd - costAtStart;
                        var totalGain = difference - manhattanDistance;

                        if (totalGain >= targetSavings)
                        {
                            cheatsThatWouldSaveAtLeastXSteps++;
                        }
                    }
                }
            }
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
