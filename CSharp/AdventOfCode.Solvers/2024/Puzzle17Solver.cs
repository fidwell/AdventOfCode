using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var registerA = int.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
        var registerB = int.Parse(Regexes.NonNegativeInteger().Match(lines[1]).Value);
        var registerC = int.Parse(Regexes.NonNegativeInteger().Match(lines[2]).Value);
        var instructions = new List<int>(Regex.Matches(lines[3], @"(\d)").Select(m => int.Parse(m.Value)));
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
                    registerA = (int)(registerA / Math.Pow(2, ComboOperand(operand)));
                    break;
                case 1: // bxl: bitwise xor
                    registerB ^= operand;
                    break;
                case 2: // bst
                    registerB = ComboOperand(operand) % 8;
                    break;
                case 3: // jnz
                    if (registerA != 0)
                    {
                        instructionPointer = instructions[instructionPointer + 1];
                        jumped = true;
                    }
                    break;
                case 4: // bxc
                    registerB ^= registerC;
                    break;
                case 5: // out
                    outputs.Add(ComboOperand(operand) % 8);
                    break;
                case 6: // bdv
                    registerB = (int)(registerA / Math.Pow(2, ComboOperand(operand)));
                    break;
                case 7: // cdv
                    registerC = (int)(registerA / Math.Pow(2, ComboOperand(operand)));
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (!jumped)
            {
                instructionPointer += 2;
            }
        }

        int ComboOperand(int operand) => operand switch
        {
            0 or 1 or 2 or 3 => operand,
            4 => registerA,
            5 => registerB,
            6 => registerC,
            _ => throw new NotSupportedException(),
        };

        return string.Join(",", outputs);
    }

    public string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }
}
