namespace com.mojang.ld22.item;


public class PowerGloveItem : Item {
	
	public override int getColor() {
		return Color.get(-1, 100, 320, 430); //sets the color of the powerglove
	}

	public override int getSprite() {
		return 7 + 4 * 32; //returns the location of the sprite(image of the glove)
	}

	public override void renderIcon(Screen screen, int x, int y) {
		screen.render(x, y, getSprite(), getColor(), 0); // Renders the icon of the power glove to the screen
	}

	public override void renderInventory(Screen screen, int x, int y) {
		screen.render(x, y, getSprite(), getColor(), 0); // renders the icon of the power glove to the screen
		Font.draw(getName(), screen, x + 8, y, Color.get(-1, 555, 555, 555)); // renders the name of the powerglove to the screen
	}

	public override string getName() {
		return "Pow glove"; //returns the name of the glove
	}

	public override bool interact(Player player, Entity entity, int attackDir) {
		if (entity is Furniture) { // If the power glove hits a furnace
			Furniture f = (Furniture) entity; // Assigns the furniture
			f.take(player); // Takes (picks up) the furniture
			return true; // Method returns true
		}
		return false; // method returns false if it did not hit a furniture entity.
	}
}
