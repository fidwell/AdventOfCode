using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.IntSpace;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle08Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, false);
    public string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool isPartTwo)
    {
        var map = new CharacterMatrix(input);
        return GetAntennas(map)
            .SelectMany(g => NodesCausedByAntennas(map, g, isPartTwo))
            .Distinct().Count().ToString();
    }

    private static IEnumerable<IEnumerable<Antenna>> GetAntennas(CharacterMatrix map) =>
        map.AllCoordinates2
            .Select(c => new Antenna(c, map.CharAt(c)))
            .Where(a => a.Type != '.').GroupBy(a => a.Type).Select(g => g);

    private static IEnumerable<Coord2d> NodesCausedByAntennas(
        CharacterMatrix map, IEnumerable<Antenna> group, bool includeHarmonics)
    {
        var allPairs = ArrayExtensions.Pairs(group.ToArray());
        foreach (var pair in allPairs)
        {
            var diff = pair[0].Coord - pair[1].Coord;

            var fromNode1 = pair[0].Coord;
            if (includeHarmonics)
                yield return fromNode1;

            do
            {
                fromNode1 = fromNode1 + diff;
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
                fromNode2 = fromNode2 - diff;
                if (map.IsInBounds(fromNode2))
                    yield return fromNode2;
                else
                    break;
            } while (includeHarmonics);
        }
    }

    private record Antenna(Coord2d Coord, char Type);
}
