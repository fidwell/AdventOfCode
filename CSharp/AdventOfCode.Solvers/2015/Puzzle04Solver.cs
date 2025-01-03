using AdventOfCode.Core.Cryptography;

namespace AdventOfCode.Solvers._2015;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);

    public override string SolvePartTwo(string input) => Solve(input, false);

    private static string Solve(string input, bool isPartOne)
    {
        for (var i = 0; i < int.MaxValue; i++)
        {
            var tryInput = $"{input}{i}".ToCharArray().Select(c => (byte)c).ToArray();
            var hash = MD5.HashData(tryInput) ?? [];

            if (hash[0] == 0 &&
                hash[1] == 0 &&
                (isPartOne ? (hash[2] <= 15) : (hash[2] == 0)))
            {
                return i.ToString();
            }
        }

        throw new Exception("No answer could be found.");
    }
}
