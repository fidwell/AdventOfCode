using AdventOfCode.Core.StringUtilities;

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

        public int TargetIndicatorLightsValue;
        public int[] ButtonWiringSchematicsValues;
        public bool[] CurrentIndicatorLights;

        public Machine(string input)
        {
            var chunks = input.Split(' ').ToList();
            TargetIndicatorLights = [.. chunks[0][1..^1].Select(c => c == '#')];
            ButtonWiringSchematics = [.. chunks[1..^1].Select(ch => ch[1..^1].Split(',').Select(int.Parse).ToArray())];
            JoltageRequirements = [.. chunks[^1][1..^1].Split(',').Select(int.Parse)];
            CurrentIndicatorLights = new bool[TargetIndicatorLights.Length];

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

        private record struct MachineState(bool[] Lights, int TotalPresses);
    }
}
