using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle12Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, (r, m) => r.Area * r.Perimeter(m));
    public string SolvePartTwo(string input) => Solve(input, (r, _) => r.Area  * r.CornerCount());

    private static string Solve(string input, Func<Region, CharacterMatrix, int> priceFunc)
    {
        var map = new CharacterMatrix(input);

        List<Region> plots = [];

        var coordsToRegionize = new List<(int, int)>(map.AllCoordinates);

        while (coordsToRegionize.Count != 0)
        {
            var start = coordsToRegionize[0];
            var coords = GeometryAlgorithms.FloodFill(map, start);
            plots.Add(new Region(map.CharAt(coords[0]), coords));
            coordsToRegionize.RemoveAll(coords.Contains);
        }

        return plots.Sum(p => priceFunc(p, map)).ToString();
    }

    private class Region(char identifier, List<(int, int)> locations)
    {
        public char Identifier { get; set; } = identifier;
        public List<(int, int)> Locations { get; } = locations;

        public int Area => Locations.Count;

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
            var sum = 0;

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

                if (!e && !n || e && n && !ne)
                    sum++;

                if (!e && !s || e && s && !se)
                    sum++;

                if (!w && !s || w && s && !sw)
                    sum++;

                if (!w && !n || w && n && !nw)
                    sum++;
            }

            return sum;
        }
    }
}
