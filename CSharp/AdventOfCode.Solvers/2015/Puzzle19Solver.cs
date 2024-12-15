using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle19Solver : IPuzzleSolver
{
    private readonly List<Replacement> Replacements = [];
    private string Target = "";

    public string SolvePartOne(string input)
    {
        ParseInput(input);
        var targetSplit = TargetMoleculePattern().Match(Target).Groups[1].Captures.Select(c => c.Value).ToArray();

        var distinctMolecules = new HashSet<string>();
        for (var i = 0; i < targetSplit.Length; i++)
        {
            var thisMolecule = targetSplit[i];
            var replacementsForThis = Replacements.Where(r => r.From == thisMolecule);
            foreach (var r in replacementsForThis)
            {
                var newMoleculeArray = targetSplit.Take(i).Concat([r.To]).Concat(targetSplit.Skip(i + 1));
                distinctMolecules.Add(string.Join("", newMoleculeArray));
            }
        }
        return distinctMolecules.Count.ToString();
    }

    public string SolvePartTwo(string input)
    {
        ParseInput(input);
        var iterations = MaybeSolveIterator(Target, 0);
        return iterations.ToString();
    }

    private int MaybeSolveIterator(string target, int iterations)
    {
        if (target == "e")
            return iterations;

        // Greedily run the rules backwards
        var validReplacements = Replacements.Where(r => target.Contains(r.To));
        if (!validReplacements.Any())
        {
            return -1;
        }

        var biggestReplacementLength = validReplacements.Max(r => r.To.Length);
        var replacements = validReplacements.Where(r => r.To.Length == biggestReplacementLength);

        var minResult = int.MaxValue;
        foreach (var replacement in replacements)
        {
            // Create candidate molecule
            var indexOf = target.IndexOf(replacement.To);
            var subTarget = string.Concat(
                target.AsSpan(0, indexOf),
                replacement.From,
                target.AsSpan(indexOf + replacement.To.Length));

            // See if our candidate is solvable
            var subResult = MaybeSolveIterator(subTarget, iterations + 1);
            if (subResult >= 0 && subResult < minResult)
            {
                minResult = subResult;
            }
        }

        return minResult == int.MaxValue ? -1 : minResult;
    }

    private void ParseInput(string input)
    {
        if (Replacements.Count == 0)
        {
            var replacementPattern = ReplacementDefinitionPattern();

            var lines = input.SplitByNewline();
            for (var i = 0; i < lines.Length; i++)
            {
                if (i < lines.Length - 1)
                {
                    var match = replacementPattern.Match(lines[i]);
                    var from = match.Groups[1].Value;
                    var to = match.Groups[2].Value;
                    Replacements.Add(new Replacement(from, to));
                }
                else
                {
                    Target = lines[i];
                }
            }
        }
    }

    private record Replacement(string From, string To);

    [GeneratedRegex(@"(\w+) => (\w+)")]
    private static partial Regex ReplacementDefinitionPattern();

    [GeneratedRegex(@"([A-Z][a-z]?){1,}")]
    private static partial Regex TargetMoleculePattern();
}
