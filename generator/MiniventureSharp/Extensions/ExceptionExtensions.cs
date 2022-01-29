using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Extensions
{
    internal static class ExceptionExtensions
    {
        public static void printStackTrace(this Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
