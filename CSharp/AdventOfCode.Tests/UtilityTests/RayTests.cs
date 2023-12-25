using AdventOfCode.Core.MathUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class RayTests
{
    [DataTestMethod]
    [DataRow(24, 13, 10, -3, 1, 2, 19, 13, 30, -2, 1, -2, 9, 18, 20, 5, DisplayName = "RaysIntersect 1")]
    [DataRow(24, 13, 10, -3, 1, 2, 18, 19, 22, -1, -1, -2, 15, 16, 16, 3, DisplayName = "RaysIntersect 2")]
    [DataRow(24, 13, 10, -3, 1, 2, 20, 25, 34, -2, -2, -4, 12, 17, 18, 4, DisplayName = "RaysIntersect 3")]
    [DataRow(24, 13, 10, -3, 1, 2, 12, 31, 28, -1, -2, -1, 6, 19, 22, 6, DisplayName = "RaysIntersect 4")]
    [DataRow(24, 13, 10, -3, 1, 2, 20, 19, 15, 1, -5, -3, 21, 14, 12, 1, DisplayName = "RaysIntersect 5")]
    public void RaysIntersect(
        double ray1Px,
        double ray1Py,
        double ray1Pz,
        double ray1Vx,
        double ray1Vy,
        double ray1Vz,
        double ray2Px,
        double ray2Py,
        double ray2Pz,
        double ray2Vx,
        double ray2Vy,
        double ray2Vz,
        double expectedX,
        double expectedY,
        double expectedZ,

        double expectedT)
    {
        var ray1 = new Ray3d(new Point3d(ray1Px, ray1Py, ray1Pz), new Point3d(ray1Vx, ray1Vy, ray1Vz));
        var ray2 = new Ray3d(new Point3d(ray2Px, ray2Py, ray2Pz), new Point3d(ray2Vx, ray2Vy, ray2Vz));
        var (collisionPoint, t) = ray1.Collision3d(ray2);
        Assert.IsNotNull(collisionPoint);
        Assert.AreEqual(collisionPoint.X, expectedX);
        Assert.AreEqual(collisionPoint.Y, expectedY);
        Assert.AreEqual(collisionPoint.Z, expectedZ);
        Assert.AreEqual(t, expectedT);
    }

    [DataTestMethod]
    [DataRow(24, 13, 70, -3, 1, -10, 19, 13, 30, -2, 1, -2, 9, 18, 20, 5, DisplayName = "RaysDontIntersect 1")]
    [DataRow(24, 13, 70, -3, 1, -10, 18, 19, 22, -1, -1, -2, 15, 16, 16, 3, DisplayName = "RaysDontIntersect 2")]
    [DataRow(24, 13, 70, -3, 1, -10, 20, 25, 34, -2, -2, -4, 12, 17, 18, 4, DisplayName = "RaysDontIntersect 3")]
    [DataRow(24, 13, 70, -3, 1, -10, 12, 31, 28, -1, -2, -1, 6, 19, 22, 6, DisplayName = "RaysDontIntersect 4")]
    [DataRow(24, 13, 70, -3, 1, -10, 20, 19, 15, 1, -5, -3, 21, 14, 12, 1, DisplayName = "RaysDontIntersect 5")]
    public void RaysDontIntersect(
        double ray1Px,
        double ray1Py,
        double ray1Pz,
        double ray1Vx,
        double ray1Vy,
        double ray1Vz,
        double ray2Px,
        double ray2Py,
        double ray2Pz,
        double ray2Vx,
        double ray2Vy,
        double ray2Vz,
        double expectedX,
        double expectedY,
        double expectedZ,

        double expectedT)
    {
        var ray1 = new Ray3d(new Point3d(ray1Px, ray1Py, ray1Pz), new Point3d(ray1Vx, ray1Vy, ray1Vz));
        var ray2 = new Ray3d(new Point3d(ray2Px, ray2Py, ray2Pz), new Point3d(ray2Vx, ray2Vy, ray2Vz));
        var (collisionPoint, t) = ray1.Collision3d(ray2);
        Assert.IsNull(collisionPoint);
    }
}
