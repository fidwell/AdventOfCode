namespace AdventOfCode.Solvers._2024;

public class Puzzle04Solver : IPuzzleSolver
{
    const string xmas = "XMAS";

    public string SolvePartOne(string input)
    {
        var wordSearch = ParseInput(input);
        var count = 0;
        for (var x = 0; x < wordSearch.GetLength(0); x++)
        {
            for (var y = 0; y < wordSearch.GetLength(1); y++)
            {
                count += XmasesStartingAt(wordSearch, x, y);
            }
        }
        return count.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var wordSearch = ParseInput(input);
        var count = 0;
        for (var x = 0; x < wordSearch.GetLength(0); x++)
        {
            for (var y = 0; y < wordSearch.GetLength(1); y++)
            {
                count += XMasLocatedAt(wordSearch, x, y) ? 1 : 0;
            }
        }

        return count.ToString();
    }

    private static char[,] ParseInput(string input)
    {
        var raw = input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        var width = raw[0].Length;
        var height = raw.Length;
        var wordSearch = new char[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                wordSearch[x, y] = raw[y][x];
            }
        }
        return wordSearch;
    }

    private static int XmasesStartingAt(char[,] wordSearch, int x, int y)
    {
        if (wordSearch[x, y] != 'X')
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

    private static bool IsXmas(char[,] wordSearch, List<(int, int)> coords)
    {
        for (int i = 0; i < 4; i++)
        {
            if (coords[i].Item1 < 0 ||
                coords[i].Item2 < 0 ||
                coords[i].Item1 >= wordSearch.GetLength(0) ||
                coords[i].Item2 >= wordSearch.GetLength(1) ||
                wordSearch[coords[i].Item1, coords[i].Item2] != xmas[i])
                return false;
        }
        return true;
    }

    private static bool XMasLocatedAt(char[,] wordSearch, int x, int y)
    {
        if (wordSearch[x, y] != 'A' ||
            x == 0 || y == 0 ||
            x == wordSearch.GetLength(0) - 1 || y == wordSearch.GetLength(1) - 1)
            return false;

        var nw = wordSearch[x - 1, y - 1];
        var se = wordSearch[x + 1, y + 1];
        var ne = wordSearch[x + 1, y - 1];
        var sw = wordSearch[x - 1, y + 1];
        return ((nw == 'M' && se == 'S') || (nw == 'S' && se == 'M')) &&
            ((ne == 'M' && sw == 'S') || (ne == 'S' && sw == 'M'));
    }
}
