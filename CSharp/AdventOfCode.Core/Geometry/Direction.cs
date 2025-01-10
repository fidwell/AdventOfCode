namespace AdventOfCode.Core.Geometry;

public enum Direction : byte
{
    Undefined = 0xff,

    Right = 0x00,
    Down = 0x01,
    Left = 0x02,
    Up = 0x03,

    East = Right,
    South = Down,
    West = Left,
    North = Up
}

public enum Direction8 : byte
{
    Undefined = 0xff,

    East = 0x00,
    Southeast = 0x01,
    South = 0x02,
    Southwest = 0x03,
    West = 0x04,
    Northwest = 0x05,
    North = 0x06,
    Northeast = 0x07
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

    public static Coord Go(this Coord start, Direction8 direction, int amount = 1) =>
        direction switch
        {
            Direction8.East => (start.Item1 + amount, start.Item2),
            Direction8.Southeast => (start.Item1 + amount, start.Item2 + amount),
            Direction8.South => (start.Item1, start.Item2 + amount),
            Direction8.Southwest => (start.Item1 - amount, start.Item2 + amount),
            Direction8.West => (start.Item1 - amount, start.Item2),
            Direction8.Northwest => (start.Item1 - amount, start.Item2 - amount),
            Direction8.North => (start.Item1, start.Item2 - amount),
            Direction8.Northeast => (start.Item1 + amount, start.Item2 - amount),
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

    public static Direction8[] AllDirection8 => [
        Direction8.East,
        Direction8.Southeast,
        Direction8.South,
        Direction8.Southwest,
        Direction8.West,
        Direction8.Northwest,
        Direction8.North,
        Direction8.Northeast
    ];
}
