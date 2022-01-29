namespace com.mojang.ld22.screen;


public class AboutMenu : Menu
{
    private Menu parent; // Creates a parent object to go back to

    /** The about menu is a read menu about what the game was made for. Only contains text and a black background */
    public AboutMenu(Menu parent)
    {
        this.parent = parent; // The parent Menu that it will go back to.
    }

    /** The update method. 60 updates per second. */
    public override void Update()
    {
        if (input.attack.clicked || input.menu.clicked)
        {
            game.Menu = parent; // If the user presses the "Attack" or "Menu" button, it will go back to the parent menu.
        }
    }

    /** Renders the text on the screen */
    public override void Render(Screen screen)
    {
        screen.Clear(0); // clears the screen to be a black color.

        /* Font.draw Parameters: Font.draw(string text, Screen screen, int x, int y, int color) */

        Font.Draw("About Minicraft", screen, (2 * 8) + 4, 1 * 8, Color.Get(0, 555, 555, 555)); //draws Title text
        Font.Draw("Minicraft was made", screen, (0 * 8) + 4, 3 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("by Markus Persson", screen, (0 * 8) + 4, 4 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("For the 22'nd ludum", screen, (0 * 8) + 4, 5 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("dare competition in", screen, (0 * 8) + 4, 6 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("december 2011.", screen, (0 * 8) + 4, 7 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("it is dedicated to", screen, (0 * 8) + 4, 9 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("my father. <3", screen, (0 * 8) + 4, 10 * 8, Color.Get(0, 333, 333, 333)); // draws text
    }
}
