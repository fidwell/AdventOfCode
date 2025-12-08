using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2023;

public class Puzzle22Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var (bricks, graph) = Initialize(input);
        return bricks.Count(b => bricks.Count - graph.IfRemoveNode(b.Id).Count(b => b != -1) - 1 == 0);
    }

    public override object SolvePartTwo(string input)
    {
        var (bricks, graph) = Initialize(input);
        return bricks.Sum(b => bricks.Count - graph.IfRemoveNode(b.Id).Count(b => b != -1) - 1);
    }

    private (List<Brick>, Graph) Initialize(string input)
    {
        var bricks = input.SplitByNewline()
            .Select((l, i) => new Brick(l, i))
            .OrderBy(b => b.Point1.Z)
            .ToList();

        // Not general, but this is my input
        const int maxX = 9;
        const int maxY = 9;
        var occupiedZs =
            Enumerable.Range(0, maxX + 1).SelectMany(x =>
            Enumerable.Range(0, maxY + 1).Select(y =>
                (x, y, 0)))
            .ToList();

        // Part 1. Fall
        foreach (var brick in bricks)
        {
            // Spaces under this cube are all elements in occupiedZs (oz) where
            // oz.X is in allcubes.x and oz.Y is in allcubes.y.
            // We can fall down to one more than the max Z in that list.
            var spacesUnder = occupiedZs.Where(oz =>
                brick.AllCubes.Select(c => c.X).Contains(oz.x) &&
                brick.AllCubes.Select(c => c.Y).Contains(oz.y));
            var maxZ = spacesUnder.Max(oz => oz.Item3);
            var minZOfThis = brick.Point1.Z; // Should be enforced by brick constructor
            var fallAmount = minZOfThis - maxZ - 1;
            brick.Fall(fallAmount);

            if (brick.Point1.Z < 1)
                throw new SolutionNotFoundException("Fell too far");

            foreach (var p in brick.AllCubes)
            {
                var occupiedZHere = occupiedZs.Single(oz => oz.x == p.X && oz.y == p.Y);
                occupiedZs.Remove(occupiedZHere);
                occupiedZs.Add((p.X, p.Y, Math.Max(occupiedZHere.Item3, p.Z)));
            }
        }

        // 2. Create dependency graph
        var edgeDictionary = new Dictionary<int, List<int>>();
        var bottomBricks = new List<int>();
        for (var i = 0; i < bricks.Count; i++)
        {
            var thisBrick = bricks[i];
            if (thisBrick.Point1.Z == 1)
            {
                bottomBricks.Add(thisBrick.Id);
            }

            for (var j = i + 1; j < bricks.Count; j++)
            {
                var thatBrick = bricks[j];
                if (Supports(thisBrick, thatBrick))
                {
                    if (!edgeDictionary.TryGetValue(thisBrick.Id, out List<int>? value))
                    {
                        value = [];
                        edgeDictionary.Add(thisBrick.Id, value);
                    }

                    value.Add(thatBrick.Id);
                }
            }
        }
        edgeDictionary.Add(-1, bottomBricks);
        var graph = new Graph(edgeDictionary, ShouldPrint);
        return (bricks, graph);
    }

    private static bool Supports(Brick lower, Brick higher) =>
        lower.AllCubes.Any(lc => higher.AllCubes.Any(hc =>
            hc.X == lc.X &&
            hc.Y == lc.Y &&
            hc.Z == lc.Z + 1));

    private class Brick
    {
        public readonly int Id;
        public readonly Point3d Point1;
        public readonly Point3d Point2;
        public List<Point3d> AllCubes;

        public Brick(string input, int id)
        {
            Id = id;
            var portions = input.Split('~').Select(p => new Point3d(p)).OrderBy(p => p.Z).ToList();
            Point1 = portions[0];
            Point2 = portions[1];
            AllCubes = [.. GetAllPoints()];
        }

        public char IdChar => (char)('A' + Id);

        public void Fall(int amount)
        {
            Point1.Z -= amount;
            Point2.Z -= amount;
            AllCubes = [.. GetAllPoints()];
        }

        private IEnumerable<Point3d> GetAllPoints()
        {
            if (Point1.X != Point2.X)
            {
                var start = Point1.X < Point2.X ? Point1.X : Point2.X;
                var end = Point1.X > Point2.X ? Point1.X : Point2.X;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(x => new Point3d(x, Point1.Y, Point1.Z));
            }
            else if (Point1.Y != Point2.Y)
            {
                var start = Point1.Y < Point2.Y ? Point1.Y : Point2.Y;
                var end = Point1.Y > Point2.Y ? Point1.Y : Point2.Y;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(y => new Point3d(Point1.X, y, Point1.Z));
            }
            else if (Point1.Z != Point2.Z)
            {
                var start = Point1.Z < Point2.Z ? Point1.Z : Point2.Z;
                var end = Point1.Z > Point2.Z ? Point1.Z : Point2.Z;
                var count = end - start + 1;
                return Enumerable.Range(start, count).Select(z => new Point3d(Point1.X, Point1.Y, z));
            }
            else
                return [Point1];
        }
    }

    private record Point3d
    {
        public readonly int X;
        public readonly int Y;
        public int Z;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3d(string input)
        {
            var portions = input.Split(',').Select(int.Parse).ToArray();
            X = portions[0];
            Y = portions[1];
            Z = portions[2];
        }
    }

    private class Graph
    {
        private readonly Dictionary<int, List<int>> _edges;

        private HashSet<int> _nodesStillVisible;

        public Graph(Dictionary<int, List<int>> edges, bool shouldPrint)
        {
            _edges = edges;
            _nodesStillVisible = [];

            if (shouldPrint)
            {
                foreach (var parent in _edges)
                {
                    foreach (var child in parent.Value)
                    {
                        Console.WriteLine($"{parent.Key} -> {child};");
                    }
                }
            }
        }

        public HashSet<int> IfRemoveNode(int removedId)
        {
            _nodesStillVisible = [];
            var remainingEdges = CopyEdges();

            foreach (var edge in remainingEdges)
            {
                edge.Value.RemoveAll(child => child == removedId);
            }

            DepthFirstSearch(remainingEdges, -1);
            return _nodesStillVisible;
        }

        public void DepthFirstSearch(Dictionary<int, List<int>> g, int v)
        {
            _nodesStillVisible.Add(v);

            if (!g.TryGetValue(v, out List<int>? value))
                return;

            foreach (var w in value)
            {
                if (!_nodesStillVisible.Contains(w))
                {
                    DepthFirstSearch(g, w);
                }
            }
        }

        private Dictionary<int, List<int>> CopyEdges() =>
            _edges.ToDictionary(entry => entry.Key, entry => new List<int>(entry.Value));
    }
}
