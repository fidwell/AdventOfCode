using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        GetRoute(new CharacterMatrix(input)).Item1.DistinctBy(x => x.Coord).Count().ToString();

    public string SolvePartTwo(string input)
    {
        var map = new CharacterMatrix(input);
        return GetRoute(map).Item1
            .Select(x => x.Coord)
            .Distinct()
            .Where(x => x != map.SingleMatch('^'))
            .Count(p => GetRoute(map, p).Item2)
            .ToString();
    }

    private static (HashSet<State>, bool) GetRoute(CharacterMatrix map, (int, int)? extraObstacle = null)
    {
        var coord = map.SingleMatch('^');
        var direction = Direction.Up;
        HashSet<State> visitedLocations = [];

        while (map.IsInBounds(coord))
        {
            var state = new State(coord.Item1, coord.Item2, direction);

            if (!visitedLocations.Add(state))
                return ([], true);

            var target = coord.Go(direction);
            
            if (!map.IsInBounds(target))
                return (visitedLocations, false);

            if (map.CharAt(target) == '#' || target == extraObstacle)
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
