namespace com.mojang.ld22.entity;


public class Workbench : Furniture {
	
	/* This is a sub-class of furniture.java, go there for more info */
	
	public Workbench() : base("Workbench") { // Name of the Workbench
		col = Color.get(-1, 100, 321, 431); // Color of the workbench
		sprite = 4; // Location of the sprite
		xr = 3; // Width of the workbench
		yr = 2; // Height of the workbench 
	}

	/** This is what occurs when the player uses the "Menu" command near this */
	public override bool use(Player player, int attackDir) {
		player.game.setMenu(new CraftingMenu(Crafting.workbenchRecipes, player)); // Sets the menu to the crafting menu with workbench recipes.
		return true;
	}
}