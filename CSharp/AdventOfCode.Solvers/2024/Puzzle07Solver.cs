using AdventOfCode.Core.Memoization;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public partial class Puzzle07Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    public string Solve(string input, bool includeConcatenation) =>
        input.SplitByNewline().Select(l => new Equation(l))
            .Where(e => e.CouldBeSolved(this, includeConcatenation))
            .Sum(e => (long)e.Answer).ToString();

    public enum Operation
    {
        Add,
        Multiply,
        Concatenate
    }

    [Memoize]
    public IEnumerable<Operation[]> GetPossibleOperationPermutations(int length, bool includeConcatenation)
    {
        ArgumentOutOfRangeException.ThrowIfZero(length);

        if (length == 1)
        {
            yield return [Operation.Add];
            yield return [Operation.Multiply];
            if (includeConcatenation)
                yield return [Operation.Concatenate];
        }
        else
        {
            var subArrays = Memoizer.Execute<IEnumerable<Operation[]>>(this, nameof(GetPossibleOperationPermutations), length - 1, includeConcatenation);

            foreach (var subarray in subArrays)
            {
                yield return [Operation.Add, .. subarray];
                yield return [Operation.Multiply, .. subarray];
                if (includeConcatenation)
                    yield return [Operation.Concatenate, .. subarray];
            }
        }
    }

    private class Equation
    {
        public Equation(string input)
        {
            var split = input.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries);
            Answer = ulong.Parse(split[0]);
            Operands = split.Skip(1).Select(uint.Parse).ToArray();
        }

        public ulong Answer { get; }
        public uint[] Operands { get; }

        public bool CouldBeSolved(Puzzle07Solver solver, bool includeConcatenation)
        {
            if (Operands.Length == 0)
                throw new InvalidDataException("No operands found");

            if (Operands.Length == 1)
                return Operands[0] == Answer;

            var operationPermutations = Memoizer.Execute<IEnumerable<Operation[]>>(solver, nameof(GetPossibleOperationPermutations), Operands.Length - 1, includeConcatenation);

            foreach (var permutation in operationPermutations)
            {
                var answer = Answer;
                // Work right-to-left so we can give up earlier
                for (var i = permutation.Length - 1; i >= 0; i--)
                {
                    if (permutation[i] == Operation.Add)
                    {
                        answer -= Operands[i + 1];
                    }
                    else if (permutation[i] == Operation.Multiply)
                    {
                        if (answer % Operands[i + 1] != 0)
                            break;
                        answer /= Operands[i + 1];
                    }
                    else
                    {
                        var operand = Operands[i + 1];
                        var operandLength = (int)Math.Log10(operand) + 1;
                        var powerOfTen = (ulong)Math.Pow(10, operandLength);
                        if ((answer - operand) % powerOfTen != 0)
                            break;
                        answer -= Operands[i + 1];
                        answer /= powerOfTen;
                    }
                }

                if (answer == Operands[0])
                    return true;
            }

            return false;
        }
    }
}
