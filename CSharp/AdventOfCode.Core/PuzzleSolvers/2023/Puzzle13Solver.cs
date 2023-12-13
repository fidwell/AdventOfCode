using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle13Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) =>
        DataReader.GetData(13, useSample).Split(Environment.NewLine)
        .Chunk()
        .Select(c => new CharacterMatrix(string.Join(Environment.NewLine, c)))
        .Select(ValueOf)
        .Sum()
        .ToString();

    public string SolvePartTwo(bool useSample = false)
    {
        throw new NotImplementedException();
    }

    private int ValueOf(CharacterMatrix matrix)
    {
        // Find horizontal reflection, if it exists
        for (var y = 0; y < matrix.LineCount - 1; y++)
        {
            if (IsRowReflectionAt(matrix, y))
            {
                return 100 * (y + 1);
            }
        }

        // Find vertical reflection, if it exists
        for (var x = 0; x < matrix.LineLength - 1; x++)
        {
            if (IsColumnReflectionAt(matrix, x))
            {
                return x + 1;
            }
        }

        return 0;
    }

    private static bool IsColumnReflectionAt(CharacterMatrix matrix, int firstColumnIndex)
    {
        var leftColumnIndex = firstColumnIndex;
        var rightColumnIndex = firstColumnIndex + 1;

        do
        {
            var lineHere = matrix.ColumnAt(leftColumnIndex);
            var lineNext = matrix.ColumnAt(rightColumnIndex);

            if (lineHere != lineNext)
                return false;

            leftColumnIndex--;
            rightColumnIndex++;

        } while (leftColumnIndex >= 0 && rightColumnIndex < matrix.LineLength);

        return true;
    }

    private static bool IsRowReflectionAt(CharacterMatrix matrix, int firstRowIndex)
    {
        var topRowIndex = firstRowIndex;
        var bottomRowIndex = firstRowIndex + 1;

        do
        {
            var lineHere = matrix.RowAt(topRowIndex);
            var lineNext = matrix.RowAt(bottomRowIndex);

            if (lineHere != lineNext)
                return false;

            topRowIndex--;
            bottomRowIndex++;

        } while (topRowIndex >= 0 && bottomRowIndex < matrix.LineCount);

        return true;
    }
}
