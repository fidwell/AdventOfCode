namespace AdventOfCode.Solvers._2016;

public class Puzzle16Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input, 272);
    public override object SolvePartTwo(string input) => Solve(input, 35651584);

    public static string Solve(string input, int targetLength)
    {
        var data = input.Trim().Select(c => c == '1').ToArray();
        if (data.Length == 5)
        {
            targetLength = 20;
        }

        while (data.Length < targetLength)
        {
            data = Iterate(data);
        }

        data = data[0..targetLength];
        var checksum = Checksum(data);
        return string.Join("", checksum.Select(x => x ? '1' : '0'));
    }

    private static bool[] Iterate(bool[] input)
    {
        var b = new bool[input.Length];
        for (var x = 0; x < input.Length; x++)
        {
            b[input.Length - 1 - x] = !input[x];
        }
        return [.. input, false, .. b];
    }

    private static bool[] Checksum(bool[] input)
    {
        while (input.Length % 2 == 0)
        {
            var nextState = new bool[input.Length / 2];
            for (var i = 0; i < nextState.Length; i++)
            {
                nextState[i] = input[i * 2] == input[i * 2 + 1];
            }
            input = nextState;
        }
        return input;
    }
}
