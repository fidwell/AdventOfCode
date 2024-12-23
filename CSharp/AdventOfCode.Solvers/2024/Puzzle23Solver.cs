﻿using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var allConnections = input.SplitByNewline().Select(l => (l.Substring(0, 2), l.Substring(3, 2)));

        var groups = GroupsOfFullyConnectedComputers(allConnections, 3);
        Console.WriteLine($"Found {groups.Count()} total groups");
        var tGroups = groups.Where(g => g.Any(n => n.StartsWith('t')));
        return tGroups.Count().ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var allConnections = input.SplitByNewline().Select(l => (l.Substring(0, 2), l.Substring(3, 2)));

        var groups = GroupsOfFullyConnectedComputers(allConnections, int.MaxValue);
        Console.WriteLine($"Found {groups.Count()} total groups");
        var firstGroup = groups.First();
        return ToString(firstGroup);
    }

    private static List<List<string>> GroupsOfFullyConnectedComputers(IEnumerable<(string, string)> allConnections, int maxSize)
    {
        var groupsOfThree = new List<List<string>>();

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
                    if (!groupsOfThree.Any(g => g.SequenceEqual(group)))
                    {
                        //Console.WriteLine($"Found a group of 3: {ToString(group)}");
                        groupsOfThree.Add(group);
                    }
                }
            }
        }

        if (maxSize == 3)
            return groupsOfThree;

        // Step 2: Find groups of size 4. (To generalize)
        var groupsOfFour = new List<List<string>>();
        foreach (var groupOfThree in groupsOfThree)
        {
            // For each group of three:
            // For each node n0:
            // Find a connection in the bag that has n0, but isn't in the group.
            // Its other end is n1.
            // See if connections in the bag exist between n1 and all other nodes in the group.
            // If so, add it to the result.
            foreach (var n0 in groupOfThree)
            {
                var potentialConnections = allConnections.Where(c =>
                    (c.Item1 == n0 && !groupOfThree.Contains(c.Item2)) ||
                    (c.Item2 == n0 && !groupOfThree.Contains(c.Item1)));
                foreach (var potentialConnection in potentialConnections)
                {
                    var n1 = potentialConnection.Item1 == n0 ? potentialConnection.Item2 : potentialConnection.Item1;
                    var otherNodesInGroup = groupOfThree.Where(n => n != n0);
                    var groupExists = true;
                    foreach (var otherNode in otherNodesInGroup)
                    {
                        if (!allConnections.Any(c =>
                            c.Item1 == n1 && c.Item2 == otherNode ||
                            c.Item2 == n1 && c.Item1 == otherNode))
                        {
                            groupExists = false;
                            break;
                        }
                    }

                    if (groupExists)
                    {
                        var newGroup = groupOfThree.Concat([n1]).OrderBy(n => n).ToList();
                        if (!groupsOfFour.Any(g => g.SequenceEqual(newGroup)))
                        {
                            Console.WriteLine($"Found a group of 4: {ToString(newGroup)}");
                            groupsOfFour.Add(newGroup);
                        }
                    }
                }
            }
        }

        return groupsOfFour;
    }

    private static string ToString(IEnumerable<string> group) => $"({string.Join(',', group)})";
}
