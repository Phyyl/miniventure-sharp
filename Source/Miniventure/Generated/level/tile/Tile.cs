namespace com.mojang.ld22.level.tile;



public class Tile
{
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

    public readonly byte id;

    public bool connectsToGrass = false;
    public bool connectsToSand = false;
    public bool connectsToLava = false;
    public bool connectsToWater = false;

    public Tile(int id)
    {
        this.id = (byte)id;

        if (tiles[id] != null)
        {
            throw new Exception("Duplicate tile ids!");
        }

        tiles[id] = this;
    }

    public virtual void Render(Screen screen, Level level, int x, int y)
    {
    }

    public virtual bool MayPass(Level level, int x, int y, Entity e)
    {
        return true;
    }

    public virtual int GetLightRadius(Level level, int x, int y)
    {
        return 0;
    }

    public virtual void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
    }

    public virtual void BumpedInto(Level level, int xt, int yt, Entity entity)
    {
    }

    public virtual void Update(Level level, int xt, int yt)
    {
    }

    public virtual void SteppedOn(Level level, int xt, int yt, Entity entity)
    {
    }

    public virtual bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        return false;
    }

    public virtual bool ConnectsToLiquid()
    {
        return connectsToWater || connectsToLava;
    }
}