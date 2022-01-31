namespace Miniventure.Levels.Tiles;

public record class Tile
{
    public static int TickCount { get; set; } = 0;
    public Random random = new();

    public static Tile[] tiles = new Tile[256];

    public static EnumDictionary<byte, Tile> All { get; } = new(v => v.ID);

    public static readonly Tile Grass = new GrassTile(0);
    public static readonly Tile Rock = new RockTile(1);
    public static readonly Tile Water = new WaterTile(2);
    public static readonly Tile Flower = new FlowerTile(3);
    public static readonly Tile Tree = new TreeTile(4);
    public static readonly Tile Dirt = new DirtTile(5);
    public static readonly Tile Sand = new SandTile(6);
    public static readonly Tile Cactus = new CactusTile(7);
    public static readonly Tile Hole = new HoleTile(8);
    public static readonly Tile TreeSapling = new SaplingTile(9, Grass, Tree);
    public static readonly Tile CactusSapling = new SaplingTile(10, Sand, Cactus);
    public static readonly Tile Farmland = new FarmTile(11);
    public static readonly Tile Wheat = new WheatTile(12);
    public static readonly Tile Lava = new LavaTile(13);
    public static readonly Tile StairsDown = new StairsTile(14, false);
    public static readonly Tile StairsUp = new StairsTile(15, true);
    public static readonly Tile InfiniteFall = new InfiniteFallTile(16);
    public static readonly Tile Cloud = new CloudTile(17);
    public static readonly Tile HardRock = new HardRockTile(18);
    public static readonly Tile IronOre = new OreTile(19, Resource.IronOre);
    public static readonly Tile GoldOre = new OreTile(20, Resource.GoldOre);
    public static readonly Tile GemOre = new OreTile(21, Resource.Gem);
    public static readonly Tile CloudCactus = new CloudCactusTile(22);

    public bool ConnectsToGrass { get; }
    public bool ConnectsToSand { get; }
    public bool ConnectsToLava { get; }
    public bool ConnectsToWater { get; }

    public byte ID { get; }

    public Tile(byte id, bool connectsToGrass = false, bool connectsToSand = false, bool connectsToLava = false, bool connectsToWater = false)
    {
        ID = id;
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