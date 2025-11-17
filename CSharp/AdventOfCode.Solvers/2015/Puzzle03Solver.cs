namespace AdventOfCode.Solvers._2015;

public class Puzzle03Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Deliver(input).Count.ToString();

    public override string SolvePartTwo(string input)
    {
        var santaDirs = input.Where((c, i) => i % 2 == 0).ToArray();
        var robotDirs = input.Where((c, i) => i % 2 == 1).ToArray();
        var santaResult = Deliver(new string(santaDirs));
        var robotResult = Deliver(new string(robotDirs));
        return santaResult.Union(robotResult).Count().ToString();
    }

    private static HashSet<(int, int)> Deliver(string input)
    {
        var currX = 0;
        var currY = 0;
        var visited = new HashSet<(int, int)>();

        foreach (var direction in input)
        {
            switch (direction)
            {
                case '^':
                    currY--;
                    break;
                case 'v':
                    currY++;
                    break;
                case '<':
                    currX--;
                    break;
                case '>':
                    currX++;
                    break;
            }

            if (!visited.Contains((currX, currY)))
            {
                visited.Add((currX, currY));
            }
        }

        return visited;
    }
}
