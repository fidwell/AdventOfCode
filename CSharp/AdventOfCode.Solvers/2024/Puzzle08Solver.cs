using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle08Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool isPartTwo)
    {
        var map = new CharacterMatrix(input);
        return GetAntennas(map)
            .SelectMany(g => NodesCausedByAntennas(map, g, isPartTwo))
            .Distinct().Count().ToString();
    }

    private static IEnumerable<IEnumerable<Antenna>> GetAntennas(CharacterMatrix map) =>
        map.AllCoordinates
            .Select(c => new Antenna(c.Item1, c.Item2, map.CharAt(c)))
            .Where(a => a.Type != '.').GroupBy(a => a.Type).Select(g => g);

    private static IEnumerable<Coord> NodesCausedByAntennas(
        CharacterMatrix map, IEnumerable<Antenna> group, bool includeHarmonics)
    {
        var allPairs = ArrayExtensions.Pairs(group.ToArray());
        foreach (var pair in allPairs)
        {
            var diff = pair[0] - pair[1];

            var fromNode1 = pair[0].Coord;
            if (includeHarmonics)
                yield return fromNode1;

            do
            {
                fromNode1 = Add(fromNode1, diff);
                if (map.IsInBounds(fromNode1))
                    yield return fromNode1;
                else
                    break;
            } while (includeHarmonics);

            var fromNode2 = pair[1].Coord;
            if (includeHarmonics)
                yield return fromNode2;

            do
            {
                fromNode2 = Subtract(fromNode2, diff);
                if (map.IsInBounds(fromNode2))
                    yield return fromNode2;
                else
                    break;
            } while (includeHarmonics);
        }
    }

    private static Coord Add(Coord a, Coord b) =>
        (a.Item1 + b.Item1, a.Item2 + b.Item2);

    private static Coord Subtract(Coord a, Coord b) =>
        (a.Item1 - b.Item1, a.Item2 - b.Item2);

    private record Antenna(int X, int Y, char Type)
    {
        public Coord Coord => (X, Y);

        public static Coord operator -(Antenna a, Antenna b) =>
            (a.X - b.X, a.Y - b.Y);
    }
}
