namespace com.mojang.ld22.screen;


public class ContainerMenu : Menu
{
    private readonly Player player; // The player that is looking inside the chest
    private readonly Inventory container; // The inventory of the chest
    private int selected = 0; // The selected item
    private readonly string title; // The title of the chest
    private int oSelected; // the old selected option (this is used to temporarily save spots moving from chest to inventory & vice-versa)
    private int window = 0; // currently selected window (player's inventory, or chest's inventory)

    /** The container menu class is used for chests */
    public ContainerMenu(Player player, string title, Inventory container)
    {
        this.player = player; // Player looking inside the chest
        this.title = title; // Title of the chest
        this.container = container; // Inventory of the chest
    }

    public override void Update()
    {
        if (input.Menu.Clicked)
        {
            game.Menu = null; // If the player selects the "menu" key, then it will exit the chest
        }

        if (input.Left.Clicked)
        { //if the left key is params pressed[]
            window = 0; // The current window will be of the chest
            int tmp = selected; // temp integer will be the currently selected
            selected = oSelected; // selected will become the oSelected
            oSelected = tmp; // oSelected will become the temp integer (save spot for when you switch)
        }
        if (input.Right.Clicked)
        { //if the right key is params pressed[]
            window = 1; // The current window will be of the player's inventory
            int tmp = selected; // temp integer will be the currently selected
            selected = oSelected; // selected will become the oSelected
            oSelected = tmp; // oSelected will become the temp integer (save spot for when you switch)
        }

        /* Below is an example of the "ternary operator"  
		  
		  which is read like (example): 
		  'bool statement ? true result : false result;'
		  
		  It's the exact same thing as:
		  
		  if (bool statement) {
    		true result;
			} else {
    		false result;
		  }
		  
		  It's just for convince sake, Google " ternary operator " for more info
		  
		 * */

        Inventory i = window == 1 ? player.inventory : container; // If the window is equal to 1, then the main inventory is the player inventory, else it's the chest's
        Inventory i2 = window == 0 ? player.inventory : container; // If the window is equal to 0, then the backup inventory is the player inventory, else it's the chest's

        int len = i.Items.Count; // Size of the main inventory
        if (input.Up.Clicked)
        {
            selected--; // If the up key is press then the selection will go up one item
        }

        if (input.Down.Clicked)
        {
            selected++; // If the down key is pressed then the selection will go down one item
        }

        if (len == 0)
        {
            selected = 0; // If the size of the inventory is 0, then it will stay selected at 0
        }

        if (selected < 0)
        {
            selected += len; // If the current selection is less than 0 (first entry) than it will loop to the bottom.
        }

        if (selected >= len)
        {
            selected -= len; // If the current selection goes past the bottom entry, then it will loop to the top.
        }

        if (input.Attack.Clicked && len > 0)
        {
            Item item = i.Items[selected];
            i.Items.RemoveAt(selected);
            i2.Add(oSelected, item);
            if (selected >= i.Items.Count)
            {
                selected = i.Items.Count - 1; // This fixes the selected item to the latest one.
            }
        }
    }

    public override void Render(Screen screen)
    {
        if (window == 1)
        {
            screen.SetOffset(6 * 8, 0); // Offsets the windows for when the player's inventory is selected
        }

        Font.RenderFrame(screen, title, 1, 1, 12, 11); // Renders the chest's window
        RenderItemList(screen, 1, 1, 12, 11, container.Items.ToArray(), window == 0 ? selected : -oSelected - 1); // renders all the items from the chest's inventory

        Font.RenderFrame(screen, "inventory", 13, 1, 13 + 11, 11); // renders the player's inventory
        RenderItemList(screen, 13, 1, 13 + 11, 11, player.inventory.Items.ToArray(), window == 1 ? selected : -oSelected - 1); // renders all the items from the player's inventory
        screen.SetOffset(0, 0); // Fixes the offset back to normal
    }
}