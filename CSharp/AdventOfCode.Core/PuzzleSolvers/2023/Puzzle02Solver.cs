using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle02Solver(int part) : IPuzzleSolver
{
    private readonly Dictionary<string, int> MaxCubes = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    public string Solve(bool useSample = false)
        => (part == 1 ? Part1(useSample) : Part2(useSample)).ToString();

    private int Part1(bool useSample) =>
        DataReader.GetData(2, 0, useSample)
            .Split(Environment.NewLine)
            .Select(g => g.Substring(g.IndexOf(":") + 2))
            .Select((g, ix) => WasGroupPossible(g) ? (ix + 1) : 0)
            .Sum();

    private int Part2(bool useSample) =>
        DataReader.GetData(2, 0, useSample)
            .Split(Environment.NewLine)
            .Select(g => g.Substring(g.IndexOf(":") + 2))
            .Select(SetPower)
            .Sum();

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
        .Aggregate((x, y) => x * y);
}
