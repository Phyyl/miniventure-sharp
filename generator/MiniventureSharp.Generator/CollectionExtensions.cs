using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Generator
{
    static class CollectionExtensions
    {
        internal static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                hashSet.Add(value);
            }
        }
    }
}
