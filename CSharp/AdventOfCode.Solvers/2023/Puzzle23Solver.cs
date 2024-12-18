using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var periodChars = matrix.FindAllCharacters2('.');
        var start = periodChars.First();
        var finish = periodChars.Last();
        var graph = new List<Edge>();
        FindGraphEdges(matrix, start, Direction.Down, finish, [], graph);
        var allPaths = FindAllPaths(graph, start, finish);
        return PathTotalLengths(graph, allPaths).Max().ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var periodChars = matrix.FindAllCharacters2('.');
        var start = periodChars.First();
        var finish = periodChars.Last();
        var graph = new List<Edge>();
        FindGraphEdges(matrix, start, Direction.Down, finish, [], graph);

        var extraEdges = new List<Edge>();
        foreach (var edge in graph.Where(e => e.Start != start && e.End != finish))
        {
            extraEdges.Add(new Edge(edge.End, edge.Start, edge.Length));
        }
        graph.AddRange(extraEdges);

        var allPaths = FindAllPaths(graph, start, finish);
        return PathTotalLengths(graph, allPaths).Max().ToString();
    }

    private static IEnumerable<int> PathTotalLengths(List<Edge> graph, List<List<Coord2d>> allPaths) =>
        allPaths.Select(path =>
            Enumerable.Range(0, path.Count - 1)
            .Sum(i => graph
                .Single(e => e.Start.Equals(path[i]) && e.End.Equals(path[i + 1]))
                .Length));

    private static void FindGraphEdges(CharacterMatrix matrix, Coord2d start, Direction initialDirection, Coord2d finish, HashSet<Coord2d> visited, List<Edge> edges)
    {
        var thisPath = WalkToNextJunction(matrix, start, initialDirection, finish, visited);
        if (thisPath.Item1 == finish)
        {
            edges.Add(new Edge(start, initialDirection, finish, thisPath.Item3, thisPath.Item2));
            return;
        }

        if (thisPath.Item3 == Direction.Undefined)
            return;

        var nextNeighbors = WalkableNeighbors(matrix, thisPath.Item1)
            .Where(n => DirectionFrom(n, thisPath.Item1) != thisPath.Item3)
            .ToList();
        edges.Add(new Edge(start, initialDirection, thisPath.Item1, thisPath.Item3, thisPath.Item2));

        foreach (var n in nextNeighbors.Where(n => !visited.Contains(n)))
        {
            FindGraphEdges(matrix, thisPath.Item1, DirectionFrom(thisPath.Item1, n), finish, visited, edges);
        }
    }

    // Returns: finishing coordinate, length of path, final direction
    private static (Coord2d, int, Direction) WalkToNextJunction(CharacterMatrix matrix, Coord2d start, Direction initialDirection, Coord2d finish, HashSet<Coord2d> visited)
    {
        var length = 0;
        var direction = initialDirection;
        var currentPosition = start;

        while (true)
        {
            var previousPosition = currentPosition;
            currentPosition = currentPosition.Go(direction);

            var nextNeighbors = WalkableNeighbors(matrix, currentPosition);

            // Junction reached
            if (currentPosition == finish || nextNeighbors.Count() > 2)
                return (currentPosition, length + 1, direction);

            visited.Add(currentPosition);

            var charHere = matrix.CharAt(currentPosition);
            if (charHere != '.' && charHere.ToDirection() != direction)
                return (new Coord2d(-1, -1), -1, Direction.Undefined); // We hit a one-way going the wrong way

            length++;

            var nextTarget = nextNeighbors.Single(n => n != previousPosition);
            direction = DirectionFrom(currentPosition, nextTarget);
        }

        throw new Exception("Never reached the finish or a junction");
    }

    private static IEnumerable<Coord2d> WalkableNeighbors(CharacterMatrix matrix, Coord2d coordinate) =>
        matrix.CoordinatesOfNeighbors(coordinate, allEight: false).Where(c => matrix.CharAt(c) != '#');

    private static List<List<Coord2d>> FindAllPaths(List<Edge> graph, Coord2d start, Coord2d end)
    {
        var paths = new List<List<Coord2d>>();
        DepthFirstSearch(graph, start, end, [], paths);
        return paths;
    }

    private static void DepthFirstSearch(List<Edge> graph,
        Coord2d current, Coord2d end,
        List<Coord2d> path, List<List<Coord2d>> paths)
    {
        path.Add(current);

        if (current == end)
        {
            paths.Add(new List<Coord2d>(path));
        }
        else
        {
            foreach (var edge in graph.Where(e => e.Start == current))
            {
                if (!path.Contains(edge.End))
                {
                    DepthFirstSearch(graph, edge.End, end, path, paths);
                }
            }
        }

        path.RemoveAt(path.Count - 1);
    }

    private static Direction DirectionFrom(Coord2d a, Coord2d b)
    {
        if (a.X > b.X)
            return Direction.Left;
        if (a.X < b.X)
            return Direction.Right;
        if (a.Y > b.Y)
            return Direction.Up;
        if (a.Y < b.Y)
            return Direction.Down;
        return Direction.Undefined;
    }

    private class Edge
    {
        public Coord2d Start;
        public Direction InitialDirection;
        public Coord2d End;
        public Direction FinalDirection;
        public int Length;

        public Edge(
            Coord2d start, Direction initialDirection,
            Coord2d end, Direction finalDirection,
            int length)
        {
            Start = start;
            InitialDirection = initialDirection;
            End = end;
            FinalDirection = finalDirection;
            Length = length;
        }

        public Edge(Coord2d start, Coord2d end, int length)
        {
            Start = start;
            InitialDirection = Direction.Undefined;
            End = end;
            FinalDirection = Direction.Undefined;
            Length = length;
        }
    }
}
