using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle19Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline();
        var atoms = lines[0].Split(", ");
        var targets = lines.Skip(1);

        var cache = atoms.ToDictionary(t => t, t => true);
        return targets.Count(t => CanYouSolve(t, atoms, cache)).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static bool CanYouSolve(string target, IEnumerable<string> atoms, Dictionary<string, bool> cache)
    {
        if (cache.TryGetValue(target, out bool cachedValue))
        {
            return cachedValue;
        }

        foreach (var atom in atoms)
        {
            if (target == atom)
            {
                cache[atom] = true;
                return true;
            }

            if (target.StartsWith(atom))
            {
                var substring = target[atom.Length..];
                if (CanYouSolve(substring, atoms, cache))
                {
                    cache[target] = true;
                    return true;
                }
            }
        }

        //Console.WriteLine($"Can't solve {target}");
        cache[target] = false;
        return false;
    }
}
