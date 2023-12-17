namespace AdventOfCode.Core.ArrayUtilities;

public enum Direction
{
    Undefined,
    Right,
    Down,
    Left,
    Up
}

public static class DirectionExtensions
{
    public static (int, int) Go(this (int, int) start, Direction direction, int amount = 1) =>
        direction switch
        {
            Direction.Right => (start.Item1 + amount, start.Item2),
            Direction.Down => (start.Item1, start.Item2 + amount),
            Direction.Left => (start.Item1 - amount, start.Item2),
            Direction.Up => (start.Item1, start.Item2 - amount),
            _ => start
        };

    public static Direction Opposite(this Direction direction) =>
        direction switch
        {
            Direction.Right => Direction.Left,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Up => Direction.Down,
            _ => Direction.Undefined
        };

    public static IEnumerable<Direction> All
    {
        get
        {
            yield return Direction.Right;
            yield return Direction.Down;
            yield return Direction.Left;
            yield return Direction.Up;
        }
    }
}
