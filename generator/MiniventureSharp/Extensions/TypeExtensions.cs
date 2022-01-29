using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Resources;

namespace MiniventureSharp.Extensions
{
    internal static class TypeExtensions
    {
        public static string getResource(this Type type, string path)
        {
            return path;
        }

        public static Stream getResourceAsStream(this Type type, string path)
        {
            return ResourceLoader.FindEmbeddedStream(path.Replace("/", "."));
        }

        public static Type getClass(this object obj) => obj?.GetType();
    }
}
