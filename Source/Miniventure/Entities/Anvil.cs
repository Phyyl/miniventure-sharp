using Miniventure.Graphics;
using Miniventure.UI;

namespace Miniventure.Entities;


public class Anvil : Furniture
{
    public Anvil()
        : base("Anvil", horizontalRadius: 3, verticalRadius: 2)
    {
        col = Color.Get(-1, 000, 111, 222);
        sprite = 0;
    }

    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Crafting.Recipes.AnvilRecipes, player);

        return true;
    }
}