using Miniventure.Crafting;
using Miniventure.Graphics;
using Miniventure.UI;

namespace Miniventure.Entities;

public class Furnace : Furniture
{
    public Furnace()
        : base("Furnace", horizontalRadius: 3, verticalRadius: 2)
    {
        col = Color.Get(-1, 000, 222, 333);
        sprite = 3;
    }

    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Recipes.FurnaceRecipes, player);

        return true;
    }
}