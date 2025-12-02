using System.Text;

namespace AdventOfCode.Solvers._2025;

public class Puzzle02Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true).ToString();
    public override string SolvePartTwo(string input) => Solve(input, false).ToString();

    private static long Solve(string input, bool isPartOne)
    {
        var ranges = input.Trim().Split(',');
        var invalidIds = new List<long>();
        foreach (var range in ranges)
        {
            var bounds = range.Split('-').Select(long.Parse).ToList();
            for (var id = bounds[0]; id <= bounds[1]; id++)
            {
                if ((isPartOne && HasInvalidPatternPartOne(id))
                    || HasInvalidPatternPartTwo(id))
                {
                    invalidIds.Add(id);
                }
            }
        }
        return invalidIds.Sum();
    }

    private static bool HasInvalidPatternPartOne(long input)
    {
        var asString = input.ToString();

        if (asString.Length % 2 == 1)
            return false;

        var firstHalf = asString[..(asString.Length / 2)];
        var secondHalf = asString[(asString.Length / 2)..];
        return firstHalf.Equals(secondHalf);
    }

    private static bool HasInvalidPatternPartTwo(long input)
    {
        var asString = input.ToString();

        for (var chunkLength = 1; chunkLength <= asString.Length / 2; chunkLength++)
        {
            if (asString.Length % chunkLength != 0)
                continue;

            var firstChunk = asString.Substring(0, chunkLength);
            var repeats = asString.Length / chunkLength;
            var comparison = Repeat(firstChunk, repeats);
            if (comparison.Equals(asString))
                return true;
        }

        return false;
    }

    private static string Repeat(string input, int count)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < count; i++)
        {
            sb.Append(input);
        }
        return sb.ToString();
    }
}
