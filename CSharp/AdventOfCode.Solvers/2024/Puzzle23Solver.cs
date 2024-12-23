using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var allConnections = input.SplitByNewline().Select(l => (l.Substring(0, 2), l.Substring(3, 2)));
        var uniqueComputerNames = UniqueComputersInConnectionList(allConnections);

        var groups = new HashSet<(string, string, string)>();

        foreach (var computer1 in uniqueComputerNames)
        {
            var thisComputerConnections = allConnections.Where(c => c.Item1 == computer1 || c.Item2 == computer1);
            var uniqueComputersConnectedToComputer1 = UniqueComputersInConnectionList(thisComputerConnections).Where(c => c != computer1);
            foreach (var computer2 in uniqueComputersConnectedToComputer1)
            {
                var secondComputerConnections = allConnections.Where(c => c.Item1 == computer2 || c.Item2 == computer2);
                var uniqueComputersConnectedToComputer2 = UniqueComputersInConnectionList(secondComputerConnections).Where(c => c != computer2);

                foreach (var computer3 in uniqueComputersConnectedToComputer2)
                {
                    if (allConnections.Any(c =>
                        c.Item1 == computer1 && c.Item2 == computer3 ||
                        c.Item1 == computer3 && c.Item2 == computer1))
                    {
                        if (computer1.StartsWith('t') ||
                            computer2.StartsWith('t') ||
                            computer3.StartsWith('t'))
                        {
                            var comps = new string[] { computer1, computer2, computer3 }.OrderBy(c => c).ToArray();
                            groups.Add((comps[0], comps[1], comps[2]));
                        }
                    }
                }
            }
        }

        return groups.Count.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<string> UniqueComputersInConnectionList(IEnumerable<(string, string)> connections) =>
        connections.Select(c => c.Item1).Concat(connections.Select(c => c.Item2)).Distinct();
}
