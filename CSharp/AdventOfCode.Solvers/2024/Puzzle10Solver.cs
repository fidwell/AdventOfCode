using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle10Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);
    public override string SolvePartTwo(string input) => Solve(input, false);

    private static string Solve(string input, bool getDistinct)
    {
        var map = new CharacterMatrix(input);
        return map.FindAllCharacters2('0')
            .Sum(s =>
            {
                var paths = NinesReachedFromHere(map, s, '0');
                return (getDistinct ? paths.Distinct() : paths).Count();
            }).ToString();
    }

    private static IEnumerable<Coord2d> NinesReachedFromHere(CharacterMatrix map, Coord2d coord, char charHere)
    {
        if (charHere == '9')
            return [coord];

        var nextChar = (char)(charHere + 1);
        return map.CoordinatesOfNeighbors(coord, allEight: false, allowWrapping: false)
            .Where(c => map.CharAt(c) == nextChar)
            .SelectMany(n => NinesReachedFromHere(map, n, nextChar));
    }
}
