using Google.OrTools.LinearSolver;

namespace AdventOfCode.Core.Matrixes;

public class MatrixExtensions
{
    public static int[] SolveSystemOfLinearEquations(int[,] coefficients, int[] solutions)
    {
        var equationCount = coefficients.GetLength(0);
        var variableCount = coefficients.GetLength(1);

        double[] solutionsAsDouble = [.. solutions.Select(s => (double)s)];

        var solver = Solver.CreateSolver("SCIP");

        var solverVariables = new Variable[variableCount];
        for (var i = 0; i < variableCount; i++)
        {
            solverVariables[i] = solver.MakeIntVar(0, int.MaxValue, $"p_{i}");
        }

        for (var solution = 0; solution < equationCount; solution++)
        {
            var constraint = solver.MakeConstraint(solutions[solution], solutions[solution], $"J_{solution}");
            for (var variable = 0; variable < variableCount; variable++)
            {
                constraint.SetCoefficient(solverVariables[variable], coefficients[solution, variable]);
            }
        }

        var objective = solver.Objective();
        for (int i = 0; i < variableCount; i++)
        {
            objective.SetCoefficient(solverVariables[i], 1);
        }
        objective.SetMinimization();

        var resultStatus = solver.Solve();
        if (resultStatus == Solver.ResultStatus.OPTIMAL)
        {
            var objectiveValue = solver.Objective().Value();
            return [.. solverVariables.Select(v => (int)v.SolutionValue())];
        }
        else
        {
            // No solution found
            return [];
        }
    }
}
