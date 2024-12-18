using AdventOfCode.Core.StringUtilities;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle08Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Sum(l => l.Length - Decode(l).Length)
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline()
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
