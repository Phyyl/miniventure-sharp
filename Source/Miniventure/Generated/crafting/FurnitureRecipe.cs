namespace com.mojang.ld22.crafting;


public class FurnitureRecipe : Recipe {
	private Type<Furniture> clazz; // class of the furniture

	public FurnitureRecipe(Type<Furniture> clazz) : base(new FurnitureItem(clazz.newInstance())) { // assigns the furniture by class
		this.clazz = clazz; // assigns the class
	}

	public override void craft(Player player) {
		try {
			player.inventory.add(0, new FurnitureItem(clazz.newInstance())); // crafts the furniture item into the player's inventory
		} catch (Exception e) {
			throw; // else it will throw an error
		}
	}
}
