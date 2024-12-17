using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var registerA = int.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
        var instructions = new List<int>(Regex.Matches(lines[3], @"(\d)").Select(m => int.Parse(m.Value)));
        var computer = new Computer((ulong)registerA, instructions);
        return string.Join(",", computer.Run());
    }

    public string SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline();
        var instructions = new List<int>(Regex.Matches(lines[3], @"(\d)").Select(m => int.Parse(m.Value)));

        var min = 35416039467418UL;//(ulong)(Math.Pow(2, 44) + Math.Pow(2, 45)) * 3 / 5;
        var max = (ulong)Math.Pow(2, 48);

        for (var regA = min; regA < max; regA += 98304)
        {
            var computer = new Computer(regA, instructions);
            var result = computer.Run();

            if (instructions.SequenceEqual(result))
            {
                return regA.ToString();
            }

            var prefixLen = 10;
            if (instructions.Count == result.Count &&
                instructions.Take(prefixLen).SequenceEqual(result.Take(prefixLen)))
            {
                var asList = string.Join(",", result);
                Console.WriteLine($"{regA},{asList}");
            }
        }

        return "No solution found";
    }

    private class Computer(ulong regA, List<int> instructions)
    {
        public ulong RegisterA { get; private set; } = regA;
        public ulong RegisterB { get; private set; } = 0;
        public ulong RegisterC { get; private set; } = 0;
        public List<int> Instructions { get; private set; } = instructions;

        public List<int> Run()
        {
            var instructionPointer = 0;
            var outputs = new List<int>();

            while (instructionPointer < instructions.Count)
            {
                var opcode = instructions[instructionPointer];
                var operand = (ulong)instructions[instructionPointer + 1];
                var jumped = false;

                switch (opcode)
                {
                    case 0: // adv: division
                        RegisterA = (ulong)(RegisterA / Math.Pow(2, ComboOperand(operand)));
                        break;
                    case 1: // bxl: bitwise xor
                        RegisterB ^= operand;
                        break;
                    case 2: // bst
                        RegisterB = ComboOperand(operand) % 8UL;
                        break;
                    case 3: // jnz
                        if (RegisterA != 0)
                        {
                            instructionPointer = instructions[instructionPointer + 1];
                            jumped = true;
                        }
                        break;
                    case 4: // bxc
                        RegisterB ^= RegisterC;
                        break;
                    case 5: // out
                        outputs.Add((int)(ComboOperand(operand) % 8UL));
                        break;
                    case 6: // bdv
                        RegisterB = (ulong)(RegisterA / Math.Pow(2, ComboOperand(operand)));
                        break;
                    case 7: // cdv
                        RegisterC = (ulong)(RegisterA / Math.Pow(2, ComboOperand(operand)));
                        break;
                    default:
                        throw new NotSupportedException();
                }

                if (!jumped)
                {
                    instructionPointer += 2;
                }
            }

            //Console.WriteLine($"{outputs.Count} numbers output");
            return outputs;
        }

        public bool OutputsSelf()
        {
            var result = Run();
            return string.Join(",", Instructions).Equals(result);
        }

        private ulong ComboOperand(ulong operand) => operand switch
        {
            0 or 1 or 2 or 3 => operand,
            4 => RegisterA,
            5 => RegisterB,
            6 => RegisterC,
            _ => throw new NotSupportedException(),
        };
    }
}
