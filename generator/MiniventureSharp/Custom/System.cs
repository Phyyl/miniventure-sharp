using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public static class SystemInfo
    {
        public static long nanoTime()
        {
            return Stopwatch.GetTimestamp();
        }

        public static long currentTimeMillis()
        {
            return Stopwatch.GetTimestamp() / TimeSpan.TicksPerMillisecond;
        }
    }
}
