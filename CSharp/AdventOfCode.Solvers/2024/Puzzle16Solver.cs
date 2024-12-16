using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle16Solver : IPuzzleSolver
{
    // Disabled for benchmarking.
    // Toggle on if you want to see the output!
    const bool ShouldPrint = true;

    private (int, int) Start;
    private (int, int) End;
    private List<Edge> Edges = [];

    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);

        Start = (1, matrix.Height - 2);
        End = (matrix.Width - 2, 1);

        var nodes = GetMapNodes(matrix);
        Edges = GetMapEdges(matrix, nodes);
        var (path, score) = BestPath();

        if (ShouldPrint)
            Print(matrix, path);
        return score.ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static List<(int, int)> GetMapNodes(CharacterMatrix matrix)
    {
        var nodes = new List<(int, int)>();
        // The border is always a solid wall.
        for (var y = 1; y < matrix.Height - 1; y++)
        {
            for (var x = 1; x < matrix.Width - 1; x++)
            {
                var charHere = matrix.CharAt(x, y);
                if (charHere != '#')
                {
                    var neighbors = matrix.CoordinatesOfNeighbors((x, y), allEight: false)
                        .Where(cn => matrix.CharAt(cn) != '#');
                    var neighborCount = neighbors.Count();
                    switch (neighborCount)
                    {
                        case 1:
                            // dead end --- ignore it
                            // (start and end aren't dead ends in the example
                            // or my input. not sure if that's always true)
                            break;
                        case 2:
                            var n1 = neighbors.First();
                            var n2 = neighbors.Last();
                            if (n1.Item1 != n2.Item1 && n1.Item2 != n2.Item2)
                            {
                                // Corner
                                nodes.Add((x, y));
                            }
                            break;
                        case 3:
                        case 4:
                            nodes.Add((x, y));
                            break;
                    }
                }
            }
        }
        return nodes;
    }

    private static List<Edge> GetMapEdges(CharacterMatrix matrix, List<(int, int)> nodes)
    {
        var edges = new List<Edge>();
        for (var i = 0; i < nodes.Count; i++)
        {
            var firstNode = nodes[i];
            // Search down and left for nodes
            var firstNodeRight = nodes.FirstOrDefault(n => n.Item1 > firstNode.Item1 && n.Item2 == firstNode.Item2);
            if (firstNodeRight != (0, 0) &&
                firstNodeRight.Item1 > firstNode.Item1) // probably redundant, but just in case
            {
                var spacesBetween = Enumerable.Range(firstNode.Item1, firstNodeRight.Item1 - firstNode.Item1);
                if (spacesBetween.All(s => matrix.CharAt(s, firstNode.Item2) != '#'))
                {
                    edges.Add(new Edge(firstNode, firstNodeRight));
                }
            }

            var firstNodeDown = nodes.FirstOrDefault(n => n.Item1 == firstNode.Item1 && n.Item2 > firstNode.Item2);
            if (firstNodeDown != (0, 0) &&
                firstNodeDown.Item2 > firstNode.Item2) // probably redundant, but just in case
            {
                var spacesBetween = Enumerable.Range(firstNode.Item2, firstNodeDown.Item2 - firstNode.Item2);
                if (spacesBetween.All(s => matrix.CharAt(firstNode.Item1, s) != '#'))
                {
                    edges.Add(new Edge(firstNode, firstNodeDown));
                }
            }
        }
        return edges;
    }

    private (List<(int, int)>, int) BestPath()
    {
        var currentMinimumScore = int.MaxValue;
        var bestPath = new List<(int, int)>();

        var visitedEdges = new HashSet<Edge>();
        var currentPath = new List<(int, int)>();
        DepthFirstSearch(Start, 0);

        void DepthFirstSearch((int, int) current, int currentScore)
        {
            currentPath.Add(current);

            if (currentScore >= currentMinimumScore)
            {
                currentPath.RemoveAt(currentPath.Count - 1);
                return;
            }

            if (current == End)
            {
                if (currentScore < currentMinimumScore)
                {
                    currentMinimumScore = currentScore;
                    bestPath = new List<(int, int)>(currentPath);
                }

                currentPath.RemoveAt(currentPath.Count - 1);
                return;
            }
            else
            {
                foreach (var edge in Edges)
                {
                    if (edge.HasEndAt(current))
                    {
                        var next = edge.OtherEndFrom(current);

                        // ensure not already visited
                        if (visitedEdges.Add(edge))
                        {
                            var edgeScore = EdgeScore(edge, currentPath);
                            DepthFirstSearch(next, currentScore + edgeScore);
                            visitedEdges.Remove(edge);
                        }
                    }
                }
            }
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        return (bestPath, currentMinimumScore);
    }

    private static int EdgeScore(Edge edge, List<(int, int)> currentPath)
    {
        var score = edge.Length;
        var isFacingLeftOrRight = currentPath.Count < 2 ||
                                  currentPath[^1].Item1 != currentPath[^2].Item1;

        if (edge.IsHorizontal != isFacingLeftOrRight)
        {
            score += 1000;
        }

        return score;
    }

    private record Edge((int, int) NodeLU, (int, int) NodeRD)
    {
        public bool Equals((int, int) a, (int, int) b) =>
            a == NodeLU && b == NodeRD ||
            a == NodeRD && b == NodeLU;
        public bool HasEndAt((int, int) here) => NodeRD == here || NodeLU == here;
        public (int, int) OtherEndFrom((int, int) here) => NodeRD == here ? NodeLU : NodeRD;
        public bool IsHorizontal => NodeRD.Item2 == NodeLU.Item2;
        public int Length => IsHorizontal
            ? NodeRD.Item1 - NodeLU.Item1
            : NodeRD.Item2 - NodeLU.Item2;
    }

    private static void Print(CharacterMatrix matrix, List<(int, int)> path)
    {
        for (var y = 0; y < matrix.Height; y++)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                if (path.Contains((x, y)))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(matrix.CharAt(x, y));
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
