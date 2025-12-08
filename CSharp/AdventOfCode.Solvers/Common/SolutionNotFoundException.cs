namespace AdventOfCode.Solvers.Common;

public class SolutionNotFoundException(string? reason = null) : Exception("Could not find a solution.")
{
    public override string Message =>
        !string.IsNullOrWhiteSpace(reason) ? $"{base.Message} {reason}" : base.Message;
}
