using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle11Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
        => Solve(input, 1);

    public string SolvePartTwo(string input)
        => Solve(input, 999999);

    private static string Solve(string input, long gapSize)
    {
        var data = new CharacterMatrix(input);
        var galaxyCoordinates = data.FindAllCharacters('#').ToArray();
        var columnsWithNoGalaxies = data.ColumnsWhere(col => col.All(c => c != '#')).ToList();
        var rowsWithNoGalaxies = data.RowsWhere(row => row.All(c => c != '#')).ToList();
        return galaxyCoordinates
            .Pairs()
            .Select(pair =>
            {
                var (bigX, smallX) = pair[0].Item1 > pair[1].Item1
                    ? (pair[0].Item1, pair[1].Item1)
                    : (pair[1].Item1, pair[0].Item1);
                var (bigY, smallY) = pair[0].Item2 > pair[1].Item2
                    ? (pair[0].Item2, pair[1].Item2)
                    : (pair[1].Item2, pair[0].Item2);

                var rawManhattanDistance = (bigX - smallX) + (bigY - smallY);

                var emptyColumnsBetween = columnsWithNoGalaxies.Count(c => c < bigX && c > smallX) * gapSize;
                var emptyRowsBetween = rowsWithNoGalaxies.Count(c => c < bigY && c > smallY) * gapSize;

                return rawManhattanDistance + emptyColumnsBetween + emptyRowsBetween;
            })
            .Sum()
            .ToString();
    }
}
