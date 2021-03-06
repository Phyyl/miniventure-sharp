namespace com.mojang.ld22.crafting;



public abstract class Recipe : ListItem {
	public List<Item> costs = new List<Item>(); // A list of costs for the recipe
	public bool canCraft = false; // checks if the player can craft the recipe
	public Item resultTemplate; // the result item of the recipe

	public Recipe(Item resultTemplate) {
		this.resultTemplate = resultTemplate; // assigns the result item
	}

	/** Adds a resource cost to the list. requires a type of resource and an amount of it */
	public virtual Recipe addCost(Resource resource, int count) {
		costs.add(new ResourceItem(resource, count)); //adds a resource cost to the list
		return this;
	}

	/** Checks if the player can craft the recipe */
	public virtual void checkCanCraft(Player player) {
		for (int i = 0; i < costs.size(); i++) { //cycles through the costs list
			Item item = costs.get(i); //current item on the list
			if (item is ResourceItem) { //if the item is a params resource[]
				ResourceItem ri = (ResourceItem) item; // Makes a ResourceItem conversion of item.
				if (!player.inventory.hasResources(ri.resource, ri.count)) {//if the player doesn't have the params resources[]
					canCraft = false;//then the player cannot craft the recipe
					return;
				}
			}
		}
		canCraft = true;//else he can craft the recipe
	}

	/** Renders the icon & text of an item to the screen. */
	public virtual void renderInventory(Screen screen, int x, int y) {
		screen.render(x, y, resultTemplate.getSprite(), resultTemplate.getColor(), 0); //renders the sprite of the item
		int textColor = canCraft ? Color.get(-1, 555, 555, 555) : Color.get(-1, 222, 222, 222); // gets the text color based on if the player can craft the item
		Font.draw(resultTemplate.getName(), screen, x + 8, y, textColor); // draws the text to the screen
	}

	public abstract void craft(Player player); // abstract method given to the sub-recipe classes.

	/** removes the resources from your inventory */
	public virtual void deductCost(Player player) {
		for (int i = 0; i < costs.size(); i++) {//loops through the costs
			Item item = costs.get(i);//gets the current item in costs
			if (item is ResourceItem) {//if the item is a params resource[]
				ResourceItem ri = (ResourceItem) item; // Makes a ResourceItem conversion of item.
				player.inventory.removeResource(ri.resource, ri.count); //removes the resources from the player's inventory.
			}
		}
	}
}