using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Core.Ranges;

/// <summary>Represent a range has start and end indexes.</summary>
public class RangeLong
{
    public long Start { get; }
    public long Length { get; }
    public long End => Start + Length;

    private RangeLong(long start, long length)
    {
        Start = start;
        Length = length;
    }

    /// <summary>Construct a Range object using the start index and length.</summary>
    /// <param name="start">Represent the inclusive start index of the range.</param>
    /// <param name="length">Represent the length of the range.</param>
    public static RangeLong ByLength(long start, long length) =>
        new(start, length);

    /// <summary>Construct a Range object using the start and end indexes.</summary>
    /// <param name="start">Represent the inclusive start index of the range.</param>
    /// <param name="end">Represent the exclusive end index of the range.</param>
    public static RangeLong ByBounds(long start, long end) =>
        new(start, end - start);

    /// <summary>Indicates whether the current Range object is equal to another object of the same type.</summary>
    /// <param name="value">An object to compare with this object</param>
    public override bool Equals([NotNullWhen(true)] object? value) =>
        value is RangeLong r &&
        r.Start.Equals(Start) &&
        r.End.Equals(End);

    /// <summary>Returns the hash code for this instance.</summary>
    public override int GetHashCode() =>
        HashCode.Combine(Start.GetHashCode(), End.GetHashCode());

    /// <summary>Converts the value of the current RangeLong object to its equivalent string representation.</summary>
    public override string ToString() => $"[{Start},{End}] ({Length})";
}
