using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public partial class Puzzle07Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, false);
    public string SolvePartTwo(string input) => Solve(input, true);

    public string Solve(string input, bool includeConcatenation) =>
        input.SplitByNewline().Select(l => new Equation(l))
            .Where(e => e.CouldBeSolved(this, includeConcatenation))
            .Sum(e => (long)e.Answer).ToString();

    protected enum Operation
    {
        Add,
        Multiply,
        Concatenate
    }

    protected readonly Dictionary<int, IEnumerable<Operation[]>> PermutationCache = [];

    protected IEnumerable<Operation[]> GetPossibleOperationPermutations(int length, bool includeConcatenation)
    {
        if (length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }
        else if (length == 1)
        {
            yield return [Operation.Add];
            yield return [Operation.Multiply];
            if (includeConcatenation)
                yield return [Operation.Concatenate];
        }
        else
        {
            IEnumerable<Operation[]> subArrays;
            if (PermutationCache.ContainsKey(length - 1))
            {
                subArrays = PermutationCache[length - 1];
            }
            else
            {
                subArrays = GetPossibleOperationPermutations(length - 1, includeConcatenation);
                PermutationCache.Add(length - 1, subArrays);
            }

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

            var operationPermutations = solver.GetPossibleOperationPermutations(Operands.Length - 1, includeConcatenation);

            foreach (var permutation in operationPermutations)
            {
                ulong total = Operands[0];
                for (var i = 0; i < permutation.Length; i++)
                {
                    if (permutation[i] == Operation.Add)
                    {
                        total += Operands[i + 1];
                    }
                    else if (permutation[i] == Operation.Multiply)
                    {
                        total *= Operands[i + 1];
                    }
                    else
                    {
                        total = MathExtensions.Concatenate(total, Operands[i + 1]);
                    }

                    // Break early if we know the answer can't be computed already
                    if (total > Answer)
                        break;
                }

                if (total == Answer)
                    return true;
            }

            return false;
        }
    }
}
