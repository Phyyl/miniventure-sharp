namespace com.mojang.ld22.screen;


public class DeadMenu : Menu
{
    private int inputDelay = 60; // variable to delay the input of the player, so they won't skip the dead menu the first second.

    /* WonMenu & DeadMenu are very params similar[] scratch that, the exact same class with text changes. */

    public DeadMenu()
    {
    }

    /** Update Method, 60 updates (ticks) per second */
    public override void Update()
    {
        if (inputDelay > 0) //If the input delay is above 0 (it starts at 60) params then[]
        {
            inputDelay--; // the inputDelay will minus by 1. 
        }
        else if (input.Attack.Clicked || input.Menu.Clicked)
        {
            game.Menu = new TitleMenu(); //If the delay is equal or lower than 0, then the person can go back to the title menu.
        }
    }

    /** Render method, draws stuff on the screen. */
    public override void Render(Screen screen)
    {
        Font.RenderFrame(screen, "", 1, 3, 18, 9); // Draws a box frame based on 4 points. You can include a title as well.
        Font.Draw("You died! Aww!", screen, 2 * 8, 4 * 8, Color.Get(-1, 555, 555, 555)); // Draws text

        int seconds = game.gameTime / 60; // The current amount of seconds in the game.
        int minutes = seconds / 60; // The current amount of minutes in the game.
        int hours = minutes / 60; // The current amount of hours in the game.
        minutes %= 60; // fixes the number of minutes in the game. Without this, 1h 24min would look like: 1h 84min.
        seconds %= 60; // fixes the number of seconds in the game. Without this, 2min 35sec would look like: 2min 155sec.

        string timestring;
        if (hours > 0)
        {
            timestring = hours + "h" + (minutes < 10 ? "0" : "") + minutes + "m";// If over an hour has passed, then it will show hours and minutes.
        }
        else
        {
            timestring = minutes + "m " + (seconds < 10 ? "0" : "") + seconds + "s";// If under an hour has passed, then it will show minutes and seconds.
        }
        Font.Draw("Time:", screen, 2 * 8, 5 * 8, Color.Get(-1, 555, 555, 555));// Draws "Time:" on the frame
        Font.Draw(timestring, screen, (2 + 5) * 8, 5 * 8, Color.Get(-1, 550, 550, 550));// Draws the current time next to "Time:"
        Font.Draw("Score:", screen, 2 * 8, 6 * 8, Color.Get(-1, 555, 555, 555));// Draws "Score:" on the frame
        Font.Draw("" + game.player.score, screen, (2 + 6) * 8, 6 * 8, Color.Get(-1, 550, 550, 550));// Draws the current score next to "Score:"
        Font.Draw("Press C to lose", screen, 2 * 8, 8 * 8, Color.Get(-1, 333, 333, 333));//Draws text
    }
}
