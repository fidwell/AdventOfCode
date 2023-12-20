namespace AdventOfCode.Core.Modules;

public abstract class Module(string name, IEnumerable<string> outputs)
{
    public readonly string Name = name;
    public IEnumerable<string> Outputs = outputs;

    public abstract IEnumerable<Pulse> ReceivePulse(Pulse pulse);

    public static Module Parse(string input)
    {
        var name = input.Split(' ').First().Substring(1);
        var outputs = input.Substring(input.IndexOf('>') + 2).Split(',', StringSplitOptions.TrimEntries);

        return input[0] switch
        {
            'b' => new Broadcaster(outputs),
            '%' => new FlipFlop(name, outputs),
            '&' => new Conjunction(name, outputs),
            _ => throw new NotImplementedException(),
        };
    }

    protected IEnumerable<Pulse> SendPulse(bool isHigh)
    {
        foreach (var output in Outputs)
        {
            yield return new Pulse(Name, output, isHigh);
        }
    }
}
