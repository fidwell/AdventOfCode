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
}
