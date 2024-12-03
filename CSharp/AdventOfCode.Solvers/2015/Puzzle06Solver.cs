using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input,
        x => 1,
        x => 0,
        x => x == 1 ? 0u : 1);

    public string SolvePartTwo(string input) => Solve(input,
        x => x + 1,
        x => x > 0 ? x - 1 : 0,
        x => x + 2);

    public static string Solve(string input,
        Func<uint, uint> onTurnOn,
        Func<uint, uint> onTurnOff,
        Func<uint, uint> onToggle)
    {
        var lights = new uint[1000, 1000];

        foreach (var instruction in input.Split('\n'))
        {
            if (instruction.Length == 0)
                continue;

            var coordinates = Digit().Matches(instruction)
                .Select(m => int.Parse(m.Value)).ToArray();
            var x1 = coordinates[0];
            var y1 = coordinates[1];
            var x2 = coordinates[2];
            var y2 = coordinates[3];

            var shouldTurnOn = instruction.StartsWith("turn on");
            var shouldTurnOff = instruction.StartsWith("turn off");

            for (var x = x1; x <= x2; x++)
            {
                for (var y = y1; y <= y2; y++)
                {
                    if (shouldTurnOn)
                    {
                        lights[x, y] = onTurnOn(lights[x, y]);
                    }
                    else if (shouldTurnOff)
                    {
                        lights[x, y] = onTurnOff(lights[x, y]);
                    }
                    else
                    {
                        lights[x, y] = onToggle(lights[x, y]);
                    }
                }
            }
        }

        uint totalBrightness = 0;
        for (var x = 0; x <= 999; x++)
        {
            for (var y = 0; y <= 999; y++)
            {
                totalBrightness += lights[x, y];
            }
        }
        return totalBrightness.ToString();
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex Digit();
}
