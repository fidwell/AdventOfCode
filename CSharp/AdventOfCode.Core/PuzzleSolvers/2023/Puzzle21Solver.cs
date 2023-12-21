using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle21Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var matrix = new CharacterMatrix(input);
        var start = matrix.FindAllCharacters('S').Single();
        var queue = new Queue<((int, int), int)>();
        var seen = new HashSet<((int, int), int)>();
        queue.Enqueue((start, 0));
        var target = matrix.Width < 20 ? 6 : 64; // 6 for sample, 64 for real

        while (queue.Count > 0)
        {
            var thisStep = queue.Dequeue();

            if (thisStep.Item2 > target)
                continue;

            seen.Add(thisStep);

            var neighbors = matrix.CoordinatesOfNeighbors(thisStep.Item1, false);
            foreach (var neighbor in neighbors)
            {
                var nextStep = (neighbor, thisStep.Item2 + 1);
                if (!seen.Contains(nextStep) &&
                    matrix.CharAt(neighbor) != '#')
                {
                    queue.Enqueue(nextStep);
                }
            }
        }

        return seen.Where(s => s.Item2 == target).Count().ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();
}
