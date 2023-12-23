using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle23Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var periodChars = matrix.FindAllCharacters('.');
        var start = periodChars.First();
        var finish = periodChars.Last();

        // Algorithm idea...
        // Depth-first search through paths.
        // Log visited spaces in a hash. (maybe not necessary)
        // Whenever you reach a junction, recur?
        // Final tree is a list of edges from c0 to c1
        // and keeping track of the length (like a
        // weighted graph).
        // Then do Dijkstra but whatever the inverse
        // of Dijkstra is, to find the longest
        // instead of the shortest.
        // Note that there can (probably will)
        // be multiple paths between the same two nodes.

        // ---

        // First we have to actually make the
        // graph by finding all the edges.

        // Given a starting node, walk through
        // the graph while there are no junctions
        // (that is, there are only 2 visitable
        // neighbors of the current coordinate.
        // Then we have a start, end, and length.
        // Add that to our graph.
        // (If we ever encountered an arrow
        // going the other way, give up on that
        // path.)
        // Now, just do that again, except recursively.
        // If we reach the desired end, stop recurring.
        // If we reach a node and all its exits are
        // already accounted for, stop recurring.
        // (So we'll have to keep track of how
        // we leave a starting node.)

        var allPaths = FindPaths(matrix, start, Direction.Down, finish, []).ToList();

        // The above should work!!! Now, just to run some graph searching on it.

        throw new NotImplementedException();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static HashSet<Edge> FindPaths(CharacterMatrix matrix, (int, int) start, Direction initialDirection, (int, int) finish, HashSet<(int, int)> visited)
    {
        var thisPath = WalkToNextJunction(matrix, start, initialDirection, finish, visited);
        if (thisPath.Item1 == finish)
            return
            [
                new Edge(start, initialDirection, finish, thisPath.Item3, thisPath.Item2)
            ];

        if (thisPath.Item3 == Direction.Undefined)
        {
            return [];
        }
        else
        {
            var nextNeighbors = WalkableNeighbors(matrix, thisPath.Item1)
                .Where(n => DirectionFrom(n, thisPath.Item1) != thisPath.Item3)
                .ToList();
            var result = new HashSet<Edge>()
            {
                new Edge(start, initialDirection, thisPath.Item1, thisPath.Item3, thisPath.Item2)
            };
            var childResults = nextNeighbors
                .SelectMany(n => FindPaths(matrix, thisPath.Item1, DirectionFrom(thisPath.Item1, n), finish, visited));
            foreach (var childResult in childResults)
            {
                result.Add(childResult);
            }
            return result;
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

            if (visited.Contains(currentPosition))
                return ((-1, -1), -1, Direction.Undefined); // We have already been this way
            visited.Add(currentPosition);

            var charHere = matrix.CharAt(currentPosition);
            if (charHere != '.' && DirectionOf(charHere) != direction)
                return ((-1, -1), -1, Direction.Undefined); // We hit a one-way going the wrong way

            length++;
            var nextNeighbors = WalkableNeighbors(matrix, currentPosition);

            if (currentPosition == finish || nextNeighbors.Count() > 2)
            {   // Junction reached
                return (currentPosition, length, direction);
            }

            var nextTarget = nextNeighbors.Single(n => n != previousPosition);
            direction = DirectionFrom(currentPosition, nextTarget);
        }

        throw new Exception("Never reached the finish or a junction");
    }

    private static IEnumerable<(int, int)> WalkableNeighbors(CharacterMatrix matrix, (int, int) coordinate) =>
        matrix.CoordinatesOfNeighbors(coordinate, allEight: false).Where(c => matrix.CharAt(c) != '#');

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

    private static Direction DirectionOf(char c) =>
        c switch
        {
            '>' => Direction.Right,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '^' => Direction.Up,
            _ => Direction.Undefined
        };

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
    }
}
