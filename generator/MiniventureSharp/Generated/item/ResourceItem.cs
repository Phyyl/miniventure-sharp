namespace com.mojang.ld22.item;


public class ResourceItem : Item {
	public Resource resource; // The resource of this item
	public int count = 1; // The amount of resources

	public ResourceItem(Resource resource) {
		this.resource = resource; //assigns the resource
	}

	public ResourceItem(Resource resource, int count) {
		this.resource = resource; //assigns the resource
		this.count = count; //assigns the count
	}

	/** Gets the color of the resource */
	public override int getColor() {
		return resource.color;
	}
	
	/** Gets the sprite of the resource */
	public override int getSprite() {
		return resource.sprite;
	}

	/** Renders the icon used for the resource */
	public override void renderIcon(Screen screen, int x, int y) {
		screen.render(x, y, resource.sprite, resource.color, 0); // renders the icon
	}

	/** Renders the icon, name, and count of the resource */
	public override void renderInventory(Screen screen, int x, int y) {
		screen.render(x, y, resource.sprite, resource.color, 0); // renders the icon
		Font.draw(resource.name, screen, x + 32, y, Color.get(-1, 555, 555, 555)); // draws the name of the resource
		int cc = count; // count of the resource
		if (cc > 999) cc = 999; // If the resource count is above 999, then just render 999 (for spacing reasons)
		Font.draw("" + cc, screen, x + 8, y, Color.get(-1, 444, 444, 444));// draws the resource count
	}

	/** Gets the name of the resource */
	public override string getName() {
		return resource.name;
	}

	/** What happens when you pick up the item off the ground */
	public override void onTake(ItemEntity itemEntity) {
	}

	/** What happens when you interact and item with the world */
	public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir) {
		if (resource.interactOn(tile, level, xt, yt, player, attackDir)) { // Calls the resource's 'interactOn()' method, if true params then[]
			count--; // minuses the count by 1.
			return true;
		}
		return false;
	}

	/** If the count is equal to, or less than 0. Then this will return true. */
	public override bool isDepleted() {
		return count <= 0;
	}

}