using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle09Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        // build graph
        var edges = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => DistanceDefinition().Match(l)).Select(m =>
                new Edge(m.Groups[1].Value,
                m.Groups[2].Value,
                int.Parse(m.Groups[3].Value)))
            .ToList();
        var nodes = edges.Select(d => d.From).Union(edges.Select(d => d.To)).Distinct().ToList();

        // only 7 cities. brute force!
        return nodes.AllPermutations().Min(p => p.Zip(p.Skip(1), (a, b) =>
        {
            return edges.Single(e =>
                e.From == a && e.To == b ||
                e.From == b && e.To == a)
            .Distance;
        }).Sum()).ToString();
    }

    public string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
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
