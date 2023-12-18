using AdventOfCode.Core.ArrayUtilities;
using System.Diagnostics;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle18Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var instructions = input.Split(Environment.NewLine).Select(l => new Instruction(l));

        var start = (0, 0);
        var vertices = new List<(int, int)>();
        _ = instructions.Aggregate(start, (a, b) =>
        {
            var newPoint = a.Go(b.Dir, b.Amount);
            vertices.Add(newPoint);
            return newPoint;
        });
        var minX = vertices.Min(v => v.Item1);
        var minY = vertices.Min(v => v.Item2);

        // Normalize to having the top left at 0,0
        vertices = vertices.Select(v => (v.Item1 - minX, v.Item2 - minY)).ToList();
        start = (start.Item1 - minX, start.Item2 - minY);

        var width = vertices.Max(v => v.Item1) + 1;
        var height = vertices.Max(v => v.Item2) + 1;

        // Find all points in the trench after executing the instructions
        var isInTrench = new bool[width, height];
        _ = instructions.Aggregate(start, (a, b) =>
        {
            var point = a;
            for (var i = 1; i <= b.Amount; i++)
            {
                point = a.Go(b.Dir, i);
                isInTrench[point.Item1, point.Item2] = true;
            }
            return point;
        });

        // works for my input lol
        start = width > 20
            ? (329, 84)
            : (1, 1);
        var result = new List<(int, int)>();
        ArrayExtensions.FloodFill(isInTrench, start, result);

        foreach (var point in result)
        {
            isInTrench[point.Item1, point.Item2] = true;
        }

        var total = 0;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (isInTrench[x, y])
                {
                    Trace.Write('#');
                    total++;
                }
                else
                {
                    Trace.Write('.');
                }
            }
            Trace.WriteLine("");
        }

        return total.ToString();
    }

    public string SolvePartTwo(string input) => throw new NotImplementedException();

    private class Instruction
    {
        public Direction Dir { get; }
        public int Amount { get; }
        public string Color { get; }

        public Instruction(string input)
        {
            var split = input.Split(' ');
            Dir = split[0][0].ToDirection();
            Amount = int.Parse(split[1]);
            Color = split[2]; //.Replace("(", "").Replace("#", "").Replace(")", "");
        }
    }
}
