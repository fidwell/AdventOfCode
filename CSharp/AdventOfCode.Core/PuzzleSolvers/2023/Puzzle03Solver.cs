using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle03Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        return matrix
            .FindAllWords(new Regex(@"\d+"))
            .Where(n => matrix.CoordinatesOfNeighbors(n).Any(coordinate => IsSymbol(matrix.CharAt(coordinate))))
            .Select(n => int.Parse(matrix.StringAt(n.StartCoordinate, n.Length)))
            .Sum()
            .ToString();
    }

    public string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var numbers = matrix.FindAllWords(new Regex(@"\d+"));

        return matrix
            .FindAllCharacters('*')
            .Select(g =>
            {
                var neighboringWords = numbers.Where(n => matrix.CoordinatesOfNeighbors(n).Contains(g));
                return neighboringWords.Count() == 2
                    ? neighboringWords.Select(w => int.Parse(w.Value)).Aggregate((a, b) => a * b)
                    : 0;
            })
            .Sum()
            .ToString();
    }

    private static bool IsSymbol(char c) => Regex.IsMatch($"{c}", @"[^\d\.\r\n]");
}
