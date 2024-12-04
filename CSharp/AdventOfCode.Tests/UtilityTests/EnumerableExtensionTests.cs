using AdventOfCode.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class EnumerableExtensionTests
{
    [TestMethod]
    public void AllPermutationsTest()
    {
        var permutations = new List<string> { "a", "b", "c" }.AllPermutations();
        Assert.AreEqual(6, permutations.Count());

        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["a", "b", "c"])));
        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["a", "c", "b"])));
        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["b", "a", "c"])));
        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["b", "c", "a"])));
        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["c", "a", "b"])));
        Assert.AreEqual(1, permutations.Count(p => Enumerable.SequenceEqual(p, ["c", "b", "a"])));
    }
}
