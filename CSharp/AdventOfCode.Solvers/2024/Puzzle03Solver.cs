using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2024;

public class Puzzle03Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)").Select(DoMul).Sum().ToString();

    public string SolvePartTwo(string input)
    {
        var muls = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)");
        var dos = Regex.Matches(input, @"do\(\)");
        var donts = Regex.Matches(input, @"don\'t\(\)");

        var matches = muls.Union(dos).Union(donts).OrderBy(m => m.Index).ToList();
        var isEnabled = true;
        var sum = 0;
        for (var i = 0; i < matches.Count; i++)
        {
            if (matches[i].Value.StartsWith("do()"))
            {
                isEnabled = true;
            }
            else if (matches[i].Value.StartsWith("don't()"))
            {
                isEnabled = false;
            }
            else if (isEnabled)
            {
                sum += DoMul(matches[i]);
            }
        }

        return sum.ToString();
    }

    private int DoMul(Match match) =>
        int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
}
