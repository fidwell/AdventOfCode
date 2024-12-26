using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle25Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var coords = Regexes.NonNegativeInteger().Matches(input).Select(m => int.Parse(m.Value)).ToArray();
        var row = coords[0];
        var col = coords[1];
        var index = ConvertCoordToIndex(row, col) - 1;
        var value = 20151125UL;
        for (var i = 0; i < index; i++)
        {
            value = value * 252533 % 33554393;
        }

        return value.ToString();
    }

    public override string SolvePartTwo(string input) => string.Empty;

    private static int ConvertCoordToIndex(int row, int col) =>
        (row + col - 2) * (row + col - 1) / 2 + col;
}
