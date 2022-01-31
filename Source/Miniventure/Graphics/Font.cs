namespace Miniventure.Graphics;

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
                screen.Render(x + i * 8, y, ix + 30 * 32, col, 0);
            }
        }
    }

    public static void RenderFrame(Screen screen, string title, int x0, int y0, int x1, int y1)
    {
        for (int y = y0; y <= y1; y++)
        {
            for (int x = x0; x <= x1; x++)
            {
                if (x == x0 && y == y0)
                {
                    screen.Render(x * 8, y * 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
                }
                else if (x == x1 && y == y0)
                {
                    screen.Render(x * 8, y * 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal);
                }
                else if (x == x0 && y == y1)
                {
                    screen.Render(x * 8, y * 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical);
                }
                else if (x == x1 && y == y1)
                {
                    screen.Render(x * 8, y * 8, 0 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Both);
                }
                else if (y == y0)
                {
                    screen.Render(x * 8, y * 8, 1 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
                }
                else if (y == y1)
                {
                    screen.Render(x * 8, y * 8, 1 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Vertical);
                }
                else if (x == x0)
                {
                    screen.Render(x * 8, y * 8, 2 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.None);
                }
                else if (x == x1)
                {
                    screen.Render(x * 8, y * 8, 2 + 13 * 32, Color.Get(-1, 1, 5, 445), MirrorFlags.Horizontal);
                }
                else
                {
                    screen.Render(x * 8, y * 8, 2 + 13 * 32, Color.Get(5, 5, 5, 5), MirrorFlags.Horizontal);
                }
            }
        }

        Draw(title, screen, x0 * 8 + 8, y0 * 8, Color.Get(5, 5, 5, 550));
    }
}