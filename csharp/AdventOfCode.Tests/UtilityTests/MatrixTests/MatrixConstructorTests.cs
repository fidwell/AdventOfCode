using AdventOfCode.Core.Matrixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests.MatrixTests;

[TestClass]
public class MatrixConstructorTests
{
    [TestMethod]
    public void MatrixInitializeFromArray()
    {
        var m = Matrix<int>.FromArray(new int[,]
        {
            { 0, 1, 2, 3 },
            { 4, 5, 6, 7 }
        });

        Assert.AreEqual(0, m[0, 0]);
        Assert.AreEqual(1, m[0, 1]);
        Assert.AreEqual(2, m[0, 2]);
        Assert.AreEqual(5, m[1, 1]);
        Assert.ThrowsException<IndexOutOfRangeException>(() => m[2, 0]);
    }

    [TestMethod]
    public void MatrixInitializeIdentity()
    {
        var m = Matrix<int>.Identity(3);

        Assert.AreEqual(1, m[0, 0]);
        Assert.AreEqual(0, m[0, 1]);
        Assert.AreEqual(0, m[0, 2]);
        Assert.AreEqual(1, m[1, 1]);
        Assert.AreEqual(1, m[2, 2]);
        Assert.ThrowsException<IndexOutOfRangeException>(() => m[4, 4]);
    }

    [TestMethod]
    public void MatrixInitializeTranspose()
    {
        var m = Matrix<int>.FromArray(new int[,]
        {
            { 1, 2 },
            { 3, 4 },
            { 5, 6 }
        });
        var actual = m.Transpose();
        var expected = Matrix<int>.FromArray(new int[,]
        {
            { 1, 3, 5 },
            { 2, 4, 6 }
        });

        Assert.AreEqual(expected, actual);
    }
}
