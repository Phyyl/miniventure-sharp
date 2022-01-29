using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Textures;

namespace MiniventureSharp.Custom
{
    public class Graphics
    {
        private readonly BufferStrategy bufferStrategy;
        public int CurrentColor;

        public Graphics(BufferStrategy bufferStrategy)
        {
            this.bufferStrategy = bufferStrategy;
        }

        public void fillRect(int x, int y, int w, int h)
        {
            if (x == 0 && y == 0 && w == bufferStrategy.Render.Width && h == bufferStrategy.Render.Height)
            {
                bufferStrategy.Render.Data.AsSpan().Fill(CurrentColor);
                return;
            }

            for (int yy = y; yy < h; yy++)
            {
                bufferStrategy.Render.Data.AsSpan().Slice(yy * bufferStrategy.Render.Width + x, w).Fill(CurrentColor);
            }
        }

        internal void drawImage(BufferedImage image, int x, int y, int w, int h, object _)
        {
            bufferStrategy.Render.DrawImage(bufferStrategy, image, new Rectangle(x, y, w, h));
        }

        internal void dispose()
        {

        }
    }
}
