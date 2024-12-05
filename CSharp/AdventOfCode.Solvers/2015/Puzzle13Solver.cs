﻿using System.Text.RegularExpressions;
using AdventOfCode.Core;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle13Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var feast = DefineFeast(input);
        var arrangements = feast.Names.Skip(1).ToList().AllPermutations()
            .Select(p => p.Concat([feast.Names.First()]).ToList());
        return arrangements.Max(a => TotalHappinessChange(a, feast.Rules)).ToString();
    }

    public string SolvePartTwo(string input)
    {
        const string me = "Andrew";
        var feast = DefineFeast(input);
        var arrangements = feast.Names.ToList().AllPermutations()
            .Select(p => p.Concat([me]).ToList());
        feast.Names = feast.Names.Append(me);
        return arrangements.Max(a => TotalHappinessChange(a, feast.Rules)).ToString();
    }

    private static int TotalHappinessChange(List<string> arrangement, IEnumerable<HappinessDefinition> rules)
    {
        var amount = 0;
        for (int i = 0; i < arrangement.Count; i++)
        {
            var toLeft = i - 1;
            var toRight = i + 1;

            if (toLeft < 0) toLeft = arrangement.Count - 1;
            if (toRight == arrangement.Count) toRight = 0;

            var ruleLeft = rules.FirstOrDefault(r => r.Source == arrangement[i] && r.Target == arrangement[toLeft]);
            var ruleRight = rules.FirstOrDefault(r => r.Source == arrangement[i] && r.Target == arrangement[toRight]);
            amount += ruleLeft?.Amount ?? 0;
            amount += ruleRight?.Amount ?? 0;
        }
        return amount;
    }

    private static Feast DefineFeast(string input)
    {
        var regexMatches = input.SplitByNewline().Select(l => Definition().Match(l));
        var rules = regexMatches.Select(m => new HappinessDefinition
        {
            Source = m.Groups[1].Value,
            Target = m.Groups[4].Value,
            Amount = int.Parse(m.Groups[3].Value) * (m.Groups[2].Value == "gain" ? 1 : -1)
        });
        return new Feast
        {
            Names = regexMatches.Select(m => m.Groups[1].Value).Distinct(),
            Rules = rules
        };
    }

    private record Feast
    {
        public IEnumerable<string> Names { get; set; } = [];
        public IEnumerable<HappinessDefinition> Rules { get; set; } = [];
    }

    private record HappinessDefinition
    {
        public string Source { get; set; } = "";
        public string Target { get; set; } = "";
        public int Amount { get; set; }
    }

    [GeneratedRegex(@"(\w+) would (gain|lose) (\d+) happiness units by sitting next to (\w+).")]
    private static partial Regex Definition();
}