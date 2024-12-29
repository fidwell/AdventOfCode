namespace AdventOfCode.Core.MathUtilities;

public static class AggregateExtensions
{
    /// <summary>
    /// Sums a collection of ulong values.
    /// </summary>
    /// <param name="values">Values to find the sum of.</param>
    /// <returns>The sum of the values.</returns>
    public static ulong Sum(this IEnumerable<ulong> values) =>
        values.Aggregate(0UL, (sum, value) => sum + value);

    /// <summary>
    /// Finds the product of a collection of int values.
    /// </summary>
    /// <param name="values">Values to find the product of.</param>
    /// <returns>The product of the values.</returns>
    public static int Product(this IEnumerable<int> values) =>
        values.Aggregate(1, (product, value) => product * value);

    /// <summary>
    /// Finds the product of a collection of int values.
    /// </summary>
    /// <param name="values">Values to find the product of.</param>
    /// <returns>The product of the values.</returns>
    public static long ProductLong(this IEnumerable<int> values) =>
        values.Aggregate(1L, (product, value) => product * value);
}
