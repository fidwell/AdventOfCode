using System.Text.RegularExpressions;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle03Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false)
    {
        var dataRaw = DataReader.GetData(3, 1, useSample);
        var asLines = dataRaw.Split(Environment.NewLine);
        var lineLength = asLines[0].Length + Environment.NewLine.Length;
        var lineCount = asLines.Length;

        var numbers = Regex.Matches(dataRaw, @"\d+");
        var numbersWhereMatch = numbers.Where(n => IsAdjacentToSymbol(n, dataRaw, lineLength, lineCount));

        return numbersWhereMatch.Select(n => int.Parse(n.Value)).Sum().ToString();
    }

    public string SolvePartTwo(bool useSample = false)
    {
        var dataRaw = DataReader.GetData(3, 1, useSample);
        var asLines = dataRaw.Split(Environment.NewLine);
        var lineLength = asLines[0].Length + Environment.NewLine.Length;
        var lineCount = asLines.Length;

        var numbers = Regex.Matches(dataRaw, @"\d+");
        var gears = Regex.Matches(dataRaw, @"\*");

        return gears.Sum(g =>
        {
            var adjacentNumbers = numbers.Where(n => AdjacentSymbolIndexes(n, dataRaw, lineLength, lineCount).Contains(g.Index));
            return adjacentNumbers.Count() == 2
                ? int.Parse(adjacentNumbers.First().Value) * int.Parse(adjacentNumbers.Last().Value)
                : 0;
        }).ToString();
    }

    private static bool IsAdjacentToSymbol(Match n, string dataRaw, int lineLength, int lineCount)
        => AdjacentSymbolIndexes(n, dataRaw, lineLength, lineCount).Any();

    private static IEnumerable<int> AdjacentSymbolIndexes(Match n, string dataRaw, int lineLength, int lineCount)
    {
        var indexes = new List<int>();
        for (int i = 0; i < n.Value.Length; i++)
        {
            var indexInData = n.Index + i;
            var x = indexInData % lineLength;
            var y = indexInData / lineLength;

            // Look at eight neighbors

            var nn = y > 0
                ? indexInData - lineLength : -1;
            var ne = y > 0 && x < lineLength - 1
                ? nn + 1 : -1;
            var nw = y > 0 && x > 0
                ? nn - 1 : -1;

            var ss = y < lineCount - 1
                ? indexInData + lineLength : -1;
            var se = y < lineCount - 1 && x < lineLength - 1
                ? ss + 1 : -1;
            var sw = y < lineCount - 1 && x > 0
                ? ss - 1 : -1;

            var ee = x < lineLength - 1
                ? indexInData + 1 : -1;
            var ww = x > 0
                ? indexInData - 1 : -1;

            var neighborIndices = new[] { nn, ne, ee, se, ss, sw, ww, nw }.Where(z => z >= 0);
            indexes.AddRange(neighborIndices.Where(ix => IsSymbol(dataRaw[ix].ToString())));
        }

        return indexes.Distinct();
    }

    private static bool IsSymbol(string c) => Regex.IsMatch(c, @"[^\d\.\r\n]");
}
