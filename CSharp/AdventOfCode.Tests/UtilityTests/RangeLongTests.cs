using AdventOfCode.Core.Ranges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class RangeLongTests
{
    [TestMethod]
    [DataRow(1, 5, 3, 7, 3, 5, DisplayName = "Simple ranges")]
    [DataRow(1, 5, 1, 5, 1, 5, DisplayName = "Identical ranges")]
    [DataRow(1, 5, 5, 10, 5, 5, DisplayName = "Flush ranges")]
    [DataRow(1, 5, 10, 17, 10, 10, DisplayName = "Non-overlapping ranges")]
    public void Intersection(long a1, long a2, long b1, long b2, long i1, long i2)
    {
        var a = RangeLong.ByBounds(a1, a2);
        var b = RangeLong.ByBounds(b1, b2);

        var expected = RangeLong.ByBounds(i1, i2);
        var actual = a.Intersection(b);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(1, 10, 4, 15, true, DisplayName = "Simple ranges")]
    [DataRow(1, 10, 1, 10, true, DisplayName = "Identical ranges")]
    [DataRow(1, 5, 5, 10, false, DisplayName = "Flush ranges")]
    [DataRow(1, 5, 10, 17, false, DisplayName = "Non-overlapping ranges")]
    public void Overlaps(long a1, long a2, long b1, long b2, bool result)
    {
        var a = RangeLong.ByBounds(a1, a2);
        var b = RangeLong.ByBounds(b1, b2);
        Assert.AreEqual(result, a.OverlapsWith(b));
    }

    [TestMethod]
    [DataRow(1, 10, 1, true)]
    [DataRow(1, 10, 4, true)]
    [DataRow(1, 10, 9, true)]
    [DataRow(1, 10, 10, false)]
    [DataRow(1, 10, 11, false)]
    [DataRow(1, 10, 0, false)]
    public void Contains(long a1, long a2, long b, bool result) =>
        Assert.AreEqual(result, RangeLong.ByBounds(a1, a2).Contains(b));

    [TestMethod]
    [DataRow(1, 10, 1, true)]
    [DataRow(1, 10, 4, true)]
    [DataRow(1, 10, 9, true)]
    [DataRow(1, 10, 10, true)]
    [DataRow(1, 10, 11, false)]
    [DataRow(1, 10, 0, false)]
    public void ContainsInclusive(long a1, long a2, long b, bool result) =>
        Assert.AreEqual(result, RangeLong.ByBounds(a1, a2).ContainsInclusive(b));

    [TestMethod]
    public void Difference_WhenRangesOverlap_ShouldReturnTwoRanges()
    {
        var a = RangeLong.ByBounds(1, 10);
        var b = RangeLong.ByBounds(5, 15);
        var differences = a.Difference(b).ToList();

        Assert.AreEqual(2, differences.Count);
        Assert.AreEqual(RangeLong.ByBounds(1, 5), differences[0]);
        Assert.AreEqual(RangeLong.ByBounds(10, 15), differences[1]);
    }

    [TestMethod]
    public void Difference_WhenRangesOverlapAndAreBackward_ShouldReturnTwoRanges()
    {
        var a = RangeLong.ByBounds(5, 15);
        var b = RangeLong.ByBounds(1, 10);
        var differences = a.Difference(b).ToList();

        Assert.AreEqual(2, differences.Count);
        Assert.AreEqual(RangeLong.ByBounds(1, 5), differences[0]);
        Assert.AreEqual(RangeLong.ByBounds(10, 15), differences[1]);
    }

    [TestMethod]
    public void Difference_WhenRangesCoincide_ShouldReturnNoRanges()
    {
        var a = RangeLong.ByBounds(1, 10);
        var b = RangeLong.ByBounds(1, 10);
        var differences = a.Difference(b).ToList();

        Assert.AreEqual(0, differences.Count);
    }

    [TestMethod]
    public void Difference_WhenRangesAreFlush_ShouldReturnOriginalRanges()
    {
        var a = RangeLong.ByBounds(1, 10);
        var b = RangeLong.ByBounds(10, 20);
        var differences = a.Difference(b).ToList();

        Assert.AreEqual(2, differences.Count);
        Assert.AreEqual(a, differences[0]);
        Assert.AreEqual(b, differences[1]);
    }

    [TestMethod]
    public void Difference_WhenRangesDoNotOverlap_ShouldReturnOriginalRanges()
    {
        var a = RangeLong.ByBounds(1, 10);
        var b = RangeLong.ByBounds(20, 30);
        var differences = a.Difference(b).ToList();

        Assert.AreEqual(2, differences.Count);
        Assert.AreEqual(a, differences[0]);
        Assert.AreEqual(b, differences[1]);
    }
}
