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
}
