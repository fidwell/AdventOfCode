namespace AdventOfCode.Solvers._2024;

public class Puzzle11Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, 25);
    public string SolvePartTwo(string input) => Solve(input, 75);

    private static string Solve(string input, int blinks)
    {
        var stones = input.Split([' ', '\n'], StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse);

        for (var i = 0; i < blinks; i++)
        {
            stones = stones.SelectMany(ProcessStone);
        }

        return stones.Count().ToString();
    }

    private static ulong[] ProcessStone(ulong stone)
    {
        if (stone == 0)
            return [1];

        var asString = stone.ToString();
        if (asString.Length % 2 == 0)
        {
            var left = ulong.Parse(asString.Substring(0, asString.Length / 2));
            var right = ulong.Parse(asString.Substring(asString.Length / 2));
            return [left, right];
        }

        return [stone * 2024];
    }
}
