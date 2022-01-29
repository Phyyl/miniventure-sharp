using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Textures;
using Vildmark.Maths;

namespace MiniventureSharp.Custom
{
    public class BufferStrategy
    {
        private Buffer[] buffers;
        private int current;

        public Buffer Display => buffers[current];
        public Buffer Render => buffers[(current + 1) % buffers.Length];

        public BufferStrategy(int n, int width, int height)
        {
            buffers = new Buffer[n];

            for (int i = 0; i < n; i++)
            {
                buffers[i] = new Buffer(width, height);
            }
        }

        public Graphics getDrawGraphics()
        {
            return new Graphics(this);
        }

        internal void Resize(int w, int h)
        {
            for (int i = 0; i < buffers.Length; i++)
            {
                buffers[i].Resize(w, h);
            }
        }

        public void UpdateTexture(GLTexture2D texture)
        {
            int width = Display.Width;
            int height = Display.Height;
            int[] data = Display.Data;

            texture.SetData(width, height, data.AsSpan());
        }

        public void show()
        {
            current++;
            current %= buffers.Length;
        }
    }
}
