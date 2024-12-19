using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle19Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var atoms = lines[0].Split(", ");
        var targets = lines.Skip(1);
        var cache = Preprocess(atoms);
        return targets.Count(t => SolutionCount(t, atoms, cache) > 0).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var lines = input.SplitByNewline();
        var atoms = lines[0].Split(", ").OrderBy(a => a.Length);
        var targets = lines.Skip(1);
        var cache = Preprocess(atoms);
        return targets.Aggregate(0UL, (a, b) => a + SolutionCount(b, atoms, cache)).ToString();
    }

    private static Dictionary<string, ulong> Preprocess(IEnumerable<string> atoms)
    {
        var cache = new Dictionary<string, ulong>();
        foreach (var atom in atoms)
        {
            cache[atom] = 1 + SolutionCount(atom, atoms, cache, isPreprocessing: true);
        }
        return cache;
    }

    private static ulong SolutionCount(string target, IEnumerable<string> atoms, Dictionary<string, ulong> cache, bool isPreprocessing = false)
    {
        if (!isPreprocessing && cache.TryGetValue(target, out ulong cachedValue))
        {
            return cachedValue;
        }

        foreach (var atom in atoms)
        {
            if (target.StartsWith(atom))
            {
                var substring = target[atom.Length..];
                var count = SolutionCount(substring, atoms, cache);

                if (cache.TryGetValue(target, out ulong existing))
                {
                    cache[target] = existing + count;
                }
                else
                {
                    Console.WriteLine($"{atom}|{substring}: {count}");
                    cache[target] = count;
                }
            }
        }
        
        if (cache.TryGetValue(target, out ulong value))
        {
            Console.WriteLine($"{cache[target]} ways to make {target}");
            return value;
        }
        else
        {
            Console.WriteLine($"Can't make {target}");
            cache[target] = 0;
            return 0;
        }
    }
}
