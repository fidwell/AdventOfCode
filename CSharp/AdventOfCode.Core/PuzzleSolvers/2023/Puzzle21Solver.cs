using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle21Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var start = matrix.FindAllCharacters('S').Single();
        var answers = new HashSet<(int, int)> { start };
        var target = matrix.Width < 20 ? 6 : 64;

        for (int i = 0; i < target; i++)
        {
            answers = new HashSet<(int, int)>(answers
                .SelectMany(a => matrix.CoordinatesOfNeighbors(a, allEight: false)
                    .Where(c => matrix.CharAt(c) != '#')));
        }

        return answers.Count.ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();
}
