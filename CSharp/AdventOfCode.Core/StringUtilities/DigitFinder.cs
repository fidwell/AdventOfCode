namespace AdventOfCode.Core.StringUtilities;

public static class DigitFinder
{
    public static IEnumerable<int> FindDigits(string input, bool allowWords)
    {
        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                yield return int.Parse(input.Substring(i, 1));
                continue;
            }

            if (!allowWords)
                continue;

            foreach (var word in DigitWords)
            {
                if (i + word.Key.Length > input.Length)
                    continue;

                if (input.Substring(i, word.Key.Length).Equals(word.Key))
                {
                    yield return word.Value;
                }
            }
        }
    }

    private static readonly Dictionary<string, int> DigitWords = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };
}
