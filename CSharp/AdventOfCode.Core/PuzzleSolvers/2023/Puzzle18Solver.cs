using AdventOfCode.Core.ArrayUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle18Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        Solve(input.Split(Environment.NewLine).Select(l => new Instruction(l, true))).ToString();

    public string SolvePartTwo(string input) =>
        Solve(input.Split(Environment.NewLine).Select(l => new Instruction(l, false)).ToList()).ToString();

    private static long Solve(IEnumerable<Instruction> instructions)
    {
        var vertices = FindVertices(instructions);
        var shoelace = Shoelace(vertices);
        var boundaryCount = instructions.Sum(i => i.Amount);
        return shoelace + boundaryCount / 2 + 1;
    }

    private static List<(long, long)> FindVertices(IEnumerable<Instruction> instructions)
    {
        var start = (0, 0);
        var vertices = new List<(long, long)>();
        _ = instructions.Aggregate(start, (a, b) =>
        {
            var newPoint = a.Go(b.Dir, b.Amount);
            vertices.Add(newPoint);
            return newPoint;
        });
        return vertices;
    }

    private static long Shoelace(IList<(long, long)> vertexes)
    {
        long result = 0;
        for (var i = 0; i < vertexes.Count; i++)
        {
            var thisOne = vertexes[i];
            var nextOne = vertexes[i == vertexes.Count - 1 ? 0 : i + 1];
            result += Determinant(thisOne.Item1, thisOne.Item2, nextOne.Item1, nextOne.Item2);
        }
        return result / 2;
    }

    private static long Determinant(long x1, long y1, long x2, long y2) => x1 * y2 - x2 * y1;

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
