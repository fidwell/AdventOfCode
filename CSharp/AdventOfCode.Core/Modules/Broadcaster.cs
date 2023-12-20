namespace AdventOfCode.Core.Modules;

public class Broadcaster(IEnumerable<string> outputs) : Module("broadcaster", outputs)
{
    public override IEnumerable<Pulse> ReceivePulse(Pulse pulse)
    {
        return SendPulse(pulse.IsHigh);
    }
}
