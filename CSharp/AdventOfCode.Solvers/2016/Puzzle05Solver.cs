using System.Text;
using AdventOfCode.Core.Hashing;

namespace AdventOfCode.Solvers._2016;

public class Puzzle05Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, true);
    public override string SolvePartTwo(string input) => Solve(input, false);

    private static string Solve(string input, bool isPartOne)
    {
        input = input.Trim();
        var result = new StringBuilder("________");

        var charsFound = 0;
        var index = 0;

        while (charsFound < 8)
        {
            var newInput = $"{input}{index}";
            var hash = Md5Hasher.Hash(newInput);

            if (hash[0] == 0 &&
                hash[1] == 0 &&
                hash[2] <= 15)
            {
                if (isPartOne)
                {
                    char thisChar = BitConverter.ToString([hash[2]])[1];
                    result[charsFound] = thisChar;
                    charsFound++;
                }
                else
                {
                    var placement = hash[2];
                    var value = (byte)(hash[3] >> 4);
                    var thisChar = BitConverter.ToString([value])[1];

                    if (placement < 8 && result[placement] == '_')
                    {
                        result[placement] = thisChar;
                        charsFound++;
                    }
                }
            }
            index++;
        }

        return result.ToString().ToLower();
    }
}
