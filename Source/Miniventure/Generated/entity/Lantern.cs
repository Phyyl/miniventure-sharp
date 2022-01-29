namespace com.mojang.ld22.entity;


public class Lantern : Furniture {
	
	/* This is a sub-class of furniture.java, go there for more info */
	
	public Lantern() : base("Lantern") { // Name of the lantern
		col = Color.get(-1, 000, 111, 555); // Color of the lantern
		sprite = 5; // Location of the sprite
		xr = 3; // Width of the lantern 
		yr = 2; // Height of the lantern 
	}

	/** Gets the size of the radius for light underground (Bigger number, larger light) */
	public override int getLightRadius() {
		return 8;
	}
}