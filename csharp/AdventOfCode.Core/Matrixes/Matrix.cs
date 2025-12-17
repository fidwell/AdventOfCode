using System.Numerics;
using System.Text;

namespace AdventOfCode.Core.Matrixes;

public class Matrix<T> : IEquatable<Matrix<T>>
    where T : INumber<T>
{
    private readonly T[,] values;
    private readonly int rows, columns;

    public int Height => rows;
    public int Width => columns;

    private Matrix(T[,] input)
    {
        values = input;
        rows = input.GetLength(0);
        columns = input.GetLength(1);
    }

    public static Matrix<T> FromArray(T[,] input) => new(input);

    public static Matrix<T> Identity(int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

        var values = new T[size, size];
        for (var x = 0; x < size; x++)
        {
            values[x, x] = T.One;
        }
        return Matrix<T>.FromArray(values);
    }

    public Matrix<T> Transpose()
    {
        var newRows = columns;
        var newColumns = rows;
        var newValues = new T[newRows, newColumns];
        for (var y = 0; y < newRows; y++)
        {
            for (var x = 0; x < newColumns; x++)
            {
                newValues[y, x] = values[x, y];
            }
        }
        return Matrix<T>.FromArray(newValues);
    }

    public void SwapRows(int rowOne, int rowTwo)
    {
        if (rowOne == rowTwo)
            return;

        if (rowOne < 0 || rowOne >= rows || rowTwo < 0 || rowTwo >= rows)
            throw new IndexOutOfRangeException();

        for (var x = 0; x < columns; x++)
        {
            (values[rowOne, x], values[rowTwo, x]) = (values[rowTwo, x], values[rowOne, x]);
        }
    }

    public void ScaleRow(int row, T factor)
    {
        if (row < 0 || row >= rows)
            throw new IndexOutOfRangeException();

        for (var x = 0; x < columns; x++)
        {
            values[row, x] *= factor;
        }
    }

    public void SumWithMultipleOfRow(int targetRow, int sourceRow, T factor)
    {
        if (targetRow < 0 || targetRow >= rows ||
            sourceRow < 0 || sourceRow >= rows)
            throw new IndexOutOfRangeException();

        for (var x = 0; x < columns; x++)
        {
            values[targetRow, x] += values[sourceRow, x] * factor;
        }
    }

    public bool IsInRowEchelonForm()
    {
        var pivotsPerRow = Enumerable.Range(0, rows).Select(y =>
        {
            var nonZeroColumns = Enumerable.Range(0, columns)
                .Select((x, i) => (x, i)).Where(x => values[y, x.x] != T.Zero).Select(x => x.i);
            return nonZeroColumns.Any() ? nonZeroColumns.First() : -1;
        }).Where(p => p >= 0).ToArray();
        // Assert pivots are in ascending order
        for (var i = 0; i < pivotsPerRow.Length - 1; i++)
        {
            if (pivotsPerRow[i] > pivotsPerRow[i + 1])
                return false;
        }
        return true;
    }

    public T this[int y, int x]
    {
        get
        {
            if (y < 0 || y >= rows || x < 0 || x >= columns)
                throw new IndexOutOfRangeException();
            return values[y, x];
        }
        set
        {
            if (y < 0 || y >= rows || x < 0 || x >= columns)
                throw new IndexOutOfRangeException();
            values[y, x] = value;
        }
    }

    public bool Equals(Matrix<T>? other)
    {
        if (other is null)
            return false;

        if (rows != other.rows || columns != other.columns)
            return false;

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                if (values[y, x] != other.values[y, x])
                    return false;
            }
        }
        return true;
    }

    public string Print(int columnWidth = 7, bool withSeparator = false)
    {
        if (rows == 0 || columns == 0)
            return "[]";
        var valueWidth = columnWidth - 2;

        var sb = new StringBuilder();
        for (var y = 0; y < rows; y++)
        {
            sb.Append('[');
            for (var x = 0; x < columns; x++)
            {
                if (withSeparator && x == columns - 1)
                {
                    sb.Append('|');
                }

                var asString = values[y, x].ToString() ?? string.Empty;
                asString = asString[..Math.Min(asString.Length, valueWidth)];
                asString = $" {asString.PadLeft(valueWidth)} ";
                sb.Append(asString);
            }
            sb.AppendLine("]");
        }
        return sb.ToString().Trim();
    }

    public override bool Equals(object? obj) => Equals(obj as Matrix<T>);

    public override int GetHashCode()
    {
        var c = 0;
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                c = HashCode.Combine(c, values[y, x]);
            }
        }
        return c;
    }

    public override string ToString() => Print();
}
