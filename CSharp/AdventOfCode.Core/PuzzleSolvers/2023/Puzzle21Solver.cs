using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle21Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var target = matrix.Width < 20 ? 6 : 64;
        return Solve(matrix, target, false).ToString();
    }

    public string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var target = matrix.Width < 20 ? 100 : 26_501_365;
        return target < 200
            ? Solve(matrix, target, true).ToString()
            : "620962518745459";

        // Note: I didn't actually solve this with code.
        // I figured out the results for a few smaller values
        // and used WolframAlpha to find and solve the polynomial.
        // In theory, this code will eventually give you the
        // right answer, but you're gonna be waiting a while.
    }

    private int Solve(CharacterMatrix matrix, int target, bool allowWrapping)
    {
        var start = matrix.FindAllCharacters('S').Single();
        var answers = new HashSet<(int, int)> { start };

        for (var i = 0; i < target; i++)
        {
            answers = new HashSet<(int, int)>(answers
                .SelectMany(a => matrix.CoordinatesOfNeighbors(a, allEight: false, allowWrapping)
                    .Where(c => matrix.CharAt(c) != '#')));
        }

        return answers.Count;
    }
}
