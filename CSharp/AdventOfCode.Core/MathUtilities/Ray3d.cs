namespace AdventOfCode.Core.MathUtilities;

public readonly record struct Ray3d
{
    public readonly Point3d Position0;
    public readonly Point3d Velocity;
    public readonly Point3d Position1;
    public readonly double SlopeXy;
    public readonly double SlopeYz;
    public readonly double SlopeZx;

    public Ray3d(Point3d position0, Point3d velocity)
    {
        Position0 = position0;
        Velocity = velocity;
        Position1 = position0.Plus(velocity);

        SlopeXy = Velocity.Y / Velocity.X;
        SlopeYz = Velocity.Z / Velocity.Y;
        SlopeZx = Velocity.X / Velocity.Z;
    }

    public static Ray3d operator -(Ray3d value, Point3d adjustment) =>
        new(value.Position0, new Point3d(
            value.Velocity.X - adjustment.X,
            value.Velocity.Y - adjustment.Y,
            value.Velocity.Z - adjustment.Z));

    public (Point3d?, double) Collision3d(Ray3d other)
    {
        // Parallel; will never intersect
        if (SlopeXy == other.SlopeXy &&
            SlopeYz == other.SlopeYz &&
            SlopeZx == other.SlopeZx)
            return (null, 0);

        var a1 = Position0.X;
        var a2 = Position0.Y;
        var a3 = Position0.Z;

        var b1 = Position1.X;
        var b2 = Position1.Y;
        var b3 = Position1.Z;

        var c1 = other.Position0.X;
        var c2 = other.Position0.Y;
        var c3 = other.Position0.Z;

        var d1 = other.Position1.X;
        var d2 = other.Position1.Y;
        var d3 = other.Position1.Z;

        var A = b1 - a1;
        var B = c1 - d1;
        var C = c1 - a1;
        var D = b2 - a2;
        var E = c2 - d2;
        var F = c2 - a2;

        var t = (C * E - F * B) / (E * A - B * D);
        var s = (D * C - A * F) / (D * B - A * E);

        var u = t * (b3 - a3) + s * (c3 - d3);
        var v = c3 - a3;

        if (u == v && 0 <= t && 0 <= s)
        {
            var intersectionX = a1 + t * (b1 - a1);
            var intersectionY = a2 + t * (b2 - a2);
            var intersectionZ = a3 + t * (b3 - a3);

            // No floats allowed
            if (intersectionX % 1 != 0 ||
                intersectionY % 1 != 0 ||
                intersectionZ % 1 != 0)
                return (null, 0);

            return (new Point3d(intersectionX, intersectionY, intersectionZ), t);
        }
        return (null, 0);
    }
}

public static class RayExtensions
{
    public static bool CollidesWithInXy(
        this Ray3d ray1, Ray3d ray2,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue) =>
        CollidesGeneral(
            ray1.Position0.X, ray1.Position0.Y, ray1.SlopeXy, ray1.Velocity.X,
            ray2.Position0.X, ray2.Position0.Y, ray2.SlopeXy, ray2.Velocity.X,
            testAreaFrom, testAreaTo);

    public static bool CollidesWithInYz(
        this Ray3d ray1, Ray3d ray2,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue) =>
        CollidesGeneral(
            ray1.Position0.Y, ray1.Position0.Z, ray1.SlopeYz, ray1.Velocity.Y,
            ray2.Position0.Y, ray2.Position0.Z, ray2.SlopeYz, ray2.Velocity.Y,
            testAreaFrom, testAreaTo);

    public static bool CollidesWithInZx(
        this Ray3d ray1, Ray3d ray2,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue) =>
        CollidesGeneral(
            ray1.Position0.Z, ray1.Position0.X, ray1.SlopeZx, ray1.Velocity.Z,
            ray2.Position0.Z, ray2.Position0.X, ray2.SlopeZx, ray2.Velocity.Z,
            testAreaFrom, testAreaTo);

    private static bool CollidesGeneral(
        double ray1a, double ray1b, double slope1, double velocity1a,
        double ray2a, double ray2b, double slope2, double velocity2a,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue)
    {
        // Parallel; will never intersect
        if (slope1 == slope2)
            return false;

        // Find point of intersection
        var x = (slope1 * ray1a - slope2 * ray2a - ray1b + ray2b) / (slope1 - slope2);
        var y = slope1 * (x - ray1a) + ray1b;

        // Also ensure that the point is in the future for both lines
        var isThisInFuture = (x - ray1a) / velocity1a > 0;
        var isOtherInFuture = (x - ray2a) / velocity2a > 0;

        if (!isThisInFuture || !isOtherInFuture)
            return false;

        return x >= testAreaFrom && y >= testAreaFrom && x <= testAreaTo && y <= testAreaTo;
    }
}
