using Coord = (int, int);

namespace AdventOfCode.Solvers._2016;

public class Puzzle13Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var number = int.Parse(input);
        var isExample = number == 10;
        var target = isExample ? (7, 4) : (31, 39);
        var path = GetPathTo(target, number, int.MaxValue);
        return (path.Count - 1).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var number = int.Parse(input);
        const int max = 50;

        var reachableSpaces = new HashSet<Coord>();

        for (var x = 0; x < max; x++)
        {
            for (var y = 0; y < max; y++)
            {
                if (!IsOpenSpace((x, y), number))
                    continue;

                if (reachableSpaces.Contains((x, y)))
                    continue;

                var path = GetPathTo((x, y), number, max);
                if (path.Count <= max + 1)
                {
                    foreach (var c in path)
                    {
                        reachableSpaces.Add((x, y));
                    }
                }
            }
        }

        return reachableSpaces.Count.ToString();
    }

    private List<Coord> GetPathTo(Coord destination, int number, int max)
    {
        var start = (1, 1);

        var queue = new PriorityQueue<List<Coord>, int>();
        queue.Enqueue([start], 0);

        while (queue.Count > 0)
        {
            var testPath = queue.Dequeue();
            var currentCoord = testPath.Last();

            if (currentCoord == destination)
            {
                return testPath;
            }

            if (testPath.Count > max)
                continue;

            var neighbors = NeighborsOf(currentCoord);
            var unvisitedNeighbors = neighbors.Where(c => !testPath.Contains(c));
            var openNeighbors = unvisitedNeighbors.Where(c => IsOpenSpace(c, number));
            foreach (var neighbor in openNeighbors)
            {
                queue.Enqueue([.. testPath, neighbor], testPath.Count + 1);
            }
        }

        return [];
    }

    private static IEnumerable<Coord> NeighborsOf(Coord c)
    {
        if (c.Item1 > 0)
            yield return (c.Item1 - 1, c.Item2);
        if (c.Item2 > 0)
            yield return (c.Item1, c.Item2 - 1);
        yield return (c.Item1 + 1, c.Item2);
        yield return (c.Item1, c.Item2 + 1);
    }

    private readonly Dictionary<Coord, bool> memoized = [];

    private bool IsOpenSpace(Coord c, int favNum)
    {
        if (memoized.TryGetValue(c, out bool result))
            return result;

        var x = c.Item1;
        var y = c.Item2;
        var z = x * x + 3 * x + 2 * x * y + y + y * y;
        z += favNum;
        var binaryRep = Convert.ToString(z, 2);
        var onesCount = binaryRep.ToCharArray().Count(c => c == '1');
        var computedAnswer = onesCount % 2 == 0;
        memoized.Add(c, computedAnswer);
        return computedAnswer;
    }
}
