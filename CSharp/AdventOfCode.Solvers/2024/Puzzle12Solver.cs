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
        public int Price2(CharacterMatrix _) => Area * CornerCount();

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

        public int CornerCount()
        {
            // a "corner" being at the top-left of a coordinate
            var cornerLocations = new HashSet<(int, int)>();
            var doubleCounted = new HashSet<(int, int)>();
            foreach (var c in Locations)
            {
                var e = Locations.Contains((c.Item1 + 1, c.Item2));
                var s = Locations.Contains((c.Item1, c.Item2 + 1));
                var w = Locations.Contains((c.Item1 - 1, c.Item2));
                var n = Locations.Contains((c.Item1, c.Item2 - 1));
                var se = Locations.Contains((c.Item1 + 1, c.Item2 + 1));
                var sw = Locations.Contains((c.Item1 - 1, c.Item2 + 1));
                var nw = Locations.Contains((c.Item1 - 1, c.Item2 - 1));
                var ne = Locations.Contains((c.Item1 + 1, c.Item2 - 1));

                if ((e && s && !se) ||
                    (!e && !s) ||
                    (se && e && !s) ||
                    (se && !e && s))
                {
                    cornerLocations.Add((c.Item1 + 1, c.Item2 + 1));
                    if (se && !e & !s)
                        doubleCounted.Add((c.Item1 + 1, c.Item2 + 1));
                }

                if ((w && s && !sw) ||
                    (!w && !s) ||
                    (sw && w && !s) ||
                    (sw && !w && s))
                {
                    cornerLocations.Add((c.Item1, c.Item2 + 1));
                    if (sw && !w & !s)
                        doubleCounted.Add((c.Item1, c.Item2 + 1));
                }

                if ((w && n && !nw) ||
                    (!w && !n) ||
                    (nw && w && !n) ||
                    (nw && !w && n))
                {
                    cornerLocations.Add((c.Item1, c.Item2));
                    if (nw && !w & !n)
                        doubleCounted.Add((c.Item1, c.Item2));
                }

                if ((e && n && !ne) ||
                    (!e && !n) ||
                    (ne && e && !n) ||
                    (ne && !e && n))
                {
                    cornerLocations.Add((c.Item1 + 1, c.Item2));
                    if (ne && !e & !n)
                        doubleCounted.Add((c.Item1 + 1, c.Item2));
                }
            }

            return cornerLocations.Count + doubleCounted.Count;
        }
    }
}
