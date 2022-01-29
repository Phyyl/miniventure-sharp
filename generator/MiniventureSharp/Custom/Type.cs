using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class Type<T>
    {
        private Type type;

        public T newInstance()
        {
            return (T)Activator.CreateInstance(type);
        }

        public static implicit operator Type<T>(Type type)
        {
            return new Type<T>()
            {
                type = type
            };
        }
    }
}
