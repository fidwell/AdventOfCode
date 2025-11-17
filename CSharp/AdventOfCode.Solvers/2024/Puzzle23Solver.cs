using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle23Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var dictionary = ConnectivityDictionary(input);
        var candidates = dictionary.Where(c => c.Key.StartsWith('t'));

        var groups = new HashSet<string>();
        foreach (var node1 in candidates)
        {
            foreach (var node2 in node1.Value)
            {
                foreach (var node3 in dictionary[node2])
                {
                    if (dictionary[node3].Contains(node1.Key))
                    {
                        groups.Add(ToString(new string[] { node1.Key, node2, node3 }.OrderBy(n => n)));
                    }
                }
            }
        }

        return groups.Count.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var dictionary = ConnectivityDictionary(input);
        BronKerbosch(dictionary, r: [], p: [.. dictionary.Keys], x: []);
        return ToString(MaximalClique);
    }

    private static Dictionary<string, List<string>> ConnectivityDictionary(string input)
    {
        var result = new Dictionary<string, List<string>>();
        var allConnections = input.SplitByNewline().Select(l => (l[..2], l.Substring(3, 2)));
        foreach (var connection in allConnections)
        {
            if (result.TryGetValue(connection.Item1, out List<string>? list1))
            {
                list1.Add(connection.Item2);
            }
            else
            {
                result.Add(connection.Item1, [connection.Item2]);
            }

            if (result.TryGetValue(connection.Item2, out List<string>? list2))
            {
                list2.Add(connection.Item1);
            }
            else
            {
                result.Add(connection.Item2, [connection.Item1]);
            }
        }
        return result;
    }

    private static List<string> MaximalClique = [];
    private static void BronKerbosch(Dictionary<string, List<string>> connectivityDictionary, List<string> r, List<string> p, List<string> x)
    {
        // https://en.wikipedia.org/wiki/Bron%E2%80%93Kerbosch_algorithm#Without_pivoting
        if (p.Count == 0 && x.Count == 0 && r.Count > MaximalClique.Count)
        {
            MaximalClique = [.. r.OrderBy(n => n)];
        }

        while (p.Count != 0)
        {
            var vertex = p[0];
            var neighborSet = connectivityDictionary[vertex];
            BronKerbosch(connectivityDictionary, [.. r, vertex], [.. p.Intersect(neighborSet)], [.. x.Intersect(neighborSet)]);
            p.Remove(vertex);
            x.Add(vertex);
        }
    }

    private static string ToString(IEnumerable<string> group) => string.Join(',', group);
}
