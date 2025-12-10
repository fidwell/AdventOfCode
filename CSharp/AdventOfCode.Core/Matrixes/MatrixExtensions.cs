using Microsoft.Z3;

namespace AdventOfCode.Core.Matrixes;

public class MatrixExtensions
{
    public static int[] SolveSystemOfLinearEquations(int[,] coefficients, int[] solutions)
    {
        var context = new Context();

        var equationCount = coefficients.GetLength(0);
        var variableCount = coefficients.GetLength(1);

        // Create variables
        var solverVariables = new IntExpr[variableCount];
        for (var i = 0; i < variableCount; i++)
        {
            solverVariables[i] = context.MkIntConst($"p_{i}");
        }

        // Add non-negative constraints
        var optimize = context.MkOptimize();
        for (var i = 0; i < variableCount; i++)
        {
            optimize.Assert(context.MkGe(solverVariables[i], context.MkInt(0)));
        }

        for (var equation = 0; equation < equationCount; equation++)
        {
            ArithExpr sum = context.MkInt(0);
            for (var variable = 0; variable < variableCount; variable++)
            {
                sum = context.MkAdd(sum, context.MkMul(context.MkInt(coefficients[equation, variable]), solverVariables[variable]));
            }
            optimize.Assert(context.MkEq(sum, context.MkInt(solutions[equation])));
        }

        // Set objective: minimize result
        ArithExpr objective = context.MkInt(0);
        for (int i = 0; i < variableCount; i++)
        {
            objective = context.MkAdd(objective, solverVariables[i]);
        }
        optimize.MkMinimize(objective);

        // Solve
        Status status = optimize.Check();

        if (status == Status.SATISFIABLE)
            return [.. solverVariables.Select(v => ((IntNum)optimize.Model.Evaluate(v)).Int)];

        // No solution found
        return [];
    }
}
