namespace com.mojang.ld22.level.tile;



public class Tile
{
    public static int TickCount = 0;
    public Random random = new();

    public static Tile[] tiles = new Tile[256];

    public static Tile grass { get; } = new GrassTile(0);
    public static Tile rock { get; } = new RockTile(1);
    public static Tile water { get; } = new WaterTile(2);
    public static Tile flower { get; } = new FlowerTile(3);
    public static Tile tree { get; } = new TreeTile(4);
    public static Tile dirt { get; } = new DirtTile(5);
    public static Tile sand { get; } = new SandTile(6);
    public static Tile cactus { get; } = new CactusTile(7);
    public static Tile hole { get; } = new HoleTile(8);
    public static Tile treeSapling { get; } = new SaplingTile(9, grass, tree);
    public static Tile cactusSapling { get; } = new SaplingTile(10, sand, cactus);
    public static Tile farmland { get; } = new FarmTile(11);
    public static Tile wheat { get; } = new WheatTile(12);
    public static Tile lava { get; } = new LavaTile(13);
    public static Tile stairsDown { get; } = new StairsTile(14, false);
    public static Tile stairsUp { get; } = new StairsTile(15, true);
    public static Tile infiniteFall { get; } = new InfiniteFallTile(16);
    public static Tile cloud { get; } = new CloudTile(17);
    public static Tile hardRock { get; } = new HardRockTile(18);

    public static Tile ironOre { get; } = new OreTile(19, Resource.ironOre);
    public static Tile goldOre { get; } = new OreTile(20, Resource.goldOre);
    public static Tile gemOre { get; } = new OreTile(21, Resource.gem);
    public static Tile cloudCactus { get; } = new CloudCactusTile(22);

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