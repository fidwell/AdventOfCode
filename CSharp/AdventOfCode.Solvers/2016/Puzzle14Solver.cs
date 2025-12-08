using AdventOfCode.Core.Hashing;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2016;

public class Puzzle14Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input.Trim(), false);
    public override object SolvePartTwo(string input) => Solve(input.Trim(), true);

    private int Solve(string salt, bool keyStretch)
    {
        var keysFound = 0;
        const int maxSearchValue = 32_768;

        for (var i = 0; i < maxSearchValue; i++)
        {
            var hash = Hash(i, salt, keyStretch);

            var tripleCharMatches = Regexes.TripleCharacter().Matches(hash);
            if (tripleCharMatches.Count != 0)
            {
                var firstTripleChar = tripleCharMatches.First().Value[0];
                var repeatedString = new string(firstTripleChar, 5);

                for (var j = 1; j <= 1000; j++)
                {
                    var futureHash = Hash(i + j, salt, keyStretch);
                    if (futureHash.Contains(repeatedString))
                    {
                        keysFound++;
                        break;
                    }
                }
            }

            if (keysFound == 64)
                return i;
        }

        throw new SolutionNotFoundException("Not enough keys found");
    }

    private readonly Dictionary<int, string> Hashes = [];

    private string Hash(int index, string salt, bool keyStretch)
    {
        if (Hashes.TryGetValue(index, out string? hash))
            return hash;

        hash = Hash($"{salt}{index}");
        if (keyStretch)
        {
            for (var i = 0; i < 2016; i++)
            {
                hash = Hash(hash);
            }
        }

        Hashes.Add(index, hash);
        return hash;
    }

    private static string Hash(string input) => Convert.ToHexString(Md5Hasher.Hash(input)).ToLower();
}
