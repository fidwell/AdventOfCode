using System.Diagnostics;

namespace AdventOfCode.Core.Modules;

public class ModuleSystem
{
    public int LowPulseCount { get; private set; } = 0;
    public int HighPulseCount { get; private set; } = 0;

    private static List<Module> Modules = [];
    private static readonly Queue<Pulse> PulseQueue = new();

    public ModuleSystem(string input)
    {
        Modules = input.Split(Environment.NewLine).Select(Module.Parse).ToList();

        foreach (var conjunction in Modules.OfType<Conjunction>())
        {
            conjunction.Initialize(Modules.Where(m => m.Outputs.Contains(conjunction.Name)).Select(m => m.Name));
        }
    }

    public void PushButton()
    {
        PulseQueue.Enqueue(new Pulse("button", "broadcaster", false));
        
        while (PulseQueue.Count != 0)
        {
            var next = PulseQueue.Dequeue();

            Trace.WriteLine($"{next.Source} -{(next.IsHigh ? "high" : "low")}-> {next.Destination}");
            
            if (next.IsHigh)
            {
                HighPulseCount++;
            }
            else
            {
                LowPulseCount++;
            }

            var destination = Modules.SingleOrDefault(m => m.Name == next.Destination);
            
            if (destination is null)
                continue;

            var results = destination.ReceivePulse(next);
            foreach (var result in results)
            {
                PulseQueue.Enqueue(result);
            }
        }

        Trace.WriteLine("");
    }
}
