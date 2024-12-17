namespace AdventOfCode.Core.IntSpace;

public enum Direction : byte
{
    Undefined,
    Right,
    Down,
    Left,
    Up
}

public static class DirectionExtensions
{
    public static Coord Go(this Coord start, Direction direction, int amount = 1) =>
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

    public static Direction ToDirection(this char input) =>
        input switch
        {
            'R' or 'r' or '>' => Direction.Right,
            'D' or 'd' or 'v' => Direction.Down,
            'L' or 'l' or '<' => Direction.Left,
            'U' or 'u' or '^' => Direction.Up,
            _ => Direction.Undefined,
        };

    public static Direction RotateRight(this Direction input) =>
        input switch
        {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Right,
            _ => Direction.Undefined
        };

    public static Direction RotateLeft(this Direction input) =>
        input switch
        {
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Down,
            Direction.Up => Direction.Left,
            _ => Direction.Undefined
        };
}
