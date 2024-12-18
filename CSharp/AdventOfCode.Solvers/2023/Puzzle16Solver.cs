using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle16Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        EnergizedCellsStartingAt(new CharacterMatrix(input), new Coord2d(-1, 0), Direction.Right).ToString();

    public override string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var maxSolution = 0;

        for (var x = 0; x < matrix.Width; x++)
        {
            var solutionFromTop = EnergizedCellsStartingAt(matrix, new Coord2d(x, -1), Direction.Down);
            if (solutionFromTop > maxSolution)
            {
                maxSolution = solutionFromTop;
            }

            var solutionFromBottom = EnergizedCellsStartingAt(matrix, new Coord2d(x, matrix.Height), Direction.Up);
            if (solutionFromBottom > maxSolution)
            {
                maxSolution = solutionFromBottom;
            }
        }

        for (var y = 0; y < matrix.Height; y++)
        {
            var solutionFromLeft = EnergizedCellsStartingAt(matrix, new Coord2d(-1, y), Direction.Right);
            if (solutionFromLeft > maxSolution)
            {
                maxSolution = solutionFromLeft;
            }

            var solutionFromRight = EnergizedCellsStartingAt(matrix, new Coord2d(matrix.Width, y), Direction.Left);
            if (solutionFromRight > maxSolution)
            {
                maxSolution = solutionFromRight;
            }
        }

        return maxSolution.ToString();
    }

    private class Beam(Coord2d location, Direction direction)
    {
        public Coord2d Location { get; set; } = location;
        public Direction Direction { get; set; } = direction;
        public bool ShouldDestroy { get; set; } = false;

        public override int GetHashCode() => (Location.X * 110 + Location.Y) * 5 + (int)Direction;
    }

    private static int EnergizedCellsStartingAt(CharacterMatrix matrix, Coord2d start, Direction dir0)
    {
        var cache = new List<int>();
        var beams = new List<Beam>
        {
            new(start, dir0)
        };

        while (beams.Count != 0)
        {
            var newBeams = new List<Beam>();
            foreach (var beam in beams)
            {
                beam.Location = beam.Location.Go(beam.Direction);

                var hash = beam.GetHashCode();
                if (!matrix.IsInBounds(beam.Location) ||
                    cache.Contains(hash))
                {
                    beam.ShouldDestroy = true;
                    continue;
                }

                cache.Add(hash);

                switch (matrix.CharAt(beam.Location))
                {
                    case '-':
                        if (beam.Direction == Direction.Down || beam.Direction == Direction.Up)
                        {
                            beam.Direction = Direction.Right;
                            newBeams.Add(new Beam(beam.Location, Direction.Left));
                        }
                        break;
                    case '|':
                        if (beam.Direction == Direction.Right || beam.Direction == Direction.Left)
                        {
                            beam.Direction = Direction.Up;
                            newBeams.Add(new Beam(beam.Location, Direction.Down));
                        }
                        break;
                    case '\\':
                        switch (beam.Direction)
                        {
                            case Direction.Left: beam.Direction = Direction.Up; break;
                            case Direction.Down: beam.Direction = Direction.Right; break;
                            case Direction.Right: beam.Direction = Direction.Down; break;
                            case Direction.Up: beam.Direction = Direction.Left; break;
                        }
                        break;
                    case '/':
                        switch (beam.Direction)
                        {
                            case Direction.Left: beam.Direction = Direction.Down; break;
                            case Direction.Down: beam.Direction = Direction.Left; break;
                            case Direction.Right: beam.Direction = Direction.Up; break;
                            case Direction.Up: beam.Direction = Direction.Right; break;
                        }
                        break;
                }
            }

            beams.RemoveAll(b => b.ShouldDestroy);
            beams.AddRange(newBeams);
        }

        // Reverse the hash codes to get the original coordinates back
        return cache.Select(c =>
        {
            var xy = c / 5;
            var y = xy % 110;
            var x = xy / 110;
            return (x, y);
        }).Distinct().Count();
    }
}
