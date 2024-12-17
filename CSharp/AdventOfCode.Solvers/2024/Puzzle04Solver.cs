using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle04Solver : PuzzleSolver
{
    const string xmas = "XMAS";

    public override string SolvePartOne(string input)
    {
        var wordSearch = new CharacterMatrix(input);
        var count = 0;
        for (var x = 0; x < wordSearch.Width; x++)
        {
            for (var y = 0; y < wordSearch.Height; y++)
            {
                count += XmasesStartingAt(wordSearch, x, y);
            }
        }
        return count.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var wordSearch = new CharacterMatrix(input);
        var count = 0;
        for (var x = 0; x < wordSearch.Width; x++)
        {
            for (var y = 0; y < wordSearch.Height; y++)
            {
                count += XMasLocatedAt(wordSearch, x, y) ? 1 : 0;
            }
        }

        return count.ToString();
    }

    private static int XmasesStartingAt(CharacterMatrix wordSearch, int x, int y)
    {
        if (wordSearch.CharAt(x, y) != 'X')
            return 0;

        var count = 0;

        var coordsS = xmas.Select((c, i) => (x, y + i)).ToList();
        if (IsXmas(wordSearch, coordsS))
            count++;

        var coordsN = xmas.Select((c, i) => (x, y - i)).ToList();
        if (IsXmas(wordSearch, coordsN))
            count++;

        var coordsE = xmas.Select((c, i) => (x + i, y)).ToList();
        if (IsXmas(wordSearch, coordsE))
            count++;

        var coordsW = xmas.Select((c, i) => (x - i, y)).ToList();
        if (IsXmas(wordSearch, coordsW))
            count++;

        var coordsSE = xmas.Select((c, i) => (x + i, y + i)).ToList();
        if (IsXmas(wordSearch, coordsSE))
            count++;

        var coordsNE = xmas.Select((c, i) => (x + i, y - i)).ToList();
        if (IsXmas(wordSearch, coordsNE))
            count++;

        var coordsSW = xmas.Select((c, i) => (x - i, y + i)).ToList();
        if (IsXmas(wordSearch, coordsSW))
            count++;

        var coordsNW = xmas.Select((c, i) => (x - i, y - i)).ToList();
        if (IsXmas(wordSearch, coordsNW))
            count++;

        return count;
    }

    private static bool IsXmas(CharacterMatrix wordSearch, List<(int, int)> coords) =>
        Enumerable.Range(1, 3).All(i => wordSearch.CharAt(coords[i].Item1, coords[i].Item2) == xmas[i]);

    private static bool XMasLocatedAt(CharacterMatrix wordSearch, int x, int y)
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
