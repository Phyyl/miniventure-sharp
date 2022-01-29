using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class Comparator<T> : IComparer<T>
    {
        public Func<T, T, int> compare { get; init; }

        public int Compare(T x, T y)
        {
            return compare.Invoke(x, y);
        }
    }
}
