using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var periodChars = matrix.FindAllCharacters('.');
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
        var periodChars = matrix.FindAllCharacters('.');
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

    private static IEnumerable<int> PathTotalLengths(List<Edge> graph, List<List<Coord>> allPaths) =>
        allPaths.Select(path =>
            Enumerable.Range(0, path.Count - 1)
            .Sum(i => graph
                .Single(e => e.Start.Equals(path[i]) && e.End.Equals(path[i + 1]))
                .Length));

    private static void FindGraphEdges(CharacterMatrix matrix, Coord start, Direction initialDirection, Coord finish, HashSet<Coord> visited, List<Edge> edges)
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
    private static (Coord, int, Direction) WalkToNextJunction(CharacterMatrix matrix, Coord start, Direction initialDirection, Coord finish, HashSet<Coord> visited)
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
                return ((-1, -1), -1, Direction.Undefined); // We hit a one-way going the wrong way

            length++;

            var nextTarget = nextNeighbors.Single(n => n != previousPosition);
            direction = DirectionFrom(currentPosition, nextTarget);
        }

        throw new Exception("Never reached the finish or a junction");
    }

    private static IEnumerable<Coord> WalkableNeighbors(CharacterMatrix matrix, Coord coordinate) =>
        matrix.CoordinatesOfNeighbors(coordinate, allEight: false).Where(c => matrix.CharAt(c) != '#');

    private static List<List<Coord>> FindAllPaths(List<Edge> graph, Coord start, Coord end)
    {
        var paths = new List<List<Coord>>();
        DepthFirstSearch(graph, start, end, [], paths);
        return paths;
    }

    private static void DepthFirstSearch(List<Edge> graph,
        Coord current, Coord end,
        List<Coord> path, List<List<Coord>> paths)
    {
        path.Add(current);

        if (current == end)
        {
            paths.Add([.. path]);
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

    private static Direction DirectionFrom(Coord a, Coord b)
    {
        if (a.Item1 > b.Item1)
            return Direction.Left;
        if (a.Item1 < b.Item1)
            return Direction.Right;
        if (a.Item2 > b.Item2)
            return Direction.Up;
        if (a.Item2 < b.Item2)
            return Direction.Down;
        return Direction.Undefined;
    }

    private class Edge
    {
        public Coord Start;
        public Direction InitialDirection;
        public Coord End;
        public Direction FinalDirection;
        public int Length;

        public Edge(
            Coord start, Direction initialDirection,
            Coord end, Direction finalDirection,
            int length)
        {
            Start = start;
            InitialDirection = initialDirection;
            End = end;
            FinalDirection = finalDirection;
            Length = length;
        }

        public Edge(Coord start, Coord end, int length)
        {
            Start = start;
            InitialDirection = Direction.Undefined;
            End = end;
            FinalDirection = Direction.Undefined;
            Length = length;
        }
    }
}
