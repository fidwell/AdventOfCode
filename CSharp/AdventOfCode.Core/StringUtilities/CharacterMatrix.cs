using System.Text.RegularExpressions;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Creates a matrix of characters from a "rectanuglar-shaped" string.
/// </summary>
public class CharacterMatrix
{
    private string _raw;
    private string[] _asLines;
    private readonly int _lineLength;
    private readonly int _lineCount;

    /// <summary>
    /// Creates a matrix of characters from a "rectanuglar-shaped" string.
    /// </summary>
    /// <param name="input">A newline-separated string of input data.</param>
    public CharacterMatrix(string input)
    {
        _raw = input.Replace(Environment.NewLine, string.Empty);
        _asLines = input.Split(Environment.NewLine);
        _lineLength = _asLines[0].Length;
        _lineCount = _asLines.Length;

        if (_asLines.Any(l => l.Length != _lineLength))
        {
            throw new ArgumentException("All lines must be the same length.", nameof(input));
        }
    }

    public int TotalLength => _raw.Length;

    /// <summary>
    /// Find the (x,y) coordinates in the matrix given a starting one-dimensional index.
    /// </summary>
    /// <param name="index">The index to convert</param>
    /// <returns>The (x,y) coordinates in the matrix</returns>
    public (int, int) CoordinatesAt(int index) => (index % _lineLength, index / _lineLength);

    /// <summary>
    /// Returns the index of a character at a specific coordinate location.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>The index of the coordinate.</returns>
    public int IndexAt(int x, int y) => y * _lineLength + x;

    /// <summary>
    /// Returns the single character value at a given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The character at this position in the matrix.</returns>
    public string CharAt(int index) => $"{_raw[index]}";

    /// <summary>
    /// Returns a string starting at a given index, of a given length.
    /// </summary>
    /// <param name="index">The starting position of the desired string.</param>
    /// <param name="length">The length of the desired string.</param>
    /// <returns>A string starting at a given index, of a given length.</returns>
    public string StringAt(int index, int length) => _raw.Substring(index, length);

    /// <summary>
    /// Finds "words" in the data that match the specified regular expression.
    /// Does not allow matching across lines.
    /// </summary>
    /// <param name="matchingPattern">The pattern to search on.</param>
    /// <returns>A list of indexes which correspond to the beginning of each word match.</returns>
    public IEnumerable<Word> FindAllMatches(Regex matchingPattern)
    {
        var result = new List<Word>();
        for (int i = 0; i < _lineCount; i++)
        {
            var matchesOnLine = matchingPattern.Matches(_asLines[i]);
            result.AddRange(matchesOnLine.Select(m => new Word(i * _lineLength + m.Index, m.Length, m.Value)));
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

    public int GoRightFrom(int index) => index + 1;
    public int GoDownFrom(int index) => index + _lineLength;
    public int GoLeftFrom(int index) => index - 1;
    public int GoUpFrom(int index) => index - _lineLength;

    /// <summary>
    /// Replaces the character value at the given index.
    /// </summary>
    /// <param name="index">The index of the character to replace.</param>
    /// <param name="value">The new value of the character.</param>
    public void SetCharacter(int index, char value)
    {
        _raw = $"{_raw.Substring(0, index)}{value}{_raw.Substring(index + 1)}";
        var (x, y) = CoordinatesAt(index);
        var line = _asLines[y];
        _asLines[y] = $"{line.Substring(0, x)}{value}{line.Substring(x + 1)}";
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
            ? index - _lineLength : -1;
        var ne = y > 0 && x < _lineLength - 1
            ? nn + 1 : -1;
        var nw = y > 0 && x > 0
            ? nn - 1 : -1;

        var ss = y < _lineCount - 1
            ? index + _lineLength : -1;
        var se = y < _lineCount - 1 && x < _lineLength - 1
            ? ss + 1 : -1;
        var sw = y < _lineCount - 1 && x > 0
            ? ss - 1 : -1;

        var ee = x < _lineLength - 1
            ? index + 1 : -1;
        var ww = x > 0
            ? index - 1 : -1;

        return new[] { nn, ne, ee, se, ss, sw, ww, nw }.Where(z => z >= 0);
    }

    public class Word(int startIndex, int length, string value)
    {
        public int StartIndex { get; } = startIndex;
        public int Length { get; } = length;
        public string Value { get; } = value;
    }
}
