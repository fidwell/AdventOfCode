using AdventOfCode.Solvers._2015;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.SolutionVerifiers;

[TestClass]
public class SolutionVerifier2015 : SolutionVerifier
{
	public SolutionVerifier2015() : base(2015) { }

	[DataTestMethod, Timeout(Timeout)]
	[DataRow(1, false, "232", DisplayName = "2015.01.1-problem")]
	[DataRow(2, false, "1783", DisplayName = "2015.01.2-problem")]
	public void Solve_2015_01(int part, bool useExample, string expected) =>
		Solve(new Puzzle01Solver(), 1, part, useExample, expected);

	[DataTestMethod, Timeout(Timeout)]
	[DataRow(1, false, "1588178", DisplayName = "2015.02.1-problem")]
	[DataRow(2, false, "3783758", DisplayName = "2015.02.2-problem")]
	public void Solve_2015_02(int part, bool useExample, string expected) =>
		Solve(new Puzzle02Solver(), 2, part, useExample, expected);

	[DataTestMethod, Timeout(Timeout)]
	[DataRow(1, false, "2565", DisplayName = "2015.03.1-problem")]
	[DataRow(2, false, "2639", DisplayName = "2015.03.2-problem")]
	public void Solve_2015_03(int part, bool useExample, string expected) =>
		Solve(new Puzzle03Solver(), 3, part, useExample, expected);
}
