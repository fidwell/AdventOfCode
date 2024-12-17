using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle17Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => string.Join(",", new Computer(input).Run());

    public string SolvePartTwo(string input)
    {
        var computer = new Computer(input);
        // todo
        return computer.RegisterA.ToString();
    }

    private class Computer
    {
        public ulong RegisterA { get; private set; }
        public int RegisterB { get; private set; } = 0;
        public int RegisterC { get; private set; } = 0;
        public List<int> Instructions { get; private set; }

        public Computer(string input)
        {
            var lines = input.SplitByNewline();
            RegisterA = ulong.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
            Instructions = new List<int>(Regexes.Digit().Matches(lines[3]).Select(m => int.Parse(m.Value)));
        }

        public List<int> Run()
        {
            var instructionPointer = 0;
            var outputs = new List<int>();

            while (instructionPointer < Instructions.Count)
            {
                var opcode = Instructions[instructionPointer];
                var operand = Instructions[instructionPointer + 1];
                var jumped = false;

                switch (opcode)
                {
                    case 0: // adv: division
                        RegisterA >>= ComboOperand(operand);
                        break;
                    case 1: // bxl: bitwise xor
                        RegisterB ^= operand;
                        break;
                    case 2: // bst
                        RegisterB = ComboOperand(operand) & 0b111;
                        break;
                    case 3: // jnz
                        if (RegisterA != 0)
                        {
                            instructionPointer = Instructions[instructionPointer + 1];
                            jumped = true;
                        }
                        break;
                    case 4: // bxc
                        RegisterB ^= RegisterC;
                        break;
                    case 5: // out
                        outputs.Add(ComboOperand(operand) & 0b111);
                        break;
                    case 6: // bdv
                        RegisterB = (int)(RegisterA >> ComboOperand(operand));
                        break;
                    case 7: // cdv
                        RegisterC = (int)(RegisterA >> ComboOperand(operand));
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

        public bool OutputsSelf()
        {
            var result = Run();
            return string.Join(",", Instructions).Equals(result);
        }

        private int ComboOperand(int operand) => operand switch
        {
            0 or 1 or 2 or 3 => operand,
            4 => (int)RegisterA, // probably safe
            5 => RegisterB,
            6 => RegisterC,
            _ => throw new NotSupportedException(),
        };
    }
}
