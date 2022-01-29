using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class RuntimeException : Exception
    {
        public RuntimeException(Exception innerException) : base(innerException.Message, innerException)
        {
        }
    }
}
