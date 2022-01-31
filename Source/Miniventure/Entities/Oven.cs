namespace Miniventure.Entities;

public class Oven : Furniture
{
    public Oven() 
        : base("Oven", 2, Color.Get(-1, 000, 332, 442), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new CraftingMenu(Recipes.OvenRecipes, player);

        return true;
    }
}