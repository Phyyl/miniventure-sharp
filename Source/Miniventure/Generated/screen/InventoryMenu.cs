namespace com.mojang.ld22.screen;


public class InventoryMenu : Menu
{
    private readonly Player player; // The player that this inventory belongs to
    private int selected = 0; // The selected item of the inventory.

    public InventoryMenu(Player player)
    {
        this.player = player; // Assigns the player that this inventory belongs to.

        if (player.activeItem != null)
        { // If the player has an active item, params then[]
            player.inventory.Items.Insert(0, player.activeItem); // that active item will go into the inventory
            player.activeItem = null; // the player will not have an active item anymore.
        }
    }

    public override void Update()
    {
        if (input.Menu.Clicked)
        {
            game.Menu = null; // If the player presses the "Menu" key, then the game will go back to the normal game
        }

        if (input.Up.Clicked)
        {
            selected--; // If the player presses up, then the selection on the list will go up by 1.
        }

        if (input.Down.Clicked)
        {
            selected++; // If the player presses down, then the selection on the list will go down by 1.
        }

        int len = player.inventory.Items.Count; // Counts how many items are in  your inventory
        if (len == 0)
        {
            selected = 0; // If your inventory is 0, then the selected item is 0.
        }

        if (selected < 0)
        {
            selected += len; // If you go past the top item in your inventory, it will loop back to the bottom
        }

        if (selected >= len)
        {
            selected -= len; // If you go past the bottom item in your inventory, it will loop to the top
        }

        if (input.Attack.Clicked && len > 0)
        { // If your inventory is not empty, and the player presses the "Attack" params key[]
            Item item = player.inventory.Items[selected]; // The item will be removed from the inventory
            player.inventory.Items.RemoveAt(selected); // The item will be removed from the inventory
            player.activeItem = item; // and that item will be placed as the player's active item
            game.Menu = null; // the game will go back to the gameplay
        }
    }

    public override void Render(Screen screen)
    {
        Font.RenderFrame(screen, "inventory", 1, 1, 12, 11); // renders the blue box for the inventory
        RenderItemList(screen, 1, 1, 12, 11, player.inventory.Items.ToArray(), selected); // renders the icon's and names of all the items in your inventory.
    }
}