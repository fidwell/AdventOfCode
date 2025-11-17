namespace AdventOfCode.Solvers._2016;

public class Puzzle09Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => DecompressedLengthOf(input, false).ToString();

    public override string SolvePartTwo(string input) => DecompressedLengthOf(input, true).ToString();

    public static long DecompressedLengthOf(string input, bool isRecursive)
    {
        input = input.Trim();
        var length = 0L;
        var index = 0;

        while (index < input.Length)
        {
            if (input[index] == '(')
            {
                var closeParen = input.IndexOf(')', index + 1);
                var markerData = input.Substring(index + 1, closeParen - index - 1)
                    .Split('x').Select(int.Parse).ToArray();
                var charCount = markerData[0];
                var repeatCount = markerData[1];

                var repeatedString = input.Substring(closeParen + 1, charCount);

                if (isRecursive)
                {
                    length += repeatCount * DecompressedLengthOf(repeatedString, true);
                }
                else
                {
                    length += repeatCount * charCount;
                }

                index = closeParen + charCount + 1;
            }
            else
            {
                index++;
                length++;
            }
        }

        return length;
    }
}
