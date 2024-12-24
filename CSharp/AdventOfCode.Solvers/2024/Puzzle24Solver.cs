using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle24Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (initialValues, setup) = ParseInput(input);
        return FindZ(setup, initialValues).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (initialValues, setup) = ParseInput(input);

        var xWires = initialValues.Where(w => w.Key.StartsWith('x')).OrderBy(x => x.Key);
        var x = 0UL;
        foreach (var wire in xWires)
        {
            x *= 2;
            if (wire.Value)
                x += 1;
        }

        var yWires = initialValues.Where(w => w.Key.StartsWith('y'));
        var y = 0UL;
        foreach (var wire in yWires)
        {
            y *= 2;
            if (wire.Value)
                y += 1;
        }

        var expectedZ = x & y;
        var actualZ = FindZ(setup, initialValues);

        throw new NotImplementedException();
    }

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

        var initialWireValueRegex = new Regex(@"([xy]\d\d): ([01])");
        var gateRegex = new Regex(@"([a-z0-9]{3}) (AND|OR|XOR) ([a-z0-9]{3}) -> ([a-z0-9]{3})");

        foreach (var line in input.SplitByNewline())
        {
            if (line.Contains(':'))
            {
                var match = initialWireValueRegex.Match(line);
                var wire = match.Groups[1].Value;
                var value = match.Groups[2].Value;
                initialValues.Add(wire, value == "1");
            }
            else
            {
                var match = gateRegex.Match(line);
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

        switch (input.Type)
        {
            case GateType.AND:
                return CalculateWire(setup, initialValues, input.WireIn1) && CalculateWire(setup, initialValues, input.WireIn2);
            case GateType.OR:
                return CalculateWire(setup, initialValues, input.WireIn1) || CalculateWire(setup, initialValues, input.WireIn2);
            case GateType.XOR:
                return CalculateWire(setup, initialValues, input.WireIn1) != CalculateWire(setup, initialValues, input.WireIn2);
            default:
                throw new NotImplementedException();
        }
    }

    private readonly record struct Gate(string WireIn1, string WireIn2, GateType Type, string WireOut);

    private enum GateType
    {
        AND,
        OR,
        XOR
    }
}
