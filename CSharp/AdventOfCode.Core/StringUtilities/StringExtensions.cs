namespace AdventOfCode.Core.StringUtilities;

public static class StringExtensions
{
    public static string[] SplitAndTrim(this string input, string separator)
        => input.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
