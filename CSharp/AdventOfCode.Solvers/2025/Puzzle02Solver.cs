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
                if ((isPartOne && HasInvalidPatternPartOne(id)) ||
                    (!isPartOne && HasInvalidPatternPartTwo(id)))
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

        var half = asString.Length / 2;
        for (var i = 0; i < half; i++)
        {
            if (asString[i] != asString[i + half])
                return false;
        }

        return true;
    }

    private static bool HasInvalidPatternPartTwo(long input)
    {
        var asString = input.ToString();

        for (var chunkLength = 1; chunkLength <= asString.Length / 2; chunkLength++)
        {
            if (asString.Length % chunkLength != 0)
                continue;

            if (HasInvalidPatternOfSize(asString, chunkLength))
                return true;
        }

        return false;
    }

    private static bool HasInvalidPatternOfSize(string input, int chunkLength)
    {
        var chunkCount = input.Length / chunkLength;
        for (var i = 0; i < chunkLength; i++)
        {
            for (var j = 0; j < chunkCount - 1; j++)
            {
                if (input[i] != input[i + chunkLength + j * chunkLength])
                    return false;
            }
        }

        return true;
    }
}
