using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle14Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        LoadNorth(Tilt(new CharacterMatrix(input), Direction.North))
        .ToString();

    public string SolvePartTwo(string input)
    {
        var state = new CharacterMatrix(input);
        var foundStates = new Dictionary<string, int>();

        const int cycleCount = 1000000000;
        var i = 0;
        var foundState = -1;
        for (; i < cycleCount; i++)
        {
            SpinCycle(state);

            if (foundStates.TryGetValue(state.DisplayString, out foundState))
            {
                // cycle detected!
                break;
            }
            else
            {
                foundStates.Add(state.DisplayString, i);
            }
        }

        var cycleLength = i - foundState;
        var endingCycle = foundState + (cycleCount - i) % cycleLength - 1;
        var endingState = foundStates.First(s => s.Value == endingCycle);
        var endingMatrix = new CharacterMatrix(endingState.Key);
        return LoadNorth(endingMatrix).ToString();
    }

    private static CharacterMatrix Tilt(CharacterMatrix matrix, Direction direction)
    {
        bool anythingMoved;
        do
        {
            anythingMoved = TryTilt(matrix, direction);
        } while (anythingMoved);
        return matrix;
    }

    private static void SpinCycle(CharacterMatrix matrix)
    {
        Tilt(matrix, Direction.North);
        Tilt(matrix, Direction.West);
        Tilt(matrix, Direction.South);
        Tilt(matrix, Direction.East);
    }

    // Returns "true" if anything moved, so we need to iterate again.
    private static bool TryTilt(CharacterMatrix matrix, Direction direction)
    {
        var anythingMoved = false;

        if (direction == Direction.North)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                for (var y = 1; y < matrix.Height; y++)
                {
                    if (matrix.CharAt(x, y) == 'O' && matrix.CharAt(x, y - 1) == '.')
                    {
                        var indexHere = matrix.IndexAt(x, y);
                        var indexNew = matrix.IndexAt(x, y - 1);
                        matrix.SetCharacter(indexHere, '.');
                        matrix.SetCharacter(indexNew, 'O');
                        anythingMoved = true;
                    }
                }
            }
        }
        else if (direction == Direction.South)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                for (var y = matrix.Height - 2; y >= 0; y--)
                {
                    if (matrix.CharAt(x, y) == 'O' && matrix.CharAt(x, y + 1) == '.')
                    {
                        var indexHere = matrix.IndexAt(x, y);
                        var indexNew = matrix.IndexAt(x, y + 1);
                        matrix.SetCharacter(indexHere, '.');
                        matrix.SetCharacter(indexNew, 'O');
                        anythingMoved = true;
                    }
                }
            }
        }
        else if (direction == Direction.East)
        {
            for (var x = matrix.Width - 2; x >= 0; x--)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    if (matrix.CharAt(x, y) == 'O' && matrix.CharAt(x + 1, y) == '.')
                    {
                        var indexHere = matrix.IndexAt(x, y);
                        var indexNew = matrix.IndexAt(x + 1, y);
                        matrix.SetCharacter(indexHere, '.');
                        matrix.SetCharacter(indexNew, 'O');
                        anythingMoved = true;
                    }
                }
            }
        }
        else // West
        {
            for (var x = 1; x < matrix.Width; x++)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    if (matrix.CharAt(x, y) == 'O' && matrix.CharAt(x - 1, y) == '.')
                    {
                        var indexHere = matrix.IndexAt(x, y);
                        var indexNew = matrix.IndexAt(x - 1, y);
                        matrix.SetCharacter(indexHere, '.');
                        matrix.SetCharacter(indexNew, 'O');
                        anythingMoved = true;
                    }
                }
            }
        }

        return anythingMoved;
    }

    private static int LoadNorth(CharacterMatrix matrix) =>
        matrix.FindAllCharacters('O')
        .Select(rockIndex => matrix.Height - matrix.CoordinatesAt(rockIndex).Item2)
        .Sum();

    private enum Direction
    {
        East,
        South,
        West,
        North
    }
}
