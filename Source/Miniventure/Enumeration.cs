using System.Diagnostics.CodeAnalysis;

namespace Miniventure;

public abstract record class Enumeration<TValue>
    where TValue : Enumeration<TValue>
{
    private static readonly List<TValue> values = new();

    public static TValue[] Values => values.ToArray();

    public Enumeration()
    {
        values.Add(this as TValue);
    }
}

public abstract record class Enumeration<TKey, TValue> : Enumeration<TValue>
    where TValue : Enumeration<TValue>
{
    private static readonly Dictionary<TKey, TValue> values = new();

    public TKey Key { get; }

    public Enumeration(TKey key)
        : base()
    {
        Key = key;
        values[key] = this as TValue;
    }

    public static bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue value)
    {
        value = GetValueOrDefault(key);
        return value is not null;
    }

    public static TValue GetValueOrDefault(TKey key)
    {
        return values.GetValueOrDefault(key);
    }
}
