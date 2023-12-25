using System.Text.RegularExpressions;
using AdventOfCode.Solvers;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public partial class Puzzle15Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        input.Split(',')
        .Select(Hash)
        .Sum()
        .ToString();

    public string SolvePartTwo(string input)
    {
        var boxes = Enumerable.Range(0, 256).Select(i => new List<Lens>()).ToArray();

        var labelRegex = new Regex(@"[a-z]+", RegexOptions.Compiled);
        var operationRegex = new Regex(@"[\-=]", RegexOptions.Compiled);
        var digitRegex = new Regex(@"\d", RegexOptions.Compiled);

        foreach (var step in input.Split(","))
        {
            var label = labelRegex.Match(step).Value;

            if (operationRegex.Match(step).Value[0] == '=')
            {
                var box = boxes[Hash(label)];
                var existingLens = box.FirstOrDefault(l => l.Label == label);
                var focalLengthStr = digitRegex.Match(step).Value;
                _ = int.TryParse(focalLengthStr, out int focalLength);
                if (existingLens != null)
                {
                    existingLens.FocalLength = focalLength;
                }
                else
                {
                    box.Add(new Lens(label, focalLength));
                }
            }
            else
            {
                var relevantBoxes = boxes.Where(b => b.Any(lens => lens.Label == label));
                foreach (var box in relevantBoxes)
                {
                    box.RemoveAll(l => l.Label == label);
                }
            }
        }

        return boxes
            .SelectMany((box, boxIndex) => box.Select((lens, lensIndex) => (1 + boxIndex) * (lensIndex + 1) * lens.FocalLength))
            .Sum()
            .ToString();
    }

    private static int Hash(string input) => input.Aggregate(0, (a, b) => (char)((a + b) * 17 % 256));

    private class Lens(string label, int focalLength)
    {
        public string Label = label;
        public int FocalLength = focalLength;
    }
}
