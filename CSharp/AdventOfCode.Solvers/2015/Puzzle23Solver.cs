using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, 0);
    public override string SolvePartTwo(string input) => Solve(input, 1);

    private static string Solve(string input, ulong regA)
    {
        var lines = input.SplitByNewline();
        var pointer = 0;
        var regB = 0;

        // In my input, b is only operated on
        // by "inc", so we can ignore b for
        // every other operation.

        while (pointer < lines.Length)
        {
            switch (lines[pointer][..3])
            {
                case "hlf":
                    regA /= 2;
                    pointer++;
                    break;
                case "tpl":
                    regA *= 3;
                    pointer++;
                    break;
                case "inc":
                    if (lines[pointer][4] == 'b')
                        regB++;
                    else
                        regA++;
                    pointer++;
                    break;
                case "jmp":
                    var offsetJmp = int.Parse(lines[pointer][4..]);
                    pointer += offsetJmp;
                    break;
                case "jie":
                    if (regA % 2 == 0)
                    {
                        var offsetJio = int.Parse(lines[pointer][6..]);
                        pointer += offsetJio;
                    }
                    else
                        pointer++;
                    break;
                case "jio":
                    if (regA == 1)
                    {
                        var offsetJio = int.Parse(lines[pointer][6..]);
                        pointer += offsetJio;
                    }
                    else
                        pointer++;
                    break;
            }
        }

        return regB.ToString();
    }
}
