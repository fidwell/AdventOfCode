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

        public int TargetIndicatorLightsValue;
        public int[] ButtonWiringSchematicsValues;
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

            TargetIndicatorLightsValue = TargetIndicatorLights.Select((b, i) => (b, i)).Sum(p => p.b ? (int)Math.Pow(2, p.i) : 0);
            ButtonWiringSchematicsValues = [.. ButtonWiringSchematics.Select(bws => (int)bws.Sum(b => Math.Pow(2, b)))];
        }

        public int FewestPressesToActivate()
        {
            var numOptions = Math.Pow(2, ButtonWiringSchematicsValues.Length);
            var min = 65535;
            for (var x = 1; x < numOptions; x++)
            {
                var total = 0;
                var bUsed = 0;
                for (var b = 0; b < ButtonWiringSchematicsValues.Length; b++)
                {
                    var useButton = (x >> b) % 2 == 1;
                    if (useButton)
                    {
                        total ^= ButtonWiringSchematicsValues[b];
                        bUsed++;
                    }
                }
                if (total == TargetIndicatorLightsValue && bUsed < min)
                    min = bUsed;
            }
            return min;
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
