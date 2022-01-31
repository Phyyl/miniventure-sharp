namespace Miniventure.Items;

public record class Resource(string Name, int Sprite, int Color) : Enumeration<Resource>
{
    public static Resource Wood { get; } = new("Wood", 1 + 4 * 32, Graphics.Color.Get(-1, 200, 531, 430));
    public static Resource Stone { get; } = new("Stone", 2 + 4 * 32, Graphics.Color.Get(-1, 111, 333, 555));
    public static Resource Flower { get; } = new PlantableResource("Flower", 0 + 4 * 32, Graphics.Color.Get(-1, 10, 444, 330), Tile.Flower, Tile.Grass);
    public static Resource Acorn { get; } = new PlantableResource("Acorn", 3 + 4 * 32, Graphics.Color.Get(-1, 100, 531, 320), Tile.TreeSapling, Tile.Grass);
    public static Resource Dirt { get; } = new PlantableResource("Dirt", 2 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 432), Tile.Dirt, Tile.Hole, Tile.Water, Tile.Lava);
    public static Resource Sand { get; } = new PlantableResource("Sand", 2 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 550), Tile.Sand, Tile.Grass, Tile.Dirt);
    public static Resource CactusFlower { get; } = new PlantableResource("Cactus", 4 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.CactusSapling, Tile.Sand);
    public static Resource Seeds { get; } = new PlantableResource("Seeds", 5 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.Wheat, Tile.Farmland);
    public static Resource Wheat { get; } = new("Wheat", 6 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550));
    public static Resource bread { get; } = new FoodResource("Bread", 8 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550), 2, 5);
    public static Resource Apple { get; } = new FoodResource("Apple", 9 + 4 * 32, Graphics.Color.Get(-1, 100, 300, 500), 1, 5);

    public static Resource Coal { get; } = new("COAL", 10 + 4 * 32, Graphics.Color.Get(-1, 000, 111, 111));
    public static Resource IronOre { get; } = new("I.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static Resource GoldOre { get; } = new("G.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 553));
    public static Resource IronIngot { get; } = new("IRON", 11 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static Resource GoldIngot { get; } = new("GOLD", 11 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 553));

    public static Resource Slime { get; } = new("SLIME", 10 + 4 * 32, Graphics.Color.Get(-1, 10, 30, 50));
    public static Resource Glass { get; } = new("glass", 12 + 4 * 32, Graphics.Color.Get(-1, 555, 555, 555));
    public static Resource Cloth { get; } = new("cloth", 1 + 4 * 32, Graphics.Color.Get(-1, 25, 252, 141));
    public static Resource Cloud { get; } = new PlantableResource("cloud", 2 + 4 * 32, Graphics.Color.Get(-1, 222, 555, 444), Tile.Cloud, Tile.InfiniteFall);
    public static Resource Gem { get; } = new("gem", 13 + 4 * 32, Graphics.Color.Get(-1, 101, 404, 545));

    public virtual bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        return false;
    }
}

public record class PlantableResource(string Name, int Sprite, int Color, Tile TargetTile, params Tile[] SourceTiles) : Resource(Name, Sprite, Color)
{
    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (SourceTiles.Contains(tile))
        {
            level.SetTile(xt, yt, TargetTile, 0);
            return true;
        }
        return false;
    }
}

public record class FoodResource(string Name, int Sprite, int Color, int Heal, int StaminaCost) : Resource(Name, Sprite, Color)
{
    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (player.Health < player.MaxHealth && player.PayStamina(StaminaCost))
        {
            player.Heal(Heal);
            return true;
        }
        return false;
    }
}
