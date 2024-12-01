namespace AdventOfCode.Solvers._2024;

public class Puzzle01Solver : IPuzzleSolver
{
	public string SolvePartOne(string input)
	{
		var (leftNums, rightNums) = ParseInput(input);
		return leftNums.Select((n, i) => Math.Abs(leftNums[i] - rightNums[i])).Sum().ToString();
	}

	public string SolvePartTwo(string input)
	{
		var (leftNums, rightNums) = ParseInput(input);
		return leftNums.Sum(l => l * rightNums.Count(r => r == l)).ToString();
	}

	private (List<int>, List<int>) ParseInput(string input)
	{
		var data = input.Split(Environment.NewLine).Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries));
		var leftNums = data.Select(line => int.Parse(line[0])).OrderBy(n => n).ToList();
		var rightNums = data.Select(line => int.Parse(line[1])).OrderBy(n => n).ToList();
		return (leftNums, rightNums);
	}
}
