using Miniventure.Audio;
using Miniventure.Graphics;

namespace Miniventure.UI;


public class TitleMenu : Menu
{
    private int selected = 0;

    private static readonly string[] options = { "Start game", "How to play", "About" };

    public TitleMenu()
    {
    }

    public override void Update()
    {
        if (input.Up.Clicked)
        {
            selected--;
        }

        if (input.Down.Clicked)
        {
            selected++;
        }

        int len = options.Length;
        if (selected < 0)
        {
            selected += len;
        }

        if (selected >= len)
        {
            selected -= len;
        }

        if (input.Attack.Clicked || input.Menu.Clicked)
        {
            if (selected == 0)
            {
                Sound.test.Play();
                game.ResetGame();
                game.Menu = null;
            }
            if (selected == 1)
            {
                game.Menu = new InstructionsMenu(this);
            }

            if (selected == 2)
            {
                game.Menu = new AboutMenu(this);
            }
        }
    }

    public override void Render(Screen screen)
    {
        screen.Clear(0);



        int h = 2;
        int w = 13;
        int titleColor = Color.Get(0, 010, 131, 551);
        int xo = (screen.Width - w * 8) / 2;
        int yo = 24;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                screen.Render(xo + x * 8, yo + y * 8, x + (y + 6) * 32, titleColor, 0);
            }
        }



        for (int i = 0; i < options.Length; i++)
        {
            string msg = options[i];
            int col = Color.Get(0, 222, 222, 222);
            if (i == selected)
            {
                msg = "> " + msg + " <";
                col = Color.Get(0, 555, 555, 555);
            }
            Font.Draw(msg, screen, (screen.Width - msg.Length * 8) / 2, (8 + i) * 8, col);
        }

        Font.Draw("(Arrow keys,X and C)", screen, 0, screen.Height - 8, Color.Get(0, 111, 111, 111));
    }
}