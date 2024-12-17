using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle03Solver : IPuzzleSolver
{
    private static readonly Regex NumberRegex = new(@"\d+", RegexOptions.Compiled);

    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        return matrix
            .FindAllWords(NumberRegex)
            .Where(n => matrix.CoordinatesOfNeighbors(n).Any(coordinate => IsSymbol(matrix.CharAt(coordinate))))
            .Select(n => int.Parse(matrix.StringAt(n.StartCoordinate, n.Length)))
            .Sum()
            .ToString();
    }

    public string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var numbers = matrix.FindAllWords(NumberRegex);
        var numbersWithNeighbors = numbers.Select(n => new
        {
            Number = n,
            Neighbors = matrix.CoordinatesOfNeighbors(n)
        }).ToList();

        return matrix
            .FindAllCharacters('*')
            .Select(g =>
            {
                var neighboringWords = numbersWithNeighbors.Where(n => n.Neighbors.Contains(g)).ToList();
                return neighboringWords.Count == 2
                    ? neighboringWords.Select(w => int.Parse(w.Number.Value)).Aggregate((a, b) => a * b)
                    : 0;
            })
            .Sum()
            .ToString();
    }

    private static bool IsSymbol(char c) =>
        c == '+' ||
        c == '*' ||
        c == '=' ||
        c == '-' ||
        c == '&' ||
        c == '#' ||
        c == '/' ||
        c == '%' ||
        c == '$' ||
        c == '@';
}
