using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle10Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (_, _, indexesOfLoop) = GetData(input);
        return (indexesOfLoop.Count() / 2).ToString();
    }

    public string SolvePartTwo(string input)
    {
        var (matrix, _, indexesOfLoop) = GetData(input);

        // Replace non-loop characters
        for (var i = 0; i < matrix.TotalLength; i++)
        {
            if (!indexesOfLoop.Contains(i))
            {
                matrix.SetCharacter(i, '.');
            }
        }

        return Enumerable.Range(0, matrix.TotalLength)
            .Count(ix => !indexesOfLoop.Contains(ix) && IsInsideLoop(matrix, ix))
            .ToString();
    }

    private static (CharacterMatrix, int, IEnumerable<int>) GetData(string input)
    {
        var matrix = new CharacterMatrix(input);
        var startingPosition = matrix.FindAllCharacters('S').Single();

        // works for my inputs :)
        var useSample = matrix.LineLength < 30;
        matrix.SetCharacter(startingPosition, useSample ? 'F' : '7');
        var indexesOfLoop = IndexesOfLoop(matrix, startingPosition);
        return (matrix, startingPosition, indexesOfLoop);
    }

    private static IEnumerable<int> IndexesOfLoop(CharacterMatrix matrix, int startingPosition)
    {
        IList<int> visitedIndexes = [startingPosition];
        var currentDirection = Direction.Down;

        do
        {
            var (nextPosition, nextDirection) = Travel(matrix, visitedIndexes.Last(), currentDirection);
            visitedIndexes.Add(nextPosition);
            currentDirection = nextDirection;
        } while (visitedIndexes.Last() != visitedIndexes.First());

        return visitedIndexes.Skip(1);
    }

    private static bool IsInsideLoop(CharacterMatrix matrix, int startingIndex)
    {
        var (x0, y0) = matrix.CoordinatesAt(startingIndex);
        if (x0 <= 0)
            return false;

        return matrix.StringAt(matrix.IndexAt(0, y0), x0)
            .Replace(".", "")
            .Replace("-", "")
            .Replace("L7", "|")
            .Replace("LJ", "")
            .Replace("F7", "")
            .Replace("FJ", "|")
            .Length % 2 == 1;
    }

    private static (int, Direction) Travel(CharacterMatrix data, int position, Direction currentDirection)
    {
        switch (data.CharAt(position))
        {
            case "|":
                return currentDirection == Direction.Up
                    ? (data.GoUpFrom(position), Direction.Up)
                    : (data.GoDownFrom(position), Direction.Down);
            case "-":
                return currentDirection == Direction.Right
                    ? (data.GoRightFrom(position), Direction.Right)
                    : (data.GoLeftFrom(position), Direction.Left);
            case "F":
                return currentDirection == Direction.Up
                    ? (data.GoRightFrom(position), Direction.Right)
                    : (data.GoDownFrom(position), Direction.Down);
            case "7":
                return currentDirection == Direction.Up
                    ? (data.GoLeftFrom(position), Direction.Left)
                    : (data.GoDownFrom(position), Direction.Down);
            case "J":
                return currentDirection == Direction.Down
                    ? (data.GoLeftFrom(position), Direction.Left)
                    : (data.GoUpFrom(position), Direction.Up);
            case "L":
                return currentDirection == Direction.Down
                    ? (data.GoRightFrom(position), Direction.Right)
                    : (data.GoUpFrom(position), Direction.Up);
        }

        throw new Exception("Didn't consider some case");
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}
