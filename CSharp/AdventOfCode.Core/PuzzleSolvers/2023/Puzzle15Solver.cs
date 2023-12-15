using System.Text.RegularExpressions;

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

        var steps = input
            .Split(',')
            .Select(s =>
            {
                var label = labelRegex.Match(s).Value;
                var operation = operationRegex.Match(s).Value[0];
                var focalLengthStr = digitRegex.Match(s).Value;
                _ = int.TryParse(focalLengthStr, out int focalLength);
                return new Step(label, operation, focalLength);
            });

        foreach (var step in steps)
        {
            if (step.Operation == Operation.Add)
            {
                var box = boxes[step.BoxNumber];
                var existingLens = box.FirstOrDefault(l => l.Label == step.Label);
                if (existingLens != null)
                {
                    existingLens.FocalLength = step.FocalLength;
                }
                else
                {
                    box.Add(new Lens(step.Label, step.FocalLength));
                }
            }
            else
            {
                var relevantBoxes = boxes.Where(b => b.Any(lens => lens.Label == step.Label));
                foreach (var box in relevantBoxes)
                {
                    box.RemoveAll(l => l.Label == step.Label);
                }
            }
        }

        return boxes
            .SelectMany((box, boxIndex) => box.Select((lens, lensIndex) => (1 + boxIndex) * (lensIndex + 1) * lens.FocalLength))
            .Sum()
            .ToString();
    }

    private static int Hash(string input)
    {
        var value = 0;

        for (var i = 0; i < input.Length; i++)
        {
            char thisChar = input[i];
            value += thisChar;
            value *= 17;
            value %= 256;
        }

        return value;
    }

    private class Step(string label, char operation, int focalLength)
    {
        public string Label = label;
        public Operation Operation = operation == '=' ? Operation.Add : Operation.Remove;
        public int FocalLength = focalLength;
        public int BoxNumber = Hash(label);

        public override string ToString() => $"{Label}{Operation}{(FocalLength > 0 ? FocalLength.ToString() : string.Empty)}";
    }

    private enum Operation
    {
        Add,
        Remove
    }

    private class Lens(string label, int focalLength)
    {
        public string Label = label;
        public int FocalLength = focalLength;
    }
}
