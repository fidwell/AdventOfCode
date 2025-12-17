using AdventOfCode.Core.Matrixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests.MatrixTests;

[TestClass]
public sealed class MatrixPrintTests
{
    [TestMethod]
    public void PrintEmptyMatrix() => PrintMatrix(new int[0, 0], "[]");

    [TestMethod]
    public void Print1x1Matrix() => PrintMatrix(new int[1, 1], "[     0]");

    [TestMethod]
    public void Print2x1Matrix() => PrintMatrix(new int[2, 1], @"[     0]
[     0]");

    [TestMethod]
    public void Print1x2Matrix() => PrintMatrix(new int[1, 2], "[     0     0]");

    [TestMethod]
    public void Print3x3Matrix() => PrintMatrix(new int[3, 3], @"[     0     0     0]
[     0     0     0]
[     0     0     0]");

    [TestMethod]
    public void Print3x3MatrixWithSeparator() => PrintMatrix(new int[3, 3], @"[     0     0 |     0]
[     0     0 |     0]
[     0     0 |     0]", true);

    private static void PrintMatrix(int[,] input, string expected, bool withSeparator = false)
    {
        var m = Matrix<int>.FromArray(input);
        var result = m.Print(withSeparator: withSeparator);
        Console.WriteLine(result);
        Assert.AreEqual(expected, result);
    }
}
