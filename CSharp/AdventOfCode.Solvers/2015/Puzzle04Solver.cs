using System.Security.Cryptography;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);

    public override string SolvePartTwo(string input) => Solve(input, false);

    private string Solve(string input, bool isPartOne)
    {
        int? result = null;
        object lockObj = new object();
        const int MaxSearchValue = 10_000_000;

        Parallel.For(0, MaxSearchValue, (i, state) =>
        {
            if (result.HasValue && i >= result.Value)
                return;

            var tryInput = $"{input}{i}".ToCharArray().Select(c => (byte)c).ToArray();
            var hash = MD5.HashData(tryInput) ?? [];

            if (hash[0] == 0 &&
                hash[1] == 0 &&
                (isPartOne ? (hash[2] <= 15) : (hash[2] == 0)))
            {
                lock (lockObj)
                {
                    if (!result.HasValue || i < result.Value)
                    {
                        if (ShouldPrint)
                        {
                            Console.WriteLine($"Found possible answer at {i}: {hash.AsString()}");
                        }
                        result = i;
                    }
                }
            }
        });

        return result?.ToString() ?? "No answer could be found.";
    }
}
