namespace Miniventure.Levels.Tiles;

public record class Tile : Enumeration<byte, Tile>
{
    public static int TickCount { get; set; } = 0;
    public Random random = new();

    public static Tile[] tiles = new Tile[256];

    public static Tile Grass { get; } = new GrassTile(0);
    public static Tile Rock { get; } = new RockTile(1);
    public static Tile Water { get; } = new WaterTile(2);
    public static Tile Flower { get; } = new FlowerTile(3);
    public static Tile Tree { get; } = new TreeTile(4);
    public static Tile Dirt { get; } = new DirtTile(5);
    public static Tile Sand { get; } = new SandTile(6);
    public static Tile Cactus { get; } = new CactusTile(7);
    public static Tile Hole { get; } = new HoleTile(8);
    public static Tile TreeSapling { get; } = new SaplingTile(9, Grass, Tree);
    public static Tile CactusSapling { get; } = new SaplingTile(10, Sand, Cactus);
    public static Tile Farmland { get; } = new FarmTile(11);
    public static Tile Wheat { get; } = new WheatTile(12);
    public static Tile Lava { get; } = new LavaTile(13);
    public static Tile StairsDown { get; } = new StairsTile(14, false);
    public static Tile StairsUp { get; } = new StairsTile(15, true);
    public static Tile InfiniteFall { get; } = new InfiniteFallTile(16);
    public static Tile Cloud { get; } = new CloudTile(17);
    public static Tile HardRock { get; } = new HardRockTile(18);

    public static Tile IronOre { get; } = new OreTile(19, Resource.IronOre);
    public static Tile GoldOre { get; } = new OreTile(20, Resource.GoldOre);
    public static Tile GemOre { get; } = new OreTile(21, Resource.Gem);
    public static Tile CloudCactus { get; } = new CloudCactusTile(22);

    public bool ConnectsToGrass { get; }
    public bool ConnectsToSand { get; }
    public bool ConnectsToLava { get; }
    public bool ConnectsToWater { get; }

    public Tile(byte id, bool connectsToGrass = false, bool connectsToSand = false, bool connectsToLava = false, bool connectsToWater = false)
        : base(id)
    {
        ConnectsToGrass = connectsToGrass;
        ConnectsToSand = connectsToSand;
        ConnectsToLava = connectsToLava;
        ConnectsToWater = connectsToWater;

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
        return ConnectsToWater || ConnectsToLava;
    }
}