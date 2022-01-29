namespace com.mojang.ld22.screen;


public class InstructionsMenu : Menu
{
    private Menu parent; // Creates a parent object to go back to

    /** The about menu is a read menu about what you have to do in the game. Only contains text and a black background */
    public InstructionsMenu(Menu parent)
    {
        this.parent = parent; // The parent Menu that it will go back to.
    }

    public override void Update()
    {
        if (input.attack.clicked || input.menu.clicked)
        {
            game.Menu= parent;  // If the user presses the "Attack" or "Menu" button, it will go back to the parent menu.
        }
    }

    /** Renders the text on the screen */
    public override void Render(Screen screen)
    {
        screen.Clear(0); // clears the screen to be a black color.

        /* Font.draw Parameters: Font.draw(string text, Screen screen, int x, int y, int color) */

        Font.Draw("HOW TO PLAY", screen, (4 * 8) + 4, 1 * 8, Color.Get(0, 555, 555, 555)); //draws Title text
        Font.Draw("Move your character", screen, (0 * 8) + 4, 3 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("with the arrow keys", screen, (0 * 8) + 4, 4 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("press C to attack", screen, (0 * 8) + 4, 5 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("and X to open the", screen, (0 * 8) + 4, 6 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("inventory and to", screen, (0 * 8) + 4, 7 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("use items.", screen, (0 * 8) + 4, 8 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("Select an item in", screen, (0 * 8) + 4, 9 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("the inventory to", screen, (0 * 8) + 4, 10 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("equip it.", screen, (0 * 8) + 4, 11 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("Kill the air wizard", screen, (0 * 8) + 4, 12 * 8, Color.Get(0, 333, 333, 333)); // draws text
        Font.Draw("to win the game!", screen, (0 * 8) + 4, 13 * 8, Color.Get(0, 333, 333, 333)); // draws text
    }
}
