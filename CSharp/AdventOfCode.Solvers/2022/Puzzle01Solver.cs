using AdventOfCode.Core.ArrayUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle01Solver() : IPuzzleSolver
{
	public string SolvePartOne(string input) =>
		input
			.Split(Environment.NewLine)
			.Chunk()
			.Max(g => g.Sum(i => int.Parse(i)))
			.ToString();

	public string SolvePartTwo(string input) =>
		input
			.Split(Environment.NewLine)
			.Chunk()
			.Select(g => g.Sum(i => int.Parse(i)))
			.OrderByDescending(s => s)
			.Take(3)
			.Sum()
			.ToString();
}
