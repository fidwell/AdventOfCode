namespace AdventOfCode.Core.Modules;

public class FlipFlop(string name, IEnumerable<string> outputs)
    : Module(name, outputs)
{
    public bool IsOn { get; private set; } = false;

    public override IEnumerable<Pulse> ReceivePulse(Pulse pulse)
    {
        if (pulse.IsHigh)
        {
            return Enumerable.Empty<Pulse>();
        }

        IsOn = !IsOn;
        return SendPulse(IsOn);
    }
}
