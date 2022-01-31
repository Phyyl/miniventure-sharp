namespace Miniventure.Entities;

public class Oven : Furniture
{
    public Oven() : base("Oven", horizontalRadius: 3, verticalRadius: 2)
    {
        col = Color.Get(-1, 000, 332, 442);
        sprite = 2;
    }

    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Recipes.OvenRecipes, player);

        return true;
    }
}