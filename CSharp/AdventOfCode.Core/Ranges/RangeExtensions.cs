namespace AdventOfCode.Core.PuzzleSolvers._2023;

public static class RangeExtensions
{
    public static Range Intersection(this Range left, Range right)
    {
        var intersectionStart = Math.Max(left.Start, right.Start);
        var intersectionEnd = Math.Min(left.Start + left.Length, right.Start + right.Length);
        var intersectionLength = Math.Max(0, intersectionEnd - intersectionStart);
        return new Range(intersectionStart, intersectionLength);
    }

    public static IEnumerable<Range> Difference(this Range left, Range other)
    {
        var intersectionStart = Math.Max(left.Start, other.Start);
        var intersectionEnd = Math.Min(left.Start + left.Length, other.Start + other.Length);

        if (intersectionStart >= intersectionEnd)
        {
            yield break;
        }

        yield return new Range(left.Start, intersectionStart - left.Start);
        yield return new Range(intersectionEnd, left.Start + left.Length - intersectionEnd);
    }
}
