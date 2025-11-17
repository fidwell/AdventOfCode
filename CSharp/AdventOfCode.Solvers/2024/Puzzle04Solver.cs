using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle04Solver : PuzzleSolver
{
    private const string _xmas = "XMAS";

    public override string SolvePartOne(string input)
    {
        var wordSearch = new CharacterMatrix(input);
        return wordSearch.AllCoordinates.Sum(c => XmasesStartingAt(wordSearch, c)).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var wordSearch = new CharacterMatrix(input);
        return wordSearch.AllCoordinates.Count(c => XmasLocatedAt(wordSearch, c.Item1, c.Item2)).ToString();
    }

    private static int XmasesStartingAt(CharacterMatrix wordSearch, (int, int) coord) =>
        wordSearch.CharAt(coord) != 'X' ? 0 :
        DirectionExtensions.All8.Count(d => IsXmas(wordSearch, [.. _xmas.Select((c, i) => coord.Go(d, i))]));

    private static bool IsXmas(CharacterMatrix wordSearch, (int, int)[] coords) =>
        Enumerable.Range(1, 3).All(i => wordSearch.CharAt(coords[i].Item1, coords[i].Item2) == _xmas[i]);

    private static bool XmasLocatedAt(CharacterMatrix wordSearch, int x, int y)
    {
        if (wordSearch.CharAt(x, y) != 'A')
            return false;

        var nw = wordSearch.CharAt(x - 1, y - 1);
        var se = wordSearch.CharAt(x + 1, y + 1);
        var ne = wordSearch.CharAt(x + 1, y - 1);
        var sw = wordSearch.CharAt(x - 1, y + 1);
        return ((nw == 'M' && se == 'S') || (nw == 'S' && se == 'M')) &&
            ((ne == 'M' && sw == 'S') || (ne == 'S' && sw == 'M'));
    }
}
