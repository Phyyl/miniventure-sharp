using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class DataBufferInt
    {
        private readonly BufferedImage image;

        public DataBufferInt(BufferedImage image)
        {
            this.image = image;
        }

        public Buffer getData()
        {
            return image.Buffer;
        }
    }
}
