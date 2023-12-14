using System.Text.RegularExpressions;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Creates a matrix of characters from a "rectanuglar-shaped" string.
/// </summary>
public class CharacterMatrix
{
    private readonly char[,] _data;

    public int Width => _data.GetLength(0);
    public int Height => _data.GetLength(1);

    /// <summary>
    /// Creates a matrix of characters from a "rectanuglar-shaped" string.
    /// </summary>
    /// <param name="input">A newline-separated string of input data.</param>
    public CharacterMatrix(string input)
    {
        var lines = input.Split(Environment.NewLine).ToArray();
        var lineLength = lines[0].Length;
        _data = new char[lineLength, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            var thisLine = lines[y];
            if (thisLine.Length != lineLength)
            {
                throw new ArgumentException("All lines must be the same length.", nameof(input));
            }

            for (var x = 0; x < thisLine.Length; x++)
            {
                _data[x, y] = thisLine[x];
            }
        }
    }

    public int TotalLength => (int)_data.LongLength;

    public string DisplayString =>
        string.Join(
            Environment.NewLine,
            Enumerable.Range(0, Height).Select(y =>
                new string(Enumerable.Range(0, Width).Select(x => _data[x, y]).ToArray())));

    /// <summary>
    /// Find the (x,y) coordinates in the matrix given a starting one-dimensional index.
    /// </summary>
    /// <param name="index">The index to convert</param>
    /// <returns>The (x,y) coordinates in the matrix</returns>
    public (int, int) CoordinatesAt(int index) => (index % Width, index / Width);

    /// <summary>
    /// Returns the index of a character at a specific coordinate location.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>The index of the coordinate.</returns>
    public int IndexAt(int x, int y) => y * Width + x;

    /// <summary>
    /// Returns the index of a character at a specific coordinate location.
    /// </summary>
    /// <param name="coord">The coordinate.</param>
    /// <returns>The index of the coordinate.</returns>
    public int IndexAt((int, int) coord) => coord.Item2 * Width + coord.Item1;

    /// <summary>
    /// Returns the single character value at a given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt(int index) => CharAt(CoordinatesAt(index));

    /// <summary>
    /// Returns the single character value at a given coordinate.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt(int x, int y) => _data[x, y];

    /// <summary>
    /// Returns the single character value at a given coordinate.
    /// </summary>
    /// <param name="coord">The coordinate.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt((int, int) coord) => _data[coord.Item1, coord.Item2];

    /// <summary>
    /// Returns a string starting at a given index, of a given length.
    /// </summary>
    /// <param name="index">The starting position of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt(int index, int length)
    {
        var coord = CoordinatesAt(index);
        var row = RowAt(coord.Item2);
        return new string(Enumerable.Range(coord.Item1, length).Select(i => row[i]).ToArray());
    }

    /// <summary>
    /// Returns the indexes of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All indexes that match that character.</returns>
    public IEnumerable<int> FindAllCharacters(char matchingChar) =>
        AllCoordinates.Where(c => _data[c.Item1, c.Item2] == matchingChar)
        .Select(IndexAt);

    /// <summary>
    /// Finds "words" in the data that match the specified regular expression.
    /// Does not allow matching across lines.
    /// </summary>
    /// <param name="matchingPattern">The pattern to search on.</param>
    /// <returns>A list of indexes which correspond to the beginning of each word match.</returns>
    public IEnumerable<Word> FindAllWords(Regex matchingPattern)
    {
        var result = new List<Word>();
        for (var i = 0; i < Height; i++)
        {
            var matchesOnLine = matchingPattern.Matches(RowAt(i));
            result.AddRange(matchesOnLine.Select(m => new Word(i * Width + m.Index, m.Length, m.Value)));
        }
        return result;
    }

    /// <summary>
    /// Finds the index values of all characters in a word.
    /// </summary>
    /// <param name="word">The word in question.</param>
    /// <returns>A list of the index values of each character in that word.</returns>
    public static IEnumerable<int> IndexesOfWord(Word word) => Enumerable.Range(word.StartIndex, word.Length);

    /// <summary>
    /// Find the index values of characters surrounding a word,
    /// given by its starting index and length.
    /// </summary>
    /// <param name="index">The index of the starting character.</param>
    /// <param name="length">The length of the word.</param>
    /// <returns>Indexes of all characters surrounding the input word.</returns>
    public IEnumerable<int> IndexesOfNeighbors(Word word) => IndexesOfNeighbors(IndexesOfWord(word));

