using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle18Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, false);
    public string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool areCornersOn)
    {
        var grid = new CharacterMatrix(input);
        var isExample = grid.Width == 6;
        var steps = isExample ? (areCornersOn ? 5 : 4) : 100;
        for (var i = 0; i < steps; i++)
        {
            grid = Step(grid, areCornersOn);
        }
        return grid.AllCoordinates.Count(c => grid.CharAt(c) == '#').ToString();
    }

    private static CharacterMatrix Step(CharacterMatrix matrix, bool areCornersOn)
    {
        var copy = new CharacterMatrix(matrix.Width, matrix.Height, '.');

        if (areCornersOn)
        {
            TurnOnCorners(matrix);
            TurnOnCorners(copy);
        }

        for (var y = 0; y < matrix.Height; y++)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                var isOn = matrix.CharAt(x, y) == '#';
                var neighborCount = matrix.CoordinatesOfNeighbors((x, y)).Count(c => matrix.CharAt(c) == '#');

                if (neighborCount == 3)
                {
                    copy.SetCharacter((x, y), '#');
                }
                else if (isOn && neighborCount == 2)
                {
                    copy.SetCharacter((x, y), '#');
                }
            }
        }
        return copy;
    }

    private static void TurnOnCorners(CharacterMatrix matrix)
    {
        matrix.SetCharacter(0, 0, '#');
        matrix.SetCharacter(matrix.Width - 1, 0, '#');
        matrix.SetCharacter(0, matrix.Height - 1, '#');
        matrix.SetCharacter(matrix.Width - 1, matrix.Height - 1, '#');
    }
}
