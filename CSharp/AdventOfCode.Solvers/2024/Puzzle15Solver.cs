using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle15Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    private string Solve(string input, bool isPartTwo)
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
        public (int, int) Robot { get; private set; }
        public List<(int, int)> Boxes { get; } = [];
        public List<(int, int)> Walls { get; } = [];
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
                            Robot = (AreBoxesWide ? x * 2 : x, y);
                        else if (line[x] == 'O')
                            Boxes.Add((AreBoxesWide ? x * 2 : x, y));
                        else if (line[x] == '#')
                        {
                            if (AreBoxesWide)
                            {
                                Walls.Add((x * 2, y));
                                Walls.Add((x * 2 + 1, y));
                            }
                            else
                            {
                                Walls.Add((x, y));
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

        private BoxDependency? CreateDependencyTree((int, int) target, Direction direction)
        {
            var firstBox = Boxes.Where(b => b == target || (AreBoxesWide && b == target.Go(Direction.Left)));
            if (!firstBox.Any())
            {
                return null;
            }

            return IterateDependency(firstBox.Single(), direction);
        }

        private BoxDependency IterateDependency((int, int) box, Direction direction) =>
            new(box, (direction == Direction.Down || direction == Direction.Up
                ? AreBoxesWide
                    ? new List<(int, int)>
                    {
                        box.Go(direction).Go(Direction.Left),
                        box.Go(direction),
                        box.Go(direction).Go(Direction.Right)
                    }.Where(Boxes.Contains)
                    : Boxes.Where(b => b == box.Go(direction))
                : Boxes.Where(b => b == box.Go(direction, AreBoxesWide ? 2 : 1)))
                .Select(b => IterateDependency(b, direction)));

        public int BoxLocationSum => Boxes.Sum(b => b.Item1 + 100 * b.Item2);

        private class BoxDependency((int, int) root, IEnumerable<BoxDependency> branches)
        {
            public (int, int) Root { get; } = root;
            public List<BoxDependency> Branches { get; } = [.. branches];

            public bool CanMove(List<(int, int)> walls, Direction direction, bool areBoxesWide) =>
                !walls.Any(w =>
                    w == Root.Go(direction) ||
                    (areBoxesWide && w == Root.Go(Direction.Right).Go(direction))) &&
                Branches.All(b => b.CanMove(walls, direction, areBoxesWide));

            public void Move(List<(int, int)> boxes, Direction direction)
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
                    if (Walls.Contains((x, y)))
                        Console.Write('#');
                    else if (Boxes.Contains((x, y)))
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
