﻿using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle16Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        EnergizedCellsStartingAt(new CharacterMatrix(input), -1, 0, Direction.Right).ToString();

    public string SolvePartTwo(string input)
    {
        var matrix = new CharacterMatrix(input);
        var maxSolution = 0;

        for (var x = 0; x < matrix.Width; x++)
        {
            var solutionFromTop = EnergizedCellsStartingAt(matrix, x, 0, Direction.Down);
            if (solutionFromTop > maxSolution)
            {
                maxSolution = solutionFromTop;
            }

            var solutionFromBottom = EnergizedCellsStartingAt(matrix, x, matrix.Height - 1, Direction.Up);
            if (solutionFromBottom > maxSolution)
            {
                maxSolution = solutionFromBottom;
            }
        }

        for (var y = 0; y < matrix.Height; y++)
        {
            var solutionFromLeft = EnergizedCellsStartingAt(matrix, 0, y, Direction.Right);
            if (solutionFromLeft > maxSolution)
            {
                maxSolution = solutionFromLeft;
            }

            var solutionFromRight = EnergizedCellsStartingAt(matrix, matrix.Width - 1, y, Direction.Left);
            if (solutionFromRight > maxSolution)
            {
                maxSolution = solutionFromRight;
            }
        }

        return maxSolution.ToString();
    }

    private class Beam(int x, int y, Direction direction)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public Direction Direction { get; set; } = direction;
        public bool ShouldDestroy { get; set; } = false;

        public override int GetHashCode() => (X * 10000 + Y) * 4 + (int)Direction;
    }

    private static int EnergizedCellsStartingAt(CharacterMatrix matrix, int x0, int y0, Direction dir0)
    {
        var isEnergized = new bool[matrix.Width, matrix.Height];
        isEnergized.Initialize();
        var cache = new List<int>();
        var beams = new List<Beam>
        {
            new(x0, y0, dir0)
        };

        while (beams.Count != 0)
        {
            var newBeams = new List<Beam>();
            foreach (var beam in beams)
            {
                switch (beam.Direction)
                {
                    case Direction.Right:
                        beam.X++;
                        break;
                    case Direction.Down:
                        beam.Y++;
                        break;
                    case Direction.Left:
                        beam.X--;
                        break;
                    case Direction.Up:
                        beam.Y--;
                        break;
                }

                var hash = beam.GetHashCode();
                if (beam.X < 0 || beam.X >= matrix.Width ||
                    beam.Y < 0 || beam.Y >= matrix.Height ||
                    cache.Contains(hash))
                {
                    beam.ShouldDestroy = true;
                    continue;
                }

                cache.Add(hash);
                isEnergized[beam.X, beam.Y] = true;

                switch (matrix.CharAt(beam.X, beam.Y))
                {
                    case '-':
                        if (beam.Direction == Direction.Down || beam.Direction == Direction.Up)
                        {
                            beam.Direction = Direction.Right;
                            newBeams.Add(new Beam(beam.X, beam.Y, Direction.Left));
                        }
                        break;
                    case '|':
                        if (beam.Direction == Direction.Right || beam.Direction == Direction.Left)
                        {
                            beam.Direction = Direction.Up;
                            newBeams.Add(new Beam(beam.X, beam.Y, Direction.Down));
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

        var energizedCells = 0;
        for (var y = 0; y < isEnergized.GetLength(0); y += 1)
        {
            for (var x = 0; x < isEnergized.GetLength(1); x += 1)
            {
                if (isEnergized[x, y])
                {
                    energizedCells++;
                }
            }
        }
        return energizedCells;
    }
}
