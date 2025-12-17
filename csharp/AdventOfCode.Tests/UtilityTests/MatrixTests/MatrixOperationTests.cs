using AdventOfCode.Core.Matrixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests.MatrixTests;

[TestClass]
public class MatrixOperationTests
{
    [TestMethod]
    public void SwapRowTest()
    {
        var original = Matrix<int>.Identity(3);
        original.SwapRows(0, 1);

        Assert.AreEqual(Matrix<int>.FromArray(new int[,]
        {
            { 0, 1, 0 },
            { 1, 0, 0 },
            { 0, 0, 1 },
        }), original);
    }

    [TestMethod]
    public void ScaleRowTest()
    {
        var original = Matrix<int>.Identity(3);
        original.ScaleRow(1, 3);

        Assert.AreEqual(Matrix<int>.FromArray(new int[,]
        {
            { 1, 0, 0 },
            { 0, 3, 0 },
            { 0, 0, 1 },
        }), original);
    }

    [TestMethod]
    public void SumWithMultipleOfRowTest()
    {
        var original = Matrix<int>.Identity(3);
        original.SumWithMultipleOfRow(1, 2, 3);

        Assert.AreEqual(Matrix<int>.FromArray(new int[,]
        {
            { 1, 0, 0 },
            { 0, 1, 3 },
            { 0, 0, 1 },
        }), original);
    }
}
