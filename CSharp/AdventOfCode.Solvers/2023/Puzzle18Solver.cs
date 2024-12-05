using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle18Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        Solve(input.SplitByNewline().Select(l => new Instruction(l, true))).ToString();

    public string SolvePartTwo(string input) =>
        Solve(input.SplitByNewline().Select(l => new Instruction(l, false)).ToList()).ToString();

    private static long Solve(IEnumerable<Instruction> instructions) =>
        Shoelace(FindVertices(instructions)) + instructions.Sum(i => i.Amount) / 2 + 1;

    private static List<(long, long)> FindVertices(IEnumerable<Instruction> instructions)
    {
        var vertices = new List<(long, long)>();
        _ = instructions.Aggregate((0, 0), (a, b) =>
        {
            var newPoint = a.Go(b.Dir, b.Amount);
            vertices.Add(newPoint);
            return newPoint;
        });
        return vertices;
    }

    private static long Shoelace(List<(long, long)> vertexes) =>
        Enumerable.Range(0, vertexes.Count)
            .Select(i => Determinant(vertexes[i], vertexes[i == vertexes.Count - 1 ? 0 : i + 1])).Sum() / 2;

    private static long Determinant((long, long) p1, (long, long) p2) => p1.Item1 * p2.Item2 - p2.Item1 * p1.Item2;

    private class Instruction
    {
        public Direction Dir { get; }
        public int Amount { get; }

        public Instruction(string input, bool isPartOne)
        {
            var split = input.Split(' ');
            if (isPartOne)
            {
                Dir = split[0][0].ToDirection();
                Amount = int.Parse(split[1]);
            }
            else
            {
                var colorString = split[2];
                Amount = Convert.ToInt32($"0x{colorString.Substring(2, 5)}", 16);
                Dir = colorString.Substring(7, 1)[0] switch
                {
                    '0' => Direction.Right,
                    '1' => Direction.Down,
                    '2' => Direction.Left,
                    '3' => Direction.Up,
                    _ => Direction.Undefined
                };
            }
        }
    }
}
