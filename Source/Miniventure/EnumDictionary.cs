using System.Reflection;
using Vildmark;

namespace Miniventure;

public class EnumDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    public EnumDictionary(Func<TValue, TKey> keySelector)
        : base(typeof(TValue)
            .GetMembers(BindingFlags.Public | BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.GetField)
            .Select(m => m switch
            {
                PropertyInfo p => p.GetValue(null),
                FieldInfo f => f.GetValue(null),
                _ => null
            })
            .OfType<TValue>()
            .ToDictionary(keySelector))
    {
    }
}