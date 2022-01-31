namespace Miniventure.Graphics;

public class Screen
{
    private static int[] dither = new int[] { 0, 8, 2, 10, 12, 4, 14, 6, 3, 11, 1, 9, 15, 7, 13, 5, };

    private readonly Pixels spriteSheet;

    public int Width { get; }
    public int Height { get; }
    public Pixels Pixels { get; }

    public int xOffset { get; set; }
    public int yOffset { get; set; }

    public Screen(int width, int height, Pixels spriteSheet)
    {
        this.spriteSheet = spriteSheet;

        Width = width;
        Height = height;

        Pixels = new Pixels(width, height);
    }

    public void Clear(int color)
    {
        for (int i = 0; i < Pixels.Length; i++)
        {
            Pixels[i] = color;
        }
    }

    public void Render(int xp, int yp, int tile, int colors, MirrorFlags flags)
    {
        xp -= xOffset;
        yp -= yOffset;

        int xTile = tile % 32;
        int yTile = tile / 32;
        int toffs = xTile * 8 + yTile * 8 * spriteSheet.Width;

        for (int y = 0; y < 8; y++)
        {
            int ys = y;

            if (flags.HasFlag(MirrorFlags.Vertical))
            {
                ys = 7 - y;
            }

            if (y + yp < 0 || y + yp >= Height)
            {
                continue;
            }

            for (int x = 0; x < 8; x++)
            {
                if (x + xp < 0 || x + xp >= Width)
                {
                    continue;
                }

                int xs = x;

                if (flags.HasFlag(MirrorFlags.Horizontal))
                {
                    xs = 7 - x;
                }

                int col = colors >> spriteSheet[xs + ys * spriteSheet.Width + toffs] * 8 & 255;

                if (col < 255)
                {
                    Pixels[x + xp + (y + yp) * Width] = col;
                }
            }
        }
    }

    public void SetOffset(int xOffset, int yOffset)
    {
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }

    public void Overlay(Screen screen, int xa, int ya)
    {
        for (int y = 0, i = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++, i++)
            {
                if (screen.Pixels[i] / 10 <= dither[(x + xa & 3) + (y + ya & 3) * 4])
                {
                    Pixels[i] = 0;
                }
            }
        }
    }

    public void RenderLight(int x, int y, int r)
    {
        x -= xOffset;
        y -= yOffset;

        int x0 = x - r;
        int x1 = x + r;
        int y0 = y - r;
        int y1 = y + r;

        if (x0 < 0)
        {
            x0 = 0;
        }

        if (y0 < 0)
        {
            y0 = 0;
        }

        if (x1 > Width)
        {
            x1 = Width;
        }

        if (y1 > Height)
        {
            y1 = Height;
        }

        for (int yy = y0; yy < y1; yy++)
        {
            int yd = yy - y;

            yd *= yd;

            for (int xx = x0; xx < x1; xx++)
            {
                int xd = xx - x;
                int dist = xd * xd + yd;

                if (dist <= r * r)
                {
                    int br = 255 - dist * 255 / (r * r);

                    if (Pixels[xx + yy * Width] < br)
                    {
                        Pixels[xx + yy * Width] = br;
                    }
                }
            }
        }
    }
}