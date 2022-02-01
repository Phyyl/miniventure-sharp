namespace Miniventure.Items;

public record class Resource(string Name, int Sprite, int Color)
{
    public static readonly Resource Wood = new("Wood", 1 + 4 * 32, Graphics.Color.Get(-1, 200, 531, 430));
    public static readonly Resource Stone = new("Stone", 2 + 4 * 32, Graphics.Color.Get(-1, 111, 333, 555));
    public static readonly Resource Flower = new PlantableResource("Flower", 0 + 4 * 32, Graphics.Color.Get(-1, 10, 444, 330), Tile.Flower, Tile.Grass);
    public static readonly Resource Acorn = new PlantableResource("Acorn", 3 + 4 * 32, Graphics.Color.Get(-1, 100, 531, 320), Tile.TreeSapling, Tile.Grass);
    public static readonly Resource Dirt = new PlantableResource("Dirt", 2 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 432), Tile.Dirt, Tile.Hole, Tile.Water, Tile.Lava);
    public static readonly Resource Sand = new PlantableResource("Sand", 2 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 550), Tile.Sand, Tile.Grass, Tile.Dirt);
    public static readonly Resource CactusFlower = new PlantableResource("Cactus", 4 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.CactusSapling, Tile.Sand);
    public static readonly Resource Seeds = new PlantableResource("Seeds", 5 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.Wheat, Tile.Farmland);
    public static readonly Resource Wheat = new("Wheat", 6 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550));
    public static readonly Resource Bread = new FoodResource("Bread", 8 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550), 2, 5);
    public static readonly Resource Apple = new FoodResource("Apple", 9 + 4 * 32, Graphics.Color.Get(-1, 100, 300, 500), 1, 5);
    public static readonly Resource Coal = new("COAL", 10 + 4 * 32, Graphics.Color.Get(-1, 000, 111, 111));
    public static readonly Resource IronOre = new("I.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static readonly Resource GoldOre = new("G.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 553));
    public static readonly Resource IronIngot = new("IRON", 11 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static readonly Resource GoldIngot = new("GOLD", 11 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 553));
    public static readonly Resource Slime = new("SLIME", 10 + 4 * 32, Graphics.Color.Get(-1, 10, 30, 50));
    public static readonly Resource Glass = new("glass", 12 + 4 * 32, Graphics.Color.Get(-1, 555, 555, 555));
    public static readonly Resource Cloth = new("cloth", 1 + 4 * 32, Graphics.Color.Get(-1, 25, 252, 141));
    public static readonly Resource Cloud = new PlantableResource("cloud", 2 + 4 * 32, Graphics.Color.Get(-1, 222, 555, 444), Tile.Cloud, Tile.InfiniteFall);
    public static readonly Resource Gem = new("gem", 13 + 4 * 32, Graphics.Color.Get(-1, 101, 404, 545));

    public static EnumDictionary<string, Resource> All { get; } = new(v => v.Name);

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
