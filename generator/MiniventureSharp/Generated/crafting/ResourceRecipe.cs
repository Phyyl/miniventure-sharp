namespace com.mojang.ld22.crafting;


public class ResourceRecipe : Recipe {
	private Resource resource; //The resource used in this recipe

	/** Adds a recipe to craft a resource */
	public ResourceRecipe(Resource resource) : base(new ResourceItem(resource, 1)) { //this goes through Recipe.java to be put on a list.
		this.resource = resource; //resource to be added
	}

	/** Adds the resource into your inventory */
	public override void craft(Player player) {
		player.inventory.add(0, new ResourceItem(resource, 1)); //adds the resource
		}
}
