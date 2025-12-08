using AdventOfCode.Core.Hashing;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2015;

public class Puzzle04Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        Enumerable.Range(0, int.MaxValue).First(i => DoesSatisfy(input, i, true));

    public override object SolvePartTwo(string input)
    {
        int? result = null;
        object lockObj = new();
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

        if (result.HasValue)
            return result.Value;
        throw new SolutionNotFoundException();
    }

    private static bool DoesSatisfy(string input, int i, bool isPartOne)
    {
        var hash = Md5Hasher.Hash($"{input}{i}");
        return
            hash[0] == 0 &&
            hash[1] == 0 &&
            (isPartOne ? (hash[2] <= 15) : (hash[2] == 0));
    }
}
