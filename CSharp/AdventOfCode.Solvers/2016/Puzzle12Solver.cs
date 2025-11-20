using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle12Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Run(input, false).ToString();
    public override string SolvePartTwo(string input) => Run(input, true).ToString();

    private static int Run(string input, bool isPartTwo)
    {
        int index = 0;
        var registers = new int[4];
        var instructions = input.SplitByNewline()
            .Select(Instruction.Parse)
            .ToArray();

        if (isPartTwo)
        {
            registers[2] = 1;
        }

        while (index < instructions.Length)
        {
            var instruction = instructions[index];
            if (instruction is CopyValueInstruction copyValueInstruction)
            {
                registers[copyValueInstruction.Target] = copyValueInstruction.Value;
                index++;
            }
            else if (instruction is CopyRegisterInstruction copyRegisterInstruction)
            {
                registers[copyRegisterInstruction.Target] = registers[copyRegisterInstruction.Source];
                index++;
            }
            else if (instruction is IncrementInstruction incrementInstruction)
            {
                registers[incrementInstruction.Target]++;
                index++;
            }
            else if (instruction is DecrementInstruction decrementInstruction)
            {
                registers[decrementInstruction.Target]--;
                index++;
            }
            else if (instruction is JumpIfNotZeroInstruction jumpInstruction)
            {
                if (registers[jumpInstruction.Register] != 0)
                {
                    index += jumpInstruction.Offset;
                }
                else
                {
                    index++;
                }
            }
            else if (instruction is JumpAlwaysInstruction jumpAlwaysInstruction)
            {
                index += jumpAlwaysInstruction.Offset;
            }
        }

        return registers[0];
    }

    private abstract class Instruction
    {
        public static Instruction Parse(string input)
        {
            var args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            switch (args[0])
            {
                case "cpy":
                    var isCpyValue = Regexes.Integer().IsMatch(args[1]);
                    return isCpyValue
                        ? new CopyValueInstruction(int.Parse(args[1]), RegisterToInt(args[2]))
                        : new CopyRegisterInstruction(RegisterToInt(args[1]), RegisterToInt(args[2]));
                case "inc":
                    return new IncrementInstruction(RegisterToInt(args[1]));
                case "dec":
                    return new DecrementInstruction(RegisterToInt(args[1]));
                case "jnz":
                    var isJnzValue = Regexes.Integer().IsMatch(args[1]);
                    return isJnzValue
                        ? new JumpAlwaysInstruction(int.Parse(args[2]))
                        : new JumpIfNotZeroInstruction(RegisterToInt(args[1]), int.Parse(args[2]));
                default:
                    throw new InvalidOperationException($"Invalid instruction {args[0]}");
            }
        }

        private static int RegisterToInt(string register) => register[0] - 'a';
    }


    private sealed class CopyValueInstruction(int value, int target) : Instruction
    {
        public int Value { get; } = value;
        public int Target { get; } = target;
    }

    private sealed class CopyRegisterInstruction(int source, int target) : Instruction
    {
        public int Source { get; } = source;
        public int Target { get; } = target;
    }

    private sealed class IncrementInstruction(int target) : Instruction
    {
        public int Target { get; } = target;
    }

    private sealed class DecrementInstruction(int target) : Instruction
    {
        public int Target { get; } = target;
    }

    private sealed class JumpIfNotZeroInstruction(int register, int offset) : Instruction
    {
        public int Register { get; } = register;
        public int Offset { get; } = offset;
    }

    private sealed class JumpAlwaysInstruction(int offset) : Instruction
    {
        public int Offset { get; } = offset;
    }
}
