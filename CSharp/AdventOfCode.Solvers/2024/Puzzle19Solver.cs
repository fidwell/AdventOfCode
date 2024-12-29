using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle19Solver : PuzzleSolver
{
    private Dictionary<string, ulong> _cache = [];
    private IEnumerable<string> _atoms = [];

    public override string SolvePartOne(string input) =>
        SetUp(input).Count(t => SolutionCount(t) > 0).ToString();

    public override string SolvePartTwo(string input) =>
        SetUp(input).Select(SolutionCount).Sum().ToString();

    private IEnumerable<string> SetUp(string input)
    {
        _cache = new Dictionary<string, ulong>
        {
            { "", 1 }
        };

        var lines = input.SplitByNewline();
        _atoms = lines[0].Split(", ");
        return lines.Skip(1);
    }

    private ulong SolutionCount(string target)
    {
        if (_cache.TryGetValue(target, out ulong cachedValue))
            return _cache[target];

        foreach (var atom in _atoms.Where(target.StartsWith))
        {
            var substring = target[atom.Length..];
            var count = SolutionCount(substring);
            _ = _cache.TryGetValue(target, out ulong existing);
            _cache[target] = existing + count;
        }

        _ = _cache.TryGetValue(target, out ulong newValue);
        return newValue;
    }
}
