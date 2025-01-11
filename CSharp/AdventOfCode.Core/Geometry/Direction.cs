namespace AdventOfCode.Core.Geometry;

public enum Direction : byte
{
    Undefined = 0xff,

    East = 0x00,
    Southeast = 0x01,
    South = 0x02,
    Southwest = 0x03,
    West = 0x04,
    Northwest = 0x05,
    North = 0x06,
    Northeast = 0x07,

    Right = East,
    Down = South,
    Left = West,
    Up = North
}

public static class DirectionExtensions
{
    public static Coord Go(this Coord start, Direction direction, int amount = 1) =>
        direction switch
        {
            Direction.East => (start.Item1 + amount, start.Item2),
            Direction.Southeast => (start.Item1 + amount, start.Item2 + amount),
            Direction.South => (start.Item1, start.Item2 + amount),
            Direction.Southwest => (start.Item1 - amount, start.Item2 + amount),
            Direction.West => (start.Item1 - amount, start.Item2),
            Direction.Northwest => (start.Item1 - amount, start.Item2 - amount),
            Direction.North => (start.Item1, start.Item2 - amount),
            Direction.Northeast => (start.Item1 + amount, start.Item2 - amount),
            _ => start
        };


    public static Direction Opposite(this Direction direction) =>
        direction switch
        {
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            Direction.West => Direction.East,
            Direction.North => Direction.South,
            Direction.Southeast => Direction.Northwest,
            Direction.Southwest => Direction.Northeast,
            Direction.Northwest => Direction.Southeast,
            Direction.Northeast => Direction.Southwest,
            _ => Direction.Undefined
        };

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

    public static Direction[] All4 => [
        Direction.East,
        Direction.South,
        Direction.West,
        Direction.North
    ];

    public static Direction[] All8 => [
        Direction.East,
        Direction.Southeast,
        Direction.South,
        Direction.Southwest,
        Direction.West,
        Direction.Northwest,
        Direction.North,
        Direction.Northeast
    ];
}
