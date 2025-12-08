using System.Text.RegularExpressions;
using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle08Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        // ----- STEP 1: Create input data structure

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
        var graph = new List<(int, int)>();

        // ----- STEP 2: Connect boxes

        // example has 20 boxes
        // input has 1000 boxes
        var connectionsToMake = boxes.Count < 100 ? 10 : 1000;
        var connectionsMade = 0;
        var testIndex = 0;

        while (connectionsMade < connectionsToMake)
        {
            var nextDistance = sortedDistances[testIndex];
            // assuming index == id
            var box1 = boxes[nextDistance.Key.Item1];
            var box2 = boxes[nextDistance.Key.Item2];

            graph.Add((nextDistance.Key.Item1, nextDistance.Key.Item2));
            connectionsMade++;
            testIndex++;
        }

        // ----- STEP 3: Find graphs
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

        var groupSizes = disconnectedGraphs.Select(g => g.Count);
        return groupSizes.OrderByDescending(g => g).Take(3).Aggregate(1, (a, b) => a * b).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    [GeneratedRegex(@"(\d+),(\d+),(\d+)")]
    private static partial Regex Point3dRegex();

    private class JunctionBox(int id, string input)
    {
        public readonly int Id = id;
        public readonly Point3d Coordinate = new(input);
    }
}
