using System.Text.RegularExpressions;
using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle08Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        // ----- STEP 1: Create input data structure
        var (boxes, sortedDistances) = SetUp(input);
        var graph = new List<(int, int)>();

        // ----- STEP 2: Connect boxes

        // example has 20 boxes
        // input has 1000 boxes
        var connectionsToMake = boxes.Count < 100 ? 10 : 1000;

        for (var i = 0; i < connectionsToMake; i++)
        {
            var nextDistance = sortedDistances[i];
            // assuming index == id; clean up later
            graph.Add((nextDistance.Key.Item1, nextDistance.Key.Item2));
        }

        // ----- STEP 3: Find graphs
        var disconnectedGraphs = AsDisconnectedGroups(graph);
        var groupSizes = disconnectedGraphs.Select(g => g.Count);
        return groupSizes.OrderByDescending(g => g).Take(3).Aggregate(1, (a, b) => a * b).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        // ----- STEP 1: Create input data structure
        var (boxes, sortedDistances) = SetUp(input);
        var graph = new List<(int, int)>();
        var nextConnectionIndex = 0;

        var disconnectedGroups = -1;
        do
        {
            var nextDistance = sortedDistances[nextConnectionIndex];
            // assuming index == id; clean up later
            graph.Add((nextDistance.Key.Item1, nextDistance.Key.Item2));

            disconnectedGroups = AsDisconnectedGroups(graph).Count
                + boxes.Where(b => graph.All(g => g.Item1 != b.Id && g.Item2 != b.Id)).Count();
            if (disconnectedGroups == 1)
            {
                return (boxes[nextDistance.Key.Item1].Coordinate.X * boxes[nextDistance.Key.Item2].Coordinate.X).ToString();
            }

            nextConnectionIndex++;
        } while (disconnectedGroups > 1);

        return "Couldn't find a solution";
    }

    private static (List<JunctionBox>, List<KeyValuePair<(int, int), double>>) SetUp(string input)
    {
        var coordRegex = Point3dRegex();
        var boxes = input.SplitByNewline().Select((l, i) => new JunctionBox(i, l))
            //.OrderBy(c => c.X).ThenBy(c => c.Y).ThenBy(c => c.Z)
            .ToList();
        var distances = new Dictionary<(int, int), double>();

        for (var i = 0; i < boxes.Count - 1; i++)
        {
            for (var j = i + 1; j < boxes.Count; j++)
            {
                var dist = Point3d.DistanceBetween(boxes[i].Coordinate, boxes[j].Coordinate);
                distances[(i, j)] = dist;
            }
        }

        var sortedDistances = distances.OrderBy(d => d.Value).ToList();
        return (boxes, sortedDistances);
    }

    private static List<List<int>> AsDisconnectedGroups(List<(int, int)> graph)
    {
        var disconnectedGraphs = new List<List<int>>();
        foreach (var edge in graph)
        {
            var a = edge.Item1;
            var b = edge.Item2;

            var groupA = disconnectedGraphs.FirstOrDefault(g => g.Contains(a));
            var groupB = disconnectedGraphs.FirstOrDefault(g => g.Contains(b));

            if (groupA is null && groupB is null)
            {
                disconnectedGraphs.Add([a, b]);
            }
            if (groupA is not null && groupB is null)
            {
                groupA.Add(b);
            }
            else if (groupA is null && groupB is not null)
            {
                groupB.Add(a);
            }
            else if (groupA == groupB)
            {
                continue;
            }
            else if (groupA is not null && groupB is not null)
            {
                disconnectedGraphs.Remove(groupA);
                disconnectedGraphs.Remove(groupB);
                disconnectedGraphs.Add([.. groupA, .. groupB]);
            }
        }
        return disconnectedGraphs;
    }

    [GeneratedRegex(@"(\d+),(\d+),(\d+)")]
    private static partial Regex Point3dRegex();

    private class JunctionBox(int id, string input)
    {
        public readonly int Id = id;
        public readonly Point3d Coordinate = new(input);
    }
}
