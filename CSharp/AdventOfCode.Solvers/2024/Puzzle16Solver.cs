using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle16Solver : IPuzzleSolver
{
    // Disabled for benchmarking.
    // Toggle on if you want to see the output!
    const bool ShouldPrint = true;

    private (int, int) Start;
    private (int, int) End;
    private readonly Dictionary<(int, int), List<Edge>> EdgeAdjacencyList = [];

    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);

        Start = (1, matrix.Height - 2);
        End = (matrix.Width - 2, 1);

        var nodes = GetMapNodes(matrix);
        GetMapEdges(matrix, nodes);
        var (path, _) = BestPath();

        if (ShouldPrint)
            Print(matrix, path);

        // To do: "Calculated" score is wrong, so we have to recalculate it
        return PathScore(path).ToString();
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

    private void GetMapEdges(CharacterMatrix matrix, List<(int, int)> nodes)
    {
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
                    if (!EdgeAdjacencyList.ContainsKey(firstNode))
                        EdgeAdjacencyList[firstNode] = [];
                    if (!EdgeAdjacencyList.ContainsKey(firstNodeRight))
                        EdgeAdjacencyList[firstNodeRight] = [];
                    EdgeAdjacencyList[firstNode].Add(new Edge(firstNode, firstNodeRight));
                    EdgeAdjacencyList[firstNodeRight].Add(new Edge(firstNode, firstNodeRight));
                }
            }

            var firstNodeDown = nodes.FirstOrDefault(n => n.Item1 == firstNode.Item1 && n.Item2 > firstNode.Item2);
            if (firstNodeDown != (0, 0) &&
                firstNodeDown.Item2 > firstNode.Item2) // probably redundant, but just in case
            {
                var spacesBetween = Enumerable.Range(firstNode.Item2, firstNodeDown.Item2 - firstNode.Item2);
                if (spacesBetween.All(s => matrix.CharAt(firstNode.Item1, s) != '#'))
                {
                    if (!EdgeAdjacencyList.ContainsKey(firstNode))
                        EdgeAdjacencyList[firstNode] = [];
                    if (!EdgeAdjacencyList.ContainsKey(firstNodeDown))
                        EdgeAdjacencyList[firstNodeDown] = [];
                    EdgeAdjacencyList[firstNode].Add(new Edge(firstNode, firstNodeDown));
                    EdgeAdjacencyList[firstNodeDown].Add(new Edge(firstNode, firstNodeDown));
                }
            }
        }
    }

    private (List<(int, int)>, int) BestPath()
    {
        var currentMinimumScore = int.MaxValue;
        var bestPath = new List<(int, int)>();

        var priorityQueue = new SortedSet<(int score, (int, int) node, List<(int, int)> path)>(
            Comparer<(int, (int, int), List<(int, int)>)>.Create((a, b) => a.Item1 != b.Item1 ? a.Item1.CompareTo(b.Item1) : a.Item2.CompareTo(b.Item2))
        );
        var visitedScores = new Dictionary<(int, int), int>();

        priorityQueue.Add((0, Start, new List<(int, int)> { Start }));

        while(priorityQueue.Count > 0)
        {
            var (currentScore, currentNode, currentPath) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (visitedScores.TryGetValue(currentNode, out var bestScoreAtNode) && currentScore >= bestScoreAtNode)
                continue;

            visitedScores[currentNode] = currentScore;

            if (currentNode == End)
            {
                bestPath = currentPath;
                currentMinimumScore = currentScore;
                break;
            }

            foreach (var edge in EdgeAdjacencyList[currentNode])
            {
                if (edge.HasEndAt(currentNode))
                {
                    var nextNode = edge.OtherEndFrom(currentNode);
                    var edgeScore = EdgeScore(edge, currentPath);
                    var heuristic = ManhattanDistance(nextNode, End);
                    var newScore = currentScore + edgeScore;
                    if (newScore < currentMinimumScore)
                    {
                        var newPath = new List<(int, int)>(currentPath) { nextNode };
                        priorityQueue.Add((newScore + heuristic, nextNode, newPath));
                    }
                }
            }
        }

        return (bestPath, currentMinimumScore);
    }

    private static int ManhattanDistance((int x, int y) current, (int x, int y) end) =>
        Math.Abs(current.x - end.x) + Math.Abs(current.y - end.y);

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

    private int PathScore(List<(int, int)> path)
    {
        var score = 0;
        var isFacingLeftOrRight = true; // starting condition given in the puzzle
        for (var i = 0; i < path.Count - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];
            var edge = EdgeAdjacencyList[from].First(e => e.NodeLU == to || e.NodeRD == to);

            if (edge.IsHorizontal != isFacingLeftOrRight)
            {
                // we must turn
                score += 1000;
                isFacingLeftOrRight = !isFacingLeftOrRight;
            }
            score += edge.Length;
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
                    Console.Write(matrix.CharAt(x, y) == '#' ? '.' : ' ');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
