using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle20Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var maze = new CharacterMatrix(input);
        var solution = Solve(maze, (-1, -1));
        var end = maze.SingleMatch('E');
        var totalSteps = solution[end.Item1, end.Item2];
        Console.WriteLine($"Without cheating, solved in {totalSteps} steps");

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
                    if (difference >= 100)
                    {
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
        throw new NotImplementedException();
    }

    private static int[,] Solve(CharacterMatrix maze, (int, int) cheat)
    {
        var start = maze.SingleMatch('S');
        var end = maze.SingleMatch('E');

        var queue = new PriorityQueue<RaceState, int>();
        var visited = new HashSet<RaceState>
        {
            new(start, 0)
        };

        var stepsAtEachTile = new int[maze.Width, maze.Height];

        queue.Enqueue(new RaceState(start, 0), 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            visited.Add(state);

            stepsAtEachTile[state.Location.Item1, state.Location.Item2] = state.PathLength;

            if (state.Location == end)
                return stepsAtEachTile;

            var neighbors = maze.CoordinatesOfNeighbors(state.Location, allEight: false);

            IEnumerable<RaceState> possibleNextStates = [];

            possibleNextStates = possibleNextStates.Concat(neighbors
                .Where(n => maze.CharAt(n) != '#' || n == cheat)
                .Select(n => new RaceState(n, state.PathLength + 1)));

            foreach (var nextState in possibleNextStates)
            {
                if (visited.Any(v => v.Location == nextState.Location && v.PathLength < nextState.PathLength))
                    continue;

                queue.Enqueue(nextState, state.PathLength + 1);
            }
        }

        Console.WriteLine("Couldn't solve the maze");
        return stepsAtEachTile;
    }

    private record struct RaceState((int, int) Location, int PathLength);

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
