using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle16Solver : IPuzzleSolver
{
    private (int, int) Start;
    private (int, int) End;
    private List<Leg> Edges = [];

    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);

        Start = (1, matrix.Height - 2);
        End = (matrix.Width - 2, 1);

        var nodes = GetMapNodes(matrix);
        Edges = GetMapLegs(matrix, nodes);
        var paths = AllPaths();
        var scores = paths.Select(PathScore);
        return scores.Min().ToString();
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

    private static List<Leg> GetMapLegs(CharacterMatrix matrix, List<(int, int)> nodes)
    {
        var legs = new List<Leg>();
        for (var i = 0; i < nodes.Count; i++)
        {
            var firstNode = nodes[i];
            // Search down and left for nodes
            var firstNodeRight = nodes.FirstOrDefault(n => n.Item1 > firstNode.Item1 && n.Item2 == firstNode.Item2);
            if (firstNodeRight != (0, 0))
            {
                var spacesBetween = Enumerable.Range(firstNode.Item1, firstNodeRight.Item1 - firstNode.Item1);
                if (spacesBetween.All(s => matrix.CharAt(s, firstNode.Item2) == '.'))
                {
                    legs.Add(new Leg(firstNode, firstNodeRight));
                }
            }

            var firstNodeDown = nodes.FirstOrDefault(n => n.Item1 == firstNode.Item1 && n.Item2 > firstNode.Item2);
            if (firstNodeDown != (0, 0))
            {
                var spacesBetween = Enumerable.Range(firstNode.Item2, firstNodeDown.Item2 - firstNode.Item2);
                if (spacesBetween.All(s => matrix.CharAt(firstNode.Item1, s) == '.'))
                {
                    legs.Add(new Leg(firstNode, firstNodeDown));
                }
            }
        }
        return legs;
    }

    private List<List<(int, int)>> AllPaths()
    {
        var allPaths = new List<List<(int, int)>>();
        var visitedLegs = new List<Leg>();
        var currentPath = new List<(int, int)>();
        DepthFirstSearch(Start);

        void DepthFirstSearch((int, int) current)
        {
            currentPath.Add(current);

            if (current == End)
            {
                allPaths.Add(new List<(int, int)>(currentPath));
            }
            else
            {
                foreach (var leg in Edges)
                {
                    if (leg.HasEndAt(current))
                    {
                        var next = leg.OtherEndFrom(current);
                        // ensure not already visited
                        if (!visitedLegs.Any(l => l.Equals(current, next)))
                        {
                            visitedLegs.Add(leg);
                            DepthFirstSearch(next);
                            visitedLegs.Remove(leg);
                        }
                    }
                }
            }
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        return allPaths;
    }

    private int PathScore(List<(int, int)> path)
    {
        var score = 0;
        var isFacingLeftOrRight = true; // starting condition given in the puzzle
        for (var i = 0; i < path.Count - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];
            var edge = Edges.Single(e => e.Equals(from, to));

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

    private record Leg((int, int) NodeLU, (int, int) NodeRD)
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
}
