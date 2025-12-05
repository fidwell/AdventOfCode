namespace AdventOfCode.Core.Ranges;

public static class RangeLongExtensions
{
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

    /// <summary>
    /// Determines whether the specified value is within the bounds of the given range.
    /// </summary>
    /// <remarks>The containment check is inclusive of the range's start and inclusive of its end.</remarks>
    /// <param name="range">The range to test for containment of the value.</param>
    /// <param name="value">The value to check for inclusion within the range.</param>
    /// <returns>true if the value is greater than or equal to the start of the range and
    /// less than or equal to the end of the range; otherwise, false.</returns>
    public static bool ContainsInclusive(this RangeLong range, long value) =>
        range.Start <= value && value <= range.End;

    /// <summary>
    /// Determines whether the specified value is within the bounds of the given range.
    /// </summary>
    /// <remarks>The containment check is inclusive of the range's start and exclusive of its end.</remarks>
    /// <param name="range">The range to test for containment of the value.</param>
    /// <param name="value">The value to check for inclusion within the range.</param>
    /// <returns>true if the value is greater than or equal to the start of the range and
    /// less than the end of the range; otherwise, false.</returns>
    public static bool Contains(this RangeLong range, long value) =>
        range.Start <= value && value < range.End;

    /// <summary>
    /// Determines whether the current range overlaps with the specified range.
    /// </summary>
    /// <param name="a">The first range to compare.</param>
    /// <param name="b">The second range to compare against.</param>
    /// <returns>true if the two ranges overlap; otherwise, false.</returns>
    public static bool OverlapsWith(this RangeLong a, RangeLong b) =>
        (a.Start <= b.Start && a.End > b.Start) ||
        (b.Start <= a.Start && b.End > a.Start);

    /// <summary>
    /// Merges overlapping or adjacent ranges in the specified sequence into the minimal set of
    /// non-overlapping ranges.
    /// </summary>
    /// <remarks>The order of the returned ranges is not guaranteed to match the input. Ranges
    /// that overlap or are adjacent will be merged into a single range.</remarks>
    /// <param name="ranges">The sequence of ranges to simplify. Cannot be null.</param>
    /// <returns>An enumerable collection of non-overlapping ranges representing the simplified form of the input.</returns>
    public static IEnumerable<RangeLong> Simplify(this IList<RangeLong> ranges)
    {
        ranges = [.. ranges.OrderBy(r => r.Start)];

        for (var i = 0; i < ranges.Count - 1; i++)
        {
            while (i < ranges.Count - 1 && ranges[i].OverlapsWith(ranges[i + 1]))
            {
                var newRange = ranges[i].MergeWith(ranges[i + 1]);
                ranges = [.. ranges.Take(i), newRange, .. ranges.Skip(i + 2)];
            }
        }

        return ranges;
    }

    /// <summary>
    /// Creates a new range that spans both specified ranges, provided they overlap.
    /// </summary>
    /// <remarks>The resulting range will have its start at the minimum of the two input ranges' starts and
    /// its end at the maximum of their ends. Both input ranges must overlap for the merge to succeed.</remarks>
    /// <param name="a">The first range to merge.</param>
    /// <param name="b">The second range to merge.</param>
    /// <returns>A new RangeLong that covers the combined extent of both input ranges.</returns>
    /// <exception cref="ArgumentException">Thrown if the specified ranges do not overlap.</exception>
    public static RangeLong MergeWith(this RangeLong a, RangeLong b)
    {
        if (!a.OverlapsWith(b))
            throw new ArgumentException("Ranges do not overlap.");

        var min = a.Start < b.Start ? a.Start : b.Start;
        var max = a.End > b.End ? a.End : b.End;
        return RangeLong.ByBounds(min, max);
    }
}
