namespace Miniventure.Entities;

public class Workbench : Furniture
{
    public Workbench() : base("Workbench", horizontalRadius: 3, verticalRadius: 2)
    {
        col = Color.Get(-1, 100, 321, 431);
        sprite = 4;
    }

    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Recipes.WorkbenchRecipes, player);

        return true;
    }
}