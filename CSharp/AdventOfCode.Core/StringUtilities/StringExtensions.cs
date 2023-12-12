namespace AdventOfCode.Core.StringUtilities;

public static class StringExtensions
{
    public static string[] SplitAndTrim(this string input, string separator)
        => input.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    public static string Repeat(this string input, int count, string separator) =>
        string.Join(separator, Enumerable.Range(0, count).Select(i => input));
}
