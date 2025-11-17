using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public partial class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (initialValues, setup) = ParseInput(input);
        return FindZ(setup, initialValues).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (initialValues, setup) = ParseInput(input);

        // Generate data for graphviz neato. Paste this in a .gv file
        // and take a look at the output. Find the anomalies!
        // You're on your own for this one!
        var graphAsString = $@"digraph {{
{string.Join(Environment.NewLine, setup.Select(g => $"{g.WireIn1} -> {g.WireOut} [color={ColorGate(g.Type)}]{Environment.NewLine}{g.WireIn2} -> {g.WireOut} [color={ColorGate(g.Type)}]"))}
}}";

        return $"cph,jqn,kwb,qkf,tgr,z12,z16,z24";
    }

    private static string ColorGate(GateType type) => type switch
    {
        GateType.AND => "red",
        GateType.OR => "green",
        GateType.XOR => "blue",
        _ => "black"
    };

    private static ulong FindZ(List<Gate> setup, Dictionary<string, bool> initialValues)
    {
        var zWires = setup.Where(s => s.WireOut.StartsWith('z')).Select(s => s.WireOut).OrderByDescending(z => z);

        var z = 0UL;
        foreach (var output in zWires)
        {
            z *= 2;
            var value = CalculateWire(setup, initialValues, output);
            if (value)
                z += 1;
        }
        return z;
    }

    private static (Dictionary<string, bool>, List<Gate>) ParseInput(string input)
    {
        var initialValues = new Dictionary<string, bool>();
        var setup = new List<Gate>();

        foreach (var line in input.SplitByNewline())
        {
            if (line.Contains(':'))
            {
                var match = InitialWireValueRegex().Match(line);
                var wire = match.Groups[1].Value;
                var value = match.Groups[2].Value;
                initialValues.Add(wire, value == "1");
            }
            else
            {
                var match = GateRegex().Match(line);
                var wire1 = match.Groups[1].Value;
                var gateType = match.Groups[2].Value;
                var wire2 = match.Groups[3].Value;
                var wire3 = match.Groups[4].Value;

                setup.Add(new Gate(wire1, wire2, gateType == "AND" ? GateType.AND : gateType == "OR" ? GateType.OR : GateType.XOR, wire3));
            }
        }

        return (initialValues, setup);
    }

    private static bool CalculateWire(List<Gate> setup, Dictionary<string, bool> initialValues, string wire)
    {
        if (initialValues.TryGetValue(wire, out var result))
            return result;

        var input = setup.Single(s => s.WireOut == wire);

        return input.Type switch
        {
            GateType.AND => CalculateWire(setup, initialValues, input.WireIn1) && CalculateWire(setup, initialValues, input.WireIn2),
            GateType.OR => CalculateWire(setup, initialValues, input.WireIn1) || CalculateWire(setup, initialValues, input.WireIn2),
            GateType.XOR => CalculateWire(setup, initialValues, input.WireIn1) != CalculateWire(setup, initialValues, input.WireIn2),
            _ => throw new NotImplementedException(),
        };
    }

    private readonly record struct Gate(string WireIn1, string WireIn2, GateType Type, string WireOut);

    private enum GateType
    {
        AND,
        OR,
        XOR
    }

    [GeneratedRegex(@"([xy]\d\d): ([01])")]
    private static partial Regex InitialWireValueRegex();

    [GeneratedRegex(@"([a-z0-9]{3}) (AND|OR|XOR) ([a-z0-9]{3}) -> ([a-z0-9]{3})")]
    private static partial Regex GateRegex();
}
