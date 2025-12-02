using AdventOfCode.Core.Optimization;
using Coord = (int, int);

namespace AdventOfCode.Solvers._2016;

public class Puzzle13Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var number = int.Parse(input);
        var isExample = number == 10;
        var target = isExample ? (7, 4) : (31, 39);

        var isOpenSpace = Memoizer.Create<Coord, bool>(c => IsOpenSpace(c, number));
        var path = GetPathTo(target, int.MaxValue, isOpenSpace);
        return (path.Count - 1).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var number = int.Parse(input);
        var isOpenSpace = Memoizer.Create<Coord, bool>(c => IsOpenSpace(c, number));

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

                var path = GetPathTo((x, y), max, isOpenSpace);
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

    private static List<Coord> GetPathTo(Coord destination, int max, Func<Coord, bool> isOpenSpace)
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
            var openNeighbors = unvisitedNeighbors.Where(c => isOpenSpace(c));
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

    private static bool IsOpenSpace(Coord c, int favNum)
    {
        var x = c.Item1;
        var y = c.Item2;
        var z = x * x + 3 * x + 2 * x * y + y + y * y;
        z += favNum;
        var binaryRep = Convert.ToString(z, 2);
        var onesCount = binaryRep.ToCharArray().Count(c => c == '1');
        return onesCount % 2 == 0;
    }
}
