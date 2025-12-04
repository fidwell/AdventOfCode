using System.Text.RegularExpressions;
using AdventOfCode.Core.ArrayUtilities;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Represents a two-dimensional grid of characters, providing methods for accessing, modifying,
/// and searching character data by coordinates, rows, columns, and patterns.
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

    /// <summary>
    /// Gets a string representation of the grid, with each row separated by a newline character.
    /// </summary>
    public string DisplayString =>
        string.Join(
            Environment.NewLine,
            Enumerable.Range(0, Height).Select(y =>
                new string([.. Enumerable.Range(0, Width).Select(x => _data[x, y])])));

    /// <summary>
    /// Retrieves the character at the specified coordinates within the grid.
    /// </summary>
    /// <remarks>When <paramref name="allowOutOfBounds"/> is <see langword="true"/>, negative or out-of-range
    /// coordinates are wrapped to valid positions within the grid using modular arithmetic. This can be useful for
    /// implementing toroidal (wraparound) behavior.</remarks>
    /// <param name="x">The zero-based column index of the character to retrieve.</param>
    /// <param name="y">The zero-based row index of the character to retrieve.</param>
    /// <param name="allowOutOfBounds">If <see langword="true"/>, coordinates that are outside the
    /// grid bounds will wrap around; otherwise, out-of-bounds coordinates will result in a default value.</param>
    /// <returns>The character at the specified coordinates. If <paramref name="allowOutOfBounds"/> is <see langword="false"/>
    /// and the coordinates are out of bounds, returns '\0'.</returns>
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
    /// Returns a string starting at a given index, of a given length.
    /// </summary>
    /// <param name="start">The starting coordinate of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt(Coord start, int length)
    {
        var row = RowAt(start.Item2);
        return new string([.. Enumerable.Range(start.Item1, length).Select(i => row[i])]);
    }

    /// <summary>
    /// Returns the coordinates of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All coordinates that match that character.</returns>
    public IEnumerable<Coord> FindAllCharacters(char matchingChar) =>
        AllCoordinates.Where(c => _data[c.Item1, c.Item2] == matchingChar);

    /// <summary>
    /// Finds the location that is the sole match for a specific character.
    /// </summary>
    /// <param name="matchingChar">A character to search for.</param>
    /// <returns>The location of the character.</returns>
    public Coord SingleMatch(char matchingChar) =>
        AllCoordinates.Single(c => matchingChar == _data[c.Item1, c.Item2]);

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
        new([.. Enumerable.Range(0, Width).Select(x => _data[x, y])]);

    /// <summary>
    /// Get a string representing the characters in the given column.
    /// </summary>
    /// <param name="y">The column index.</param>
    /// <returns>The string value of this column.</returns>
    public string ColumnAt(int x) =>
        new([.. Enumerable.Range(0, Height).Select(y => _data[x, y])]);

    /// <summary>
    /// Sets the character at the specified coordinates within the grid.
    /// </summary>
    /// <param name="coordinates">The coordinates, represented as a <see cref="Coord"/>, indicating
    /// the position in the grid where the character will be set.</param>
    /// <param name="value">The character to assign at the specified coordinates.</param>
    public void SetCharacter(Coord coordinates, char value) => SetCharacter(coordinates.Item1, coordinates.Item2, value);

    /// <summary>
    /// Sets the character at the specified coordinates within the grid.
    /// </summary>
    /// <param name="x">The zero-based column index at which to set the character. Must be within the valid range of columns.</param>
    /// <param name="y">The zero-based row index at which to set the character. Must be within the valid range of rows.</param>
    /// <param name="value">The character to assign at the specified coordinates.</param>
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
    /// Gets an enumerable collection of all coordinates within the bounds defined by the current width and height.
    /// </summary>
    public IEnumerable<Coord> AllCoordinates => ArrayExtensions.AllPoints(Width, Height);

    /// <summary>
    /// Determines whether the specified coordinate is within the bounds of the grid.
    /// </summary>
    /// <param name="coordinate">The coordinate to check for validity within the grid. The first
    /// item represents the horizontal position; the /// second item represents the vertical position.</param>
    /// <returns>true if the coordinate is inside the grid boundaries; otherwise, false.</returns>
    public bool IsInBounds(Coord coordinate) =>
        coordinate.Item1 >= 0 &&
        coordinate.Item2 >= 0 &&
        coordinate.Item1 < Width &&
        coordinate.Item2 < Height;

    /// <summary>
    /// Finds the coordinate values of all characters in a word.
    /// </summary>
    /// <param name="word">The word in question.</param>
    /// <returns>A list of the coordinate values of each character in that word.</returns>
    private static IEnumerable<Coord> CoordinatesOfWord(Word word) =>
        Enumerable.Range(0, word.Length).Select(x => (word.StartCoordinate.Item1 + x, word.StartCoordinate.Item2));

    /// <summary>
    /// Returns the coordinates of all neighboring cells that are adjacent to any of the specified
    /// coordinates, excluding the original coordinates themselves.
    /// </summary>
    /// <param name="coordinates">A collection of coordinates for which to find all adjacent neighbor
    /// coordinates. Cannot be null.</param>
    /// <returns>An enumerable collection of coordinates representing the neighbors of the specified
    /// coordinates, excluding any coordinates that are present in the input collection. The collection
    /// contains no duplicate coordinates.</returns>
    private IEnumerable<Coord> CoordinatesOfNeighbors(IEnumerable<Coord> coordinates) => coordinates
        .SelectMany(c => CoordinatesOfNeighbors(c))
        .Where(c => !coordinates.Contains(c))
        .Distinct();

    /// <summary>
    /// Returns the coordinates of neighboring cells adjacent to the specified cell within the grid.
    /// </summary>
    /// <param name="coordinate">The coordinate of the cell for which to find neighboring cell coordinates.</param>
    /// <param name="allEight">Indicates whether to include diagonal neighbors in addition to orthogonal
    /// neighbors. If <see langword="true"/>, all eight surrounding cells are considered; otherwise,
    /// only the four orthogonal neighbors are included.</param>
    /// <param name="allowWrapping">Indicates whether neighbor coordinates should wrap around grid
    /// boundaries. If <see langword="true"/>, neighbors beyond the grid edges are included as wrapped
    /// coordinates.</param>
    /// <returns>An enumerable collection of coordinates representing the neighboring cells of the
    /// specified cell. The collection may contain fewer than eight coordinates if the cell is on a
    /// grid edge and wrapping is not allowed.</returns>
    public IEnumerable<Coord> CoordinatesOfNeighbors(
        Coord coordinate, bool allEight = true, bool allowWrapping = false)
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
    /// Returns the values of all neighboring cells adjacent to the specified coordinate.
    /// </summary>
    /// <param name="coordinate">The coordinate for which to retrieve neighboring cell values.</param>
    /// <param name="allEight">Indicates whether to include diagonal neighbors in addition to
    /// orthogonal ones. If <see langword="true"/>, all eight adjacent cells are considered;
    /// otherwise, only the four orthogonal neighbors are included.</param>
    /// <returns>An enumerable collection of characters representing the values of the neighboring
    /// cells. The collection will be empty if the coordinate has no valid neighbors.</returns>
    public IEnumerable<char> ValuesOfNeighbors(Coord coordinate, bool allEight = true) =>
        CoordinatesOfNeighbors(coordinate, allEight, allowWrapping: false)
        .Select(c => _data[c.Item1, c.Item2]);

    public class Word(Coord startCoordinate, int length, string value)
    {
        public Coord StartCoordinate { get; } = startCoordinate;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
