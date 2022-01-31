namespace Miniventure.Entities;

public class Furnace : Furniture
{
    public Furnace()
        : base("Furnace", 3, Color.Get(-1, 000, 222, 333), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new CraftingMenu(Recipes.FurnaceRecipes, player);

        return true;
    }
}