namespace AdventOfCode.Core.Matrixes;

public static class GaussianEliminator
{
    public static void Reduce(Matrix<double> matrix)
    {
        // https://en.wikipedia.org/wiki/Gaussian_elimination#Pseudocode

        var pivotRow = 0;
        var pivotColumn = 0;

        while (pivotRow < matrix.Height && pivotColumn < matrix.Width)
        {
            // Find the kth pivot:
            var pivotMax = Argmax(pivotRow, matrix.Height, i => Math.Abs(matrix[i, pivotColumn]));

            if (matrix[pivotMax, pivotColumn] == 0)
            {
                // No pivot in this column, pass to next column
                pivotColumn++;
            }
            else
            {
                if (pivotRow != pivotMax)
                {
                    matrix.SwapRows(pivotRow, pivotMax);
                }

                // If we want to reduce, so our pivot is 1, scale this row
                /*
                if (matrix[pivotRow, pivotColumn] != 1)
                {
                    matrix.ScaleRow(pivotRow, 1 / matrix[pivotRow, pivotColumn]);
                }
                */

                // Do for all rows below pivot:
                for (var i = pivotRow + 1; i < matrix.Height; i++)
                {
                    var f = matrix[i, pivotColumn] / matrix[pivotRow, pivotColumn];
                    matrix.SumWithMultipleOfRow(i, pivotRow, -f);
                }

                pivotRow++;
                pivotColumn++;
            }
        }
    }

    private static int Argmax(int h, int m, Func<int, double> f)
    {
        var i_max = h;
        var val_max = f(h);
        for (var i = h + 1; i < m; i++)
        {
            var valHere = f(i);
            if (valHere > val_max)
            {
                i_max = i;
                val_max = valHere;
            }
        }
        return i_max;
    }
}
