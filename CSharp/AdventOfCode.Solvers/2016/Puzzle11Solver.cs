using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2016;

public partial class Puzzle11Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input, false);
    public override object SolvePartTwo(string input) => Solve(input, true);

    private static object Solve(string input, bool isPartTwo)
    {
        var initialState = GetInitialState(input, isPartTwo);
        var queue = new PriorityQueue<PuzzleState, int>();
        queue.Enqueue(initialState, 0);

        var visitedStates = new HashSet<int>();

        while (queue.Count > 0)
        {
            var thisState = queue.Dequeue();

            if (thisState.IsFinalState())
                return thisState.StepsFromStart;

            visitedStates.Add(thisState.Hash);

            var nextStates = thisState.GetNextStates();
            var validStates = nextStates.Where(s => s.IsValidState());
            var unvisitedStates = validStates.Where(s => !visitedStates.Contains(s.Hash));
            foreach (var nextState in unvisitedStates)
            {
                queue.Enqueue(nextState, nextState.Score);
            }
        }

        throw new SolutionNotFoundException("Couldn't find end state");
    }

    private static PuzzleState GetInitialState(string input, bool isPartTwo)
    {
        var directions = input.SplitByNewline();

        var microchips = directions.SelectMany((d, i) =>
        {
            var regexMatches = MicrochipDefinition().Matches(d)
                .Select(g => g.Groups[1].Value);
            return regexMatches.Select(r => (name: r, floor: i));
        });

        var generators = directions.SelectMany((d, i) =>
        {
            var regexMatches = GeneratorDefinition().Matches(d)
                .Select(g => g.Groups[1].Value);
            return regexMatches.Select(r => (name: r, floor: i));
        });

        Pair[] pairs = [.. microchips.Select(m =>
        {
            var (_, floor) = generators.Single(g => g.name == m.name);
            return new Pair(m.floor, floor);
        })];

        if (isPartTwo)
        {
            pairs = [.. pairs, new(0, 0), new(0, 0)];
        }

        return new PuzzleState(0, 0, pairs);
    }

    private class PuzzleState(int stepsFromStart, int elevator, Pair[] pairs)
    {
        public readonly int StepsFromStart = stepsFromStart;
        public readonly int Elevator = elevator;
        public readonly Pair[] Pairs = pairs;

        private int? _hash;
        public int Hash => _hash ??= ComputeHash(Elevator, Pairs);

        private static int ComputeHash(int elevator, Pair[] pairs)
        {
            var sorted = pairs
                .Select(p => (Math.Min(p.Microchip, p.Generator), Math.Max(p.Microchip, p.Generator)))
                .OrderBy(p => p.Item1)
                .ThenBy(p => p.Item2);
            return HashCode.Combine(
                elevator,
                sorted.Aggregate(0, (acc, p) => HashCode.Combine(acc, p.Item1, p.Item2)));
        }

        public int Score => StepsFromStart - Elevator - Pairs.Sum(p => p.Microchip + p.Generator);

        public bool IsFinalState() => Pairs.All(p => p.Microchip == 3 && p.Generator == 3);

        public bool IsValidState()
        {
            foreach (var pair in Pairs)
            {
                if (pair.Microchip == pair.Generator)
                    continue;

                if (Pairs.Any(p => p.Generator == pair.Microchip))
                    return false;
            }

            return true;
        }

        public IEnumerable<PuzzleState> GetNextStates()
        {
            if (Pairs.Any(p => p.Generator < Elevator || p.Microchip < Elevator))
            {
                foreach (var nextState in StatesWhenElevatorMoves(this, Elevator - 1))
                {
                    yield return nextState;
                }
            }

            if (Elevator < 3)
            {
                foreach (var nextState in StatesWhenElevatorMoves(this, Elevator + 1))
                {
                    yield return nextState;
                }
            }
        }

        private static IEnumerable<PuzzleState> StatesWhenElevatorMoves(PuzzleState state, int to)
        {
            // Yuck.

            // Find all combinations of one or two items
            // that are on the same floor as the elevator.
            var matches = new List<(int index, string property)>();

            for (var i = 0; i < state.Pairs.Length; i++)
            {
                if (state.Pairs[i].Generator == state.Elevator)
                    matches.Add((i, "g"));
                if (state.Pairs[i].Microchip == state.Elevator)
                    matches.Add((i, "m"));
            }

            // Return any new states where we move just one thing.
            foreach (var (index, property) in matches)
            {
                var newPairs = state.Pairs.Select((p, i) =>
                    i == index
                        ? (property == "m" ? new Pair(to, p.Generator) : new Pair(p.Microchip, to))
                        : new Pair(p.Microchip, p.Generator)
                ).ToArray();

                yield return new PuzzleState(state.StepsFromStart + 1, to, newPairs);
            }

            // Return any new states where we move two things.
            for (var i = 0; i < matches.Count - 1; i++)
            {
                for (var j = i + 1; j < matches.Count; j++)
                {
                    var (index1, prop1) = matches[i];
                    var (index2, prop2) = matches[j];

                    var newPairs = state.Pairs.Select((p, idx) =>
                    {
                        // Moving same items in a single pair
                        if (idx == index1 && idx == index2)
                            return new Pair(to, to);

                        // Moving items from different pairs
                        if (idx == index1)
                            return prop1 == "m" ? new Pair(to, p.Generator) : new Pair(p.Microchip, to);
                        if (idx == index2)
                            return prop2 == "m" ? new Pair(to, p.Generator) : new Pair(p.Microchip, to);

                        // Not moving anything in other pairs
                        return new Pair(p.Microchip, p.Generator);
                    }).ToArray();

                    yield return new PuzzleState(state.StepsFromStart + 1, to, newPairs);
                }
            }
        }
    }

    private record Pair(int Microchip, int Generator);

    [GeneratedRegex(@" (\w+) generator")]
    private static partial Regex GeneratorDefinition();

    [GeneratedRegex(@" (\w+)\-compatible microchip")]
    private static partial Regex MicrochipDefinition();
}