    /// <summary>
    /// Get a string representing the characters in the given row.
    /// </summary>
    /// <param name="x">The row index.</param>
    /// <returns>The string value of this row.</returns>
    public string RowAt(int y) =>
        new(Enumerable.Range(0, Width)
            .Select(x => _data[x, y])
            .ToArray());

    /// <summary>
    /// Get a string representing the characters in the given column.
    /// </summary>
    /// <param name="y">The column index.</param>
    /// <returns>The string value of this column.</returns>
    public string ColumnAt(int x) =>
        new(Enumerable.Range(0, Height)
            .Select(y => _data[x, y])
            .ToArray());

    public int GoRightFrom(int index) => index + 1;
    public int GoDownFrom(int index) => index + Width;
    public int GoLeftFrom(int index) => index - 1;
    public int GoUpFrom(int index) => index - Width;

    /// <summary>
    /// Replaces the character value at the given index.
    /// </summary>
    /// <param name="index">The index of the character to replace.</param>
    /// <param name="value">The new value of the character.</param>
    public void SetCharacter(int index, char value)
    {
        var coords = CoordinatesAt(index);
        _data[coords.Item1, coords.Item2] = value;
    }

    /// <summary>
    /// Finds the column indexes where the data matches a given expression.
    /// </summary>
    /// <param name="matcher">The function to match this column's data on.</param>
    /// <returns>The column indexes of the matrix that match.</returns>
    public IEnumerable<int> ColumnsWhere(Func<IEnumerable<char>, bool> matcher) =>
        Enumerable.Range(0, Width)
            .Select((c, i) => new { data = ColumnAt(c), i })
            .Where(x => matcher(x.data))
            .Select(c => c.i);

    /// <summary>
    /// Finds the row indexes where the data matches a given expression.
    /// </summary>
    /// <param name="matcher">The function to match this row's data on.</param>
    /// <returns>The row indexes of the matrix that match.</returns>
    public IEnumerable<int> RowsWhere(Func<IEnumerable<char>, bool> matcher) =>
        Enumerable.Range(0, Height)
            .Select((r, i) => new { data = RowAt(r), i })
            .Where(x => matcher(x.data))
            .Select(r => r.i);

    /// <summary>
    /// Find the index values of characters surrounding a group
    /// of other characters, given by their indexes.
    /// </summary>
    /// <param name="indexes">All indexes of characters.</param>
    /// <returns>Indexes of all characters surrounding the input character indexes.</returns>
    private IEnumerable<int> IndexesOfNeighbors(IEnumerable<int> indexes) => indexes
        .SelectMany(IndexesOfNeighbors)
        .Where(ix => !indexes.Contains(ix))
        .Distinct();

    /// <summary>
    /// Find the index values of the (up to) eight characters
    /// surrounding the character at the given index. Will
    /// omit values if the index is at the edges of the matrix.
    /// </summary>
    /// <param name="index">The index to search around.</param>
    /// <returns>Indexes of all characters surrounding the input character index.</returns>
    private IEnumerable<int> IndexesOfNeighbors(int index)
    {
        var (x, y) = CoordinatesAt(index);

        var nn = y > 0
            ? index - Width : -1;
        var ne = y > 0 && x < Width - 1
            ? nn + 1 : -1;
        var nw = y > 0 && x > 0
            ? nn - 1 : -1;

        var ss = y < Height - 1
            ? index + Width : -1;
        var se = y < Height - 1 && x < Width - 1
            ? ss + 1 : -1;
        var sw = y < Height - 1 && x > 0
            ? ss - 1 : -1;

        var ee = x < Width - 1
            ? index + 1 : -1;
        var ww = x > 0
            ? index - 1 : -1;

        return new[] { nn, ne, ee, se, ss, sw, ww, nw }.Where(z => z >= 0);
    }

    private IEnumerable<(int, int)> AllCoordinates
        => Enumerable.Range(0, Height).SelectMany(y => Enumerable.Range(0, Width).Select(x => (x, y)));

    public class Word(int startIndex, int length, string value)
    {
        public int StartIndex { get; } = startIndex;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
