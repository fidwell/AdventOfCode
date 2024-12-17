namespace AdventOfCode.Solvers;

public abstract class PuzzleSolver
{
    public bool ShouldPrint { get; set; }

    public abstract string SolvePartOne(string input);
    public abstract string SolvePartTwo(string input);
}
