namespace AdventOfCode.Core.Ranges;

public static class RangeExtensions
{
    public static int Length(this Range range) => range.End.Value - range.Start.Value;

    /// <summary>
    /// Calculates the intersection of two ranges and returns a new range representing the overlapping portion.
    /// </summary>
    /// <remarks>If the ranges do not overlap, the returned range will have a length of zero and its start
    /// will be set to the maximum of the two start values.</remarks>
    /// <param name="left">The first range to compare for intersection.</param>
    /// <param name="right">The second range to compare for intersection.</param>
    /// <returns>A new RangeLong representing the overlapping portion of the two ranges. If the ranges
    /// do not overlap, returns a range with zero length.</returns>
    public static RangeLong Intersection(this RangeLong left, RangeLong right)
    {
        var intersectionStart = Math.Max(left.Start, right.Start);
        var intersectionEnd = Math.Min(left.Start + left.Length, right.Start + right.Length);
        var intersectionLength = Math.Max(0, intersectionEnd - intersectionStart);
        return RangeLong.ByLength(intersectionStart, intersectionLength);
    }

    /// <summary>
    /// Calculates the set difference between two ranges, returning the portions of each range
    /// that do not overlap with /// the other.
    /// </summary>
    /// <remarks>The method yields zero, one, or two ranges depending on the relationship between the input
    /// ranges. If the ranges partially overlap, only the non-overlapping segments are returned.
    /// If the ranges do not overlap, both are returned; if they are equal or fully overlap,
    /// an empty collection is returned.</remarks>
    /// <param name="one">The first range to compare.</param>
    /// <param name="two">The second range to compare.</param>
    /// <returns>An enumerable collection of ranges representing the non-overlapping segments of the input ranges. If the ranges
    /// do not overlap, both are returned; if they are equal or fully overlap, an empty collection is returned.</returns>
    public static IEnumerable<RangeLong> Difference(this RangeLong one, RangeLong two)
    {
        if (!one.OverlapsWith(two))
        {
            yield return one;
            yield return two;
            yield break;
        }

        if (one.Equals(two))
            yield break;

        if (one.Start > two.Start)
        {
            (two, one) = (one, two);
        }

        var intersectionStart = Math.Max(one.Start, two.Start);
        var intersectionEnd = Math.Min(one.End, two.End);

        if (intersectionStart >= intersectionEnd)
            yield break;

        yield return RangeLong.ByBounds(one.Start, intersectionStart);
        yield return RangeLong.ByBounds(intersectionEnd, two.End);
    }

    public static bool ContainsInclusive(this RangeLong range, long value) => range.Start <= value && value <= range.End;

    public static bool Contains(this RangeLong range, long value) => range.Start <= value && value < range.End;

    public static bool OverlapsWith(this RangeLong a, RangeLong b) =>
        (a.Start <= b.Start && a.End > b.Start) ||
        (b.Start <= a.Start && b.End > a.Start);

    public static RangeLong MergeWith(this RangeLong a, RangeLong b)
    {
        var min = a.Start < b.Start ? a.Start : b.Start;
        var max = a.End > b.End ? a.End : b.End;
        return RangeLong.ByBounds(min, max);
    }
}
