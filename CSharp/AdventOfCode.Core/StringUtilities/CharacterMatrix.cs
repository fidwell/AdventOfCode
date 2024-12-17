using System.Text.RegularExpressions;
using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.IntSpace;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Creates a matrix of characters from a "rectanuglar-shaped" string.
/// </summary>
public class CharacterMatrix
{
    private readonly char[,] _data;

    public int Width { get; private set; }
    public int Height { get; private set; }

    /// <summary>
    /// Creates a matrix of characters from a "rectanuglar-shaped" string.
    /// </summary>
    /// <param name="input">A newline-separated string of input data.</param>
    public CharacterMatrix(string input)
    {
        var lines = input.SplitByNewline().ToArray();
        Width = lines[0].Length;
        Height = lines.Length;
        _data = new char[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            var thisLine = lines[y];
            if (thisLine.Length != Width)
            {
                throw new ArgumentException("All lines must be the same length.", nameof(input));
            }

            for (var x = 0; x < thisLine.Length; x++)
            {
                _data[x, y] = thisLine[x];
            }
        }
    }

    /// <summary>
    /// Creates a matrix of characters initialized to one value.
    /// </summary>
    /// <param name="width">The width of the matrix.</param>
    /// <param name="height">The height of the matrix.</param>
    /// <param name="value">The character to set every value to.</param>
    public CharacterMatrix(int width, int height, char value)
    {
        Width = width;
        Height = height;
        _data = new char[Width, Height];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                _data[x, y] = value;
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
    /// If the coordinates are out of bounds, a null value is returned.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The character at this position in the matrix,
    /// or null if the coordinates are out of bounds.</returns>
    public char CharAt(int x, int y, bool allowOutOfBounds = false)
    {
        if (allowOutOfBounds)
        {
            while (x < 0) x += Width;
            while (y < 0) y += Height;
            while (x >= Width) x -= Width;
            while (y >= Height) y -= Height;
            return _data[x, y];
        }

        return x < 0 || x >= Width || y < 0 || y >= Height
            ? '\0'
            : _data[x, y];
    }

    /// <summary>
    /// Returns the single character value at a given coordinate.
    /// </summary>
    /// <param name="coord">The coordinate.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt(Coord coord, bool allowOutOfBounds = false) =>
        CharAt(coord.Item1, coord.Item2, allowOutOfBounds);

    /// <summary>
    /// Returns the single character value at a given coordinate.
    /// </summary>
    /// <param name="coord">The coordinate.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt(Coord2d coord, bool allowOutOfBounds = false) =>
        CharAt(coord.X, coord.Y, allowOutOfBounds);

    /// <summary>
    /// Returns a string starting at a given index, of a given length.
    /// </summary>
    /// <param name="start">The starting coordinate of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt(Coord start, int length)
    {
        var row = RowAt(start.Item2);
        return new string(Enumerable.Range(start.Item1, length).Select(i => row[i]).ToArray());
    }

    /// <summary>
    /// Returns the coordinates of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All coordinates that match that character.</returns>
    public IEnumerable<Coord> FindAllCharacters(char matchingChar) =>
        AllCoordinates.Where(c => _data[c.Item1, c.Item2] == matchingChar);

    /// <summary>
    /// Returns the coordinates of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All coordinates that match that character.</returns>
    public IEnumerable<Coord2d> FindAllCharacters2(char matchingChar) =>
        AllCoordinates2.Where(c => _data[c.X, c.Y] == matchingChar);

    public Coord2d SingleMatch(char matchingChar) =>
        AllCoordinates2.Single(c => matchingChar == _data[c.X, c.Y]);

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
    public IEnumerable<Coord> CoordinatesOfNeighbors(Word word) => CoordinatesOfNeighbors(CoordinatesOfWord(word));

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
    public void SetCharacter(Coord coordinates, char value) => SetCharacter(coordinates.Item1, coordinates.Item2, value);

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
    public IEnumerable<Coord> AllCoordinates => ArrayExtensions.AllPoints(Width, Height);

    /// <summary>
    /// Returns a collection of all x,y pairs that are valid for this matrix.
    /// </summary>
    public IEnumerable<Coord2d> AllCoordinates2 => ArrayExtensions.AllPoints(Width, Height).Select(c => new Coord2d(c));

    /// <summary>
    /// Determines whether a given coordinate is in-bounds for the matrix.
    /// </summary>
    /// <param name="coordinate">The coordinate to check.</param>
    /// <returns>Whether the coordinate is in-bounds for the matrix.</returns>
    public bool IsInBounds(Coord coordinate) =>
        coordinate.Item1 >= 0 &&
        coordinate.Item2 >= 0 &&
        coordinate.Item1 < Width &&
        coordinate.Item2 < Height;

    /// <summary>
    /// Determines whether a given coordinate is in-bounds for the matrix.
    /// </summary>
    /// <param name="coordinate">The coordinate to check.</param>
    /// <returns>Whether the coordinate is in-bounds for the matrix.</returns>
    public bool IsInBounds(Coord2d coordinate) =>
        coordinate.X >= 0 &&
        coordinate.Y >= 0 &&
        coordinate.X < Width &&
        coordinate.Y < Height;

    /// <summary>
    /// Finds the coordinate values of all characters in a word.
    /// </summary>
    /// <param name="word">The word in question.</param>
    /// <returns>A list of the coordinate values of each character in that word.</returns>
    private static IEnumerable<Coord> CoordinatesOfWord(Word word) =>
        Enumerable.Range(0, word.Length).Select(x => (word.StartCoordinate.Item1 + x, word.StartCoordinate.Item2));

    /// <summary>
    /// Find the coordinates of characters surrounding a group
    /// of other characters, given by their coordinates.
    /// </summary>
    /// <param name="coordinates">All coordinates of characters.</param>
    /// <returns>Coordinates of all characters surrounding the input character coordinates.</returns>
    private IEnumerable<Coord> CoordinatesOfNeighbors(IEnumerable<Coord> coordinates) => coordinates
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
    public IEnumerable<Coord> CoordinatesOfNeighbors(Coord coordinate, bool allEight = true, bool allowWrapping = false)
    {
        var (x, y) = coordinate;

        if (allowWrapping || x > 0) yield return (x - 1, y);
        if (allowWrapping || x < Width - 1) yield return (x + 1, y);

        if (allowWrapping || y > 0) yield return (x, y - 1);
        if (allowWrapping || y < Height - 1) yield return (x, y + 1);

        if (allEight)
        {
            if (x > 0 && y > 0) yield return (x - 1, y - 1);
            if (x < Width - 1 && y < Height - 1) yield return (x + 1, y + 1);

            if (x > 0 && y < Height - 1) yield return (x - 1, y + 1);
            if (x < Width - 1 && y > 0) yield return (x + 1, y - 1);
        }
    }

    /// <summary>
    /// Find the coordinate values of the (up to) eight characters
    /// surrounding the character at the given index. Will
    /// omit values if the coordinate is at the edges of the matrix.
    /// </summary>
    /// <param name="coordinate">The index to search around.</param>
    /// <returns>Coordinates of all characters surrounding the input character index.</returns>
    public IEnumerable<Coord2d> CoordinatesOfNeighbors(Coord2d coordinate, bool allEight = true, bool allowWrapping = false) =>
        CoordinatesOfNeighbors((coordinate.X, coordinate.Y), allEight, allowWrapping).Select(c => new Coord2d(c));

    public class Word(Coord startCoordinate, int length, string value)
    {
        public Coord StartCoordinate { get; } = startCoordinate;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
