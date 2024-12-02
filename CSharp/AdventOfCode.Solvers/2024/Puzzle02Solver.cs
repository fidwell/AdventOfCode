namespace AdventOfCode.Solvers._2024;

// needs severe refactoring!

public class Puzzle02Solver : IPuzzleSolver
{
	public string SolvePartOne(string input)
	{
		var count = 0;
		foreach (var report in input.Split(Environment.NewLine))
		{
			var levels = report.Split(' ').Select(int.Parse).ToArray();
			if (IsSafe(levels))
				count++;
		}
		return count.ToString();
	}

	public string SolvePartTwo(string input)
	{
		var count = 0;
		foreach (var report in input.Split(Environment.NewLine))
		{
			var levels = report.Split(' ').Select(int.Parse).ToArray();

			if (IsSafe(levels) || IsSafeIfDampened(levels))
			{
				count++;
			}
		}
		return count.ToString();
	}

	private bool IsSafe(int[] levels)
	{
		var sorted = levels.OrderBy(a => a).ToArray();
		var reversed = sorted.Reverse().ToArray();
		if (Enumerable.SequenceEqual(levels, sorted) || Enumerable.SequenceEqual(levels, reversed))
		{
			var withNextElement = levels.Zip(levels.Skip(1), Tuple.Create);
			var isSafe = withNextElement.All(x =>
			{
				var diff = Math.Abs(x.Item1 - x.Item2);
				return diff <= 3 && diff >= 1;
			});
			if (isSafe)
				return true;
		}
		return false;
	}

	private bool IsSafeIfDampened(int[] levels)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			var copy = new int[levels.Length];
			Array.Copy(levels, 0, copy, 0, levels.Length);
			var x = copy?.ToList();
			x.RemoveAt(i);
			copy = x.ToArray();
			if (IsSafe(copy))
			{
				return true;
			}
		}

		return false;
	}
}
