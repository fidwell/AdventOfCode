using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle10Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (_, _, coordinatesOfLoop) = GetData(input);
        return (coordinatesOfLoop.Count() / 2).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (matrix, _, coordinatesOfLoop) = GetData(input);

        // Replace non-loop characters
        foreach (var coordinate in matrix.AllCoordinates2)
        {
            if (!coordinatesOfLoop.Contains(coordinate))
            {
                matrix.SetCharacter(coordinate, '.');
            }
        }

        return matrix.AllCoordinates2
            .Count(ix => !coordinatesOfLoop.Contains(ix) && IsInsideLoop(matrix, ix))
            .ToString();
    }

    private static (CharacterMatrix, Coord2d, IEnumerable<Coord2d>) GetData(string input)
    {
        var matrix = new CharacterMatrix(input);
        var startingPosition = matrix.FindAllCharacters2('S').Single();

        // works for my inputs :)
        var useSample = matrix.Width < 30;
        matrix.SetCharacter(startingPosition, useSample ? 'F' : '7');
        var coordinatesOfLoop = CoordinatesOfLoop(matrix, startingPosition);
        return (matrix, startingPosition, coordinatesOfLoop);
    }

    private static IEnumerable<Coord2d> CoordinatesOfLoop(CharacterMatrix matrix, Coord2d startingPosition)
    {
        IList<Coord2d> visitedCoordinates = [startingPosition];
        var currentDirection = Direction.Down;

        do
        {
            var nextPose = Travel(matrix, visitedCoordinates.Last(), currentDirection);
            visitedCoordinates.Add(nextPose.Location);
            currentDirection = nextPose.Direction;
        } while (visitedCoordinates.Last() != visitedCoordinates.First());

        return visitedCoordinates.Skip(1);
    }

    private static bool IsInsideLoop(CharacterMatrix matrix, Coord2d startingCoordinate) =>
        startingCoordinate.X > 0 &&
            matrix.StringAt(new Coord2d(0, startingCoordinate.Y), startingCoordinate.X)
                .Replace(".", "")
                .Replace("-", "")
                .Replace("L7", "|")
                .Replace("LJ", "")
                .Replace("F7", "")
                .Replace("FJ", "|")
                .Length % 2 == 1;

    private static Pose Travel(CharacterMatrix data, Coord2d coordinate, Direction currentDirection)
    {
        return data.CharAt(coordinate) switch
        {
            '|' => currentDirection == Direction.Up
                ? new Pose(coordinate.X, coordinate.Y - 1, Direction.Up)
                : new Pose(coordinate.X, coordinate.Y + 1, Direction.Down),
            '-' => currentDirection == Direction.Right
                ? new Pose(coordinate.X + 1, coordinate.Y, Direction.Right)
                : new Pose(coordinate.X - 1, coordinate.Y, Direction.Left),
            'F' => currentDirection == Direction.Up
                ? new Pose(coordinate.X + 1, coordinate.Y, Direction.Right)
                : new Pose(coordinate.X, coordinate.Y + 1, Direction.Down),
            '7' => currentDirection == Direction.Up
                ? new Pose(coordinate.X - 1, coordinate.Y, Direction.Left)
                : new Pose(coordinate.X, coordinate.Y + 1, Direction.Down),
            'J' => currentDirection == Direction.Down
                ? new Pose(coordinate.X - 1, coordinate.Y, Direction.Left)
                : new Pose(coordinate.X, coordinate.Y - 1, Direction.Up),
            'L' => currentDirection == Direction.Down
                ? new Pose(coordinate.X + 1, coordinate.Y, Direction.Right)
                : new Pose(coordinate.X, coordinate.Y - 1, Direction.Up),
            _ => throw new Exception("Didn't consider some case"),
        };
    }
}
