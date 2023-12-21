using AdventOfCode.Core.ArrayUtilities;
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

    public string DisplayString =>
        string.Join(
            Environment.NewLine,
            Enumerable.Range(0, Height).Select(y =>
                new string(Enumerable.Range(0, Width).Select(x => _data[x, y]).ToArray())));

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
    /// <param name="start">The starting coordinate of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt((int, int) start, int length)
    {
        var row = RowAt(start.Item2);
        return new string(Enumerable.Range(start.Item1, length).Select(i => row[i]).ToArray());
    }

    /// <summary>
    /// Returns the coordinates of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All coordinates that match that character.</returns>
    public IEnumerable<(int, int)> FindAllCharacters(char matchingChar) =>
        AllCoordinates.Where(c => _data[c.Item1, c.Item2] == matchingChar);

    /// <summary>
    /// Finds "words" in the data that match the specified regular expression.
    /// Does not allow matching across lines.
    /// </summary>
    /// <param name="matchingPattern">The pattern to search on.</param>
    /// <returns>A list of indexes which correspond to the beginning of each word match.</returns>
    public IEnumerable<Word> FindAllWords(Regex matchingPattern)
    {
        var result = new List<Word>();
        for (var y = 0; y < Height; y++)
        {
            var matchesOnLine = matchingPattern.Matches(RowAt(y));
            result.AddRange(matchesOnLine.Select(m => new Word((m.Index, y), m.Length, m.Value)));
        }
        return result;
    }

    /// <summary>
    /// Find the coordinate values of characters surrounding a word,
    /// given by its starting index and length.
    /// </summary>
    /// <param name="index">The index of the starting character.</param>
    /// <param name="length">The length of the word.</param>
    /// <returns>Coordinate of all characters surrounding the input word.</returns>
    public IEnumerable<(int, int)> CoordinatesOfNeighbors(Word word) => CoordinatesOfNeighbors(CoordinatesOfWord(word));

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

    /// <summary>
    /// Replaces the character value at the given index.
    /// </summary>
    /// <param name="coordinates">The coordinates of the character to replace.</param>
    /// <param name="value">The new value of the character.</param>
    public void SetCharacter((int, int) coordinates, char value) => SetCharacter(coordinates.Item1, coordinates.Item2, value);

    /// <summary>
    /// Replaces the character value at the given index.
    /// </summary>
    /// <param name="x">The x coordinate of the character to replace.</param>
    /// <param name="y">The y coordinate of the character to replace.</param>
    /// <param name="value">The new value of the character.</param>
    public void SetCharacter(int x, int y, char value) => _data[x, y] = value;

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
    /// Returns a collection of all x,y pairs that are valid for this matrix.
    /// </summary>
    public IEnumerable<(int, int)> AllCoordinates => ArrayExtensions.AllPoints(Width, Height);

    /// <summary>
    /// Determines whether a given coordinate is in-bounds for the matrix.
    /// </summary>
    /// <param name="coordinate">The coordinate to check.</param>
    /// <returns>Whether the coordinate is in-bounds for the matrix.</returns>
    public bool IsInBounds((int, int) coordinate) =>
        coordinate.Item1 >= 0 &&
        coordinate.Item2 >= 0 &&
        coordinate.Item1 < Width &&
        coordinate.Item2 < Height;

    /// <summary>
    /// Finds the coordinate values of all characters in a word.
    /// </summary>
    /// <param name="word">The word in question.</param>
    /// <returns>A list of the coordinate values of each character in that word.</returns>
    private static IEnumerable<(int, int)> CoordinatesOfWord(Word word) =>
        Enumerable.Range(0, word.Length).Select(x => (word.StartCoordinate.Item1 + x, word.StartCoordinate.Item2));

    /// <summary>
    /// Find the coordinates of characters surrounding a group
    /// of other characters, given by their coordinates.
    /// </summary>
    /// <param name="coordinates">All coordinates of characters.</param>
    /// <returns>Coordinates of all characters surrounding the input character coordinates.</returns>
    private IEnumerable<(int, int)> CoordinatesOfNeighbors(IEnumerable<(int, int)> coordinates) => coordinates
        .SelectMany(c => CoordinatesOfNeighbors(c))
        .Where(c => !coordinates.Contains(c))
        .Distinct();

    /// <summary>
    /// Find the coordinate values of the (up to) eight characters
    /// surrounding the character at the given index. Will
    /// omit values if the coordinate is at the edges of the matrix.
    /// </summary>
    /// <param name="coordinate">The index to search around.</param>
    /// <returns>Coordinates of all characters surrounding the input character index.</returns>
    public IEnumerable<(int, int)> CoordinatesOfNeighbors((int, int) coordinate, bool allEight = true)
    {
        var (x, y) = coordinate;

        if (x > 0) yield return (x - 1, y);
        if (x < Width - 1) yield return (x + 1, y);

        if (y > 0) yield return (x, y - 1);
        if (y < Height - 1) yield return (x, y + 1);

        if (allEight)
        {
            if (x > 0 && y > 0) yield return (x - 1, y - 1);
            if (x < Width - 1 && y < Height - 1) yield return (x + 1, y + 1);

            if (x > 0 && y < Height - 1) yield return (x - 1, y + 1);
            if (x < Width - 1 && y > 0) yield return (x + 1, y - 1);
        }
    }

    public class Word((int, int) startCoordinate, int length, string value)
    {
        public (int, int) StartCoordinate { get; } = startCoordinate;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
