using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle08Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Sum(l => l.Length - Decode(l).Length)
            .ToString();

    public string SolvePartTwo(string input) =>
        input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Sum(l => EncodedLength(l) - l.Length)
            .ToString();

    private static string Decode(string input) =>
        HexCode().Replace(input[1..^1]
            .Replace("\\\\", "!")
            .Replace("\\\"", "!"), "!");

    private static int EncodedLength(string input) =>
        input.Select(c => c switch
        {
            '\"' => 2,
            '\\' => 2,
            _ => 1
        }).Sum() + 2; // add outer quotes

    [GeneratedRegex(@"\\x[a-f0-9]{2}")]
    private static partial Regex HexCode();
}
