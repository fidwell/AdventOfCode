using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var allConnections = input.SplitByNewline().Select(l => (l.Substring(0, 2), l.Substring(3, 2)));

        var groups = GroupsOfFullyConnectedComputers(allConnections);
        Console.WriteLine($"Found {groups.Count()} total groups");
        var tGroups = groups.Where(g => g.Any(n => n.StartsWith('t')));
        return tGroups.Count().ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static List<List<string>> GroupsOfFullyConnectedComputers(IEnumerable<(string, string)> allConnections)
    {
        var result = new List<List<string>>();

        // Step 1: Find groups of size 3.
        foreach (var connection1 in allConnections)
        {
            // For each connection in the bag:
            // Find all second connections with a shared node.
            // For each of the second connections:
            // See if there's a third connection that matches.
            // If so, add it to the result.

            var nodeA = connection1.Item1;
            var nodeC = connection1.Item2;

            var connection2s = allConnections.Where(c =>
                c != connection1 &&
                (c.Item1 == nodeA || c.Item2 == nodeA));

            foreach (var connection2 in connection2s)
            {
                var nodeB = connection2.Item1 == nodeA ? connection2.Item2 : connection2.Item1;
                var matches = allConnections.Where(c => c == (nodeC, nodeB) || c == (nodeB, nodeC));
                if (matches.Any())
                {
                    var group = new[] { nodeA, nodeB, nodeC }.OrderBy(n => n).ToList();
                    if (!result.Any(g => g.SequenceEqual(group)))
                    {
                        Console.WriteLine($"Found a group of 3: {ToString(group)}");
                        result.Add(group);
                    }
                }
            }
        }

        return result;
    }

    private static string ToString(IEnumerable<string> group) => $"({string.Join(',', group)})";
}
