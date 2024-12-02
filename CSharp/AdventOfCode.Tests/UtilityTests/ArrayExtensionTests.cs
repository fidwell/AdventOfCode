using AdventOfCode.Core.ArrayUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class ArrayExtensionTests
{
    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, 0, new[] { 2, 3 })]
    [DataRow(new[] { 1, 2, 3 }, 1, new[] { 1, 3 })]
    [DataRow(new[] { 1, 2, 3 }, 2, new[] { 1, 2 })]
    public void ArrayCopyExceptTests(int[] input, int index, int[] expected)
    {
        var copy = input.CopyExcept(index);
        Assert.IsTrue(Enumerable.SequenceEqual(copy, expected));
    }
}
