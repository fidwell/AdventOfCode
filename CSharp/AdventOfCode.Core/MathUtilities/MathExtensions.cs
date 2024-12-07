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

    public static ulong Concatenate(ulong a, ulong b)
    { // https://stackoverflow.com/a/26853517/436282
        if (b < 10) return 10 * a + b;
        if (b < 100) return 100 * a + b;
        if (b < 1000) return 1000 * a + b;
        if (b < 10000) return 10000 * a + b;
        if (b < 100000) return 100000 * a + b;
        if (b < 1000000) return 1000000 * a + b;
        if (b < 10000000) return 10000000 * a + b;
        if (b < 100000000) return 100000000 * a + b;
        if (b < 1000000000) return 1000000000 * a + b;
        if (b < 10000000000) return 10000000000 * a + b;
        if (b < 100000000000) return 100000000000 * a + b;
        if (b < 1000000000000) return 1000000000000 * a + b;
        if (b < 10000000000000) return 10000000000000 * a + b;
        if (b < 100000000000000) return 100000000000000 * a + b;
        if (b < 1000000000000000) return 1000000000000000 * a + b;
        if (b < 10000000000000000) return 10000000000000000 * a + b;
        if (b < 100000000000000000) return 100000000000000000 * a + b;
        if (b < 1000000000000000000) return 1000000000000000000 * a + b;
        return 10000000000000000000 * a + b;
    }
}
