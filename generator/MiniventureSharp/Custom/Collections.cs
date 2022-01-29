using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public static class Collections
    {
        public static void sort<T>(List<T> list, Comparator<T> comparator)
        {
            list.Sort(comparator);
        }
    }
}
