using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle19Solver : PuzzleSolver
{
    private readonly List<Replacement> Replacements = [];
    private string Target = "";
    private readonly Random Random = new();

    public override object SolvePartOne(string input)
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
        return distinctMolecules.Count;
    }

    public override object SolvePartTwo(string input)
    {
        ParseInput(input);
        for (var attempts = 0; attempts < 100; attempts++)
        {
            var result = MaybeSolvePartTwo();
            if (result >= 0)
                return result;
        }
        throw new SolutionNotFoundException();
    }

    public int MaybeSolvePartTwo()
    {
        var target = Target;
        var iterations = 0;
        while (target != "e" && iterations < Target.Length)
        {
            var replacementCandidates = Replacements
                .Where(r => target.Contains(r.To))
                .OrderBy(r => Random.Next())
                .ToList();
            if (replacementCandidates.Count == 0)
                return -1;

            foreach (var replacement in replacementCandidates)
            {
                var index = target.LastIndexOf(replacement.To);
                target = string.Join("", [
                    .. target[..index],
                    replacement.From,
                    .. target[(index + replacement.To.Length)..]
                ]);
                iterations++;
                break;
            }
        }

        return iterations;
    }

    private void ParseInput(string input)
    {
        Replacements.Clear();
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

    private record Replacement(string From, string To);

    [GeneratedRegex(@"(\w+) => (\w+)")]
    private static partial Regex ReplacementDefinitionPattern();

    [GeneratedRegex(@"([A-Z][a-z]?){1,}")]
    private static partial Regex TargetMoleculePattern();
}
