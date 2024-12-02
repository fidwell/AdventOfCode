namespace AdventOfCode.Core.StringUtilities;

public static class StringExtensions
{
    public static string[] SplitAndTrim(this string input, string separator)
        => input.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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
