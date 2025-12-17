using AdventOfCode.Core.Matrixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests.MatrixTests;

[TestClass]
public class GaussianEliminationTests
{
    [TestMethod]
    public void RowEchelonTest1()
    {
        var m = Matrix<double>.Identity(4);
        Assert.IsTrue(m.IsInRowEchelonForm());
    }

    [TestMethod]
    public void RowEchelonTest2()
    {
        var m = Matrix<double>.FromArray(new double[,]
        {
            { 1, 3, 4 },
            { 0, 5, 6 },
            { 0, 0, 4 }
        });
        Assert.IsTrue(m.IsInRowEchelonForm());
    }

    [TestMethod]
    public void RowEchelonTest3()
    {
        var m = Matrix<double>.FromArray(new double[,]
        {
            { 0, 2, 1, -1 },
            { 0, 0, 3, 1 },
            { 0, 0, 0, 0 }
        });
        Assert.IsTrue(m.IsInRowEchelonForm());
    }

    [TestMethod]
    public void RowEchelonTest4()
    {
        var m = Matrix<double>.FromArray(new double[,]
        {
            { 0, 2, 1, -1 },
            { 1, 0, 3, 1 },
            { 0, 0, 5, 0 }
        });
        Assert.IsFalse(m.IsInRowEchelonForm());
    }

    [TestMethod]
    public void Reduce1()
    {
        var m = Matrix<double>.FromArray(new double[,]
        {
            { 1, 3, 1, 9 },
            { 1, 1, -1, 1 },
            { 3, 11, 5, 35 }
        });
        GaussianEliminator.Reduce(m);
        Console.WriteLine(m);
        Assert.IsTrue(m.IsInRowEchelonForm());
    }

    [TestMethod]
    public void Reduce2()
    {
        var m = Matrix<double>.FromArray(new double[,]
        {
            { 3, -2, -1, 1, 1 },
            { 6, -8, 1, 2, 4 }
        });
        GaussianEliminator.Reduce(m);
        Console.WriteLine(m);
        Assert.IsTrue(m.IsInRowEchelonForm());
    }
}
