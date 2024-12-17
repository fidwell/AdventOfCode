using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var map = new CharacterMatrix(input);
        return GetRoute(map, map.SingleMatch('^'), Direction.Up).Item1.DistinctBy(x => x.Location).Count().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var map = new CharacterMatrix(input);
        var originalRoute = GetRoute(map, map.SingleMatch('^'), Direction.Up).Item1;
        return originalRoute
            .Select(x => x.Location)
            .Distinct()
            .Where(x => x != map.SingleMatch('^'))
            .Count(obstacle =>
            {
                var start = originalRoute.First(c => c.Location.Go(c.Direction) == obstacle);
                return GetRoute(map, start.Location, start.Direction, obstacle).Item2;
            })
            .ToString();
    }

    private static (HashSet<Pose>, bool) GetRoute(
        CharacterMatrix map,
        Coord2d coord,
        Direction direction,
        Coord2d? extraObstacle = null)
    {
        HashSet<Pose> visitedLocations = [];

        while (map.IsInBounds(coord))
        {
            var state = new Pose(coord, direction);

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
}
