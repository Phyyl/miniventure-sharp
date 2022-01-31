using Miniventure.Entities;
using Miniventure.Graphics;
using Miniventure.Items;

namespace Miniventure.UI;


public class ContainerMenu : Menu
{
    private Player player;
    private Inventory container;
    private int selected = 0;
    private string title;
    private int oSelected;
    private int window = 0;


    public ContainerMenu(Player player, string title, Inventory container)
    {
        this.player = player;
        this.title = title;
        this.container = container;
    }

    public override void Update()
    {
        if (input.Menu.Clicked)
        {
            game.Menu = null;
        }

        if (input.Left.Clicked)
        {
            window = 0;
            int tmp = selected;
            selected = oSelected;
            oSelected = tmp;
        }
        if (input.Right.Clicked)
        {
            window = 1;
            int tmp = selected;
            selected = oSelected;
            oSelected = tmp;
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

        Inventory i = window == 1 ? player.inventory : container;
        Inventory i2 = window == 0 ? player.inventory : container;

        int len = i.Items.Count;
        if (input.Up.Clicked)
        {
            selected--;
        }

        if (input.Down.Clicked)
        {
            selected++;
        }

        if (len == 0)
        {
            selected = 0;
        }

        if (selected < 0)
        {
            selected += len;
        }

        if (selected >= len)
        {
            selected -= len;
        }

        if (input.Attack.Clicked && len > 0)
        {
            Item item = i.Items[selected];
            i.Items.RemoveAt(selected);
            i2.Add(oSelected, item);
            if (selected >= i.Items.Count)
            {
                selected = i.Items.Count - 1;
            }
        }
    }

    public override void Render(Screen screen)
    {
        if (window == 1)
        {
            screen.SetOffset(6 * 8, 0);
        }

        Font.RenderFrame(screen, title, 1, 1, 12, 11);
        RenderItemList(screen, 1, 1, 12, 11, container.Items.ToArray(), window == 0 ? selected : -oSelected - 1);

        Font.RenderFrame(screen, "inventory", 13, 1, 13 + 11, 11);
        RenderItemList(screen, 13, 1, 13 + 11, 11, player.inventory.Items.ToArray(), window == 1 ? selected : -oSelected - 1);
        screen.SetOffset(0, 0);
    }
}