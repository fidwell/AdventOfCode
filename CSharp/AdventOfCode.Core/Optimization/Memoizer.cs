namespace AdventOfCode.Core.Optimization;

public static class Memoizer
{
    public static Func<T, TResult> Create<T, TResult>(Func<T, TResult> func) where T : notnull
    {
        var cache = new Dictionary<T, TResult>();
        return (key) =>
        {
            if (cache.TryGetValue(key, out TResult? value))
                return value;

            value = func(key);
            cache[key] = value;
            return value;
        };
    }

    public static Func<T1, T2, TResult> Create<T1, T2, TResult>(Func<T1, T2, TResult> func)
        where T1 : notnull
        where T2 : notnull
    {
        var cache = new Dictionary<ValueTuple<T1, T2>, TResult>();
        return (a, b) =>
        {
            var key = ValueTuple.Create(a, b);
            if (cache.TryGetValue(key, out TResult? value))
            {
                return value;
            }

            value = func(a, b);
            cache[key] = value;
            return value;
        };
    }
}
