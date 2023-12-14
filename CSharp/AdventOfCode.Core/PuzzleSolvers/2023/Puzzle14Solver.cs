using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle14Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        LoadNorth(Tilt(new CharacterMatrix(input), Direction.North))
        .ToString();

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static CharacterMatrix Tilt(CharacterMatrix matrix, Direction direction)
    {
        bool anythingMoved;
        do
        {
            (matrix, anythingMoved) = TryTilt(matrix, direction);
        } while (anythingMoved);
        return matrix;
    }

    // Returns "true" if anything moved, so we need to iterate again.
    private static (CharacterMatrix, bool) TryTilt(CharacterMatrix matrix, Direction direction)
    {
        var anythingMoved = false;

        // to do: directions other than North
        for (var x = 0; x < matrix.Width; x++)
        {
            for (var y = 1; y < matrix.Height; y++)
            {
                if (matrix.CharAt(x, y) == 'O' && matrix.CharAt(x, y - 1) == '.')
                {
                    var indexHere = matrix.IndexAt(x, y);
                    var indexNew = matrix.IndexAt(x, y - 1);
                    matrix.SetCharacter(indexHere, '.');
                    matrix.SetCharacter(indexNew, 'O');
                    anythingMoved = true;
                }
            }
        }

        return (matrix, anythingMoved);
    }

    private static int LoadNorth(CharacterMatrix matrix) =>
        matrix.FindAllCharacters('O')
        .Select(rockIndex => matrix.Height - matrix.CoordinatesAt(rockIndex).Item2)
        .Sum();

    private enum Direction
    {
        East,
        South,
        West,
        North
    }
}
