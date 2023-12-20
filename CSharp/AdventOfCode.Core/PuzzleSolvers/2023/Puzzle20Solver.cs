namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle20Solver : IPuzzleSolver
{
    private static IEnumerable<Module> Modules = [];
    private static readonly Queue<(string, string, Pulse)> PulseQueue = new();

    public string SolvePartOne(string input)
    {
        PulseQueue.Clear();
        Modules = input.Split(Environment.NewLine).Select(Parse);
        var broadcaster = Modules.OfType<Broadcaster>().Single();
        var pulseCount = 0;

        broadcaster.ReceivePulse("button", Pulse.Low);
        while (PulseQueue.Count != 0)
        {
            var next = PulseQueue.Dequeue();
            pulseCount++;
            var destination = Modules.Single(m => m.Name == next.Item2);
            destination.ReceivePulse(next.Item1, next.Item3);
        }

        return pulseCount.ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private static Module Parse(string input)
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

    private abstract class Module(string name, IEnumerable<string> outputs)
    {
        public readonly string Name = name;
        public IEnumerable<string> Outputs = outputs;
        
        public abstract void ReceivePulse(string source, Pulse pulse);

        protected void SendPulse(Pulse pulse)
        {
            foreach (var output in Outputs)
            {
                PulseQueue.Enqueue((Name, output, pulse));
            }
        }
    }

    private class Broadcaster(IEnumerable<string> outputs) : Module("broadcaster", outputs)
    {
        public override void ReceivePulse(string source, Pulse pulse)
        {
            SendPulse(pulse);
        }
    }

    private class FlipFlop(string name, IEnumerable<string> outputs)
        : Module(name, outputs)
    {
        public bool IsOn { get; private set; } = false;

        public override void ReceivePulse(string source, Pulse pulse)
        {
            if (pulse == Pulse.Low)
            {
                IsOn = !IsOn;
                SendPulse(IsOn ? Pulse.High : Pulse.Low);
            }
        }
    }

    private class Conjunction(string name, IEnumerable<string> outputs)
        : Module(name, outputs)
    {
        private readonly Dictionary<string, Pulse> _memory = [];

        public override void ReceivePulse(string source, Pulse pulse)
        {
            _memory.TryAdd(source, Pulse.Low);
            _memory[source] = pulse;

            if (_memory.All(m => m.Value == Pulse.High))
            {
                SendPulse(Pulse.Low);
            }
            else
            {
                SendPulse(Pulse.High);
            }
        }
    }

    private enum Pulse
    {
        Low,
        High
    }
}
