using com.mojang.ld22.gfx;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Textures;
using Vildmark.Helpers;
using Vildmark.Windowing;

namespace MiniventureSharp.Custom
{
    public class Canvas : VildmarkGame
    {
        private BufferStrategy bufferStrategy;
        private RenderContext renderContext;
        private GLTexture2D texture;

        private readonly List<KeyListener> keyListeners = new();

        public Font Font;

        public Canvas()
        {
            bufferStrategy = new BufferStrategy(2, Window.Size.X, Window.Size.Y);
        }

        public int getWidth()
        {
            return Window.Size.X;
        }

        public int getHeight()
        {
            return Window.Size.Y;
        }

        public bool hasFocus()
        {
            return true;
        }

        public void addKeyListener(KeyListener listener)
        {
            keyListeners.Add(listener);
        }

        public BufferStrategy getBufferStrategy()
        {
            return bufferStrategy;
        }

        public BufferStrategy createBufferStrategy(int n)
        {
            return bufferStrategy = new(n, Window.Size.X, Window.Size.Y);
        }

        public void requestFocus()
        {

        }

        public void setMinimumSize(Dimension dimension)
        {
        }

        public void setMaximumSize(Dimension dimension)
        {
        }

        public void setPreferredSize(Dimension dimension)
        {
            bufferStrategy.Resize(dimension.Width, dimension.Height);
            Window.Size = new Vector2i(dimension.Width, dimension.Height);
        }

        public override void Load()
        {
            renderContext = Create2DRenderContext();

            Window.OnKeyPressed += key =>
            {
                foreach (var keyListener in keyListeners)
                {
                    keyListener.keyPressed(new KeyEvent((int)key));
                }
            };

            Window.OnKeyReleased += key =>
            {
                foreach (var keyListener in keyListeners)
                {
                    keyListener.keyReleased(new KeyEvent((int)key));
                }
            };
        }

        public override void Render(float delta)
        {
            texture ??= new GLTexture2D(bufferStrategy.Display.Width, bufferStrategy.Display.Height, pixelInternalFormat: PixelInternalFormat.Rgb);
            bufferStrategy.UpdateTexture(texture);

            renderContext.Begin();
            renderContext.RenderRectangle(new System.Drawing.RectangleF(0, 0, Window.Size.X, Window.Size.Y), texture);
            renderContext.End();
        }

        protected override void InitializeWindowSettings(WindowSettings settings)
        {
            base.InitializeWindowSettings(settings);

            settings.Border = OpenTK.Windowing.Common.WindowBorder.Fixed;
        }
    }
}
