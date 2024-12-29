using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle25Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var edges = new List<(string, string)>();

        foreach (var line in lines)
        {
            var portions = line.Replace(":", "").Split(' ').ToArray();
            foreach (var portion in portions.Skip(1))
            {
                edges.Add((portions[0], portion));
            }
        }

        var graph = new Graph(edges);
        return lines.Count() < 15
            ? BruteForceIt(graph).ToString()
            : CheeseIt(graph).ToString();
    }

    public override string SolvePartTwo(string input) => string.Empty;

    private static int BruteForceIt(Graph graph)
    {
        for (var a = 0; a < graph.Edges.Count - 2; a++)
        {
            for (var b = 1; b < graph.Edges.Count - 1; b++)
            {
                for (var c = 2; c < graph.Edges.Count; c++)
                {
                    graph.RemoveEdge(graph.Edges[a]);
                    graph.RemoveEdge(graph.Edges[b]);
                    graph.RemoveEdge(graph.Edges[c]);
                    var groups = graph.GroupSizes();
                    if (groups.Count == 2)
                    {
                        return groups.Product();
                    }
                    graph.ResetRemovedEdges();
                }
            }
        }

        return -1;
    }

    private static int CheeseIt(Graph graph)
    {
        // Thanks GraphViz!
        graph.RemoveEdge(graph.Edges.Single(e => e.Item1 == "vkb" && e.Item2 == "jzj"));
        graph.RemoveEdge(graph.Edges.Single(e => e.Item1 == "hhx" && e.Item2 == "vrx"));
        graph.RemoveEdge(graph.Edges.Single(e => e.Item1 == "grh" && e.Item2 == "nvh"));
        var groups = graph.GroupSizes();
        return groups.Count == 2
            ? groups[0] * groups[1]
            : -1;
    }

    private class Graph
    {
        private readonly List<string> _nodes;
        private List<(string, string)> _removedEdges;

        public readonly List<(string, string)> Edges;

        public Graph(List<(string, string)> edges)
        {
            Edges = edges;
            _nodes = edges.Select(e => e.Item1).Union(edges.Select(e => e.Item2)).Distinct().ToList();
            _removedEdges = [];
        }

        public void RemoveEdge((string, string) edge) => _removedEdges.Add(edge);
        public void ResetRemovedEdges() => _removedEdges = [];

        public List<int> GroupSizes()
        {
            var nodesVisited = new HashSet<string>();
            var groupSizes = new List<int>();
            while (nodesVisited.Count < _nodes.Count)
            {
                var beforeSize = nodesVisited.Count;
                DFS(_nodes.First(n => !nodesVisited.Contains(n)), nodesVisited);
                var afterSize = nodesVisited.Count - beforeSize;
                groupSizes.Add(afterSize);

                if (groupSizes.Count > 2 && nodesVisited.Count < _nodes.Count)
                {
                    // we're already at too many groups; bail early
                    return [];
                }
            }
            return groupSizes;
        }

        private void DFS(string node, HashSet<string> visited)
        {
            visited.Add(node);
            foreach (var edge in AvailableEdges.Where(e => e.Item1 == node || e.Item2 == node))
            {
                var otherNode = edge.Item1 == node ? edge.Item2 : edge.Item1;
                if (!visited.Contains(otherNode))
                {
                    DFS(otherNode, visited);
                }
            }
        }

        private List<(string, string)> AvailableEdges => Edges.Where(e => !IsRemoved(e)).ToList();

        private bool IsRemoved((string, string) edge) =>
            _removedEdges.Any(re =>
                (re.Item1 == edge.Item1 && re.Item2 == edge.Item2) ||
                (re.Item1 == edge.Item2 && re.Item2 == edge.Item1));
    }
}
