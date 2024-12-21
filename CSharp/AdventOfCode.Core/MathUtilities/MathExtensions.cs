namespace AdventOfCode.Core.MathUtilities;

public static class MathExtensions
{
    public static long LCM(long a, long b)
    {
        var larger = a > b ? a : b;
        for (var i = larger; ; i += larger)
        {
            if (i % a == 0 && i % b == 0)
            {
                return i;
            }
        }
    }

    public static bool IsWholeNumber(this double x) => x - Math.Floor(x) <= double.Epsilon;

    public static (double, double) SolveSystemOfEquations(double a1, double a2, double b1, double b2, double c1, double c2)
        => ((c1 * b2 - b1 * c2) / (a1 * b2 - b1 * a2), (a1 * c2 - c1 * a2) / (a1 * b2 - b1 * a2));

    /// <summary>
    /// Performs a modulo operation which allows for alternate behavior
    /// from the standard '%' operator when the dividend is negative.
    /// </summary>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static int Modulo(int dividend, int divisor) => (dividend % divisor + divisor) % divisor;

    /// <summary>
    /// Todo - summarize
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int ManhattanDistance(this (int, int) a, (int, int) b) => Math.Abs(b.Item1 - a.Item1) + Math.Abs(b.Item2 - a.Item2);
}
