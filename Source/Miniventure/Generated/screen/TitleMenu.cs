namespace com.mojang.ld22.screen;


public class TitleMenu : Menu
{
    private int selected = 0; // Currently selected option

    private static readonly string[] options = { "Start game", "How to play", "About" }; // Options that are on the main menu, each seperated by a comma.

    public TitleMenu()
    {
    }

    public override void Update()
    {
        if (input.up.clicked)
        {
            selected--; // If the player presses the up key, then move up 1 option in the list
        }

        if (input.down.clicked)
        {
            selected++; // If the player presses the down key, then move down 1 option in the list
        }

        int len = options.Length; // The size of the list (normally 3 options)
        if (selected < 0)
        {
            selected += len; // If the selected option is less than 0, then move it to the last option of the list.
        }

        if (selected >= len)
        {
            selected -= len; // If the selected option is more than or equal to the size of the list, then move it back to 0;
        }

        if (input.attack.clicked || input.menu.clicked)
        { //If either the "Attack" or "Menu" keys are pressed params then[]
            if (selected == 0)
            { //If the selection is 0 ("Start game")
                Sound.test.Play(); //Play a sound
                game.ResetGame(); //Reset the game
                game.Menu = null; //Set the menu to null (No menus active)
            }
            if (selected == 1)
            {
                game.Menu = new InstructionsMenu(this); //If the selection is 1 ("How to play") then go to the instructions menu.
            }

            if (selected == 2)
            {
                game.Menu = new AboutMenu(this); //If the selection is 2 ("About") then go to the about menu.
            }
        }
    }

    public override void Render(Screen screen)
    {
        screen.Clear(0);// Clears the screen to a black color.

        /* This section is used to display the minicraft title */

        int h = 2; // Height of squares (on the spritesheet)
        int w = 13; // Width of squares (on the spritesheet)
        int titleColor = Color.Get(0, 010, 131, 551); //Colors of the title
        int xo = (screen.Width - (w * 8)) / 2; // X location of the title
        int yo = 24; // Y location of the title
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                screen.Render(xo + (x * 8), yo + (y * 8), x + ((y + 6) * 32), titleColor, 0); // Loops through all the squares to render them all on the screen.
            }
        }

        /* This section is used to display this options on the screen */

        for (int i = 0; i < options.Length; i++)
        { // Loops through all the options in the list
            string msg = options[i]; // Text of the current option
            int col = Color.Get(0, 222, 222, 222); // Color of unselected text
            if (i == selected)
            { // If the current option is the option that is selected
                msg = "> " + msg + " <"; // Add the cursors to the sides of the message
                col = Color.Get(0, 555, 555, 555); // change the color of the option
            }
            Font.Draw(msg, screen, (screen.Width - (msg.Length * 8)) / 2, (8 + i) * 8, col); // Draw the current option to the screen
        }

        Font.Draw("(Arrow keys,X and C)", screen, 0, screen.Height - 8, Color.Get(0, 111, 111, 111)); // Draw text at the bottom
    }
}