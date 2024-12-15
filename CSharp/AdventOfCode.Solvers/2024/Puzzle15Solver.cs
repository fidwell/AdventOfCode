using AdventOfCode.Core.ArrayUtilities;
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

        private BoxDependency IterateDependency((int, int) box, Direction direction)
        {
            IEnumerable<(int, int)> subBoxes;
            switch (direction)
            {
                /* .V...
                 * .[]..
                 * [][].
                 */
                case Direction.Down:
                    if (AreBoxesWide)
                    {
                        var dl = box.Go(Direction.Down).Go(Direction.Left);
                        var dm = box.Go(Direction.Down);
                        var dr = box.Go(Direction.Down).Go(Direction.Right);
                        subBoxes = new List<(int, int)> { dl, dm, dr }.Where(Boxes.Contains);
                    }
                    else
                    {
                        subBoxes = Boxes.Where(b => b == box.Go(Direction.Down));
                    }
                    break;

                case Direction.Up:
                    if (AreBoxesWide)
                    {
                        var ul = box.Go(Direction.Up).Go(Direction.Left);
                        var um = box.Go(Direction.Up);
                        var ur = box.Go(Direction.Up).Go(Direction.Right);
                        subBoxes = new List<(int, int)> { ul, um, ur }.Where(Boxes.Contains);
                    }
                    else
                    {
                        subBoxes = Boxes.Where(b => b == box.Go(Direction.Up));
                    }
                    break;

                // >[][]
                case Direction.Right:
                    subBoxes = Boxes.Where(b => b == box.Go(Direction.Right, AreBoxesWide ? 2 : 1));
                    break;

                // [][]<
                case Direction.Left:
                    subBoxes = Boxes.Where(b => b == box.Go(Direction.Left, AreBoxesWide ? 2 : 1));
                    break;

                default: throw new ArgumentException(nameof(direction));
            }
            return new BoxDependency(box, subBoxes.Select(b => IterateDependency(b, direction)));
        }

        public int BoxLocationSum => Boxes.Sum(b => b.Item1 + 100 * b.Item2);

        private class BoxDependency((int, int) root, IEnumerable<BoxDependency> branches)
        {
            public (int, int) Root { get; } = root;
            public List<BoxDependency> Branches { get; } = branches.ToList();

            public bool CanMove(List<(int, int)> walls, Direction direction, bool areBoxesWide)
            {
                if (walls.Any(w =>
                    w == Root.Go(direction) ||
                    (areBoxesWide && w == Root.Go(Direction.Right).Go(direction))))
                    return false;

                return Branches.All(b => b.CanMove(walls, direction, areBoxesWide));
            }

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
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size * (AreBoxesWide ? 2 : 1); x++)
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
