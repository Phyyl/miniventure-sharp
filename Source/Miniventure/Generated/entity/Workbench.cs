namespace com.mojang.ld22.entity;


public class Workbench : Furniture
{



    public Workbench() : base("Workbench", horizontalRadius: 3, verticalRadius: 2)
    {

        col = Color.Get(-1, 100, 321, 431);
        sprite = 4;
    }


    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Crafting.WorkbenchRecipes, player);

        return true;
    }
}