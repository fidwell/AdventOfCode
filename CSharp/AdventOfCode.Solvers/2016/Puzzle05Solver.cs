using AdventOfCode.Core.Hashing;

namespace AdventOfCode.Solvers._2016;

public class Puzzle05Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);
    public override string SolvePartTwo(string input) => Solve(input, false);

    private static string Solve(string input, bool isPartOne)
    {
        var results = GetHashes(input, isPartOne ? 19_000_000 : 30_000_000);
        if (isPartOne)
        {
            var chars = results
                .OrderBy(r => r.Key)
                .Take(8)
                .Select(r => BitConverter.ToString([r.Value[2]])[1])
                .ToArray();
            return new string(chars).ToLower();
        }
        else
        {
            var chars = results
                .OrderBy(r => r.Key)
                .Select(r => (r.Value[2], (byte)(r.Value[3] >> 4)))
                .Where(r => r.Item1 < 8)
                .OrderBy(r => r.Item1)
                .GroupBy(r => r.Item1)
                .Select(g => BitConverter.ToString([g.First().Item2])[1])
                .ToArray();
            return new string(chars).ToLower();
        }
    }

    private static Dictionary<int, byte[]> GetHashes(string input, int maxIndex)
    {
        input = input.Trim();
        var results = new Dictionary<int, byte[]>();
        object lockObj = new object();

        Parallel.For(0, maxIndex, (index, state) =>
        {
            var newInput = $"{input}{index}";
            var hash = Md5Hasher.Hash(newInput);
            if (hash[0] == 0 &&
                hash[1] == 0 &&
                hash[2] <= 15)
            {
                lock (lockObj)
                {
                    results.Add(index, hash);
                }
            }
        });
        return results;
    }
}
