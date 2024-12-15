using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle15Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var warehouse = new Warehouse(input);
        while (!warehouse.IsDone)
        {
            warehouse.Step();
        }
        return warehouse.BoxLocationSum.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var warehouse = new WideWarehouse(input);
        warehouse.Print();
        while (!warehouse.IsDone)
        {
            warehouse.Step();
            warehouse.Print();
        }
        return warehouse.BoxLocationSum.ToString();
    }

    private class Warehouse
    {
        public (int, int) Robot { get; private set; }
        public List<(int, int)> Boxes { get; } = [];
        public List<(int, int)> Walls { get; } = [];
        public string RobotInstructions { get; private set; } = string.Empty;

        private readonly int _size;
        private int _step = 0;

        public bool IsDone => _step >= RobotInstructions.Length;

        public Warehouse(string input)
        {
            var y = 0;
            foreach (var line in input.SplitByNewline())
            {
                if (line.StartsWith('#'))
                {
                    for (var x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '@')
                            Robot = (x, y);
                        else if (line[x] == 'O')
                            Boxes.Add((x, y));
                        else if (line[x] == '#')
                            Walls.Add((x, y));
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
            if (PushThingAt(robotTarget, direction))
            {
                Robot = robotTarget;
            }
            _step++;
        }

        private bool PushThingAt((int, int) location, Direction direction)
        {
            if (Walls.Contains(location))
                return false;

            if (Boxes.Contains(location))
            {
                var target = location.Go(direction);
                if (PushThingAt(target, direction))
                {
                    Boxes.RemoveAll(b => b == location);
                    Boxes.Add(target);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public int BoxLocationSum => Boxes.Sum(b => b.Item1 + 100 * b.Item2);

        public void Print()
        {
            if (_step > 0)
                Console.WriteLine(RobotInstructions[_step - 1]);
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (Walls.Contains((x, y)))
                        Console.Write('#');
                    else if (Boxes.Contains((x, y)))
                        Console.Write('O');
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

    private class WideWarehouse
    {
        public (int, int) Robot { get; private set; }
        public List<(int, int)> Boxes { get; } = [];
        public List<(int, int)> Walls { get; } = [];
        public string RobotInstructions { get; private set; } = string.Empty;

        private readonly int _size;
        private int _step = 0;

        public bool IsDone => _step >= RobotInstructions.Length;

        public WideWarehouse(string input)
        {
            var y = 0;
            foreach (var line in input.SplitByNewline())
            {
                if (line.StartsWith('#'))
                {
                    for (var x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '@')
                            Robot = (x * 2, y);
                        else if (line[x] == 'O')
                            Boxes.Add((x * 2, y));
                        else if (line[x] == '#')
                        {
                            Walls.Add((x * 2, y));
                            Walls.Add((x * 2 + 1, y));
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
            var boxCount = Boxes.Count;

            var robotTarget = Robot.Go(direction);
            var boxTree = CreateDependencyTree(Boxes, robotTarget, direction);

            if (boxTree is null && !Walls.Any(w => w == robotTarget))
            {
                // no boxes; move at will
                Robot = robotTarget;
            }
            else if (boxTree?.CanMove(Walls, direction) ?? false)
            {
                boxTree.Move(Boxes, direction);
                Robot = robotTarget;
            }
            else
            {
                // nobody can move
            }

            if (boxCount != Boxes.Count)
                throw new Exception("Box count changed");
            if (Boxes.Distinct().Count() != Boxes.Count)
                throw new Exception("Boxes overlap");
            _step++;
        }

        private BoxDependency? CreateDependencyTree(List<(int, int)> boxes, (int, int) target, Direction direction)
        {
            var firstBox = Boxes.Where(b => b == target || b == target.Go(Direction.Left));
            if (!firstBox.Any())
            {
                return null;
            }

            return IterateDependency(firstBox.Single(), boxes, direction);
        }

        private static BoxDependency IterateDependency((int, int) box, List<(int, int)> boxes, Direction direction)
        {
            IEnumerable<(int, int)> subBoxes;
            switch (direction)
            {
                /* .V...
                 * .[]..
                 * [][].
                 */
                case Direction.Down:
                    var dl = box.Go(Direction.Down).Go(Direction.Left);
                    var dm = box.Go(Direction.Down);
                    var dr = box.Go(Direction.Down).Go(Direction.Right);
                    subBoxes = new List<(int, int)> { dl, dm, dr }.Where(boxes.Contains);
                    break;

                case Direction.Up:
                    var ul = box.Go(Direction.Up).Go(Direction.Left);
                    var um = box.Go(Direction.Up);
                    var ur = box.Go(Direction.Up).Go(Direction.Right);
                    subBoxes = new List<(int, int)> { ul, um, ur }.Where(boxes.Contains);
                    break;

                // >[][]
                case Direction.Right:
                    subBoxes = boxes.Where(b => b == box.Go(Direction.Right, 2));
                    break;

                // [][]<
                case Direction.Left:
                    subBoxes = boxes.Where(b => b == box.Go(Direction.Left, 2));
                    break;

                default: throw new ArgumentException(nameof(direction));
            }
            return new BoxDependency(box, subBoxes.Select(b => IterateDependency(b, boxes, direction)));
        }

        public int BoxLocationSum => Boxes.Sum(b => b.Item1 + 100 * b.Item2);

        private class BoxDependency((int, int) root, IEnumerable<BoxDependency> branches)
        {
            public (int, int) Root { get; } = root;
            public List<BoxDependency> Branches { get; } = branches.ToList();

            public bool CanMove(List<(int, int)> walls, Direction direction)
            {
                return Branches.Count != 0
                    ? Branches.All(b => b.CanMove(walls, direction))
                    : !walls.Any(w => w == Root.Go(direction) || w == Root.Go(Direction.Right).Go(direction));
            }

            public void Move(List<(int, int)> boxes, Direction direction)
            {
                foreach (var b in Branches)
                {
                    b.Move(boxes, direction);
                }
                boxes.RemoveAll(b => b == Root);
                boxes.Add(Root.Go(direction));
            }
        }

        public void Print()
        {
            Console.WriteLine(_step);
            if (_step > 0)
                Console.WriteLine(RobotInstructions[_step - 1]);
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size * 2; x++)
                {
                    if (Walls.Contains((x, y)))
                        Console.Write('#');
                    else if (Boxes.Contains((x, y)))
                    {
                        Console.Write("[]");
                        x++;
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
