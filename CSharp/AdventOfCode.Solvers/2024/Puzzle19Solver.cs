using AdventOfCode.Core.Memoization;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle19Solver : PuzzleSolver
{
    private IEnumerable<string> _atoms = [];

    public override string SolvePartOne(string input) =>
        SetUp(input).Count(t => Memoizer.Execute<ulong>(this, nameof(SolutionCount), t) > 0).ToString();

    public override string SolvePartTwo(string input) =>
        SetUp(input).Aggregate(0UL, (a, b) => a + Memoizer.Execute<ulong>(this, nameof(SolutionCount), b)).ToString();

    private IEnumerable<string> SetUp(string input)
    {
        var lines = input.SplitByNewline();
        _atoms = lines[0].Split(", ");
        return lines.Skip(1);
    }

    [Memoize]
    public ulong SolutionCount(string target) =>
        target.Length == 0
            ? 1
            : _atoms
                .Where(target.StartsWith)
                .Aggregate(0UL, (sum, a) => sum + Memoizer.Execute<ulong>(this, nameof(SolutionCount), target[a.Length..]));
}
