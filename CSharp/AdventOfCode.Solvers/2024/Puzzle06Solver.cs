using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var map = new CharacterMatrix(input);
        var coord = map.SingleMatch('^');
        var direction = Direction.Up;
        HashSet<(int, int)> visitedLocations = [coord];

        while (coord.Item1 >= 0 &&
            coord.Item2 >= 0 &&
            coord.Item1 < map.Width &&
            coord.Item2 < map.Height)
        {
            var target = direction switch
            {
                Direction.Right => (coord.Item1 + 1, coord.Item2),
                Direction.Down => (coord.Item1, coord.Item2 + 1),
                Direction.Left => (coord.Item1 - 1, coord.Item2),
                Direction.Up => (coord.Item1, coord.Item2 - 1),
                _ => throw new Exception("Invalid direction")
            };

            if (map.CharAt(target) == '#')
            {
                direction = direction.RotateRight();
            }
            else
            {
                coord = target;
                visitedLocations.Add(coord);
            }
        }

        return (visitedLocations.Count - 1).ToString();
    }

    public string SolvePartTwo(string input)
    {
        // try brute force lol
        // it doesn't work anyway

        var map = new CharacterMatrix(input);
        var start = map.SingleMatch('^');
        var direction = Direction.Up;

        var invalidChars = new char[] { '^', '#' };
        var answers = 0;

        foreach (var possibleLocation in map.AllCoordinates)
        {
            if (invalidChars.Contains(map.CharAt(possibleLocation)))
                continue;

            var coord = start;
            var done = false;
            var steps = 0;

            while (coord.Item1 >= 0 &&
                coord.Item2 >= 0 &&
                coord.Item1 < map.Width &&
                coord.Item2 < map.Height)
            {
                steps++;
                var target = direction switch
                {
                    Direction.Right => (coord.Item1 + 1, coord.Item2),
                    Direction.Down => (coord.Item1, coord.Item2 + 1),
                    Direction.Left => (coord.Item1 - 1, coord.Item2),
                    Direction.Up => (coord.Item1, coord.Item2 - 1),
                    _ => throw new Exception("Invalid direction")
                };

                if (map.CharAt(target) == '#' || target == possibleLocation)
                {
                    direction = direction.RotateRight();
                }
                else
                {
                    coord = target;
                }

                if (coord == start && direction == Direction.Up)
                {
                    // we're in a loop!
                    answers++;
                    done = true;
                    break;
                }

                if (steps > map.Width * map.Height)
                {
                    done = true;
                    break;
                }
            }

            if (done)
                continue;
        }

        return answers.ToString();
    }
}
