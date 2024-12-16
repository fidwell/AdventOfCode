using System.Collections.Immutable;
using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle16Solver : IPuzzleSolver
{
    // Disabled for benchmarking and performance.
    // Toggle on if you want to see the output!
    const bool ShouldPrint = false;

    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var result = Solve(matrix);
        if (ShouldPrint)
        {
            Print(matrix, result.Item2.First().Select(p => p));
        }
        return result.Item1.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var results = Solve(matrix);
        var coords = results.Item2.SelectMany(p => p).Distinct().ToList();
        if (ShouldPrint)
        {
            Print(matrix, coords);
        }
        return (coords.Count + 1).ToString();
    }

    private static (int, List<List<(int, int)>>) Solve(CharacterMatrix matrix)
    {
        var start = (1, matrix.Height - 2);
        var end = (matrix.Width - 2, 1);

        var bestScore = int.MaxValue;
        var bestPaths = new List<List<(int, int)>>();
        var minScores = new Dictionary<ReindeerState, int>();

        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(new State(new ReindeerState(start, Direction.Right), 0, []), 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Score > bestScore)
                continue;

            if (state.ReindeerState.Location == end)
            {
                if (state.Score < bestScore)
                {
                    bestPaths = [state.Path];
                    bestScore = state.Score;
                }
                else
                {
                    bestPaths.Add(state.Path);
                }
                continue;
            }

            if (matrix.CharAt(state.ReindeerState.Location.Go(state.ReindeerState.Direction)) != '#')
                Enqueue(state.Forward());
            if (matrix.CharAt(state.ReindeerState.Location.Go(state.ReindeerState.Direction.RotateLeft())) != '#')
                Enqueue(state.TurnLeft());
            if (matrix.CharAt(state.ReindeerState.Location.Go(state.ReindeerState.Direction.RotateRight())) != '#')
                Enqueue(state.TurnRight());
        }

        return (bestScore, bestPaths);

        void Enqueue(State state)
        {
            if (!minScores.TryGetValue(state.ReindeerState, out int value) || value >= state.Score)
            {
                value = state.Score;
                minScores[state.ReindeerState] = value;
                queue.Enqueue(state, value);
            }
        }
    }

    private readonly record struct State(ReindeerState ReindeerState, int Score, List<(int, int)> Path)
    {
        public readonly State Forward() =>
            new(ReindeerState.Forward(), Score + 1, [.. Path, ReindeerState.Location.Go(ReindeerState.Direction)]);

        public readonly State TurnLeft() =>
            new(ReindeerState.TurnLeft(), Score + 1000, Path);

        public readonly State TurnRight() =>
            new(ReindeerState.TurnRight(), Score + 1000, Path);
    }

    private record struct ReindeerState((int, int) Location, Direction Direction)
    {
        public readonly ReindeerState Forward() => new(Location.Go(Direction), Direction);
        public readonly ReindeerState TurnLeft() => new(Location, Direction.RotateLeft());
        public readonly ReindeerState TurnRight() => new(Location, Direction.RotateRight());
    }

    private static void Print(CharacterMatrix matrix, IEnumerable<(int, int)> coords)
    {
        for (var y = 0; y < matrix.Height; y++)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                if (coords.Contains((x, y)))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(matrix.CharAt(x, y) == '#' ? '.' : ' ');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
