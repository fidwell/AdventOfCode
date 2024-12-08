using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle08Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var map = new CharacterMatrix(input);
        var groups = GetAntennas(map);
        var nodes = groups.SelectMany(g => NodesCausedByAntennas(map, g, false));
        return nodes.Distinct().Count().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var map = new CharacterMatrix(input);
        var groups = GetAntennas(map);
        var nodes = groups.SelectMany(g => NodesCausedByAntennas(map, g, true));
        return nodes.Distinct().Count().ToString();
    }

    private static IEnumerable<IEnumerable<Antenna>> GetAntennas(CharacterMatrix map) =>
        map.AllCoordinates
            .Select(c => new Antenna(c.Item1, c.Item2, map.CharAt(c)))
            .Where(a => a.Type != '.').GroupBy(a => a.Type).Select(g => g);

    private static IEnumerable<(int, int)> NodesCausedByAntennas(
        CharacterMatrix map, IEnumerable<Antenna> group, bool includeHarmonics)
    {
        var allPairs = ArrayExtensions.Pairs(group.ToArray());
        foreach (var pair in allPairs)
        {
            var diff = pair[0] - pair[1];

            if (includeHarmonics)
            {
                var fromNode1 = pair[0].Coord;
                while (map.IsInBounds(fromNode1))
                {
                    yield return fromNode1;
                    fromNode1 = (fromNode1.Item1 + diff.Item1, fromNode1.Item2 + diff.Item2);
                }

                var fromNode2 = pair[1].Coord;
                while (map.IsInBounds(fromNode2))
                {
                    yield return fromNode2;
                    fromNode2 = (fromNode2.Item1 - diff.Item1, fromNode2.Item2 - diff.Item2);
                }
            }
            else
            {
                var node1 = (pair[0].X + diff.Item1, pair[0].Y + diff.Item2);
                var node2 = (pair[1].X - diff.Item1, pair[1].Y - diff.Item2);

                if (map.IsInBounds(node1))
                    yield return node1;
                if (map.IsInBounds(node2))
                    yield return node2;
            }
        }
    }

    private record Antenna(int X, int Y, char Type)
    {
        public (int, int) Coord => (X, Y);

        public static (int, int) operator -(Antenna a, Antenna b) =>
            (a.X - b.X, a.Y - b.Y);
    }
}
