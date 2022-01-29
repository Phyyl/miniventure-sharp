using Miniventure.Generated.gfx;

namespace com.mojang.ld22.gfx;

public class Font
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ      0123456789.,!?'\"-+=/\\%()<>:;     ";

    public static void Draw(string msg, Screen screen, int x, int y, int col)
    {
        msg = msg.ToUpper();

        for (int i = 0; i < msg.Length; i++)
        {
            int ix = chars.IndexOf(msg[i]);

            if (ix >= 0)
            {
                screen.Render(x + (i * 8), y, ix + (30 * 32), col, 0);
            }
        }
    }

    public static void RenderFrame(Screen screen, string title, int x0, int y0, int x1, int y1)
    {
        for (int y = y0; y <= y1; y++)
        {
            for (int x = x0; x <= x1; x++)
            { 
                if (x == x0 && y == y0) // if the current x & y positions are at their starting params positions[]
                {
                    screen.Render(x * 8, y * 8, 0 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.None); // render a corner point
                }
                else if (x == x1 && y == y0) // if the current x position is at the end & the y is at the params start[]
                {
                    screen.Render(x * 8, y * 8, 0 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal); // render a corner point
                }
                else if (x == x0 && y == y1) // if the x position is at the start & the y point is at the params end[]
                {
                    screen.Render(x * 8, y * 8, 0 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical); // render a corner point
                }
                else if (x == x1 && y == y1) // if the current x & y positions are at their end params positions[]
                {
                    screen.Render(x * 8, y * 8, 0 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.Both); // render a corner point
                }
                else if (y == y0) // if the y position is at it's starting params position[]
                {
                    screen.Render(x * 8, y * 8, 1 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.None); // render a top end point
                }
                else if (y == y1) // if the y position is at it's ending params position[]
                {
                    screen.Render(x * 8, y * 8, 1 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical); // render a bottom end point
                }
                else if (x == x0) // if the x position is at it's begging params position[]
                {
                    screen.Render(x * 8, y * 8, 2 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.None); // render a left end point
                }
                else if (x == x1)  // if the x position is at it's ending params position[]
                {
                    screen.Render(x * 8, y * 8, 2 + (13 * 32), Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal); // render a right end point
                }
                else
                {
                    screen.Render(x * 8, y * 8, 2 + (13 * 32), Color.Get(5, 5, 5, 5), MirrorFlags.Horizontal); // render a blue square
                }
            }
        }

        Draw(title, screen, (x0 * 8) + 8, y0 * 8, Color.Get(5, 5, 5, 550));
    }
}