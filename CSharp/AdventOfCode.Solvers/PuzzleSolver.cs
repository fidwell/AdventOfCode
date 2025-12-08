namespace AdventOfCode.Solvers;

public abstract class PuzzleSolver
{
    public bool ShouldPrint { get; set; }

    public abstract object SolvePartOne(string input);
    public abstract object SolvePartTwo(string input);
}
