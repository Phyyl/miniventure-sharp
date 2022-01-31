namespace com.mojang.ld22.screen;


public class InventoryMenu : Menu
{
    private Player player;
    private int selected = 0;

    public InventoryMenu(Player player)
    {
        this.player = player;

        if (player.activeItem != null)
        {
            player.inventory.Items.Insert(0, player.activeItem);
            player.activeItem = null;
        }
    }

    public override void Update()
    {
        if (input.Menu.Clicked)
        {
            game.Menu = null;
        }

        if (input.Up.Clicked)
        {
            selected--;
        }

        if (input.Down.Clicked)
        {
            selected++;
        }

        int len = player.inventory.Items.Count;
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
            Item item = player.inventory.Items[selected];
            player.inventory.Items.RemoveAt(selected);
            player.activeItem = item;
            game.Menu = null;
        }
    }

    public override void Render(Screen screen)
    {
        Font.RenderFrame(screen, "inventory", 1, 1, 12, 11);
        RenderItemList(screen, 1, 1, 12, 11, player.inventory.Items.ToArray(), selected);
    }
}