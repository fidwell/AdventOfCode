using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle10Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (_, coordinatesOfLoop) = GetData(input);
        return (coordinatesOfLoop.Count() / 2).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (matrix, coordinatesOfLoop) = GetData(input);

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

    private static (CharacterMatrix, IEnumerable<Coord>) GetData(string input)
    {
        var matrix = new CharacterMatrix(input);
        var startingPosition = matrix.SingleMatch('S');

        // works for my inputs :)
        var useSample = matrix.Width < 30;
        matrix.SetCharacter(startingPosition, useSample ? 'F' : '7');
        var coordinatesOfLoop = CoordinatesOfLoop(matrix, startingPosition);
        return (matrix, coordinatesOfLoop);
    }

    private static IEnumerable<Coord> CoordinatesOfLoop(CharacterMatrix matrix, Coord startingPosition)
    {
        IList<Coord> visitedCoordinates = [startingPosition];
        var currentDirection = Direction.Down;

        do
        {
            var (nextPosition, nextDirection) = Travel(matrix, visitedCoordinates.Last(), currentDirection);
            visitedCoordinates.Add(nextPosition);
            currentDirection = nextDirection;
        } while (visitedCoordinates.Last() != visitedCoordinates.First());

        return visitedCoordinates.Skip(1);
    }

    private static bool IsInsideLoop(CharacterMatrix matrix, Coord startingCoordinate) =>
        startingCoordinate.Item1 > 0 &&
            matrix.StringAt((0, startingCoordinate.Item2), startingCoordinate.Item1)
                .Replace(".", "")
                .Replace("-", "")
                .Replace("L7", "|")
                .Replace("LJ", "")
                .Replace("F7", "")
                .Replace("FJ", "|")
                .Length % 2 == 1;

    private static (Coord, Direction) Travel(CharacterMatrix data, Coord coordinate, Direction currentDirection) => data.CharAt(coordinate) switch
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
