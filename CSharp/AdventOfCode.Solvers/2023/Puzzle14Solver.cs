using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle14Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        LoadNorth(Tilt(new CharacterMatrix(input), Direction.North))
        .ToString();

    public override string SolvePartTwo(string input)
    {
        var state = new CharacterMatrix(input);
        var foundStates = new Dictionary<string, int>();
        const int cycleCount = 1_000_000_000;
        var i = 0;
        var foundState = -1;
        for (; i < cycleCount; i++)
        {
            SpinCycle(state);
            var asString = state.DisplayString;

            if (foundStates.TryGetValue(asString, out foundState))
            {
                // cycle detected!
                break;
            }
            else
            {
                foundStates.Add(asString, i);
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
            anythingMoved = false;
            foreach (var rock in matrix.FindAllCharacters('O'))
            {
                var candidatePosition = rock.Go(direction);
                if (matrix.CharAt(candidatePosition) == '.')
                {
                    matrix.SetCharacter(rock, '.');
                    matrix.SetCharacter(candidatePosition, 'O');
                    anythingMoved = true;
                }
            }
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

    private static int LoadNorth(CharacterMatrix matrix) =>
        matrix.FindAllCharacters('O')
        .Select(coord => matrix.Height - coord.Item2)
        .Sum();
}
