using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(new CharacterMatrix(input), 1, 3).ToString();
    public string SolvePartTwo(string input) => Solve(new CharacterMatrix(input), 4, 10).ToString();

    private static int Solve(CharacterMatrix matrix, int minSteps, int maxSteps)
    {
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(new State(0, (0, 0), (0, 0)), 0);
        var cache = new HashSet<State>();
        var destination = (matrix.Width - 1, matrix.Height - 1);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Location == destination)
                return state.Heat;

            if (cache.Contains(state))
                continue;

            cache.Add(state);

            var possibleNexts = new (int, int)[]
            {
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1)
            }.Where(d => d != state.Direction && !(d.Item1 == -state.Direction.Item1 && d.Item2 == -state.Direction.Item2));

            foreach (var next in possibleNexts)
            {
                var x0 = state.Location.Item1;
                var y0 = state.Location.Item2;
                var h0 = state.Heat;

                for (var i = 1; i <= maxSteps; i++)
                {
                    x0 += next.Item1;
                    y0 += next.Item2;

                    if (matrix.IsInBounds((x0, y0)))
                    {
                        h0 += matrix.CharAt(x0, y0) - '0';
                        if (i >= minSteps)
                        {
                            queue.Enqueue(new State(h0, (x0, y0), next), h0);
                        }
                    }
                }
            }
        }

        throw new Exception("Couldn't complete execution.");
    }

    private class State(int heat, (int, int) location, (int, int) direction)
    {
        public int Heat = heat;
        public (int, int) Location = location;
        public (int, int) Direction = direction;

        public override bool Equals(object? obj) =>
            obj is State other && other.Location == Location && Direction == other.Direction;

        public override int GetHashCode() => HashCode.Combine(Location, Direction);
    }
}
