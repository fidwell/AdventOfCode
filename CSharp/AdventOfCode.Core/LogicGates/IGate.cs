namespace AdventOfCode.Core.LogicGates;

public abstract class Gate(string output)
{
    public string Output { get; } = output;
}

public sealed class InputGate(string input, string output) : Gate(output)
{
    public string Input { get; } = input;
}

public sealed class AndGate(string input1, string input2, string output) : Gate(output)
{
    public string Input1 { get; } = input1;
    public string Input2 { get; } = input2;
}

public sealed class OrGate(string input1, string input2, string output) : Gate(output)
{
    public string Input1 { get; } = input1;
    public string Input2 { get; } = input2;
}

public sealed class RshiftGate(string input, byte amount, string output) : Gate(output)
{
    public string Input { get; } = input;
    public byte Amount { get; } = amount;
}

public sealed class LshiftGate(string input, byte amount, string output) : Gate(output)
{
    public string Input { get; } = input;
    public byte Amount { get; } = amount;
}

public sealed class NotGate(string input, string output) : Gate(output)
{
    public string Input { get; } = input;
}
