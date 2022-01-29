namespace com.mojang.ld22.level.tile;


public class InfiniteFallTile : Tile {
	
	/* This will be easy :D */
	
	public InfiniteFallTile(int id) : base(id) { // assigns the id
	}

	/** Infinite fall tile doesn't render anything! */
	public override void render(Screen screen, Level level, int x, int y) {
	}

	/** Update method, updates (ticks) 60 times a second */
	public override void tick(Level level, int xt, int yt) {
	}

	/** Determines if an entity can pass through this tile */
	public override bool mayPass(Level level, int x, int y, Entity e) {
		if (e is AirWizard) return true; // If the entity is an Air Wizard, than it can pass through
		return false; // else the entity can't pass through
	}
}
