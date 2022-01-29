namespace com.mojang.ld22.entity;


public class Furniture : Entity {
	private int pushTime = 0; // time for each push.
	private int pushDir = -1; // the direction to push the furniture
	public int col, sprite; // color and sprite variables
	public string name; // name of the furniture
	private Player shouldTake; // the player that should take the furniture

	public Furniture(string name) {
		this.name = name; // assigns the name
		xr = 3; // the x radius of the furniture
		yr = 3; // the y radius of the furniture
	}

	/** Update method, updates (ticks) 60 times a second */
	public override void tick() {
		if (shouldTake != null) { // if the player that should take this params exists[]
			if (shouldTake.activeItem is PowerGloveItem) { // if the player's current item is the power params glove[]
				remove(); // remove this from the world
				shouldTake.inventory.add(0, shouldTake.activeItem); // puts the power glove into the player's inventory
				shouldTake.activeItem = new FurnitureItem(this); // make this the player's current item.
			}
			shouldTake = null; // the player is now dereferenced.
		}
		if (pushDir == 0) move(0, +1); // if the pushDir is 0, then move down
		if (pushDir == 1) move(0, -1); // if the pushDir is 1, then move up
		if (pushDir == 2) move(-1, 0); // if the pushDir is 2, then move left
		if (pushDir == 3) move(+1, 0); // if the pushDir is 3, then move right
		pushDir = -1; // makes pushDir -1 so it won't repeat itself.
		if (pushTime > 0) pushTime--; // if pushTime is larger than 0, then subtract pushTime by 1.
	}

	/** Draws the furniture to the screen */
	public override void render(Screen screen) {
		screen.render(x - 8, y - 8 - 4, sprite * 2 + 8 * 32, col, 0); // renders the top-left part of the furniture.
		screen.render(x - 0, y - 8 - 4, sprite * 2 + 8 * 32 + 1, col, 0); // renders the top-right part of the furniture.
		screen.render(x - 8, y - 0 - 4, sprite * 2 + 8 * 32 + 32, col, 0); // renders the bottom-left part of the furniture.
		screen.render(x - 0, y - 0 - 4, sprite * 2 + 8 * 32 + 33, col, 0); // renders the bottom-right part of the furniture.
	}

	/** Determines if this entity can block others */
	public override bool blocks(Entity e) {
		return true; // yes this can block your way (Needed for pushing)
	}

	/** What happens when this is touched by another entity */
	 //@Override
	public virtual void touchedBy(Entity entity) {
		if (entity is Player && pushTime == 0) { // if the entity is the player, and push time equals params 0[]
			pushDir = ((Player) entity).dir; // pushDir is equal to the direction that the player is
			pushTime = 10; // pushTime equals 10
		}
	}

	/** Used in PowerGloveItem.java */
	public virtual void take(Player player) {
		shouldTake = player; // assigns the player that should take this
	}
}