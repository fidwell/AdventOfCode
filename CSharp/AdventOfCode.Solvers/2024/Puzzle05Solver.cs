using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var (rules, updates) = ParseInput(input);
        return updates
            .Where(u => rules.All(r => DoesUpdateSatisfyRule(u, r)))
            .Select(u => u[u.Length / 2])
            .Sum().ToString();
    }

    public string SolvePartTwo(string input)
    {
        var (rules, updates) = ParseInput(input);
        return updates.Where(u => rules.Any(r => !DoesUpdateSatisfyRule(u, r)))
            .Sum(u => FindMiddle(u, rules)).ToString();
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

    private static int FindMiddle(int[] update, List<(int, int)> rules)
    {
        for (var i = 0; i < update.Length; i++)
        {
            var item = update[i];
            var countBefore = rules.Count(r => r.Item1 == item && update.Contains(r.Item2));
            var countAfter = rules.Count(r => r.Item2 == item && update.Contains(r.Item1));
            if (countBefore == countAfter)
                return item;
        }
        throw new Exception("Couldn't find the middle value.");
    }
}
