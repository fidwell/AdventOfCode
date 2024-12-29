using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle02Solver() : PuzzleSolver
{
    private readonly Dictionary<string, int> MaxCubes = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    public override string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Select(g => g.Substring(g.IndexOf(":") + 2))
            .Select((g, ix) => WasGroupPossible(g) ? (ix + 1) : 0)
            .Sum()
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline()
            .Select(g => g.Substring(g.IndexOf(":") + 2))
            .Select(SetPower)
            .Sum()
            .ToString();

    private bool WasGroupPossible(string gameData) => gameData.Split("; ").All(WasGamePossible);

    private bool WasGamePossible(string gameData) => gameData
        .Split(", ")
        .All(cubeData =>
        {
            var d = cubeData.Split(" ");
            return int.Parse(d[0]) <= MaxCubes[d[1]];
        });

    private int SetPower(string gameData) => gameData
        .Split([',', ';']).Select(s => s.Trim())
        .GroupBy(v => v.Split(" ")[1])
        .Select(g => g.Select(x => int.Parse(x.Split(" ")[0])).Max())
        .Product();
}
