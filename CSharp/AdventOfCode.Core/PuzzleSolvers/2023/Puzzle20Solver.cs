using AdventOfCode.Core.Modules;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle20Solver : IPuzzleSolver
{

    public string SolvePartOne(string input)
    {
        var system = new ModuleSystem(input);
        for (int i = 0; i < 1000; i++)
        {
            system.PushButton();
        }
        return (system.HighPulseCount * system.LowPulseCount).ToString();
    }

    public string SolvePartTwo(string input) =>
        new ModuleSystem(input).FindRx().ToString();
}
