using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class BufferedImage
    {
        public const int TYPE_INT_RGB = 1;

        public Buffer Buffer { get; }

        public BufferedImage(int width, int height, int _)
            : this(new Buffer(width, height))
        {

        }

        public BufferedImage(Buffer buffer)
        {
            Buffer = buffer;
        }

        public int getHeight()
        {
            return Buffer.Width;
        }

        public int getWidth()
        {
            return Buffer.Width;
        }

        public unsafe Buffer getRGB(int x, int y, int width, int height, int[] pixels, int offset, int stride)
        {
            return Buffer;
        }

        public Raster getRaster()
        {
            return new(this);
        }

        public void setRGB(int x, int y, int w, int h, Buffer pixels, int offset, int stride)
        {

        }

        public Image getScaledInstance(int v1, int v2, int scale)
        {
            return null;
        }

        public class Raster
        {
            private readonly BufferedImage image;

            public Raster(BufferedImage image)
            {
                this.image = image;
            }

            public DataBufferInt getDataBuffer()
            {
                return new DataBufferInt(image);
            }
        }
    }
}
