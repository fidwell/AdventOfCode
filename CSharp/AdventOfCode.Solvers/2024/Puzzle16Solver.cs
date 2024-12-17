using System.Collections.Immutable;
using AdventOfCode.Core.Geometry;
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

    private static (int, List<List<Coord2d>>) Solve(CharacterMatrix matrix)
    {
        var start = new Coord2d(1, matrix.Height - 2);
        var end = new Coord2d(matrix.Width - 2, 1);

        var bestScore = int.MaxValue;
        var bestPaths = new List<List<Coord2d>>();
        var minScores = new Dictionary<Pose, int>();

        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(new State(new Pose(start, Direction.Right), 0, []), 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state.Score > bestScore)
                continue;

            if (state.Pose.Location == end)
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

            if (matrix.CharAt(state.Pose.Location.Go(state.Pose.Direction)) != '#')
                Enqueue(state.Forward());
            if (matrix.CharAt(state.Pose.Location.Go(state.Pose.Direction.RotateLeft())) != '#')
                Enqueue(state.TurnLeft());
            if (matrix.CharAt(state.Pose.Location.Go(state.Pose.Direction.RotateRight())) != '#')
                Enqueue(state.TurnRight());
        }

        return (bestScore, bestPaths);

        void Enqueue(State state)
        {
            if (!minScores.TryGetValue(state.Pose, out int value) || value >= state.Score)
            {
                value = state.Score;
                minScores[state.Pose] = value;
                queue.Enqueue(state, value);
            }
        }
    }

    private readonly record struct State(Pose Pose, int Score, List<Coord2d> Path)
    {
        public readonly State Forward() =>
            new(Pose.Forward(), Score + 1, [.. Path, Pose.Location.Go(Pose.Direction)]);

        public readonly State TurnLeft() =>
            new(Pose.TurnLeft(), Score + 1000, Path);

        public readonly State TurnRight() =>
            new(Pose.TurnRight(), Score + 1000, Path);
    }

    private static void Print(CharacterMatrix matrix, IEnumerable<Coord2d> coords)
    {
        for (var y = 0; y < matrix.Height; y++)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                if (coords.Contains(new(x, y)))
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
