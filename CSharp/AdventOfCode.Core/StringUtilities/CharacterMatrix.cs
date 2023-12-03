using System.Text.RegularExpressions;

namespace AdventOfCode.Core.StringUtilities;

/// <summary>
/// Creates a matrix of characters from a "rectanuglar-shaped" string.
/// </summary>
public class CharacterMatrix
{
    private readonly string _rawWithNewLines;
    private readonly string _raw;
    private readonly string[] _asLines;
    private readonly int _lineLength;
    private readonly int _lineCount;

    /// <summary>
    /// Creates a matrix of characters from a "rectanuglar-shaped" string.
    /// </summary>
    /// <param name="input">A newline-separated string of input data.</param>
    public CharacterMatrix(string input)
    {
        _rawWithNewLines = input;
        _raw = _rawWithNewLines.Replace(Environment.NewLine, string.Empty);
        _asLines = input.Split(Environment.NewLine);
        _lineLength = _asLines[0].Length;
        _lineCount = _asLines.Length;

        if (_asLines.Any(l => l.Length != _lineLength))
        {
            throw new ArgumentException("All lines must be the same length.", nameof(input));
        }
    }

    public string CharAt(int index) => WordAt(index, 1);

    public string WordAt(int index, int length) => _raw.Substring(index, length);


    private int IndexAt(int x, int y) => y * _lineLength + x;
    private (int, int) CoordinatesAt(int index) => (index % _lineLength, index / _lineLength);

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
