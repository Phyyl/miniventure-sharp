using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Extensions
{
    internal static class CollectionsExtensions
    {
        public static int size<T>(this List<T> list)
        {
            return list.Count;
        }

        public static T get<T>(this List<T> list, int index)
        {
            return list[index];
        }

        public static void add<T>(this List<T> list, T value)
        {
            list.Add(value);
        }

        public static void add<T>(this List<T> list, int index, T value)
        {
            list.Insert(index, value);
        }

        public static void removeAll<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Remove(item);
            }
        }

        public static void remove<T>(this List<T> list, T item)
        {
            list.Remove(item);
        }

        public static void addAll<T>(this List<T> list, IEnumerable<T> items)
        {
            list.AddRange(items);
        }

        public static void clear<T>(this List<T> list)
        {
            list.Clear();
        }

        public static T remove<T>(this List<T> list, int i)
        {
            T item = list[i];
            list.RemoveAt(i);
            return item;
        }

        public static bool contains<T>(this List<T> list, T item)
        {
            return list.Contains(item);
        }
    }
}
