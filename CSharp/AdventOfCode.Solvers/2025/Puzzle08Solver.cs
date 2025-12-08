using AdventOfCode.Core.Sets;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers.Common;

using Point = AdventOfCode.Core.MathUtilities.Point3d<long>;

namespace AdventOfCode.Solvers._2025;

public partial class Puzzle08Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var (boxes, sortedDistances) = SetUp(input);
        var disjointedSets = new DisjointSet(boxes.Count);

        // example has 20 boxes
        // input has 1000 boxes
        var connectionsToMake = boxes.Count < 100 ? 10 : 1000;
        for (var i = 0; i < connectionsToMake; i++)
        {
            var nextDistance = sortedDistances[i];
            disjointedSets.Connect(nextDistance.Item1, nextDistance.Item2);
        }

        return disjointedSets.Sets.Select(s => s.Count).OrderByDescending(c => c)
            .Take(3).Aggregate(1, (a, b) => a * b);
    }

    public override object SolvePartTwo(string input)
    {
        var (boxes, sortedDistances) = SetUp(input);
        var disjointedSets = new DisjointSet(boxes.Count);

        var nextConnectionIndex = 0;
        do
        {
            var nextDistance = sortedDistances[nextConnectionIndex];
            disjointedSets.Connect(nextDistance.Item1, nextDistance.Item2);

            if (disjointedSets.Sets.Count == 1)
                return boxes[nextDistance.Item1].X * boxes[nextDistance.Item2].X;

            nextConnectionIndex++;
        } while (disjointedSets.Sets.Count > 1);

        throw new SolutionNotFoundException();
    }

    private static (List<Point>, List<(int, int)>) SetUp(string input)
    {
        var boxes = input.SplitByNewline().Select(l => new Point(l)).ToList();
        var distances = new Dictionary<(int, int), long>();

        for (var i = 0; i < boxes.Count - 1; i++)
        {
            for (var j = i + 1; j < boxes.Count; j++)
            {
                distances[(i, j)] = Point.SquareDistanceBetween(boxes[i], boxes[j]);
            }
        }
        var sortedDistances = distances.OrderBy(d => d.Value).Select(d => d.Key).ToList();
        return (boxes, sortedDistances);
    }
}
