namespace com.mojang.ld22.level.tile;



public class Tile {
	public static int tickCount = 0; //A global tickCount used in the Lava & water tiles.
	public Random random = new Random(); // Random is used for random numbers (duh!).

	public static Tile[] tiles = new Tile[256]; // An array of tiles
	public static Tile grass = new GrassTile(0); // creates a grass tile with the Id of 0, (I don't need to explain the other simple ones)
	public static Tile rock = new RockTile(1); 
	public static Tile water = new WaterTile(2); 
	public static Tile flower = new FlowerTile(3); 
	public static Tile tree = new TreeTile(4); 
	public static Tile dirt = new DirtTile(5); 
	public static Tile sand = new SandTile(6); 
	public static Tile cactus = new CactusTile(7); 
	public static Tile hole = new HoleTile(8);
	public static Tile treeSapling = new SaplingTile(9, grass, tree); // tree sapling; plant on grass, eventually becomes a tree
	public static Tile cactusSapling = new SaplingTile(10, sand, cactus); // cactus sapling; plant on sand, eventually becomes a cactus
	public static Tile farmland = new FarmTile(11); // farmland (tilled dirt)
	public static Tile wheat = new WheatTile(12); //wheat (planted dirt)
	public static Tile lava = new LavaTile(13);
	public static Tile stairsDown = new StairsTile(14, false); // Stairs leading down
	public static Tile stairsUp = new StairsTile(15, true); // Stairs leading up
	public static Tile infiniteFall = new InfiniteFallTile(16); // Air tile in the sky.
	public static Tile cloud = new CloudTile(17);
	public static Tile hardRock = new HardRockTile(18);
	/* Ores */
	public static Tile ironOre = new OreTile(19, Resource.ironOre); 
	public static Tile goldOre = new OreTile(20, Resource.goldOre);
	public static Tile gemOre = new OreTile(21, Resource.gem);
	public static Tile cloudCactus = new CloudCactusTile(22);

	public readonly byte id; // Id of the tile

	public bool connectsToGrass = false; // sees if the tile connects to grass
	public bool connectsToSand = false; // sees if the tile connects to sand
	public bool connectsToLava = false; // sees if the tile connects to lava
	public bool connectsToWater = false; // sees if the tile connects to water

	public Tile(int id) {
		this.id = (byte) id; // creates the id
		if (tiles[id] != null) throw new Exception("Duplicate tile ids!"); // You cannot have over-lapping ids
		tiles[id] = this; // Assigns the id
	}

	/** Render method, used in sub-classes */
	public virtual void render(Screen screen, Level level, int x, int y) {
	}

	/** Returns if the player can walk on it, overrides in sub-classes  */
	public virtual bool mayPass(Level level, int x, int y, Entity e) {
		return true;
	}

	/** Gets the light radius of a tile, Bigger number = bigger circle */
	public virtual int getLightRadius(Level level, int x, int y) {
		return 0;
	}

	/** What happens when you hit the tile (ex: punching a tree) */
	public virtual void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir) {
	}

	/** What happens when you run into the tile (ex: run into a cactus) */
	public virtual void bumpedInto(Level level, int xt, int yt, Entity entity) {
	}

	/** Update method */
	public virtual void tick(Level level, int xt, int yt) {
	}

	/** What happens when you are inside the tile (ex: lava) */
	public virtual void steppedOn(Level level, int xt, int yt, Entity entity) {
	}

	/** What happens when you hit an item on a tile (ex: Pickaxe on rock) */
	public virtual bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir) {
		return false;
	}

	/** Sees if the tile connects to Water or Lava. */
	public virtual bool connectsToLiquid() {
		return connectsToWater || connectsToLava;
	}
}