//using AdventOfCode.Core.StringUtilities;
//using AdventOfCode.Data;

//namespace AdventOfCode.Core.PuzzleSolvers;

//public class Puzzle01Part2Solver : IPuzzleSolver
//{
//    public string Solve(bool useSample = false)
//        => DataReader
//            .GetData(1, 2, useSample)
//            .Split(Environment.NewLine)
//            .Where(l => !string.IsNullOrWhiteSpace(l))
//            .Select(GetFirstAndLastDigits)
//            .Select(pair => int.Parse($"{pair.Item1}{pair.Item2}"))
//            .Sum()
//            .ToString();

//    private (int, int) GetFirstAndLastDigits(string input)
//    {
//    }
//}
