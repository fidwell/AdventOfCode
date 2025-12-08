using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle06Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var operands = lines.Take(lines.Length - 1)
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
            .ToList();
        var operations = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        long total = 0;
        for (var i = 0; i < operations.Count; i++)
        {
            total += Operate(operands.Select(line => line[i]), operations[i][0]);
        }

        return total;
    }

    public override object SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline(options: StringSplitOptions.RemoveEmptyEntries);
        var operandLine = lines.Length - 1;

        var operationIndexes = Operator().Matches(lines[operandLine]).Select(m => m.Index).ToList();
        operationIndexes = [.. operationIndexes, lines[operandLine].Length + 1];
        long total = 0;

        for (var i = 0; i < operationIndexes.Count - 1; i++)
        {
            var thisOperation = lines[operandLine][operationIndexes[i]];
            var operands = Enumerable.Range(operationIndexes[i], operationIndexes[i + 1] - operationIndexes[i] - 1)
                .Select(c => ReadNumberAt(lines, c));
            var result = Operate(operands, thisOperation);

            total += result;
        }

        return total;
    }

    private static int ReadNumberAt(string[] input, int col) =>
        int.Parse($"{input[0][col]}{input[1][col]}{input[2][col]}{input[3][col]}"
            .Replace("*", "")
            .Replace("+", ""));

    private static long Operate(IEnumerable<int> operands, char operatorKind) =>
        operatorKind == '+' ? operands.Sum() : operands.Aggregate(1L, (a, b) => a * b);

    [GeneratedRegex(@"[\+\*]")]
    private static partial Regex Operator();
}
