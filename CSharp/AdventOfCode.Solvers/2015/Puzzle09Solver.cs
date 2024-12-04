using AdventOfCode.Core;
using AdventOfCode.Core.StringUtilities;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle09Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (edges, nodes) = BuildGraph(input);
        return RouteDistances(edges, nodes).Min().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var (edges, nodes) = BuildGraph(input);
        return RouteDistances(edges, nodes).Max().ToString();
    }

    /*
     * Since the puzzle input only has 8 cities, finding all
     * the different route permutations and summing up their
     * distances (brute force) is more than efficient enough
     * (even though e're double-counting!).
     */
    private static IEnumerable<int> RouteDistances(List<Edge> edges, List<string> nodes) =>
        nodes.AllPermutations().Select(p => p.Zip(p.Skip(1), (a, b) =>
        {
            return edges.Single(e =>
                e.From == a && e.To == b ||
                e.From == b && e.To == a)
            .Distance;
        }).Sum());

    private static (List<Edge>, List<string>) BuildGraph(string input)
    {
        var edges = input.SplitByNewline()
            .Select(l => DistanceDefinition().Match(l)).Select(m =>
                new Edge(m.Groups[1].Value,
                m.Groups[2].Value,
                int.Parse(m.Groups[3].Value)))
            .ToList();
        var nodes = edges.Select(d => d.From).Union(edges.Select(d => d.To)).Distinct().ToList();
        return (edges, nodes);
    }

    [GeneratedRegex(@"(\w+) to (\w+) = (\d+)")]
    private static partial Regex DistanceDefinition();

    private class Edge(string from, string to, int distance)
    {
        public string From { get; } = from;
        public string To { get; } = to;
        public int Distance { get; } = distance;
    }
}
