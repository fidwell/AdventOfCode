using AdventOfCode.Core.LogicGates;
using AdventOfCode.Core.StringUtilities;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle07Solver : PuzzleSolver
{
    private readonly Dictionary<string, ushort> _signals = [];

    public override string SolvePartOne(string input) =>
        CalculateForWire("a", ParseGateDefinitions(input)).ToString();

    public override string SolvePartTwo(string input)
    {
        var gates = ParseGateDefinitions(input);
        var oldA = CalculateForWire("a", gates).ToString();
        var bGate = gates.Single(g => g.Output == "b");
        gates.Remove(bGate);
        gates.Add(new InputGate(oldA, "b"));
        _signals.Clear();
        return CalculateForWire("a", gates).ToString();
    }

    private ushort CalculateForWire(string wire, List<Gate> gates)
    {
        if (_signals.TryGetValue(wire, out ushort value))
            return value;

        if (ushort.TryParse(wire, out ushort signalStrength))
            return signalStrength;

        var gate = gates.Single(g => g.Output == wire);

        if (gate is InputGate inputGate)
        {
            value = CalculateForWire(inputGate.Input, gates);
        }
        else if (gate is AndGate andGate)
        {
            var first = CalculateForWire(andGate.Input1, gates);
            var second = CalculateForWire(andGate.Input2, gates);
            value = (ushort)(first & second);
        }
        else if (gate is OrGate orGate)
        {
            var first = CalculateForWire(orGate.Input1, gates);
            var second = CalculateForWire(orGate.Input2, gates);
            value = (ushort)(first | second);
        }
        else if (gate is RshiftGate rShiftGate)
        {
            var input = CalculateForWire(rShiftGate.Input, gates);
            value = (ushort)(input >> rShiftGate.Amount);
        }
        else if (gate is LshiftGate lShiftGate)
        {
            var input = CalculateForWire(lShiftGate.Input, gates);
            value = (ushort)(input << lShiftGate.Amount);
        }
        else if (gate is NotGate notGate)
        {
            var input = CalculateForWire(notGate.Input, gates);
            value = (ushort)~input;
        }

        _signals.TryAdd(wire, value);
        return value;
    }

    private static List<Gate> ParseGateDefinitions(string input)
    {
        var instructions = input.SplitByNewline();
        var gates = new List<Gate>();
        foreach (var instruction in instructions)
        {
            if (instruction.Length == 0)
                continue;

            if (instruction.Contains(" AND "))
            {
                var parsed = BinaryOperator().Matches(instruction)[0];
                gates.Add(new AndGate(parsed.Groups[1].Value, parsed.Groups[2].Value, parsed.Groups[3].Value));
            }
            else if (instruction.Contains(" OR "))
            {
                var parsed = BinaryOperator().Matches(instruction)[0];
                gates.Add(new OrGate(parsed.Groups[1].Value, parsed.Groups[2].Value, parsed.Groups[3].Value));
            }
            else if (instruction.Contains(" RSHIFT "))
            {
                var parsed = BinaryOperator().Matches(instruction)[0];
                gates.Add(new RshiftGate(parsed.Groups[1].Value, byte.Parse(parsed.Groups[2].Value), parsed.Groups[3].Value));
            }
            else if (instruction.Contains(" LSHIFT "))
            {
                var parsed = BinaryOperator().Matches(instruction)[0];
                gates.Add(new LshiftGate(parsed.Groups[1].Value, byte.Parse(parsed.Groups[2].Value), parsed.Groups[3].Value));
            }
            else if (instruction.Contains("NOT "))
            {
                var parsed = UnaryOperator().Matches(instruction)[0];
                gates.Add(new NotGate(parsed.Groups[1].Value, parsed.Groups[2].Value));
            }
            else
            {
                var parsed = InputOperator().Matches(instruction)[0];
                gates.Add(new InputGate(parsed.Groups[1].Value, parsed.Groups[2].Value));
            }
        }
        return gates;
    }

    [GeneratedRegex(@"(\w+) [A-Z]+ (\w+) -> ([a-z]+)")]
    private static partial Regex BinaryOperator();

    [GeneratedRegex(@"[A-Z]+ ([a-z]+) -> ([a-z]+)")]
    private static partial Regex UnaryOperator();

    [GeneratedRegex(@"(\w+) -> ([a-z]+)")]
    private static partial Regex InputOperator();
}
