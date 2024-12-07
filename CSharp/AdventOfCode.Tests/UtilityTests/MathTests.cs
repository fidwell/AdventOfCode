using AdventOfCode.Core.MathUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class MathTests
{
    [DataTestMethod]
    [DataRow(0UL, 0UL, 0UL)]
    [DataRow(0UL, 1UL, 1UL)]
    [DataRow(8UL, 4UL, 84UL)]
    [DataRow(86UL, 4UL, 864UL)]
    [DataRow(8UL, 64UL, 864UL)]
    [DataRow(93UL, 125UL, 93_125UL)]
    [DataRow(9_223_372_036UL, 854_775_807UL, 9_223_372_036_854_775_807UL)]
    [DataRow(18_446_744_073UL, 709_551_615UL, 18_446_744_073_709_551_615UL)]
    [DataRow(1_844_674_407_370_955_161UL, 5UL, 18_446_744_073_709_551_615UL)]
    [DataRow(1UL, 8_446_744_073_709_551_615UL, 18_446_744_073_709_551_615UL)]
    public void ConcatenationTest(ulong a, ulong b, ulong expected) =>
        Assert.AreEqual(expected, MathExtensions.Concatenate(a, b));
}
