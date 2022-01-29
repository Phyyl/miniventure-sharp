using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace MiniventureSharp.Custom
{
    public class Buffer
    {
        public int[] Data { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Length => Data.Length;

        public int this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public int this[int x, int y]
        {
            get => Data[x + y * Width];
            set => Data[x + y * Width] = value;
        }

        public Buffer(int width, int height, int[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        public Buffer(int width, int height)
            : this(width, height, new int[width * height])
        {
        }

        public void Resize(int width, int height)
        {
            Data = new int[width * height];
            Width = width;
            Height = height;
        }

        public void DrawImage(BufferStrategy bufferStrategy, BufferedImage image, Rectangle rectangle)
        {
            Buffer source = image.Buffer;
            Buffer dest = bufferStrategy.Render;

            for (int y = 0; y < rectangle.Height; y++)
            {
                for (int x = 0; x < rectangle.Width; x++)
                {
                    dest[x, y] = source[(int)(x / (float)rectangle.Width * source.Width), (int)(y / (float)rectangle.Height * source.Height)];
                }
            }
        }

        public unsafe void Save(string path)
        {
            fixed (int* ptr = Data)
            {
                using Bitmap bitmap = new Bitmap(Width, Height, Width * sizeof(int), System.Drawing.Imaging.PixelFormat.Format32bppRgb, new IntPtr(ptr));
                bitmap.Save(path);
            }
        }
    }
}
