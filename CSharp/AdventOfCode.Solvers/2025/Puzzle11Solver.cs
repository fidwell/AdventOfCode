using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle11Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        DepthFirstSearch(ParseInput(input), "you", "out");

    public override object SolvePartTwo(string input)
    {
        Cache.Clear();
        var map = ParseInput(input);
        var fromSvrToFft = DepthFirstSearch(map, "svr", "fft");
        var fromSvrToDac = DepthFirstSearch(map, "svr", "dac");
        Cache.Clear();
        var fromDacToFft = DepthFirstSearch(map, "dac", "fft");
        Cache.Clear();
        var fromFftToDac = DepthFirstSearch(map, "fft", "dac");
        Cache.Clear();

        if (fromDacToFft > 0)
        {
            // svr -> dac -> fft -> out
            var fromFftToOut = DepthFirstSearch(map, "fft", "out");
            return (long)fromSvrToDac * fromDacToFft * fromFftToOut;
        }
        else
        {
            // svr -> fft -> dac -> out
            var fromDacToOut = DepthFirstSearch(map, "dac", "out");
            return (long)fromSvrToFft * fromFftToDac * fromDacToOut;
        }
    }

    private static List<Node> ParseInput(string input)
    {
        var map = input.SplitByNewline()
            .Select(l => l.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries))
            .Select(l => new Node(l.First(), l.Skip(1)))
            .ToList();
        map.Add(new Node("out", []));
        return map;
    }

    private readonly Dictionary<string, int> Cache = [];

    private int DepthFirstSearch(List<Node> map, string startNodeId, string endNodeId, int pathSoFar = 0)
    {
        if (Cache.TryGetValue(startNodeId, out var pathLength))
            return pathLength;

        var thisNode = map.Single(n => n.Id == startNodeId);
        if (thisNode.Id == endNodeId)
            return 1;

        var result = thisNode.Outputs.Sum(n => DepthFirstSearch(map, n, endNodeId, pathSoFar + 1));
        Cache.Add(startNodeId, result);
        return result;
    }

    private class Node(string id, IEnumerable<string> neighbors)
    {
        public string Id { get; } = id;
        public HashSet<string> Outputs { get; } = [.. neighbors];
        public override string ToString() => $"{Id}: {string.Join(", ", Outputs)}";
    }
}
