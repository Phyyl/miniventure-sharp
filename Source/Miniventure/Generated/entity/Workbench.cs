namespace com.mojang.ld22.entity;


public class Workbench : Furniture
{

    /* This is a sub-class of furniture.java, go there for more info */

    public Workbench() : base("Workbench", horizontalRadius: 3, verticalRadius: 2)
    { // Name of the Workbench
        //TODO: col should be sent through constructor
        col = Color.Get(-1, 100, 321, 431); // Color of the workbench
        sprite = 4; // Location of the sprite
    }

    /** This is what occurs when the player uses the "Menu" command near this */
    public override bool Use(Player player, Direction attackDir)
    {
        player.game.Menu = new CraftingMenu(Crafting.WorkbenchRecipes, player);

        return true;
    }
}