using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(new CharacterMatrix(input), 1, 3).ToString();
    public string SolvePartTwo(string input) => Solve(new CharacterMatrix(input), 4, 10).ToString();

    private static int Solve(CharacterMatrix matrix, int minSteps, int maxSteps)
    {
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(new State(0, new Pose()), 0);
        var cache = new HashSet<State>();
        var destination = (matrix.Width - 1, matrix.Height - 1);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Pose.Location == destination)
                return state.Heat;

            if (cache.Contains(state))
                continue;

            cache.Add(state);

            var possibleNexts = DirectionExtensions.All.Where(d => d != state.Pose.Direction && d != state.Pose.Direction.Opposite());
            foreach (var next in possibleNexts)
            {
                var loc0 = state.Pose.Location;
                var heat0 = state.Heat;

                for (var i = 1; i <= maxSteps; i++)
                {
                    loc0 = loc0.Go(next);

                    if (matrix.IsInBounds(loc0))
                    {
                        heat0 += matrix.CharAt(loc0) - '0';
                        if (i >= minSteps)
                        {
                            queue.Enqueue(new State(heat0, new Pose(loc0, next)), heat0);
                        }
                    }
                }
            }
        }

        throw new Exception("Couldn't complete execution.");
    }

    private class State(int heat, Pose pose)
    {
        public int Heat = heat;
        public Pose Pose = pose;

        public override bool Equals(object? obj) =>
            obj is State other && other.Pose == Pose;

        public override int GetHashCode() => Pose.GetHashCode();
    }
}
