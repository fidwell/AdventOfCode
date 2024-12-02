using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public class Puzzle01Solver : IPuzzleSolver
{
	public string SolvePartOne(string input)
	{
		var opens = Regex.Matches(input, @"\(");
		var closes = Regex.Matches(input, @"\)");
		return (opens.Count - closes.Count).ToString();
	}

	public string SolvePartTwo(string input)
	{
		var floor = 0;

		for (int i = 0; i < input.Length; i++)
		{
			floor += input[i] == '(' ? 1 : -1;
			if (floor == -1)
			{
				return (i + 1).ToString();
			}
		}

		throw new Exception("Couldn't find the answer");
	}
}
