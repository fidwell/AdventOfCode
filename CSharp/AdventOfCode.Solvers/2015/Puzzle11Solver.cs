using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle11Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        do
        {
            input = Increment(input);
        } while (!IsValidPassword(input));
        return input;
    }

    public override string SolvePartTwo(string input) =>
        SolvePartOne(SolvePartOne(input));

    private static bool IsValidPassword(string input) =>
        HasSequence(input) &&
        !input.Contains('i') &&
        !input.Contains('o') &&
        !input.Contains('l') &&
        Regexes.DoubleCharacter().Matches(input).Count >= 2;

    private static bool HasSequence(string input)
    {
        for (var i = 0; i < input.Length - 3; i++)
        {
            if (input[i] == input[i + 1] - 1 &&
                input[i + 1] == input[i + 2] - 1)
                return true;
        }
        return false;
    }

    private static string Increment(string input)
    {
        if (input.Length == 0)
            return input;

        var asBytes = input.ToCharArray();
        var i = asBytes.Length - 1;
        asBytes[i]++;
        if (asBytes[i] == 'z' + 1)
        {
            var substr = new string(asBytes.Take(asBytes.Length - 1).ToArray());
            substr = Increment(substr);
            return $"{substr}a";
        }
        return new string(asBytes);
    }
}
