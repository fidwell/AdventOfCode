using AdventOfCode.Core.IntSpace;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var map = new CharacterMatrix(input);
        return GetRoute(map, map.SingleMatch('^'), Direction.Up).Item1.DistinctBy(x => x.Coord).Count().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var map = new CharacterMatrix(input);
        var originalRoute = GetRoute(map, map.SingleMatch('^'), Direction.Up).Item1;
        return originalRoute
            .Select(x => x.Coord)
            .Distinct()
            .Where(x => x != map.SingleMatch('^'))
            .Count(obstacle =>
            {
                var start = originalRoute.First(c => c.Coord.Go(c.Direction) == obstacle);
                return GetRoute(map, start.Coord, start.Direction, obstacle).Item2;
            })
            .ToString();
    }

    private static (HashSet<State>, bool) GetRoute(
        CharacterMatrix map,
        (int, int) coord,
        Direction direction,
        (int, int)? extraObstacle = null)
    {
        HashSet<State> visitedLocations = [];

        while (map.IsInBounds(coord))
        {
            var state = new State(coord.Item1, coord.Item2, direction);

            if (!visitedLocations.Add(state))
                return ([], true);

            var target = coord.Go(direction);

            if (map.IsInBounds(target) &&
                (map.CharAt(target) == '#' ||
                target == extraObstacle))
            {
                direction = direction.RotateRight();
            }
            else
            {
                coord = target;
            }
        }

        return (visitedLocations, false);
    }

    private record State(int X, int Y, Direction Direction)
    {
        public (int, int) Coord => (X, Y);
    }
}
