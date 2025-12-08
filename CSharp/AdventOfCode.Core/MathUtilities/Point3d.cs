using System.Numerics;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.MathUtilities;

public readonly record struct Point3d<T> where T : INumber<T>
{
    public readonly T X;
    public readonly T Y;
    public readonly T Z;

    public Point3d(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3d(string input)
    {
        var portions = input.SplitAndTrim(',').Select(x => T.Parse(x, null)).ToArray();
        X = portions[0];
        Y = portions[1];
        Z = portions[2];
    }

    public static Point3d<T> operator +(Point3d<T> point1, Point3d<T> point2) =>
        new(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);

    public static T SquareDistanceBetween(Point3d<T> a, Point3d<T> b)
    {
        // Warning --- does not do its own type checking.
        // If you overflow, you'll get the wrong answer.
        var x = a.X - b.X;
        var y = a.Y - b.Y;
        var z = a.Z - b.Z;
        return x * x + y * y + z * z;
    }

    public override string ToString() => $"({X},{Y},{Z})";

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
