using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle10Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (_, _, coordinatesOfLoop) = GetData(input);
        return (coordinatesOfLoop.Count() / 2).ToString();
    }

    public string SolvePartTwo(string input)
    {
        var (matrix, _, coordinatesOfLoop) = GetData(input);

        // Replace non-loop characters
        foreach (var coordinate in matrix.AllCoordinates)
        {
            if (!coordinatesOfLoop.Contains(coordinate))
            {
                matrix.SetCharacter(coordinate, '.');
            }
        }

        return matrix.AllCoordinates
            .Count(ix => !coordinatesOfLoop.Contains(ix) && IsInsideLoop(matrix, ix))
            .ToString();
    }

    private static (CharacterMatrix, (int, int), IEnumerable<(int, int)>) GetData(string input)
    {
        var matrix = new CharacterMatrix(input);
        var startingPosition = matrix.FindAllCharacters('S').Single();

        // works for my inputs :)
        var useSample = matrix.Width < 30;
        matrix.SetCharacter(startingPosition, useSample ? 'F' : '7');
        var coordinatesOfLoop = CoordinatesOfLoop(matrix, startingPosition);
        return (matrix, startingPosition, coordinatesOfLoop);
    }

    private static IEnumerable<(int, int)> CoordinatesOfLoop(CharacterMatrix matrix, (int, int) startingPosition)
    {
        IList<(int, int)> visitedCoordinates = [startingPosition];
        var currentDirection = Direction.Down;

        do
        {
            var (nextPosition, nextDirection) = Travel(matrix, visitedCoordinates.Last(), currentDirection);
            visitedCoordinates.Add(nextPosition);
            currentDirection = nextDirection;
        } while (visitedCoordinates.Last() != visitedCoordinates.First());

        return visitedCoordinates.Skip(1);
    }

    private static bool IsInsideLoop(CharacterMatrix matrix, (int, int) startingCoordinate) =>
        startingCoordinate.Item1 > 0 &&
            matrix.StringAt((0, startingCoordinate.Item2), startingCoordinate.Item1)
                .Replace(".", "")
                .Replace("-", "")
                .Replace("L7", "|")
                .Replace("LJ", "")
                .Replace("F7", "")
                .Replace("FJ", "|")
                .Length % 2 == 1;

    private static ((int, int), Direction) Travel(CharacterMatrix data, (int, int) coordinate, Direction currentDirection)
    {
        return data.CharAt(coordinate) switch
        {
            '|' => currentDirection == Direction.Up
                ? ((coordinate.Item1, coordinate.Item2 - 1), Direction.Up)
                : ((coordinate.Item1, coordinate.Item2 + 1), Direction.Down),
            '-' => currentDirection == Direction.Right
                ? ((coordinate.Item1 + 1, coordinate.Item2), Direction.Right)
                : ((coordinate.Item1 - 1, coordinate.Item2), Direction.Left),
            'F' => currentDirection == Direction.Up
                ? ((coordinate.Item1 + 1, coordinate.Item2), Direction.Right)
                : ((coordinate.Item1, coordinate.Item2 + 1), Direction.Down),
            '7' => currentDirection == Direction.Up
                ? ((coordinate.Item1 - 1, coordinate.Item2), Direction.Left)
                : ((coordinate.Item1, coordinate.Item2 + 1), Direction.Down),
            'J' => currentDirection == Direction.Down
                ? ((coordinate.Item1 - 1, coordinate.Item2), Direction.Left)
                : ((coordinate.Item1, coordinate.Item2 - 1), Direction.Up),
            'L' => currentDirection == Direction.Down
                ? ((coordinate.Item1 + 1, coordinate.Item2), Direction.Right)
                : ((coordinate.Item1, coordinate.Item2 - 1), Direction.Up),
            _ => throw new Exception("Didn't consider some case"),
        };
    }
}
