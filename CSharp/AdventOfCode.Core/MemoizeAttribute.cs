using System.Collections.Concurrent;

namespace AdventOfCode.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class MemoizeAttribute : Attribute
{
}

public static class MemoizationCache
{
    private static readonly ConcurrentDictionary<string, object> _cache = new();

    public static T GetOrAdd<T>(string key, Func<T> valueFactory) =>
        (T)_cache.GetOrAdd(key, _ => valueFactory());

    public static string GetCacheKey(string methodName, object[] args) =>
        $"{methodName}:{string.Join(",", args)}";

    public static void Clear() => _cache.Clear();
}

public class Memoizer
{
    public static T Execute<T>(object target, string methodName, params object[] args)
    {
        var method = target.GetType().GetMethod(methodName);
        if (method is null)
        {
            throw new MissingMethodException($"Method {methodName} not found on type {target.GetType().Name}");
        }

        var isMemoized = method.GetCustomAttributes(typeof(MemoizeAttribute), false).Length != 0;

        if (isMemoized)
        {
            var key = MemoizationCache.GetCacheKey(methodName, args);
            return MemoizationCache.GetOrAdd(key, () => (T)method.Invoke(target, args));
        }

        return (T)method.Invoke(target, args);
    }
}
