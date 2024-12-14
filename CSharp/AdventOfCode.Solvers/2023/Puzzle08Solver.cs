using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle08Solver : IPuzzleSolver
{
    private char[] _instructions = [];
    private IEnumerable<Node> _nodes = [];

    public string SolvePartOne(string input)
    {
        Init(input);
        return Travel(_nodes.First(n => n.Name == "AAA"), n => n.Name == "ZZZ").ToString();
    }

    public string SolvePartTwo(string input)
    {
        Init(input);
        var startingNodes = _nodes.Where(n => n.Name[2] == 'A');
        var steps = startingNodes.Select(n => Travel(n, n => n.Name[2] == 'Z')).Select(s => (long)s);
        var total = steps.Aggregate(MathExtensions.LCM);
        return total.ToString();
    }

    private void Init(string input)
    {
        var data = input.SplitByNewline(StringSplitOptions.None);
        _instructions = data[0].ToCharArray();
        _nodes = data.Skip(2).Select(l => new Node(l));
    }

    private int Travel(Node startingNode, Func<Node, bool> atEndCheck)
    {
        var currentNode = startingNode;
        var currentInstructionIndex = 0;
        var totalSteps = 0;

        while (!atEndCheck(currentNode))
        {
            var currentInstruction = _instructions[currentInstructionIndex];
            currentNode = _nodes.First(n => n.Name == currentNode.Apply(currentInstruction));
            totalSteps++;
            currentInstructionIndex++;
            if (currentInstructionIndex >= _instructions.Length)
            {
                currentInstructionIndex = 0;
            }
        }

        return totalSteps;
    }

    private class Node(string input)
    {
        public string Name { get; private set; } = input.Substring(0, 3);
        public string Left { get; private set; } = input.Substring(7, 3);
        public string Right { get; private set; } = input.Substring(12, 3);

        public string Apply(char direction) => direction == 'R' ? Right : Left;

        public override string ToString() => $"{Name} = ({Left}, {Right})";
    }
}
