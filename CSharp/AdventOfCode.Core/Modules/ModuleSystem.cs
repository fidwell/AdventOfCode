using AdventOfCode.Core.MathUtilities;

namespace AdventOfCode.Core.Modules;

public class ModuleSystem
{
    public int LowPulseCount { get; private set; } = 0;
    public int HighPulseCount { get; private set; } = 0;

    private List<Module> Modules = [];
    private readonly Queue<Pulse> PulseQueue = new();

    private List<Conjunction> _inputsToConjunctor = [];

    private int _pushCount = 0;

    public ModuleSystem(string input)
    {
        Modules = input.Split(Environment.NewLine).Select(Module.Parse).ToList();
        foreach (var conjunction in Modules.OfType<Conjunction>())
        {
            conjunction.Initialize(Modules.Where(m => m.Outputs.Contains(conjunction.Name)).Select(m => m.Name));
        }
    }

    public long FindRx()
    {
        var hb = Modules.Single(m => m.Outputs.All(o => o == "rx"));
        var inputsToHb = Modules
            .Where(m => m.Outputs.All(o => o == hb.Name))
            .Select(m => m.Name);
        // inputs to inputsToHb
        _inputsToConjunctor = Modules
            .OfType<Conjunction>()
            .Where(m => m.Outputs.Any(o => inputsToHb.Contains(o)))
            .ToList();
        var conjunctorCounts = new Dictionary<string, int>();

        while (true)
        {
            PushButton();

            foreach (var input in _inputsToConjunctor
                .Where(i => i.LowPulses == 1 && !conjunctorCounts.ContainsKey(i.Name)))
            {
                conjunctorCounts[input.Name] = _pushCount;
            }

            if (conjunctorCounts.Count == _inputsToConjunctor.Count)
                break;
        }

        return conjunctorCounts.Select(c => (long)c.Value).Aggregate(MathExtensions.LCM);
    }

    public void PushButton()
    {
        _pushCount++;
        PulseQueue.Clear();
        PulseQueue.Enqueue(new Pulse("button", "broadcaster", false));
        
        while (PulseQueue.Count != 0)
        {
            var next = PulseQueue.Dequeue();

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
    }
}
