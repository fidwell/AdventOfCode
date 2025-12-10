using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2025;

public class Puzzle10Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        input.SplitByNewline().Select(l => new Machine(l)).Sum(m => m.FewestPressesToActivate());

    public override object SolvePartTwo(string input) => throw new NotImplementedException();

    private class Machine
    {
        public bool[] TargetIndicatorLights;
        public int[][] ButtonWiringSchematics;
        public int[] JoltageRequirements;

        public bool[] CurrentIndicatorLights;

        public Machine(string input)
        {
            var chunks = input.Split(' ').ToList();
            TargetIndicatorLights = [.. chunks[0][1..^1].Select(c => c == '#')];
            ButtonWiringSchematics = [.. chunks[1..^1].Select(ch => ch[1..^1].Split(',').Select(int.Parse).ToArray())];
            JoltageRequirements = [.. chunks[^1][1..^1].Split(',').Select(int.Parse)];
            CurrentIndicatorLights = new bool[TargetIndicatorLights.Length];
        }

        public int FewestPressesToActivate()
        {
            var queue = new PriorityQueue<MachineState, int>();
            queue.Enqueue(new MachineState(new bool[TargetIndicatorLights.Length], 0), 0);

            while (queue.Count > 0)
            {
                var thisState = queue.Dequeue();
                if (Enumerable.SequenceEqual(thisState.Lights, TargetIndicatorLights))
                    return thisState.TotalPresses;

                // Generate next states
                foreach (var schematic in ButtonWiringSchematics)
                {
                    var beforePressing = thisState.Lights.ToArray();
                    foreach (var button in schematic)
                    {
                        beforePressing[button] = !beforePressing[button];
                    }
                    var nextState = new MachineState(beforePressing, thisState.TotalPresses + 1);
                    queue.Enqueue(nextState, thisState.TotalPresses + 1);
                }
            }

            throw new SolutionNotFoundException();
        }

        private record struct MachineState(bool[] Lights, int TotalPresses);
    }
}
