using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        GetRoute(new CharacterMatrix(input)).Item1.DistinctBy(x => x.Coord).Count().ToString();

    public string SolvePartTwo(string input)
    {
        // works for example, but not input.
        // 1601 is too high
        // (it's not off-by-one; 1600 isn't right either)
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
            {
                // problematic?:
                // obstacle: (76, 108); broke at coord (84, 108); length 122
                if (visitedLocations.Count < 500)
                    Console.WriteLine($"obstacle: {extraObstacle}; broke at coord {coord}; length {visitedLocations.Count}");
                return ([], true);
            }

            var target = coord.Go(direction);
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
