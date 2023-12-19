namespace AdventOfCode.Core.Ranges;

public static class RangeExtensions
{
    public static RangeLong Intersection(this RangeLong left, RangeLong right)
    {
        var intersectionStart = Math.Max(left.Start, right.Start);
        var intersectionEnd = Math.Min(left.Start + left.Length, right.Start + right.Length);
        var intersectionLength = Math.Max(0, intersectionEnd - intersectionStart);
        return new RangeLong(intersectionStart, intersectionLength);
    }

    public static IEnumerable<RangeLong> Difference(this RangeLong left, RangeLong other)
    {
        var intersectionStart = Math.Max(left.Start, other.Start);
        var intersectionEnd = Math.Min(left.Start + left.Length, other.Start + other.Length);

        if (intersectionStart >= intersectionEnd)
        {
            yield break;
        }

        yield return new RangeLong(left.Start, intersectionStart - left.Start);
        yield return new RangeLong(intersectionEnd, left.Start + left.Length - intersectionEnd);
    }
}
