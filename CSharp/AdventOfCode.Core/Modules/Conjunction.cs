namespace AdventOfCode.Core.Modules;

public class Conjunction(string name, IEnumerable<string> outputs)
    : Module(name, outputs)
{
    public readonly Dictionary<string, bool> Memory = [];

    public int LowPulses { get; private set; } = 0;

    public void Initialize(IEnumerable<string> inputNames)
    {
        Memory.Clear();
        foreach (var input in inputNames)
        {
            Memory.Add(input, false);
        }
    }

    public override IEnumerable<Pulse> ReceivePulse(Pulse pulse)
    {
        Memory[pulse.Source] = pulse.IsHigh;

        if (Memory.All(m => m.Value))
        {
            LowPulses++;
            return SendPulse(false);
        }
        else
        {
            return SendPulse(true);
        }
    }
}