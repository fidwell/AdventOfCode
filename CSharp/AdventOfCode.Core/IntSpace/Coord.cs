global using Coord = (int, int);

namespace AdventOfCode.Core.IntSpace;

public record Coord2d
{
    public int X { get; init; }
    public int Y { get; init; }

    public Coord2d(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coord2d((int, int) coord)
    {
        X = coord.Item1;
        Y = coord.Item2;
    }

    public static Coord2d operator +(Coord2d a, Coord2d b) =>
        new(a.X + b.X, a.Y + b.Y);

    public static Coord2d operator -(Coord2d a, Coord2d b) =>
        new(a.X - b.X, a.Y - b.Y);
}
