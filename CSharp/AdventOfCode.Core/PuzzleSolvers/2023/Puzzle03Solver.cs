using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle03Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false)
    {
        var matrix = new CharacterMatrix(DataReader.GetData(3, useSample));
        return matrix
            .FindAllWords(new Regex(@"\d+"))
            .Where(n => matrix.IndexesOfNeighbors(n).Any(index => IsSymbol(matrix.CharAt(index))))
            .Select(n => int.Parse(matrix.StringAt(n.StartIndex, n.Length)))
            .Sum()
            .ToString();
    }

    public string SolvePartTwo(bool useSample = false)
    {
        var matrix = new CharacterMatrix(DataReader.GetData(3, useSample));
        var numbers = matrix.FindAllWords(new Regex(@"\d+"));

        return matrix
            .FindAllWords(new Regex(@"\*"))
            .Select(g =>
            {
                var neighboringWords = numbers.Where(n => matrix.IndexesOfNeighbors(n).Contains(g.StartIndex));
                return neighboringWords.Count() == 2
                    ? neighboringWords.Select(w => int.Parse(w.Value)).Aggregate((a, b) => a * b)
                    : 0;
            })
            .Sum()
            .ToString();
    }

    private static bool IsSymbol(string c) => Regex.IsMatch(c, @"[^\d\.\r\n]");
}
