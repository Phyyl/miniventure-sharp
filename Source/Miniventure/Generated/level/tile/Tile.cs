namespace com.mojang.ld22.level.tile;



public class Tile
{
    public static int TickCount { get; set; } = 0;
    public Random Random { get; } = new(); // Random is used for random numbers (duh!).

    public static Tile[] Tiles = new Tile[256]; // An array of tiles

    public static Tile Grass { get; } = new GrassTile(0); // creates a grass tile with the Id of 0, (I don't need to explain the other simple ones)
    public static Tile Rock { get; } = new RockTile(1);
    public static Tile Water { get; } = new WaterTile(2);
    public static Tile Flower { get; } = new FlowerTile(3);
    public static Tile Tree { get; }= new TreeTile(4);
    public static Tile Dirt { get; }= new DirtTile(5);
    public static Tile Sand { get; } = new SandTile(6);
    public static Tile Cactus { get; } = new CactusTile(7);
    public static Tile Hole { get; } = new HoleTile(8);
    public static Tile TreeSapling { get; } = new SaplingTile(9, Grass, Tree); // tree sapling; plant on grass, eventually becomes a tree
    public static Tile CactusSapling { get; } = new SaplingTile(10, Sand, Cactus); // cactus sapling; plant on sand, eventually becomes a cactus
    public static Tile Farmland { get; } = new FarmTile(11); // farmland (tilled dirt)
    public static Tile Wheat { get; } = new WheatTile(12); //wheat (planted dirt)
    public static Tile Lava { get; } = new LavaTile(13);
    public static Tile StairsDown { get; } = new StairsTile(14, false); // Stairs leading down
    public static Tile StairsUp { get; } = new StairsTile(15, true); // Stairs leading up
    public static Tile InfiniteFall { get; } = new InfiniteFallTile(16); // Air tile in the sky.
    public static Tile Cloud { get; } = new CloudTile(17);
    public static Tile HardRock { get; } = new HardRockTile(18);
    /* Ores */
    public static Tile IronOre { get; } = new OreTile(19, Resource.ironOre);
    public static Tile GoldOre { get; } = new OreTile(20, Resource.goldOre);
    public static Tile GemOre { get; } = new OreTile(21, Resource.gem);
    public static Tile CloudCactus { get; } = new CloudCactusTile(22);

    public readonly byte id;

    public bool connectsToGrass = false;
    public bool connectsToSand = false;
    public bool connectsToLava = false;
    public bool connectsToWater = false;

    public Tile(int id)
    {
        this.id = (byte)id;

        if (Tiles[id] != null)
        {
            throw new Exception("Duplicate tile ids!");
        }

        Tiles[id] = this;
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