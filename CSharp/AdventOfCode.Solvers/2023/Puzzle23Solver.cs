using AdventOfCode.Core.IntSpace;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle23Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
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

    public string SolvePartTwo(string input)
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

    private static IEnumerable<int> PathTotalLengths(List<Edge> graph, List<List<(int, int)>> allPaths) =>
        allPaths.Select(path =>
            Enumerable.Range(0, path.Count - 1)
            .Sum(i => graph
                .Single(e => e.Start.Equals(path[i]) && e.End.Equals(path[i + 1]))
                .Length));

    private static void FindGraphEdges(CharacterMatrix matrix, (int, int) start, Direction initialDirection, (int, int) finish, HashSet<(int, int)> visited, List<Edge> edges)
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
    private static ((int, int), int, Direction) WalkToNextJunction(CharacterMatrix matrix, (int, int) start, Direction initialDirection, (int, int) finish, HashSet<(int, int)> visited)
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

    private static IEnumerable<(int, int)> WalkableNeighbors(CharacterMatrix matrix, (int, int) coordinate) =>
        matrix.CoordinatesOfNeighbors(coordinate, allEight: false).Where(c => matrix.CharAt(c) != '#');

    private static List<List<(int, int)>> FindAllPaths(List<Edge> graph, (int, int) start, (int, int) end)
    {
        var paths = new List<List<(int, int)>>();
        DepthFirstSearch(graph, start, end, [], paths);
        return paths;
    }

    private static void DepthFirstSearch(List<Edge> graph,
        (int, int) current, (int, int) end,
        List<(int, int)> path, List<List<(int, int)>> paths)
    {
        path.Add(current);

        if (current == end)
        {
            paths.Add(new List<(int, int)>(path));
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

    private static Direction DirectionFrom((int, int) a, (int, int) b)
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
        public (int, int) Start;
        public Direction InitialDirection;
        public (int, int) End;
        public Direction FinalDirection;
        public int Length;

        public Edge(
            (int, int) start, Direction initialDirection,
            (int, int) end, Direction finalDirection,
            int length)
        {
            Start = start;
            InitialDirection = initialDirection;
            End = end;
            FinalDirection = finalDirection;
            Length = length;
        }

        public Edge((int, int) start, (int, int) end, int length)
        {
            Start = start;
            InitialDirection = Direction.Undefined;
            End = end;
            FinalDirection = Direction.Undefined;
            Length = length;
        }
    }
}
