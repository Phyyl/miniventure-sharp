namespace Miniventure.Entities;

public class Anvil : Furniture
{
    public Anvil()
        : base("Anvil", 0, Color.Get(-1, 000, 111, 222), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override bool Use(Player player, Direction attackDir)
    {
        Game.Instance.Menu = new CraftingMenu(Crafting.Recipes.AnvilRecipes, player);

        return true;
    }
}