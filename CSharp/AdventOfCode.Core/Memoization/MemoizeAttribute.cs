using System.Collections.Concurrent;
using System.Reflection;

namespace AdventOfCode.Core.Memoization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class MemoizeAttribute : Attribute
{
}

public static class MemoizationCache
{
    private static readonly ConcurrentDictionary<string, object> _cache = new();

    public static T GetOrAdd<T>(string key, Func<T> valueFactory) =>
        (T)_cache.GetOrAdd(key, _ =>
        {
            var result = valueFactory();
            return result is null
                ? throw new InvalidOperationException($"The value factory for key \"{key}\" returned null, but type {typeof(T)} is non-nullable.")
                : (object)result;
        });

    public static string GetCacheKey(string methodName, object[] args) =>
        $"{methodName}:{string.Join(",", args)}";

    public static void Clear() => _cache.Clear();
}

public class Memoizer
{
    public static T Execute<T>(object target, string methodName, params object[] args) where T : notnull
    {
        var method = target.GetType().GetMethod(methodName)
            ?? throw new MissingMethodException($"Method {methodName} not found on type {target.GetType().Name}");

        if (method.GetCustomAttributes(typeof(MemoizeAttribute), false).Length != 0)
        {
            var key = MemoizationCache.GetCacheKey(methodName, args);
            return MemoizationCache.GetOrAdd(key, () => Invoke<T>(method, target, args));
        }

        return Invoke<T>(method, target, args);
    }

    private static T Invoke<T>(MethodInfo method, object target, params object[] args)
    {
        var result = method.Invoke(target, args);
        return result is null
            ? throw new InvalidOperationException($"The method {method.Name} returned null, but type {typeof(T)} is non-nullable.")
            : (T)result;
    }
}
