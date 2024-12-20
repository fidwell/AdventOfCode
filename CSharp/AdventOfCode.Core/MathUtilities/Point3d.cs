using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.MathUtilities;

public record Point3d
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;

    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3d(string input)
    {
        var portions = input.SplitAndTrim(',').Select(double.Parse).ToArray();
        X = portions[0];
        Y = portions[1];
        Z = portions[2];
    }

    public Point3d Plus(Point3d other) =>
        new(X + other.X, Y + other.Y, Z + other.Z);

    public override int GetHashCode() =>
        HashCode.Combine(X, Y, Z);
}
