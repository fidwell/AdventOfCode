namespace AdventOfCode.Core.StringUtilities;

public static class StringExtensions
{
    public static string[] SplitAndTrim(this string input, string separator,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        => input.Split(separator, options);

    public static string[] SplitAndTrim(this string input, char separator,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        => input.Split(separator, options);

    /// <summary>
    /// Splits a string into an array of strings, based on newlines.
    /// Supports an input that uses both "\n" and "\r\n" for newlines.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="options">Flags for StringSplitOptions.</param>
    /// <returns>An array of strings.</returns>
    public static string[] SplitByNewline(this string input,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        => input.Replace("\r", "").SplitAndTrim('\n', options);

    /// <summary>
    /// Splits a string into a collection of arrays of strings.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A collection of string arrays.</returns>
    public static IEnumerable<string[]> Chunk(this string input) =>
        input
            .Split(["\n\n", "\r\n\r\n"], StringSplitOptions.None)
            .Select(c => c.SplitByNewline());

    public static string Repeat(this string input, int count, string separator) =>
        string.Join(separator, Enumerable.Range(0, count).Select(i => input));

    public static int CharacterDifferenceCount(this string a, string b)
    {
        if (a.Length != b.Length)
            throw new Exception("String lengths must be equal.");

        return Enumerable.Range(0, a.Length).Sum(i => a[i] != b[i] ? 1 : 0);
    }

    /// <summary>
    /// Converts a string input to an enumerable, where each element is an
    /// array of the elements on that line, parsed to integers.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>An enumerable of arrays of ints.</returns>
    public static IEnumerable<int[]> AsListOfIntArrays(this string input) =>
        input.Split(Environment.NewLine)
             .Select(l =>
                 l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                  .Select(int.Parse)
                  .ToArray());
}
