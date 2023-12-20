namespace AdventOfCode.Core.Modules;

public class Conjunction(string name, IEnumerable<string> outputs)
    : Module(name, outputs)
{
    private readonly Dictionary<string, bool> _memory = [];
    
    public void Initialize(IEnumerable<string> inputNames)
    {
        _memory.Clear();
        foreach (var input in inputNames)
        {
            _memory.Add(input, false);
        }
    }

    public override IEnumerable<Pulse> ReceivePulse(Pulse pulse)
    {
        _memory[pulse.Source] = pulse.IsHigh;

        return _memory.All(m => m.Value)
            ? SendPulse(false)
            : SendPulse(true);
    }
}