namespace AdventOfCode.Core.MathUtilities;

public class Ray3d
{
    public Point3d Position0;
    public Point3d Velocity;
    public double SlopeXy;

    public Ray3d(Point3d position0, Point3d velocity)
    {
        Position0 = position0;
        Velocity = velocity;

        SlopeXy = Velocity.Y / Velocity.X;
    }
}

public static class RayExtensions
{
    public static bool CollidesWithInXy(
        this Ray3d ray1, Ray3d ray2,
        long testAreaFrom = long.MinValue,
        long testAreaTo = long.MaxValue)
    {
        // Parallel; will never intersect
        if (ray1.SlopeXy == ray2.SlopeXy)
            return false;

        // Find point of intersection
        var x = (ray1.SlopeXy * ray1.Position0.X - ray2.SlopeXy * ray2.Position0.X - ray1.Position0.Y + ray2.Position0.Y) / (ray1.SlopeXy - ray2.SlopeXy);
        var y = ray1.SlopeXy * (x - ray1.Position0.X) + ray1.Position0.Y;

        // Also ensure that the point is in the future for both lines
        var isThisInFuture = (x - ray1.Position0.X) / ray1.Velocity.X > 0;
        var isOtherInFuture = (x - ray2.Position0.X) / ray2.Velocity.X > 0;

        if (!isThisInFuture || !isOtherInFuture)
            return false;

        return x >= testAreaFrom && y >= testAreaFrom && x <= testAreaTo && y <= testAreaTo;
    }
}
