﻿using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle13Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) =>
        GetMatrixes(useSample)
        .Select(m => ValueOf(m, 0))
        .Sum()
        .ToString();

    public string SolvePartTwo(bool useSample = false) =>
        GetMatrixes(useSample)
        .Select(m => ValueOf(m, 1))
        .Sum()
        .ToString();

    private IEnumerable<CharacterMatrix> GetMatrixes(bool useSample) =>
        DataReader.GetData(13, useSample).Split(Environment.NewLine)
        .Chunk()
        .Select(c => new CharacterMatrix(string.Join(Environment.NewLine, c)));

    private static int ValueOf(CharacterMatrix matrix, int differencesRequired)
    {
        // Find horizontal reflection, if it exists
        for (var y = 0; y < matrix.LineCount - 1; y++)
        {
            if (IsRowReflectionAt(matrix, y, differencesRequired))
            {
                return 100 * (y + 1);
            }
        }

        // Find vertical reflection, if it exists
        for (var x = 0; x < matrix.LineLength - 1; x++)
        {
            if (IsColumnReflectionAt(matrix, x, differencesRequired))
            {
                return x + 1;
            }
        }

        throw new Exception("Zero reflections found");
    }

    private static bool IsColumnReflectionAt(CharacterMatrix matrix, int firstIndex, int differencesRequired) =>
        IsReflectionAt(matrix, firstIndex, differencesRequired, (m, x) => m.ColumnAt(x), matrix.LineLength);

    private static bool IsRowReflectionAt(CharacterMatrix matrix, int firstIndex, int differencesRequired) =>
        IsReflectionAt(matrix, firstIndex, differencesRequired, (m, x) => m.RowAt(x), matrix.LineCount);

    private static bool IsReflectionAt(CharacterMatrix matrix, int startingIndex, int differencesRequired, Func<CharacterMatrix, int, string> func, int max)
    {
        var firstIndex = startingIndex;
        var secondIndex = startingIndex + 1;
        var differenceCount = 0;

        do
        {
            var lineHere = func(matrix, firstIndex);
            var lineNext = func(matrix, secondIndex);
            differenceCount += lineHere.CharacterDifferenceCount(lineNext);

            if (differenceCount > differencesRequired)
                return false;

            firstIndex--;
            secondIndex++;

        } while (firstIndex >= 0 && secondIndex < max);

        return differenceCount == differencesRequired;
    }
}