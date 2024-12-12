using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle12Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, (r, m) => r.Price1(m));
    public string SolvePartTwo(string input) => Solve(input, (r, m) => r.Price2(m));

    private static string Solve(string input, Func<Region, CharacterMatrix, int> priceFunc)
    {
        var map = new CharacterMatrix(input);

        List<Region> plots = [];

        var coordsToRegionize = new List<(int, int)>(map.AllCoordinates);

        while (coordsToRegionize.Count != 0)
        {
            var start = coordsToRegionize[0];
            var identifier = map.CharAt(start);
            var coords = new List<(int, int)>();
            FloodFill(map, identifier, start, coords);
            plots.Add(new Region(identifier, coords));
            coordsToRegionize.RemoveAll(coords.Contains);
        }

        return plots.Sum(p => priceFunc(p, map)).ToString();
    }

    private static void FloodFill(CharacterMatrix map, char targetChar, (int, int) start, List<(int, int)> result)
    {
        var queue = new Queue<(int, int)>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var coord = queue.Dequeue();
            if (!result.Contains(coord))
            {
                result.Add(coord);
                foreach (var neighbor in map.CoordinatesOfNeighbors(coord, allEight: false).Where(n => map.CharAt(n) == targetChar))
                    queue.Enqueue(neighbor);
            }
        }
    }

    private class Region(char identifier, List<(int, int)> locations)
    {
        public char Identifier { get; set; } = identifier;
        public List<(int, int)> Locations { get; } = locations;

        public int Area => Locations.Count;
        public int Price1(CharacterMatrix map) => Area * Perimeter(map);
        public int Price2(CharacterMatrix _) => Area * SideCount();

        public int Perimeter(CharacterMatrix map)
        { // can probably be improved
            var sum = 0;
            foreach (var a in Locations)
            {
                var hasTopBorder = a.Item2 == 0 || !Locations.Any(b => a.Item1 == b.Item1 && a.Item2 == b.Item2 + 1);
                var hasBottomBorder = a.Item2 == map.Height - 1 || !Locations.Any(b => a.Item1 == b.Item1 && a.Item2 == b.Item2 - 1);
                var hasRightBorder = a.Item1 == map.Width - 1 || !Locations.Any(b => a.Item1 == b.Item1 - 1 && a.Item2 == b.Item2);
                var hasLeftBorder = a.Item1 == 0 || !Locations.Any(b => a.Item1 == b.Item1 + 1 && a.Item2 == b.Item2);
                sum +=
                    (hasTopBorder ? 1 : 0) +
                    (hasBottomBorder ? 1 : 0) +
                    (hasRightBorder ? 1 : 0) +
                    (hasLeftBorder ? 1 : 0);
            }
            return sum;
        }

        public int SideCount()
        {
            // Use the wall-follower maze algorithm.

            var sides = 0;
            var initial = Locations[0]; // hopefully a top edge
            var current = initial;
            var direction = Direction.Right;
            var isBackAtStart = false;

            do
            {
                if (!Locations.Contains(current))
                    throw new Exception("Went off region");

                // Find neighbors
                var hasRight = Locations.Any(l => l.Item1 == current.Item1 + 1 && l.Item2 == current.Item2);
                var hasDown = Locations.Any(l => l.Item1 == current.Item1 && l.Item2 == current.Item2 + 1);
                var hasLeft = Locations.Any(l => l.Item1 == current.Item1 - 1 && l.Item2 == current.Item2);
                var hasUp = Locations.Any(l => l.Item1 == current.Item1 && l.Item2 == current.Item2 - 1);

                var wallOnLeft = direction switch
                {
                    Direction.Right => !hasUp,
                    Direction.Down => !hasRight,
                    Direction.Left => !hasDown,
                    Direction.Up => !hasLeft
                };

                if (!wallOnLeft)
                {
                    direction = direction.RotateLeft();
                    current = current.Go(direction);
                    sides++;
                    continue;
                }

                var wallAhead = direction switch
                {
                    Direction.Right => !hasRight,
                    Direction.Down => !hasDown,
                    Direction.Left => !hasLeft,
                    Direction.Up => !hasUp
                };
                if (!wallAhead)
                {
                    current = current.Go(direction);
                }
                else
                {
                    direction = direction.RotateRight();
                    sides++;
                }
                isBackAtStart = current == initial && direction == Direction.Right;
            } while (!isBackAtStart);
            if (Identifier == 'A')
                Console.WriteLine($"{Identifier} region has {sides} sides");
            return sides;
            // 799676 is too low
            // Does not take into account HOLES!
        }

        public override string ToString() => $"{Identifier} of size {Locations.Count}";
    }
}
