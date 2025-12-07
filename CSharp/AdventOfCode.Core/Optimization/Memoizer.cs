namespace AdventOfCode.Core.Optimization;

public static class Memoizer
{
    public static Func<T, TResult> Memoize<T, TResult>(
        this Func<T, Func<T, TResult>, TResult> f) where T : notnull
    {
        var cache = new Dictionary<T, TResult>();
        TResult self(T a)
        {
            if (cache.TryGetValue(a, out var value))
                return value;
            value = f(a, self);
            cache.Add(a, value);
            return value;
        }

        return self;
    }
}
