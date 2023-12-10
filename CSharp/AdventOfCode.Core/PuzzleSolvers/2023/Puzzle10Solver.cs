using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle10Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false)
    {
        var data = new CharacterMatrix(DataReader.GetData(10, 0, useSample));
        var startingPosition = data.FindAllMatches(new Regex(@"S")).Single().StartIndex;

        // works for my inputs :)
        data.SetCharacter(startingPosition, useSample ? 'F' : '7');
        int[] visitedIndexes = [startingPosition];
        var currentDirection = Direction.Down;

        do
        {
            var (nextPosition, nextDirection) = Travel(data, visitedIndexes[visitedIndexes.Length - 1], currentDirection);
            visitedIndexes = [.. visitedIndexes, nextPosition];
            currentDirection = nextDirection;
        } while (visitedIndexes[visitedIndexes.Length - 1] != visitedIndexes[0]);

        return ((visitedIndexes.Length - 1) / 2).ToString();
    }

    public string SolvePartTwo(bool useSample = false)
    {
        throw new NotImplementedException();
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
