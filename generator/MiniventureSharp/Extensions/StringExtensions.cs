using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Extensions
{
    internal static class StringExtensions
    {
        public static int indexOf(this string str, char chr)
        {
            return str.IndexOf(chr);
        }

        public static char charAt(this string str, int index)
        {
            return str[index];
        }

        public static string toUpperCase(this string str)
        {
            return str.ToUpper();
        }

        public static string toLowerCase(this string str)
        {
            return str.ToLower();
        }
    }
}
