using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Solvers;

namespace AdventOfCode.Solvers._2023;

public class Puzzle20Solver : IPuzzleSolver
{

    public string SolvePartOne(string input)
    {
        var system = new ModuleSystem(input);
        for (int i = 0; i < 1000; i++)
        {
            system.PushButton();
        }
        return (system.HighPulseCount * system.LowPulseCount).ToString();
    }

    public string SolvePartTwo(string input) =>
        new ModuleSystem(input).FindRx().ToString();

    private class ModuleSystem
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

    private abstract class Module(string name, IEnumerable<string> outputs)
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

    private class Broadcaster(IEnumerable<string> outputs) : Module("broadcaster", outputs)
    {
        public override IEnumerable<Pulse> ReceivePulse(Pulse pulse)
        {
            return SendPulse(pulse.IsHigh);
        }
    }

    private class Conjunction(string name, IEnumerable<string> outputs)
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

            return SendPulse(true);
        }
    }

    private class FlipFlop(string name, IEnumerable<string> outputs)
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

    private class Pulse(string source, string destination, bool isHigh)
    {
        public readonly string Source = source;
        public readonly string Destination = destination;
        public readonly bool IsHigh = isHigh;
    }
}
