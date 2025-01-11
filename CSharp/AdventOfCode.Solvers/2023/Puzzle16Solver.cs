using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle16Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        EnergizedCellsStartingAt(new CharacterMatrix(input), (-1, 0), Direction.Right).ToString();

    public override string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var maxSolution = 0;

        for (var x = 0; x < matrix.Width; x++)
        {
            var solutionFromTop = EnergizedCellsStartingAt(matrix, (x, -1), Direction.Down);
            if (solutionFromTop > maxSolution)
            {
                maxSolution = solutionFromTop;
            }

            var solutionFromBottom = EnergizedCellsStartingAt(matrix, (x, matrix.Height), Direction.Up);
            if (solutionFromBottom > maxSolution)
            {
                maxSolution = solutionFromBottom;
            }
        }

        for (var y = 0; y < matrix.Height; y++)
        {
            var solutionFromLeft = EnergizedCellsStartingAt(matrix, (-1, y), Direction.Right);
            if (solutionFromLeft > maxSolution)
            {
                maxSolution = solutionFromLeft;
            }

            var solutionFromRight = EnergizedCellsStartingAt(matrix, (matrix.Width, y), Direction.Left);
            if (solutionFromRight > maxSolution)
            {
                maxSolution = solutionFromRight;
            }
        }

        return maxSolution.ToString();
    }

    private class Beam(Pose pose)
    {
        public Pose Pose { get; set; } = pose;
        public bool ShouldDestroy { get; set; } = false;

        public override int GetHashCode() => (Pose.Location.Item1 * 110 + Pose.Location.Item2) * 8 + (int)Pose.Direction;
    }

    private static int EnergizedCellsStartingAt(CharacterMatrix matrix, (int, int) start, Direction dir0)
    {
        var cache = new List<int>();
        var beams = new List<Beam>
        {
            new(new(start, dir0))
        };

        while (beams.Count != 0)
        {
            var newBeams = new List<Beam>();
            foreach (var beam in beams)
            {
                beam.Pose = beam.Pose.Forward();

                var hash = beam.GetHashCode();
                if (!matrix.IsInBounds(beam.Pose.Location) ||
                    cache.Contains(hash))
                {
                    beam.ShouldDestroy = true;
                    continue;
                }

                cache.Add(hash);

                switch (matrix.CharAt(beam.Pose.Location))
                {
                    case '-':
                        if (beam.Pose.Direction == Direction.Down || beam.Pose.Direction == Direction.Up)
                        {
                            beam.Pose = beam.Pose.Facing(Direction.Right);
                            newBeams.Add(new Beam(new Pose(beam.Pose.Location, Direction.Left)));
                        }
                        break;
                    case '|':
                        if (beam.Pose.Direction == Direction.Right || beam.Pose.Direction == Direction.Left)
                        {
                            beam.Pose = beam.Pose.Facing(Direction.Up);
                            newBeams.Add(new Beam(new Pose(beam.Pose.Location, Direction.Down)));
                        }
                        break;
                    case '\\':
                        switch (beam.Pose.Direction)
                        {
                            case Direction.Left: beam.Pose = beam.Pose.Facing(Direction.Up); break;
                            case Direction.Down: beam.Pose = beam.Pose.Facing(Direction.Right); break;
                            case Direction.Right: beam.Pose = beam.Pose.Facing(Direction.Down); break;
                            case Direction.Up: beam.Pose = beam.Pose.Facing(Direction.Left); break;
                        }
                        break;
                    case '/':
                        switch (beam.Pose.Direction)
                        {
                            case Direction.Left: beam.Pose = beam.Pose.Facing(Direction.Down); break;
                            case Direction.Down: beam.Pose = beam.Pose.Facing(Direction.Left); break;
                            case Direction.Right: beam.Pose = beam.Pose.Facing(Direction.Up); break;
                            case Direction.Up: beam.Pose = beam.Pose.Facing(Direction.Right); break;
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
            var xy = c / 8;
            var y = xy % 110;
            var x = xy / 110;
            return (x, y);
        }).Distinct().Count();
    }
}
