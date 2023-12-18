using AdventOfCode.Core.ArrayUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle18Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var instructions = input.Split(Environment.NewLine).Select(l => new Instruction(l));
        throw new NotImplementedException();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private class Instruction
    {
        public Direction Dir { get; }
        public int Amount { get; }
        public string Color { get; }

        public Instruction(string input)
        {
            var split = input.Split(' ');
            Dir = split[0][0].ToDirection();
            Amount = int.Parse(split[1]);
            Color = split[2]; //.Replace("(", "").Replace("#", "").Replace(")", "");
        }
    }
}
