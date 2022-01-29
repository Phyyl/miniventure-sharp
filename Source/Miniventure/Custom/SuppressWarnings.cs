using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class SuppressWarningsAttribute : Attribute
    {
       public SuppressWarningsAttribute(string suppress)
        {
            Suppress = suppress;
        }

        public string Suppress { get; }
    }
}
