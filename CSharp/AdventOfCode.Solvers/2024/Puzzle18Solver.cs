using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle18Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var coords = ParseCoords(input);
        var memory = FillMemory(coords);
        var bestPath = Solve(memory);

        if (ShouldPrint)
            Print(memory, bestPath);

        return (bestPath.Count - 1).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var coords = ParseCoords(input);
        var minIndex = 0;
        var maxIndex = coords.Count - 1;

        while (minIndex != maxIndex)
        {
            // Binary search:
            // Find the lowest index at which we cannot solve.
            var middleCoordIndex = (minIndex + maxIndex) / 2;
            var memory = FillMemory(coords, middleCoordIndex);

            if (ShouldPrint)
                Print(memory, []);

            var canSolve = Solve(memory).Count != 0;

            if (canSolve)
                minIndex = middleCoordIndex + 1;
            else
                maxIndex = middleCoordIndex;
        }

        var resultCoord = coords[maxIndex - 1];
        return $"{resultCoord.Item1},{resultCoord.Item2}";
    }

    private static bool[,] FillMemory(List<(int, int)> coords, int? maxCoords = null)
    {
        var isExample = coords.Count == 25;
        var size = isExample ? 7 : 71;
        var memory = new bool[size, size];

        var coordCount = maxCoords.HasValue ? maxCoords.Value : (isExample ? 12 : 1024);
        foreach (var coord in coords.Take(coordCount))
        {
            memory[coord.Item1, coord.Item2] = true;
        }
        return memory;
    }

    private static List<(int, int)> ParseCoords(string input) =>
        input.SplitByNewline().Select(l =>
        {
            var matches = Regexes.NonNegativeInteger().Matches(l).Select(m => int.Parse(m.Value)).ToArray();
            return (matches[0], matches[1]);
        }).ToList();

    private static List<(int, int)> Solve(bool[,] maze)
    {
        var mazeSize = maze.GetLength(0);
        var end = (mazeSize - 1, mazeSize - 1);

        var queue = new PriorityQueue<State, int>();
        var visited = new HashSet<(int, int)>();
        var parent = new Dictionary<(int, int), (int, int)>();

        visited.Add((0, 0));
        queue.Enqueue(new State(0, 0, 0), 0);

        while (queue.Count > 0)
        {
            var (x, y, length) = queue.Dequeue();
            if ((x, y) == end)
                return ReconstructPath(parent, (x, y));

            foreach (var (nx, ny) in new[] {
                (x + 1, y),
                (x, y + 1),
                (x, y - 1),
                (x - 1, y)
            })
            {
                if (nx < 0 || ny < 0 ||
                    nx > mazeSize - 1 || ny > mazeSize - 1 ||
                    maze[nx, ny] ||
                    visited.Contains((nx, ny))
                    )
                    continue;

                visited.Add((nx, ny));
                parent[(nx, ny)] = (x, y);
                var manhattanDistance = (end.Item1 - nx) + (end.Item2 - ny);
                var priority = length + 1 + manhattanDistance;
                var newState = new State(nx, ny, length + 1);
                queue.Enqueue(newState, priority);
            }
        }

        return [];
    }

    private static List<(int, int)> ReconstructPath(Dictionary<(int, int), (int, int)> parent, (int X, int Y) end)
    {
        var path = new List<(int, int)>();
        for (var current = end; parent.ContainsKey(current); current = parent[current])
            path.Add(current);

        path.Add((0, 0));
        path.Reverse();
        return path;
    }

    private readonly record struct State(int X, int Y, int Length);

    private static void Print(bool[,] memory, List<(int, int)> bestPath)
    {
        Console.WriteLine();
        for (var y = 0; y < memory.GetLength(0); y++)
        {
            for (var x = 0; x < memory.GetLength(1); x++)
            {
                if (bestPath.Contains((x, y)))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(memory[x, y] ? '#' : '.');
                }
            }
            Console.WriteLine();
        }
    }
}
