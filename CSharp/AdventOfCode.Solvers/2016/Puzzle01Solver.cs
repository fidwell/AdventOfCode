using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle01Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var pose = new Pose((0, 0), Direction.Up);

        foreach (var step in input.Split(", ", StringSplitOptions.RemoveEmptyEntries))
        {
            if (step.StartsWith('R'))
                pose = pose.TurnRight();
            else
                pose = pose.TurnLeft();
            var amount = int.Parse(Regexes.NonNegativeInteger().Match(step).Value);
            pose.Location = pose.Location.Go(pose.Direction, amount);
        }

        return pose.Location.ManhattanDistance((0, 0)).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var pose = new Pose((0, 0), Direction.Up);
        var visited = new HashSet<(int, int)>();

        foreach (var step in input.Split(", ", StringSplitOptions.RemoveEmptyEntries))
        {
            if (step.StartsWith('R'))
                pose = pose.TurnRight();
            else
                pose = pose.TurnLeft();
            var amount = int.Parse(Regexes.NonNegativeInteger().Match(step).Value);

            // Enumerate coordinates we will pass
            foreach (var i in Enumerable.Range(0, amount))
            {
                var subLocation = pose.Location.Go(pose.Direction, i);
                if (visited.Contains(subLocation))
                {
                    return subLocation.ManhattanDistance((0, 0)).ToString();
                }
                visited.Add(subLocation);
            }

            pose.Location = pose.Location.Go(pose.Direction, amount);
        }

        return "Could not solve";
    }
}
