namespace AdventOfCode.Core.Modules;

public class Pulse(string source, string destination, bool isHigh)
{
    public readonly string Source = source;
    public readonly string Destination = destination;
    public readonly bool IsHigh = isHigh;
}
