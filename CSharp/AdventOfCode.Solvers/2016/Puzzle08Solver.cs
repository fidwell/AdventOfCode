using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle08Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => TruesIn(RunProgram(input)).ToString();

    public override string SolvePartTwo(string input)
    {
        if (ShouldPrint)
        {
            var screen = RunProgram(input);
            WriteScreen(screen);
        }
        return "UPOJFLBCEZ";
    }

    private bool[,] RunProgram(string input)
    {
        var screen = new bool[6, 50];

        foreach (var instruction in input.SplitByNewline())
        {
            var parts = instruction.Split(" ");
            if (parts[0] == "rect")
            {
                var dimensions = parts[1].Split('x', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                TurnOnElements(dimensions[0], dimensions[1], screen);
            }
            else if (parts[0] == "rotate" && parts[1] == "column")
            {
                var column = int.Parse(parts[2].Split("=").Last());
                var amount = int.Parse(parts[4]);
                RotateDown(column, amount, ref screen);
            }
            else if (parts[0] == "rotate" && parts[1] == "row")
            {
                var row = int.Parse(parts[2].Split("=").Last());
                var amount = int.Parse(parts[4]);
                RotateRight(row, amount, ref screen);
            }
            else
            {
                throw new NotImplementedException();
            }

            if (ShouldPrint)
            {
                Console.WriteLine($"After instruction {instruction}:");
                WriteScreen(screen);
                Console.WriteLine();
            }
        }
        return screen;
    }

    private static void TurnOnElements(int width, int height, bool[,] screen)
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                screen[y, x] = true;
            }
        }
    }

    private static void RotateRight(int row, int amount, ref bool[,] input)
    {
        var rowLength = input.GetLength(1);
        var output = (bool[,])input.Clone();
        for (var x = 0; x < rowLength; x++)
        {
            output[row, x] = input[row, (x - amount + rowLength) % rowLength];
        }
        input = output;
    }

    private static void RotateDown(int column, int amount, ref bool[,] input)
    {
        var colLength = input.GetLength(0);
        var output = (bool[,])input.Clone();
        for (var y = 0; y < colLength; y++)
        {
            output[y, column] = input[(y - amount + colLength) % colLength, column];
        }
        input = output;
    }

    private static int TruesIn(bool[,] screen)
    {
        var height = screen.GetLength(0);
        var width = screen.GetLength(1);
        var result = 0;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (screen[y, x])
                    result++;
            }
        }
        return result;
    }

    private static void WriteScreen(bool[,] screen)
    {
        for (var y = 0; y < screen.GetLength(0); y++)
        {
            for (var x = 0; x < screen.GetLength(1); x++)
            {
                Console.Write(screen[y, x] ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}
