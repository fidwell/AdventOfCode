using AdventOfCode.Core.Matrixes;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle10Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        input.SplitByNewline().Select(l => new Machine(l))
        .Sum(m => m.FewestPressesToActivate());

    public override object SolvePartTwo(string input) =>
        input.SplitByNewline().Select(l => new Machine(l))
        .Sum(m => m.FewestPressesToConfigureJoltage());

    private class Machine
    {
        public int[][] ButtonWiringSchematics;
        public int[] JoltageRequirements;

        // On/off values of each light represented as a bit mask.
        // e.g. 5 => 0101 => light 0 on, light 1 off, etc.
        public int TargetIndicatorLights;

        public Machine(string input)
        {
            var chunks = input.Split(' ').ToList();
            ButtonWiringSchematics = [.. chunks[1..^1].Select(ch => ch[1..^1].Split(',').Select(int.Parse).ToArray())];
            JoltageRequirements = [.. chunks[^1][1..^1].Split(',').Select(int.Parse)];

            TargetIndicatorLights = chunks[0][1..^1].Select(c => c == '#')
                .Select((b, i) => (b, i)).Sum(p => p.b ? (int)Math.Pow(2, p.i) : 0);
        }

        public int FewestPressesToActivate()
        {
            // 5 => 0101 => light 0 is toggled, light 1 is not toggled, etc.
            var buttonWiringSchematicsValues = ButtonWiringSchematics.Select(bws => (int)bws.Sum(b => Math.Pow(2, b))).ToArray();

            var numOptions = Math.Pow(2, buttonWiringSchematicsValues.Length);
            var min = 65535;
            for (var x = 1; x < numOptions; x++)
            {
                var total = 0;
                var bUsed = 0;
                for (var b = 0; b < buttonWiringSchematicsValues.Length; b++)
                {
                    var useButton = (x >> b) % 2 == 1;
                    if (useButton)
                    {
                        total ^= buttonWiringSchematicsValues[b];
                        bUsed++;
                    }
                }
                if (total == TargetIndicatorLights && bUsed < min)
                    min = bUsed;
            }
            return min;
        }

        public int FewestPressesToConfigureJoltage()
        {
            // System of linear equations. For buttons b0 through bn, pressing each p0 through
            // pn times, find the values for joltages J0 through Jm.

            // For each joltage Jm, bit mask each button schematic bi = bi^m to find
            // whether that button modifies that joltage.
            // Sum of the number of presses of each button should then equal Jm.
            // J0 = p0*b0^0 + p1*b1^0 + ... + pn*bn^0
            // J1 = p0*b0^1 + p1*b1^1 + ... + pn*bn^1

            // For example machine 1 this becomes:
            // 3 = p0*b0^0 + p1*b1^0 + p2*b2^0 + p4*b4^0 + p5*b5^0 + p6*b6^0
            // 5 = p0*b0^1 + p1*b1^1 + p2*b2^1 + p4*b4^1 + p5*b5^1 + p6*b6^1
            // 4 = p0*b0^2 + p1*b1^2 + p2*b2^2 + p4*b4^2 + p5*b5^2 + p6*b6^2
            // 7 = p0*b0^3 + p1*b1^3 + p2*b2^3 + p4*b4^3 + p5*b5^3 + p6*b6^3

            // 3 = p0*0 + p1*0 + p2*0 + p4*0 + p5*1 + p6*1 (110000 or 48 --- only buttons 5 and 6 operate on J0)
            // 5 = p0*0 + p1*1 + p2*0 + p4*0 + p5*0 + p6*1 (100010 or 34)
            // 4 = p0*0 + p1*0 + p2*1 + p4*1 + p5*1 + p6*0 (011100 or 28)
            // 7 = p0*1 + p1*1 + p2*0 + p4*1 + p5*0 + p6*0 (001011 or 11)

            // p0 through p6 are the unknowns
            // Solution array J0..Jm is simply JoltageRequirements
            // Coefficient matrix is the bit representation of buttonOperationMasks
            var coefficientMatrix = new int[JoltageRequirements.Length, ButtonWiringSchematics.Length];

            for (var j = 0; j < JoltageRequirements.Length; j++)
            {
                for (var b = 0; b < ButtonWiringSchematics.Length; b++)
                {
                    if (ButtonWiringSchematics[b].Contains(j))
                        coefficientMatrix[j, b] = 1;
                }
            }

            return MatrixExtensions.SolveSystemOfLinearEquations(coefficientMatrix, JoltageRequirements).Sum();
        }
    }
}
