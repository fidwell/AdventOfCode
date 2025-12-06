using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle06Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var operands = lines.Take(lines.Length - 1)
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
            .ToList();
        var operations = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        long total = 0;
        for (var i = 0; i < operations.Count; i++)
        {
            var thisOperands = operands.Select(line => line[i]);
            if (operations[i] == "+")
            {
                var sum = thisOperands.Sum();
                total += (long)sum;
            }
            else
            {
                var product = thisOperands.Aggregate(1L, (a, b) => a * b);
                total += product;
            }
        }

        return total.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline(options: StringSplitOptions.None);

        // Todo - find this dynamically (in case there's no trailing newline)
        var operandLine = lines.Length - 2;

        var operationIndexes = Operator().Matches(lines[operandLine]).Select(m => m.Index).ToList();
        operationIndexes = [.. operationIndexes, lines[operandLine].Length + 1];
        long total = 0;

        for (var i = 0; i < operationIndexes.Count - 1; i++)
        {
            var thisOperation = lines[operandLine][operationIndexes[i]];
            var result = thisOperation == '+' ? 0L : 1L;

            for (var c = operationIndexes[i + 1] - 2; c >= operationIndexes[i]; c--)
            {
                var num = ReadNumberAt(lines, c);
                if (thisOperation == '+')
                {
                    result += num;
                }
                else
                {
                    result *= num;
                }
            }

            total += result;
        }

        return total.ToString();
    }

    private static int ReadNumberAt(string[] input, int col)
    {
        var asString = $"{input[0][col]}{input[1][col]}{input[2][col]}{input[3][col]}"
            .Replace("*", "")
            .Replace("+", "");
        return int.Parse(asString);
    }

    [GeneratedRegex(@"[\+\*]")]
    private static partial Regex Operator();
}
