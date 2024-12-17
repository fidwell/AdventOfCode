using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle19Solver : PuzzleSolver
{
    private readonly List<Replacement> Replacements = [];
    private string[] Target = [];

    public override string SolvePartOne(string input)
    {
        ParseInput(input);

        var distinctMolecules = new HashSet<string>();
        for (var i = 0; i < Target.Length; i++)
        {
            var thisMolecule = Target[i];
            var replacementsForThis = Replacements.Where(r => r.From == thisMolecule);
            foreach (var r in replacementsForThis)
            {
                var newMoleculeArray = Target.Take(i).Concat(r.To).Concat(Target.Skip(i + 1));
                distinctMolecules.Add(string.Join("", newMoleculeArray));
            }
        }
        return distinctMolecules.Count.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
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
                    var to = match.Groups[2].Captures.Select(c => c.Value).ToArray();
                    Replacements.Add(new Replacement(from, to));
                }
                else
                {
                    var targetSplit = TargetMoleculePattern().Match(lines[i]).Groups[1].Captures.Select(c => c.Value).ToArray();
                    Target = targetSplit;
                }
            }
        }
    }

    private record Replacement(string From, string[] To);

    [GeneratedRegex(@"(\w{1,2}) => ([A-Z][a-z]?){1,}")]
    private static partial Regex ReplacementDefinitionPattern();

    [GeneratedRegex(@"([A-Z][a-z]?){1,}")]
    private static partial Regex TargetMoleculePattern();
}
