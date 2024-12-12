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
        public int Price2(CharacterMatrix map) => Area * SideCount();

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
            // Try to use the wall-follower maze algorithm.
            // It doesn't work. The visited hash set is supposed
            // to keep the algorithm from backtracking, but
            // it also prevents following a single-tile-wide
            // shape. Needs rethinking.

            var sides = 0;
            var initial = Locations[0]; // hopefully a top-left corner; if we're wrong, this might not be
            var current = initial;
            var direction = Locations.Any(l => l.Item1 == current.Item1 + 1 && l.Item2 == current.Item2)
                ? Direction.Right
                : Direction.Down;
            var visited = new HashSet<(int, int)>();

            do
            {
                visited.Add(current);

                if (!Locations.Contains(current))
                    throw new Exception("Went off region");

                // Find neighbors
                var hasRight = Locations.Any(l => l.Item1 == current.Item1 + 1 && l.Item2 == current.Item2);
                var hasDown = Locations.Any(l => l.Item1 == current.Item1 && l.Item2 == current.Item2 + 1);
                var hasLeft = Locations.Any(l => l.Item1 == current.Item1 - 1 && l.Item2 == current.Item2);
                var hasUp = Locations.Any(l => l.Item1 == current.Item1 && l.Item2 == current.Item2 - 1);

                if (direction == Direction.Right)
                {
                    if (hasUp && !visited.Contains(current.Go(Direction.Up)))
                    {
                        sides++;
                        direction = Direction.Up;
                    }
                    else if (hasRight)
                    {
                        current = current.Go(direction);
                    }
                    else
                    {
                        sides++;
                        direction = Direction.Down;
                    }
                }
                else if (direction == Direction.Down)
                {
                    if (hasRight && !visited.Contains(current.Go(Direction.Right)))
                    {
                        sides++;
                        direction = Direction.Right;
                    }
                    else if (hasDown)
                    {
                        current = current.Go(direction);
                    }
                    else
                    {
                        sides++;
                        direction = Direction.Left;
                    }
                }
                else if (direction == Direction.Left)
                {
                    if (hasDown && !visited.Contains(current.Go(Direction.Down)))
                    {
                        sides++;
                        direction = Direction.Down;
                    }
                    else if (hasLeft)
                    {
                        current = current.Go(direction);
                    }
                    else
                    {
                        sides++;
                        direction = Direction.Up;
                    }
                }
                else if (direction == Direction.Up)
                {
                    if (hasLeft && !visited.Contains(current.Go(Direction.Left)))
                    {
                        sides++;
                        direction = Direction.Left;
                    }
                    else if (hasUp)
                    {
                        current = current.Go(direction);
                    }
                    else
                    {
                        sides++;
                        direction = Direction.Right;
                    }
                }
            } while (initial != current);

            sides++;
            Console.WriteLine($"{Identifier} region has {sides} sides");
            return sides;
        }

        public override string ToString() => $"{Identifier} of size {Locations.Count}";
    }
}
