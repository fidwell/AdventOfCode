using AdventOfCode.Solvers._2024;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2024 : SolutionVerifier
{
	public SolutionVerifier2024() : base(2024) { }

	[DataTestMethod, Timeout(Timeout)]
	[DataRow(1, true, "X", DisplayName = "2024.01.1-e")]
	[DataRow(1, false, "X", DisplayName = "2024.01.1-r")]
	[DataRow(2, true, "X", DisplayName = "2024.01.2-e")]
	[DataRow(2, false, "X", DisplayName = "2024.01.2-r")]
	public void Solve_2024_01(int part, bool useExample, string expected) =>
		Solve(new Puzzle01Solver(), 1, part, useExample, expected);
}
