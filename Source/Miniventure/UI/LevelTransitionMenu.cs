namespace Miniventure.UI;


public class LevelTransitionMenu : Menu
{
    private int dir;
    private int time = 0;

    public LevelTransitionMenu(int dir)
    {
        this.dir = dir;
    }

    public override void Update()
    {
        time += 2;
        if (time == 30)
        {
            game.ChangeLevel(dir);
        }

        if (time == 60)
        {
            game.Menu = null;
        }
    }

    public override void Render(Screen screen)
    {
        for (int x = 0; x < Game.GameWidth / 3; x++)
        {
            for (int y = 0; y < Game.GameHeight / 3; y++)
            {
                int dd = y + x % 2 * 2 + x / 3 - time;
                if (dd < 0 && dd > -30)
                {
                    if (dir > 0)
                    {
                        screen.Render(x * 8, y * 8, 0, 0, 0);
                    }
                    else
                    {
                        screen.Render(x * 8, screen.Height - y * 8 - 8, 0, 0, 0);
                    }
                }
            }
        }
    }
}
