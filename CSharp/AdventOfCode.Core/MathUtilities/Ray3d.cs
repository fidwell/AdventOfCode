namespace AdventOfCode.Core.MathUtilities;

public class Ray3d
{
    public Point3d Position0;
    public Point3d Velocity;
    public double SlopeXy;
    public double SlopeYz;
    public double SlopeZx;

    public Ray3d(Point3d position0, Point3d velocity)
    {
        Position0 = position0;
        Velocity = velocity;

        SlopeXy = Velocity.Y / Velocity.X;
        SlopeYz = Velocity.Z / Velocity.Y;
        SlopeZx = Velocity.X / Velocity.Z;
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

    private static bool CollidesGeneral(
        double ray1a, double ray1b, double slope1, double velocity1a,
        double ray2a, double ray2b, double slope2, double velocity2a,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue
        )
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
