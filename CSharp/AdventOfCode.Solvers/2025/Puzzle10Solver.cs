using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2025;

public class Puzzle10Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        input.SplitByNewline().Select(l => new Machine(l)).Sum(m => m.FewestPressesToActivate());

    public override object SolvePartTwo(string input)
    {
        var machines = input.SplitByNewline().Select(l => new Machine(l));

        var sum = 0;
        object lockObj = new();
        Parallel.ForEach(machines, machine =>
        {
            lock (lockObj)
            {
                sum += machine.FewestPressesToConfigureJoltage();
            }
        });

        return sum;
    }

    private class Machine
    {
        public bool[] TargetIndicatorLights;
        public int[][] ButtonWiringSchematics;
        public int[] JoltageRequirements;

        public bool[] CurrentIndicatorLights;
        public int[] CurrentJoltages;

        public Machine(string input)
        {
            var chunks = input.Split(' ').ToList();
            TargetIndicatorLights = [.. chunks[0][1..^1].Select(c => c == '#')];
            ButtonWiringSchematics = [.. chunks[1..^1].Select(ch => ch[1..^1].Split(',').Select(int.Parse).ToArray())];
            JoltageRequirements = [.. chunks[^1][1..^1].Split(',').Select(int.Parse)];
            CurrentIndicatorLights = new bool[TargetIndicatorLights.Length];
            CurrentJoltages = new int[JoltageRequirements.Length];
        }

        public int FewestPressesToActivate()
        {
            var queue = new PriorityQueue<MachineActivationState, int>();
            queue.Enqueue(new MachineActivationState(new bool[TargetIndicatorLights.Length], 0), 0);

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
                    var nextState = new MachineActivationState(beforePressing, thisState.TotalPresses + 1);
                    queue.Enqueue(nextState, thisState.TotalPresses + 1);
                }
            }

            throw new SolutionNotFoundException();
        }

        public int FewestPressesToConfigureJoltage()
        {
            var queue = new PriorityQueue<JoltageState, int>();
            queue.Enqueue(new JoltageState(new int[JoltageRequirements.Length], 0), 0);

            while (queue.Count > 0)
            {
                var thisState = queue.Dequeue();
                if (Enumerable.SequenceEqual(thisState.Joltages, JoltageRequirements))
                {
                    Console.WriteLine($"Found solution for a single machine in {thisState.TotalPresses} presses");
                    return thisState.TotalPresses;
                }

                // Generate next states
                foreach (var schematic in ButtonWiringSchematics)
                {
                    var beforePressing = thisState.Joltages.ToArray();
                    var isValidState = true;
                    foreach (var button in schematic)
                    {
                        beforePressing[button] += 1;

                        if (beforePressing[button] > JoltageRequirements[button])
                        {
                            isValidState = false;
                            break;
                        }
                    }

                    if (isValidState)
                    {
                        var nextState = new JoltageState(beforePressing, thisState.TotalPresses + 1);
                        queue.Enqueue(nextState, DistanceToTarget(beforePressing));
                    }
                }
            }

            throw new SolutionNotFoundException();
        }

        private int DistanceToTarget(int[] state)
        {
            var result = 0;
            for (var i = 0; i < state.Length; i++)
            {
                result += JoltageRequirements[i] - state[i];
            }
            return result;
        }

        private record struct MachineActivationState(bool[] Lights, int TotalPresses);
        private record struct JoltageState(int[] Joltages, int TotalPresses);
    }
}
