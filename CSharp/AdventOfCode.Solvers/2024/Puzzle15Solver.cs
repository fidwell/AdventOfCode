﻿using AdventOfCode.Core.IntSpace;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle15Solver : IPuzzleSolver
{
    // Disabled for benchmarking and performance.
    // Toggle on if you want to see the output!
    const bool ShouldPrint = false;

    public string SolvePartOne(string input) => Solve(input, false);
    public string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool isPartTwo)
    {
        var warehouse = new Warehouse(input, isPartTwo);
        while (!warehouse.IsDone)
        {
            warehouse.Step();

            if (ShouldPrint)
            {
                warehouse.Print();
            }
        }
        return warehouse.BoxLocationSum.ToString();
    }

    private class Warehouse
    {
        public Coord2d Robot { get; private set; }
        public List<Coord2d> Boxes { get; } = [];
        public List<Coord2d> Walls { get; } = [];
        public string RobotInstructions { get; private set; } = string.Empty;
        public bool AreBoxesWide { get; }
        private readonly int _size;

        private int _step = 0;
        public bool IsDone => _step >= RobotInstructions.Length;

        public Warehouse(string input, bool isWide)
        {
            AreBoxesWide = isWide;

            var y = 0;
            foreach (var line in input.SplitByNewline())
            {
                if (line.StartsWith('#'))
                {
                    for (var x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '@')
                            Robot = new(AreBoxesWide ? x * 2 : x, y);
                        else if (line[x] == 'O')
                            Boxes.Add(new(AreBoxesWide ? x * 2 : x, y));
                        else if (line[x] == '#')
                        {
                            if (AreBoxesWide)
                            {
                                Walls.Add(new(x * 2, y));
                                Walls.Add(new(x * 2 + 1, y));
                            }
                            else
                            {
                                Walls.Add(new(x, y));
                            }
                        }
                    }

                    y++;
                }
                else
                {
                    RobotInstructions = $"{RobotInstructions}{line}";
                }
            }
            _size = y;
        }

        public void Step()
        {
            var direction = RobotInstructions[_step].ToDirection();
            var robotTarget = Robot.Go(direction);
            var boxTree = CreateDependencyTree(robotTarget, direction);

            if (boxTree is null && !Walls.Any(w => w == robotTarget))
            {
                // no boxes; move at will
                Robot = robotTarget;
            }
            else if (boxTree?.CanMove(Walls, direction, AreBoxesWide) ?? false)
            {
                boxTree.Move(Boxes, direction);
                Robot = robotTarget;
            }

            _step++;
        }

        private BoxDependency? CreateDependencyTree(Coord2d target, Direction direction)
        {
            var firstBox = Boxes.Where(b => b == target || (AreBoxesWide && b == target.Go(Direction.Left)));
            if (!firstBox.Any())
            {
                return null;
            }

            return IterateDependency(firstBox.Single(), direction);
        }

        private BoxDependency IterateDependency(Coord2d box, Direction direction) =>
            new(box, (direction == Direction.Down || direction == Direction.Up
                ? AreBoxesWide
                    ? new List<Coord2d>
                    {
                        box.Go(direction).Go(Direction.Left),
                        box.Go(direction),
                        box.Go(direction).Go(Direction.Right)
                    }.Where(Boxes.Contains)
                    : Boxes.Where(b => b == box.Go(direction))
                : Boxes.Where(b => b == box.Go(direction, AreBoxesWide ? 2 : 1)))
                .Select(b => IterateDependency(b, direction)));

        public int BoxLocationSum => Boxes.Sum(b => b.X + 100 * b.Y);

        private class BoxDependency(Coord2d root, IEnumerable<BoxDependency> branches)
        {
            public Coord2d Root { get; } = root;
            public List<BoxDependency> Branches { get; } = branches.ToList();

            public bool CanMove(List<Coord2d> walls, Direction direction, bool areBoxesWide) =>
                !walls.Any(w =>
                    w == Root.Go(direction) ||
                    (areBoxesWide && w == Root.Go(Direction.Right).Go(direction))) &&
                Branches.All(b => b.CanMove(walls, direction, areBoxesWide));

            public void Move(List<Coord2d> boxes, Direction direction)
            {
                // if another boxed pushed this one, there
                // was an overlapping tree structure, but
                // that's okay
                if (boxes.Any(b => b == Root))
                {
                    foreach (var b in Branches)
                    {
                        b.Move(boxes, direction);
                    }
                    boxes.RemoveAll(b => b == Root);
                    boxes.Add(Root.Go(direction));
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(_step);
            if (_step > 0)
                Console.WriteLine(RobotInstructions[_step - 1]);
            for (var y = 0; y < _size; y++)
            {
                for (var x = 0; x < _size * (AreBoxesWide ? 2 : 1); x++)
                {
                    if (Walls.Contains(new(x, y)))
                        Console.Write('#');
                    else if (Boxes.Contains(new(x, y)))
                    {
                        if (AreBoxesWide)
                        {
                            Console.Write("[]");
                            x++;
                        }
                        else
                        {
                            Console.Write('O');
                        }
                    }
                    else if (Robot == (x, y))
                        Console.Write('@');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
