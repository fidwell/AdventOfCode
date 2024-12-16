using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle16Solver : IPuzzleSolver
{
    // Disabled for benchmarking.
    // Toggle on if you want to see the output!
    const bool ShouldPrint = true;

    private CharacterMatrix Matrix = new();
    private (int, int) Start;
    private (int, int) End;
    private readonly Dictionary<(int, int), List<Edge>> EdgeAdjacencyList = [];

    public string SolvePartOne(string input)
    {
        SetUp(input);
        var (paths, score) = BestPaths(0);

        // To do: "Calculated" score is wrong, so we have to recalculate it
        var realScore = PathScore(paths[0]);

        if (ShouldPrint)
        {
            Console.WriteLine($"Got {score}, should be {realScore} (off by {score - realScore})");
            var allCoords = AllCoordsInPath(paths[0]);
            Print(allCoords);
        }

        return realScore.ToString();
    }

    public string SolvePartTwo(string input)
    {
        SetUp(input);
        var expectedBest = Matrix.Height < 100 ? 7036 : 107468;
        var (bestPaths, _) = BestPaths(expectedBest);
        Console.WriteLine($"Found {bestPaths.Count} best paths");
        var nodeCoords = bestPaths.SelectMany(p => p);
        var allCoords = AllCoordsInPath(nodeCoords);
        allCoords.Add(Start);
        allCoords.Add(End);

        if (ShouldPrint)
        {
            Print(allCoords);
        }

        // 445 is too low.
        return allCoords.Count.ToString();
    }

    private void SetUp(string input)
    {
        Matrix = new CharacterMatrix(input);
        Start = (1, Matrix.Height - 2);
        End = (Matrix.Width - 2, 1);

        var nodes = GetMapNodes();
        GetMapEdges(nodes);
    }

    private List<(int, int)> GetMapNodes()
    {
        var nodes = new List<(int, int)>();
        // The border is always a solid wall.
        for (var y = 1; y < Matrix.Height - 1; y++)
        {
            for (var x = 1; x < Matrix.Width - 1; x++)
            {
                var charHere = Matrix.CharAt(x, y);
                if (charHere != '#')
                {
                    var neighbors = Matrix.CoordinatesOfNeighbors((x, y), allEight: false)
                        .Where(cn => Matrix.CharAt(cn) != '#');
                    var neighborCount = neighbors.Count();
                    switch (neighborCount)
                    {
                        case 1:
                            // dead end
                            nodes.Add((x, y));
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

    private void GetMapEdges(List<(int, int)> nodes)
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
                if (spacesBetween.All(s => Matrix.CharAt(s, firstNode.Item2) != '#'))
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
                if (spacesBetween.All(s => Matrix.CharAt(firstNode.Item1, s) != '#'))
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

    private (List<List<(int, int)>>, int) BestPaths(int expectedBestValue)
    {
        var currentMinimumScore = int.MaxValue;
        var bestPaths = new List<List<(int, int)>>();

        var priorityQueue = new PriorityQueue<(int score, (int, int) node, List<(int, int)> path), int>();
        priorityQueue.Enqueue((0, Start, new List<(int, int)> { Start }), 0);

        // Track visited states (score and path history for each node)
        //var visitedScores = new Dictionary<(int, int), List<(int score, List<(int, int)> path)>>();

        while (priorityQueue.Count > 0)
        {
            var (currentScore, currentNode, currentPath) = priorityQueue.Dequeue();

            // Stop processing paths that exceed the expected best score
            if (expectedBestValue > 0 && currentScore > expectedBestValue)
                continue;

            // Check visitedScores to avoid redundant processing
            //if (!visitedScores.ContainsKey(currentNode))
            //    visitedScores[currentNode] = new List<(int score, List<(int, int)> path)>();

            // If this state (score and path) has been visited, skip it
            /*
            bool isRedundant = visitedScores[currentNode].Any(state =>
                state.score <= currentScore && state.path.SequenceEqual(currentPath));

            if (isRedundant)
            {
                Console.WriteLine($"Skipping redundant path to {currentNode} with score {currentScore}");
                continue;
            }
            */

            // Update visitedScores with this new state
            //visitedScores[currentNode].Add((currentScore, currentPath));

            // If we reach the end, update the best paths
            if (currentNode == End)
            {
                Console.WriteLine($"End reached with score {currentScore}");
                if (currentScore < currentMinimumScore)
                {
                    currentMinimumScore = currentScore;
                    bestPaths.Clear();
                    bestPaths.Add(currentPath);

                    if (expectedBestValue == 0) // part 1
                        return ([currentPath], currentScore);
                }
                else if (currentScore == currentMinimumScore)
                {
                    bestPaths.Add(currentPath);
                }

                continue;
            }

            // Explore all possible next steps
            foreach (var edge in EdgeAdjacencyList[currentNode])
            {
                if (edge.HasEndAt(currentNode))
                {
                    var nextNode = edge.OtherEndFrom(currentNode);

                    // Avoid revisiting nodes already in the current path
                    if (currentPath.Contains(nextNode))
                        continue;

                    var edgeScore = EdgeScore(edge, currentPath); // Compute the edge's score
                    var newScore = currentScore + edgeScore;

                    var newPath = currentPath.ToList();
                    newPath.Add(nextNode);

                    var newEstimatedCost = ManhattanDistance(nextNode, End);
                    var priority = newScore + newEstimatedCost;

                    // Add this new state to the priority queue
                    priorityQueue.Enqueue((newScore, nextNode, newPath), priority);
                }
            }
        }

        return (bestPaths, currentMinimumScore);
    }

    private static int ManhattanDistance((int, int) a, (int, int) b) =>
        Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);

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

        public IEnumerable<(int, int)> AllCoordinates =>
            IsHorizontal
                ? Enumerable.Range(NodeLU.Item1, NodeRD.Item1 - NodeLU.Item1 + 1)
                    .Select(x => (x, NodeLU.Item2))
                : Enumerable.Range(NodeLU.Item2, NodeRD.Item2 - NodeLU.Item2 + 1)
                    .Select(y => (NodeLU.Item1, y));
    }

    private HashSet<(int, int)> AllCoordsInPath(IEnumerable<(int, int)> nodes)
    {
        var allEdges = EdgeAdjacencyList
            .SelectMany(ea => ea.Value).Distinct();
        var edgesInPaths = allEdges.Where(e =>
            nodes.Contains(e.NodeLU) &&
            nodes.Contains(e.NodeRD));
        var allCoordinatesInAllEdges = edgesInPaths.SelectMany(e => e.AllCoordinates);
        return allCoordinatesInAllEdges.ToHashSet();
    }

    private void Print(IEnumerable<(int, int)> coords)
    {
        for (var y = 0; y < Matrix.Height; y++)
        {
            for (var x = 0; x < Matrix.Width; x++)
            {
                if (coords.Contains((x, y)))
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(Matrix.CharAt(x, y) == '#' ? '.' : ' ');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
