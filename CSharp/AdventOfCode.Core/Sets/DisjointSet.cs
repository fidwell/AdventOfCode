namespace AdventOfCode.Core.Sets;

/// <summary>
/// Represents a collection of disjoint sets, supporting grouping operations on elements.
/// </summary>
/// <remarks>Use the Connect method to merge sets containing specific elements. The DisjointSet class is useful
/// for tracking connectivity or partitioning elements into non-overlapping groups, such as in union-find
/// algorithms. See <see href="https://en.wikipedia.org/wiki/Disjoint-set_data_structure"/>.</remarks>
/// <param name="count">The number of initial singleton sets to create. Each set contains a
/// unique element from 0 to count - 1.</param>
public class DisjointSet(int count)
{
    public List<List<long>> Sets = [.. Enumerable.Range(0, count).Select(i => new List<long> { i })];

    public void Connect(int a, int b)
    {
        var groupA = Sets.First(g => g.Contains(a));
        var groupB = Sets.First(g => g.Contains(b));

        if (groupA != groupB)
        {
            Sets.Remove(groupA);
            Sets.Remove(groupB);
            Sets.Add([.. groupA, .. groupB]);
        }
    }
}
