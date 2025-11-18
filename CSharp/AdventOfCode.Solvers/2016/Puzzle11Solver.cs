using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public partial class Puzzle11Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool isPartTwo)
    {
        var initialState = GetInitialState(input);

        if (isPartTwo)
        {
            initialState.Floors[0].Add("gel");
            initialState.Floors[0].Add("mel");
            initialState.Floors[0].Add("gdi");
            initialState.Floors[0].Add("mdi");
        }

        var queue = new PriorityQueue<PuzzleState, int>();
        queue.Enqueue(initialState, 0);

        var visitedStates = new HashSet<string>();

        while (queue.Count > 0)
        {
            var thisState = queue.Dequeue();

            if (thisState.IsFinalState())
                return $"{thisState.StepsFromStart} --- {thisState.Hash}";

            visitedStates.Add(thisState.Hash);

            var nextStates = thisState.GetNextStates();
            var validStates = nextStates.Where(s => s.IsValidState());
            var unvisitedStates = validStates.Where(s => !visitedStates.Contains(s.Hash));
            var closerStates = unvisitedStates.Where(s => s.StepsFromStart < 73);
            foreach (var nextState in closerStates)
            {
                // Need faster priority score algorithm
                queue.Enqueue(nextState, nextState.Score * nextState.StepsFromStart);
            }
        }

        throw new Exception("Couldn't find end state");
    }

    private static PuzzleState GetInitialState(string input)
    {
        var state = new PuzzleState(0);
        var directions = input.SplitByNewline();
        for (var f = 0; f < directions.Length; f++)
        {
            var generators = GeneratorDefinition().Matches(directions[f])
                .Select(g => $"g{g.Groups[1].Value[..2]}");
            var microchips = MicrochipDefinition().Matches(directions[f])
                .Select(g => $"m{g.Groups[1].Value[..2]}");
            List<string> items = [.. generators, .. microchips];
            items.Sort();
            state.Floors[f].AddRange(items);
        }
        return state;
    }

    private class PuzzleState(int stepsFromStart)
    {
        public int StepsFromStart = stepsFromStart;
        public int Elevator = 0;
        public List<List<string>> Floors = [[], [], [], []];

        public bool IsValidState()
        {
            foreach (var floor in Floors)
            {
                var microchipsHere = floor.Where(f => f.StartsWith('m'));
                var generatorsHere = floor.Where(f => f.StartsWith('g'));
                if (generatorsHere.Any() &&
                    microchipsHere.Any(m => generatorsHere.All(g => g[1..] != m[1..])))
                    return false;
            }
            return true;
        }

        public bool IsFinalState() =>
            Floors[0].Count == 0 &&
            Floors[1].Count == 0 &&
            Floors[2].Count == 0;

        public int Score => // Lower is better
            Floors[0].Count * 64 +
            Floors[1].Count * 16 +
            Floors[2].Count * 4 +
            Floors[3].Count * 1;

        public IEnumerable<PuzzleState> GetNextStates()
        {
            // If there are no items, the elevator cannot move
            if (Floors[Elevator].Count == 0)
                throw new Exception("Invalid state");

            if (AreItemsBelowElevator())
            {
                foreach (var nextState in StatesWhenElevatorMoves(this, Elevator, Elevator - 1))
                {
                    yield return nextState;
                }
            }

            if (Elevator < 3)
            {
                foreach (var nextState in StatesWhenElevatorMoves(this, Elevator, Elevator + 1))
                {
                    yield return nextState;
                }
            }

            yield break;
        }

        private bool AreItemsBelowElevator()
        {
            if (Elevator == 0)
                return false;

            for (int i = 0; i < Elevator; i++)
            {
                if (Floors[i].Count > 0)
                    return true;
            }
            return false;
        }

        private static IEnumerable<PuzzleState> StatesWhenElevatorMoves(PuzzleState state, int from, int to)
        {
            // the elevator takes 1 thing
            foreach (var item in state.Floors[from])
            {
                var floorsCopy = Copy(state.Floors);
                floorsCopy[from].Remove(item);
                floorsCopy[to].Add(item);
                floorsCopy[to].Sort();
                yield return new PuzzleState(state.StepsFromStart + 1)
                {
                    Elevator = to,
                    Floors = floorsCopy
                };
            }

            // the elevator takes 2 things in any combination
            if (state.Floors[from].Count > 1)
            {
                for (var i = 0; i < state.Floors[from].Count - 1; i++)
                {
                    var item1 = state.Floors[from][i];
                    for (var j = i + 1; j < state.Floors[from].Count; j++)
                    {
                        var item2 = state.Floors[from][j];

                        var floorsCopy = Copy(state.Floors);
                        floorsCopy[from].Remove(item1);
                        floorsCopy[from].Remove(item2);
                        floorsCopy[to].Add(item1);
                        floorsCopy[to].Add(item2);
                        floorsCopy[to].Sort();
                        yield return new PuzzleState(state.StepsFromStart + 1)
                        {
                            Elevator = to,
                            Floors = floorsCopy
                        };
                    }
                }
            }
        }

        public string Hash => $"{Elevator}|{string.Join("|", Floors.Select(f => string.Join(",", f)))}";
    }

    private static List<List<string>> Copy(List<List<string>> input)
    {
        var result = new List<List<string>>();
        foreach (var list in input)
        {
            //var sublist = new List<string>();
            ////  just use  tolist() instead
            //foreach (var item in list)
            //    sublist.Add(item);
            result.Add(list.ToList());
        }
        return result;
    }

    [GeneratedRegex(@" (\w+) generator")]
    private static partial Regex GeneratorDefinition();

    [GeneratedRegex(@" (\w+)\-compatible microchip")]
    private static partial Regex MicrochipDefinition();
}
