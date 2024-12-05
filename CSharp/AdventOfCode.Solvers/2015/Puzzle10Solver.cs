using System.Text;

namespace AdventOfCode.Solvers._2015;

public class Puzzle10Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, 40);
    public string SolvePartTwo(string input) => Solve(input, 50);

    private static string Solve(string input, int iterations)
    {
        for (var i = 0; i < iterations; i++)
        {
            input = LookAndSay(input);
        }

        return input.Length.ToString();
    }

    private static string LookAndSay(string input)
    {
        var result = new StringBuilder();
        var current = input[0];
        var count = 1;

        for (var i = 1; i < input.Length; i++)
        {
            if (input[i] == current)
            {
                count++;
            }
            else
            {
                result.Append($"{count}{current}");
                count = 1;
                current = input[i];
            }
        }

        result.Append($"{count}{current}");
        return result.ToString();
    }
}
