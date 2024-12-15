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
        throw new NotImplementedException();
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
}
