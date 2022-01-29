namespace com.mojang.ld22.entity;


public class Furnace : Furniture {
	
	/* This is a sub-class of furniture.java, go there for more info */
	
	public Furnace() : base("Furnace") { // Name of the furnace
		col = Color.get(-1, 000, 222, 333); // Color of the furnace
		sprite = 3; // Location of the sprite
		xr = 3; // Width of the furnace (in-game, not sprite) 
		yr = 2; // Height of the furnace (in-game, not sprite) 
	}

	/** This is what occurs when the player uses the "Menu" command near this */
	public override bool use(Player player, int attackDir) {
		player.game.setMenu(new CraftingMenu(Crafting.furnaceRecipes, player)); // Sets the menu to the crafting menu with furnace recipes.
		return true;
	}
}