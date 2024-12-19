using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle19Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (atoms, targets) = Parse(input);
        var cache = Preprocess(atoms);
        return targets.Count(t => SolutionCount(t, atoms, cache) > 0).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (atoms, targets) = Parse(input);
        var cache = Preprocess(atoms);
        return targets.Aggregate(0UL, (a, b) => a + SolutionCount(b, atoms, cache)).ToString();
    }

    private static (IEnumerable<string>, IEnumerable<string>) Parse(string input)
    {
        var lines = input.SplitByNewline();
        return (lines[0].Split(", "), lines.Skip(1));
    }

    private static Dictionary<string, ulong> Preprocess(IEnumerable<string> atoms)
    {
        var cache = new Dictionary<string, ulong>();
        foreach (var atom in atoms.OrderBy(a => a.Length))
        {
            cache[atom] = 1 + SolutionCount(atom, atoms, cache, isPreprocessing: true);
        }
        return cache;
    }

    private static ulong SolutionCount(
        string target,
        IEnumerable<string> atoms,
        Dictionary<string, ulong> cache,
        bool isPreprocessing = false)
    {
        if (!isPreprocessing && cache.TryGetValue(target, out ulong cachedValue))
            return cachedValue;

        foreach (var atom in atoms.Where(target.StartsWith))
        {
            var substring = target[atom.Length..];
            var count = SolutionCount(substring, atoms, cache);
            _ = cache.TryGetValue(target, out ulong existing);
            cache[target] = existing + count;
        }

        _ = cache.TryGetValue(target, out ulong value);
        return value;
    }
}
