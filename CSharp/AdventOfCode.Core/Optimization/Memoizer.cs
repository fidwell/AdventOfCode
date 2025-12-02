namespace AdventOfCode.Core.Optimization;

public class Memoizer<TInput, TOutput>(Func<TInput, TOutput> func) where TInput : notnull
{
    private readonly Dictionary<TInput, TOutput> cachedValues = [];

    public TOutput Get(TInput input)
    {
        if (cachedValues.TryGetValue(input, out TOutput? output))
        {
            return output;
        }

        output = func(input);
        cachedValues.Add(input, output);
        return output;
    }
}
