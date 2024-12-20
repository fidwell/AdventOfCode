using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle20Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var maze = new CharacterMatrix(input);
        var solution = CalculateStepCosts(maze);
        var end = maze.SingleMatch('E');
        var totalSteps = solution[end.Item1, end.Item2];
        //Console.WriteLine($"Without cheating, solved in {totalSteps} steps");
        var isExample = maze.Width == 15;

        var cheatsThatWouldSaveAtLeast100Steps = 0;

        for (var y = 1; y < maze.Height - 1; y++)
        {
            for (var x = 1; x < maze.Width - 1; x++)
            {
                if (maze.CharAt((x, y)) != '#')
                    continue;

                var neighbors = maze.CoordinatesOfNeighbors((x, y), allEight: false)
                    .Where(n => solution[n.Item1, n.Item2] > 0)
                    .ToArray();
                if (neighbors.Length == 2)
                {
                    var n1 = neighbors[0];
                    var n2 = neighbors[1];
                    var n1am = solution[n1.Item1, n1.Item2];
                    var n2am = solution[n2.Item1, n2.Item2];
                    var difference = Math.Abs(n1am - n2am) - 2;
                    if (difference >= (isExample ? 1 : 100))
                    {
                        if (isExample)
                            Console.WriteLine($"With cheat at {(x, y)}, saved {difference} steps");
                        cheatsThatWouldSaveAtLeast100Steps++;
                    }
                }
            }
        }

        return cheatsThatWouldSaveAtLeast100Steps.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var maze = new CharacterMatrix(input);
        var solution = CalculateStepCosts(maze);
        var end = maze.SingleMatch('E');
        var totalSteps = solution[end.Item1, end.Item2];
        Console.WriteLine($"Without cheating, solved in {totalSteps} steps");
        var isExample = maze.Width == 15;

        const int cheatMax = 20;
        var cheatsThatWouldSaveAtLeast100Steps = 0;
        var savingsDict = new Dictionary<int, int>();

        for (var y = 1; y < maze.Height - 1; y++)
        {
            for (var x = 1; x < maze.Width - 1; x++)
            {
                if (maze.CharAt((x, y)) == '#')
                    continue;

                var cheatStart = (x, y);
                // Find cheat endpoint
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

                        var costAtStart = solution[cheatStart.x, cheatStart.y];
                        var costAtEnd = solution[cheatEnd.x0, cheatEnd.y0];
                        var difference = costAtEnd - costAtStart;
                        var totalGain = difference - manhattanDistance;

                        if (totalGain >= (isExample ? 50 : 100))
                        {
                            if (savingsDict.ContainsKey(totalGain))
                            {
                                savingsDict[totalGain]++;
                            }
                            else
                            {
                                savingsDict[totalGain] = 1;
                            }
                            cheatsThatWouldSaveAtLeast100Steps++;
                        }
                    }
                }
            }
        }

        foreach (var entry in savingsDict.OrderBy(d => d.Key))
        {
            Console.WriteLine($"There are {entry.Value} cheats that save {entry.Key} picoseconds.");
        }

        return cheatsThatWouldSaveAtLeast100Steps.ToString();
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

    private static void Print(CharacterMatrix maze, IEnumerable<(int, int)> path)
    {
        Console.WriteLine();
        for (var y = 0; y < maze.Height; y++)
        {
            for (var x = 0; x < maze.Width; x++)
            {
                if (path.Contains((x, y)))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(maze.CharAt(x, y));
                }
            }
            Console.WriteLine();
        }
    }
}
