using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.Hashing;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2016;

public class Puzzle17Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var passcode = input.Trim();
        var startingState = new State((0, 0), string.Empty);

        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(startingState, 0);

        while (queue.Count > 0)
        {
            var thisState = queue.Dequeue();
            if (thisState.IsEndState)
                return thisState.PathSoFar;

            foreach (var nextState in thisState.NextStates(passcode))
            {
                queue.Enqueue(nextState, thisState.PathSoFar.Length + 1);
            }
        }

        throw new SolutionNotFoundException();
    }

    public override object SolvePartTwo(string input)
    {
        var passcode = input.Trim();
        var startingState = new State((0, 0), string.Empty);
        var longestSolution = 0;

        var queue = new Queue<State>();
        queue.Enqueue(startingState);

        while (queue.Count != 0)
        {
            var thisState = queue.Dequeue();
            if (thisState.IsEndState && thisState.PathSoFar.Length > longestSolution)
            {
                longestSolution = thisState.PathSoFar.Length;
            }

            foreach (var nextState in thisState.NextStates(passcode))
            {
                queue.Enqueue(nextState);
            }
        }

        return longestSolution;
    }

    public record struct State(Coord Location, string PathSoFar)
    {
        public readonly IEnumerable<State> NextStates(string passcode)
        {
            if (IsEndState)
                yield break;

            // Probably faster without string conversion
            var md5Hash = Md5Hasher.HashToString($"{passcode}{PathSoFar}");

            // Up
            if (md5Hash[0] >= 'b' && md5Hash[0] <= 'f' &&
                Location.Item2 > 0)
                yield return new State(Location.Go(Direction.Up), $"{PathSoFar}U");

            // Down
            if (md5Hash[1] >= 'b' && md5Hash[1] <= 'f' &&
                Location.Item2 < 3)
                yield return new State(Location.Go(Direction.Down), $"{PathSoFar}D");

            // Left
            if (md5Hash[2] >= 'b' && md5Hash[2] <= 'f' &&
                Location.Item1 > 0)
                yield return new State(Location.Go(Direction.Left), $"{PathSoFar}L");

            // Right
            if (md5Hash[3] >= 'b' && md5Hash[3] <= 'f' &&
                Location.Item1 < 3)
                yield return new State(Location.Go(Direction.Right), $"{PathSoFar}R");
        }

        public readonly bool IsEndState => Location == (3, 3);
    }
}
