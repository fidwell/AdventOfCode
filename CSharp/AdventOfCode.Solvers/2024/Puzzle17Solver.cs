using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var instructions = new List<int>(Regexes.Digit().Matches(lines[3]).Select(m => int.Parse(m.Value)));
        var registerA = ulong.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
        return string.Join(",", Run(instructions, registerA));
    }

    public string SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline();
        var instructions = new List<int>(Regexes.Digit().Matches(lines[3]).Select(m => int.Parse(m.Value)));

        return "Not found yet";
    }

    private static List<int> Run(List<int> instructions, ulong a)
    {
        var b = 0;
        var c = 0;
        var instructionPointer = 0;
        var outputs = new List<int>();

        while (instructionPointer < instructions.Count)
        {
            var opcode = instructions[instructionPointer];
            var operand = instructions[instructionPointer + 1];
            var jumped = false;

            switch (opcode)
            {
                case 0: // adv: division
                    a >>= ComboOperand(operand, a, b, c);
                    break;
                case 1: // bxl: bitwise xor
                    b ^= operand;
                    break;
                case 2: // bst
                    b = ComboOperand(operand, a, b, c) & 0b111;
                    break;
                case 3: // jnz
                    if (a != 0)
                    {
                        instructionPointer = instructions[instructionPointer + 1];
                        jumped = true;
                    }
                    break;
                case 4: // bxc
                    b ^= c;
                    break;
                case 5: // out
                    outputs.Add(ComboOperand(operand, a, b, c) & 0b111);
                    break;
                case 6: // bdv
                    b = (int)(a >> ComboOperand(operand, a, b, c));
                    break;
                case 7: // cdv
                    c = (int)(a >> ComboOperand(operand, a, b, c));
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!jumped)
            {
                instructionPointer += 2;
            }
        }

        return outputs;
    }

    private static int ComboOperand(int operand, ulong a, int b, int c) => operand switch
    {
        0 or 1 or 2 or 3 => operand,
        4 => (int)a, // probably safe
        5 => b,
        6 => c,
        _ => throw new NotSupportedException(),
    };
}
