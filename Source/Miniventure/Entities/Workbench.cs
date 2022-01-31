namespace Miniventure.Entities;

public class Workbench : Furniture
{
    public Workbench()
        : base("Workbench", 4, Color.Get(-1, 100, 321, 431), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new CraftingMenu(Recipes.WorkbenchRecipes, player);

        return true;
    }
}