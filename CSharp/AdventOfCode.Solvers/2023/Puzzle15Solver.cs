using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public partial class Puzzle15Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input.Split(',')
        .Select(Hash)
        .Sum()
        .ToString();

    public override string SolvePartTwo(string input)
    {
        var boxes = Enumerable.Range(0, 256).Select(i => new List<Lens>()).ToArray();

        foreach (var step in input.Split(","))
        {
            var label = Label().Match(step).Value;

            if (Operation().Match(step).Value[0] == '=')
            {
                var box = boxes[Hash(label)];
                var existingLens = box.FirstOrDefault(l => l.Label == label);
                var focalLengthStr = Regexes.Digit().Match(step).Value;
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

    [GeneratedRegex(@"[a-z]+", RegexOptions.Compiled)]
    private static partial Regex Label();

    [GeneratedRegex(@"[\-=]", RegexOptions.Compiled)]
    private static partial Regex Operation();
}
