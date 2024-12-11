namespace AdventOfCode.Solvers._2024;

public class Puzzle11Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, 25);
    public string SolvePartTwo(string input) => Solve(input, 75);

    private static string Solve(string input, int blinks)
    {
        var stones = input.Split([' ', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(ulong.Parse)
            .ToDictionary(val => val, _ => 1UL);
        for (var i = 0; i < blinks; i++)
        {
            var newDict = new Dictionary<ulong, ulong>();
            foreach (var stone in stones)
            {
                var results = ProcessStone(stone.Key);
                foreach (var newStone in results)
                {
                    if (!newDict.TryAdd(newStone, stone.Value))
                    {
                        newDict[newStone] += stone.Value;
                    }
                }
            }
            stones = newDict;
        }
        return stones.Select(s => (decimal)s.Value).Sum().ToString();
    }

    private static ulong[] ProcessStone(ulong stone)
    {
        if (stone == 0)
            return [1];

        var asString = stone.ToString();
        if (asString.Length % 2 == 0)
        {
            var left = ulong.Parse(asString[..(asString.Length / 2)]);
            var right = ulong.Parse(asString[(asString.Length / 2)..]);
            return [left, right];
        }

        return [stone * 2024];
    }
}
