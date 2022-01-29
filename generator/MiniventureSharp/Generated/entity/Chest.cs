namespace com.mojang.ld22.entity;


public class Chest : Furniture {
	public Inventory inventory = new Inventory(); // Inventory of the chest

	/* This is a sub-class of furniture.java, go there for more info */
	
	public Chest() : base("Chest") { //Name of the chest
		col = Color.get(-1, 110, 331, 552); // Color of the chest
		sprite = 1; // Location of the sprite
	}

	/** This is what occurs when the player uses the "Menu" command near this */
	public override bool use(Player player, int attackDir) {
		player.game.setMenu(new ContainerMenu(player, "Chest", inventory)); // Opens up a menu with the player's inventory and the chest's inventory
		return true;
	}
}