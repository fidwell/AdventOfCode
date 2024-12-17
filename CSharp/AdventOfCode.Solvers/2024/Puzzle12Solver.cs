using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle12Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, (r, m) => r.Area * r.Perimeter(m));
    public string SolvePartTwo(string input) => Solve(input, (r, _) => r.Area * r.CornerCount());

    private static string Solve(string input, Func<Region, CharacterMatrix, int> priceFunc)
    {
        var map = new CharacterMatrix(input);

        List<Region> plots = [];

        var coordsToRegionize = new List<Coord2d>(map.AllCoordinates2);

        while (coordsToRegionize.Count != 0)
        {
            var start = coordsToRegionize[0];
            var coords = GeometryAlgorithms.FloodFill(map, start);
            plots.Add(new Region(map.CharAt(coords[0]), coords));
            coordsToRegionize.RemoveAll(coords.Contains);
        }

        return plots.Sum(p => priceFunc(p, map)).ToString();
    }

    private class Region(char identifier, List<Coord2d> locations)
    {
        public char Identifier { get; set; } = identifier;
        public List<Coord2d> Locations { get; } = locations;

        public int Area => Locations.Count;

        public int Perimeter(CharacterMatrix map)
        { // can probably be improved
            var sum = 0;
            foreach (var a in Locations)
            {
                var hasTopBorder = a.Y == 0 || !Locations.Any(b => a.X == b.X && a.Y == b.Y + 1);
                var hasBottomBorder = a.Y == map.Height - 1 || !Locations.Any(b => a.X == b.X && a.Y == b.Y - 1);
                var hasRightBorder = a.X == map.Width - 1 || !Locations.Any(b => a.X == b.X - 1 && a.Y == b.Y);
                var hasLeftBorder = a.X == 0 || !Locations.Any(b => a.X == b.X + 1 && a.Y == b.Y);
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
                var e = Locations.Any(l => l.X == c.X + 1 && l.Y == c.Y);
                var s = Locations.Any(l => l.X == c.X && l.Y == c.Y + 1);
                var w = Locations.Any(l => l.X == c.X - 1 && l.Y == c.Y);
                var n = Locations.Any(l => l.X == c.X && l.Y == c.Y - 1);
                var se = Locations.Any(l => l.X == c.X + 1 && l.Y == c.Y + 1);
                var sw = Locations.Any(l => l.X == c.X - 1 && l.Y == c.Y + 1);
                var nw = Locations.Any(l => l.X == c.X - 1 && l.Y == c.Y - 1);
                var ne = Locations.Any(l => l.X == c.X + 1 && l.Y == c.Y - 1);

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
