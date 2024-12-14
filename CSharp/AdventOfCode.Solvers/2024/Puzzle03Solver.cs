using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2024;

public partial class Puzzle03Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        MulCommand().Matches(input).Select(DoMul).Sum().ToString();

    public string SolvePartTwo(string input)
    {
        var matches =
            MulCommand().Matches(input)
            .Union(DoCommand().Matches(input))
            .Union(DontCommand().Matches(input))
            .OrderBy(m => m.Index).ToList();
        var isEnabled = true;
        var sum = 0;

        foreach (var m in matches)
        {
            if (m.Value.StartsWith("do()"))
            {
                isEnabled = true;
            }
            else if (m.Value.StartsWith("don't()"))
            {
                isEnabled = false;
            }
            else if (isEnabled)
            {
                sum += DoMul(m);
            }
        }

        return sum.ToString();
    }

    private int DoMul(Match match) =>
        int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulCommand();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoCommand();

    [GeneratedRegex(@"don\'t\(\)")]
    private static partial Regex DontCommand();
}
