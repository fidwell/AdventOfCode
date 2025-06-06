using AdventOfCode.Core.Hashing;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        for (var i = 0; i < int.MaxValue; i++)
        {
            if (DoesSatisfy(input, i, true))
            {
                return i.ToString();
            }
        }
        throw new Exception("No answer could be found.");
    }

    public override string SolvePartTwo(string input)
    {
        int? result = null;
        object lockObj = new object();
        const int MaxSearchValue = 10_000_000;

        Parallel.For(0, MaxSearchValue, (i, state) =>
        {
            if ((result.HasValue && i >= result.Value) ||
                !DoesSatisfy(input, i, false))
                return;

            lock (lockObj)
            {
                if (!result.HasValue || i < result.Value)
                {
                    result = i;
                }
            }
        });

        return result?.ToString() ?? "No answer could be found.";
    }

    private bool DoesSatisfy(string input, int i, bool isPartOne)
    {
        var hash = Md5Hasher.Hash($"{input}{i}");
        var doesSatisfy =
            hash[0] == 0 &&
            hash[1] == 0 &&
            (isPartOne ? (hash[2] <= 15) : (hash[2] == 0));

        if (doesSatisfy && ShouldPrint)
        {
            Console.WriteLine($"Found possible answer at {i}: {hash.AsString()}");
        }
        return doesSatisfy;
    }
}
