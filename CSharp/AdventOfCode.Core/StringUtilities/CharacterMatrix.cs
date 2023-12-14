using System.Text.RegularExpressions;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Creates a matrix of characters from a "rectanuglar-shaped" string.
/// </summary>
public class CharacterMatrix
{
    private string _raw;
    private string[] _asRows;
    private string[] _asColumns;
    private bool _dataChanged;

    public readonly int LineLength;
    public readonly int LineCount;

    private string[] Columns
    {
        get
        {
            if (_dataChanged)
            {
                CalculateColumns();
                CalculateRows();
                _dataChanged = false;
            }
            return _asColumns;
        }
    }

    private string[] Rows
    {
        get
        {
            if (_dataChanged)
            {
                CalculateColumns();
                CalculateRows();
                _dataChanged = false;
            }
            return _asRows;
        }
    }

    /// <summary>
    /// Creates a matrix of characters from a "rectanuglar-shaped" string.
    /// </summary>
    /// <param name="input">A newline-separated string of input data.</param>
    public CharacterMatrix(string input)
    {
        _raw = input.Replace(Environment.NewLine, string.Empty);
        _asRows = input.Split(Environment.NewLine);
        LineLength = _asRows[0].Length;
        LineCount = _asRows.Length;

        if (Rows.Any(l => l.Length != LineLength))
        {
            throw new ArgumentException("All lines must be the same length.", nameof(input));
        }

        CalculateColumns();
    }

    public int TotalLength => _raw.Length;

    /// <summary>
    /// Find the (x,y) coordinates in the matrix given a starting one-dimensional index.
    /// </summary>
    /// <param name="index">The index to convert</param>
    /// <returns>The (x,y) coordinates in the matrix</returns>
    public (int, int) CoordinatesAt(int index) => (index % LineLength, index / LineLength);

    /// <summary>
    /// Returns the index of a character at a specific coordinate location.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>The index of the coordinate.</returns>
    public int IndexAt(int x, int y) => y * LineLength + x;

    /// <summary>
    /// Returns the single character value at a given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public string CharAt(int index) => $"{_raw[index]}";

    /// <summary>
    /// Returns the single character value at a given coordinate.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public char CharAt(int x, int y) => _raw[IndexAt(x, y)];

    /// <summary>
    /// Returns a string starting at a given index, of a given length.
    /// </summary>
    /// <param name="index">The starting position of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt(int index, int length) => _raw.Substring(index, length);

    /// <summary>
    /// Returns the indexes of all instances of the specified characeter.
    /// </summary>
    /// <param name="matchingChar">The character to search for.</param>
    /// <returns>All indexes that match that character.</returns>
    public IEnumerable<int> FindAllCharacters(char matchingChar)
    {
        var minIndex = _raw.IndexOf(matchingChar);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = _raw.IndexOf(matchingChar, minIndex + 1);
        }
    }

    /// <summary>
    /// Finds "words" in the data that match the specified regular expression.
    /// Does not allow matching across lines.
    /// </summary>
    /// <param name="matchingPattern">The pattern to search on.</param>
    /// <returns>A list of indexes which correspond to the beginning of each word match.</returns>
    public IEnumerable<Word> FindAllWords(Regex matchingPattern)
    {
        var result = new List<Word>();
        for (var i = 0; i < LineCount; i++)
        {
            var matchesOnLine = matchingPattern.Matches(Rows[i]);
            result.AddRange(matchesOnLine.Select(m => new Word(i * LineLength + m.Index, m.Length, m.Value)));
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

    public string RowAt(int x) => _asRows[x];
    public string ColumnAt(int y) => _asColumns[y];

    public int GoRightFrom(int index) => index + 1;
    public int GoDownFrom(int index) => index + LineLength;
    public int GoLeftFrom(int index) => index - 1;
    public int GoUpFrom(int index) => index - LineLength;

    /// <summary>
    /// Replaces the character value at the given index.
    /// </summary>
    /// <param name="index">The index of the character to replace.</param>
    /// <param name="value">The new value of the character.</param>
    public void SetCharacter(int index, char value)
    {
        _raw = $"{_raw.Substring(0, index)}{value}{_raw.Substring(index + 1)}";
        var (x, y) = CoordinatesAt(index);
        var line = Rows[y];
        Rows[y] = $"{line.Substring(0, x)}{value}{line.Substring(x + 1)}";

        CalculateRows();
        CalculateColumns();
    }

    /// <summary>
    /// Finds the column indexes where the data matches a given expression.
    /// </summary>
    /// <param name="matcher">The function to match this column's data on.</param>
    /// <returns>The column indexes of the matrix that match.</returns>
    public IEnumerable<int> ColumnsWhere(Func<IEnumerable<char>, bool> matcher)
    {
        for (var x = 0; x < LineLength; x++)
        {
            var indexesHere = Enumerable.Range(0, LineCount).Select(y => IndexAt(x, y));
            if (matcher(indexesHere.Select(i => _raw[i])))
                yield return x;
        }
    }

    /// <summary>
    /// Finds the row indexes where the data matches a given expression.
    /// </summary>
    /// <param name="matcher">The function to match this row's data on.</param>
    /// <returns>The row indexes of the matrix that match.</returns>
    public IEnumerable<int> RowsWhere(Func<IEnumerable<char>, bool> matcher)
    {
        for (var y = 0; y < Rows.Length; y++)
        {
            if (matcher(Rows[y].ToCharArray()))
                yield return y;
        }
    }

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
            ? index - LineLength : -1;
        var ne = y > 0 && x < LineLength - 1
            ? nn + 1 : -1;
        var nw = y > 0 && x > 0
            ? nn - 1 : -1;

        var ss = y < LineCount - 1
            ? index + LineLength : -1;
        var se = y < LineCount - 1 && x < LineLength - 1
            ? ss + 1 : -1;
        var sw = y < LineCount - 1 && x > 0
            ? ss - 1 : -1;

        var ee = x < LineLength - 1
            ? index + 1 : -1;
        var ww = x > 0
            ? index - 1 : -1;

        return new[] { nn, ne, ee, se, ss, sw, ww, nw }.Where(z => z >= 0);
    }

    private void CalculateRows()
    {
        var result = new List<string>();
        for (int i = 0; i < _raw.Length; i += LineLength)
        {
            result.Add(_raw.Substring(i, LineLength));
        }
        _asRows = result.ToArray();
    }

    private void CalculateColumns()
    {
        var result = new List<string>();
        for (var x = 0; x < LineLength; x++)
        {
            var indexesHere = Enumerable.Range(0, LineCount).Select(y => IndexAt(x, y));
            result.Add(string.Join("", indexesHere.Select(i => _raw[i])));
        }
        _asColumns = result.ToArray();
    }

    public class Word(int startIndex, int length, string value)
    {
        public int StartIndex { get; } = startIndex;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
