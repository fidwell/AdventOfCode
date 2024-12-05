using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (rules, updates) = ParseInput(input);
        return updates
            .Where(u => rules.All(r => DoesUpdateSatisfyRule(u, r)))
            .Select(MiddleValue)
            .Sum().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var (rules, updates) = ParseInput(input);
        var unsatisfiedUpdates = updates.Where(u => rules.Any(r => !DoesUpdateSatisfyRule(u, r))).ToList();
        unsatisfiedUpdates.ForEach(u => FixUpdate(u, rules));
        return unsatisfiedUpdates.Select(MiddleValue).Sum().ToString();
    }

    private static (List<(int, int)>, List<int[]>) ParseInput(string input)
    {
        var lines = input.SplitByNewline(StringSplitOptions.TrimEntries);
        var rules = new List<(int, int)>();
        var updates = new List<int[]>();

        foreach (var line in lines)
        {
            if (line.Contains('|'))
            {
                rules.Add((int.Parse(line.Substring(0, 2)), int.Parse(line.Substring(3, 2))));
            }
            else if (line.Contains(','))
            {
                updates.Add(line.Split(',').Select(int.Parse).ToArray());
            }
        }
        return (rules, updates);
    }

    private static bool DoesUpdateSatisfyRule(int[] update, (int, int) rule) =>
        !update.Any(p => p == rule.Item1) || !update.Any(p => p == rule.Item2) ||
        Array.IndexOf(update, rule.Item1) < Array.IndexOf(update, rule.Item2);

    private static void FixUpdate(int[] update, List<(int, int)> rules)
    {
        Array.Sort(update, (a, b) =>
        {
            var matchingRules = rules.Where(r => (r.Item1 == a && r.Item2 == b) || (r.Item1 == b && r.Item2 == a));
            if (!matchingRules.Any()) return 0;
            var rule = matchingRules.Single();
            if (rule.Item1 == a) return -1;
            return 1;
        });
    }

    private static int MiddleValue(int[] update) => update[update.Length / 2];
}
