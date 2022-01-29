namespace com.mojang.ld22.entity;


public class Oven : Furniture
{

    /* This is a sub-class of furniture.java, go there for more info */

    public Oven() : base("Oven", horizontalRadius: 3, verticalRadius: 2)
    { // Name of the oven.
        col = Color.Get(-1, 000, 332, 442); // Color of the oven
        sprite = 2; // Location of the sprite
    }

    /** This is what occurs when the player uses the "Menu" command near this */
    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Crafting.OvenRecipes, player);

        return true;
    }
}