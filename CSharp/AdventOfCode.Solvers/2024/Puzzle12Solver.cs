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
            // Todo
            return 0;
        }

        public override string ToString() => $"{Identifier} of size {Locations.Count}";
    }
}
