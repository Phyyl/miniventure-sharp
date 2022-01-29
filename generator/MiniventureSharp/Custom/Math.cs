using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public static class Math
    {
        public static double cos(double value) => CSMath.Cos(value);
        public static double sin(double value) => CSMath.Sin(value);
        public static int min(int a, int b) => CSMath.Min(a, b);
        public static int max(int a, int b) => CSMath.Max(a, b);
        public static double sqrt(double value) => CSMath.Sqrt(value);
        public static double log(double value) => CSMath.Log(value);
        public static double abs(double value) => CSMath.Abs(value);
    }
}
