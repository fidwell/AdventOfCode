using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.MathUtilities;

public readonly record struct Point3d
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

    public static Point3d operator +(Point3d point1, Point3d point2) =>
        new(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);

    public static double DistanceBetween(Point3d a, Point3d b)
    {
        var x = a.X - b.X;
        var y = a.Y - b.Y;
        var z = a.Z - b.Z;
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public override int GetHashCode() =>
        HashCode.Combine(X, Y, Z);
}
