using AdventOfCode.Solvers;

namespace AdventOfCode.Core.PuzzleSolvers._2022;

public class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (stacks, moves) = SetUp(input);

        foreach (var move in moves)
        {
            for (var i = 0; i < move.Count; i++)
            {
                stacks[move.To - 1].Push(stacks[move.From - 1].Pop());
            }
        }

        return new string(stacks.Select(s => s.Pop()).ToArray());
    }

    public string SolvePartTwo(string input)
    {
        var (stacks, moves) = SetUp(input);

        foreach (var move in moves)
        {
            var popped = new Stack<char>();
            for (var i = 0; i < move.Count; i++)
            {
                popped.Push(stacks[move.From - 1].Pop());
            }
            while (popped.Count > 0)
            {
                stacks[move.To - 1].Push(popped.Pop());
            }
        }

        return new string(stacks.Select(s => s.Pop()).ToArray());
    }

    private (List<Stack<char>>, IEnumerable<Move>) SetUp(string input)
    {
        var lines = input.Split(Environment.NewLine).ToArray();

        var moveStartLine = lines.TakeWhile(l => !l.StartsWith("move")).Count();
        var stackNumberLine = moveStartLine - 2;
        var stackCount = lines[stackNumberLine].Max() - '0';

        var stacks = new List<Stack<char>>();
        for (var i = 0; i < stackCount; i++)
        {
            stacks.Add([]);
        }

        for (var i = stackNumberLine - 1; i >= 0; i--)
        {
            for (var s = 0; s < stackCount; s++)
            {
                var c = lines[i][s * 4 + 1];
                if (c != ' ')
                {
                    stacks[s].Push(c);
                }
            }
        }

        var moves = lines.Skip(moveStartLine).Select(l =>
        {
            var portions = l.Split(' ');
            return new Move(int.Parse(portions[1]), int.Parse(portions[3]), int.Parse(portions[5]));
        });

        return (stacks, moves);
    }

    private class Move(int count, int from, int to)
    {
        public readonly int Count = count;
        public readonly int From = from;
        public readonly int To = to;
    }
}
