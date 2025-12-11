using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle11Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        FindAllPaths(ParseInput(input), "you", "out").Count;

    public override object SolvePartTwo(string input)
    {
        Cache.Clear();
        var map = ParseInput(input);
        var fromSvrToFft = FindAllPaths(map, "svr", "fft");
        var fromSvrToDac = FindAllPaths(map, "svr", "dac");
        Cache.Clear();
        var fromDacToFft = FindAllPaths(map, "dac", "fft");
        Cache.Clear();
        var fromFftToDac = FindAllPaths(map, "fft", "dac");
        Cache.Clear();

        if (fromDacToFft.Count > 0)
        {
            // svr -> dac -> fft -> out
            var fromFftToOut = FindAllPaths(map, "fft", "out");
            return (long)fromSvrToDac.Count * fromDacToFft.Count * fromFftToOut.Count;
        }
        else
        {
            // svr -> fft -> dac -> out
            var fromDacToOut = FindAllPaths(map, "dac", "out");
            return (long)fromSvrToFft.Count * fromFftToDac.Count * fromDacToOut.Count;
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

    private List<string[]> FindAllPaths(List<Node> map, string start, string end) =>
        [.. DepthFirstSearch(map, [], start, end)];

    private readonly Dictionary<string, List<string[]>> Cache = [];

    private List<string[]> DepthFirstSearch(List<Node> map, string[] pathSoFar, string startNodeId, string endNodeId)
    {
        if (Cache.TryGetValue(startNodeId, out var paths))
            return paths;

        var thisNode = map.Single(n => n.Id == startNodeId);
        string[] thisPath = [.. pathSoFar, thisNode.Id];

        if (thisNode.Id == endNodeId)
            return [thisPath];

        var result = thisNode.Outputs.SelectMany(n => DepthFirstSearch(map, thisPath, n, endNodeId)).ToList();
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
