namespace AdventOfCode.Core.Ranges;

public class RangeLong(long start, long length)
{
    public long Start { get; private set; } = start;
    public long Length { get; private set; } = length;
    public long End => Start + Length;

    public override string ToString() => $"[{Start},{End}] ({Length})";
}
