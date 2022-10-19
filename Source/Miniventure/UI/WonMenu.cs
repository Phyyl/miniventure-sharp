namespace Miniventure.UI;

public class WonMenu : Menu
{
    private int inputDelay = 60;

    public WonMenu()
    {
    }

    public override void Update()
    {
        if (inputDelay > 0)
        {
            inputDelay--;
        }
        else if (input.Attack.Clicked || input.Menu.Clicked)
        {
            game.Menu = new TitleMenu();
        }
    }

    public override void Render(Screen screen)
    {
        Font.RenderFrame(screen, "", 1, 3, 18, 9);
        Font.Draw("You won! Yay!", screen, 2 * 8, 4 * 8, Color.Get(-1, 555, 555, 555));

        int seconds = Game.Instance.state.gameTime / 60;
        int minutes = seconds / 60;
        int hours = minutes / 60;
        minutes %= 60;
        seconds %= 60;

        string timestring;
        if (hours > 0)
        {
            timestring = hours + "h" + (minutes < 10 ? "0" : "") + minutes + "m";
        }
        else
        {
            timestring = minutes + "m " + (seconds < 10 ? "0" : "") + seconds + "s";
        }
        Font.Draw("Time:", screen, 2 * 8, 5 * 8, Color.Get(-1, 555, 555, 555));
        Font.Draw(timestring, screen, (2 + 5) * 8, 5 * 8, Color.Get(-1, 550, 550, 550));
        Font.Draw("Score:", screen, 2 * 8, 6 * 8, Color.Get(-1, 555, 555, 555));
        Font.Draw("" + Game.Instance.state.player.score, screen, (2 + 6) * 8, 6 * 8, Color.Get(-1, 550, 550, 550));
        Font.Draw("Press C to win", screen, 2 * 8, 8 * 8, Color.Get(-1, 333, 333, 333));
    }
}
