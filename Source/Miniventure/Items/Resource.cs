using Miniventure.Entities;
using Miniventure.Levels;
using Miniventure.Levels.Tiles;

namespace Miniventure.Items;

public record class Resource(string Name, int Sprite, int Color) : Enumeration<Resource>
{
    public static Resource wood = new("Wood", 1 + 4 * 32, Graphics.Color.Get(-1, 200, 531, 430));
    public static Resource stone = new("Stone", 2 + 4 * 32, Graphics.Color.Get(-1, 111, 333, 555));
    public static Resource flower = new PlantableResource("Flower", 0 + 4 * 32, Graphics.Color.Get(-1, 10, 444, 330), Tile.flower, Tile.grass);
    public static Resource acorn = new PlantableResource("Acorn", 3 + 4 * 32, Graphics.Color.Get(-1, 100, 531, 320), Tile.treeSapling, Tile.grass);
    public static Resource dirt = new PlantableResource("Dirt", 2 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 432), Tile.dirt, Tile.hole, Tile.water, Tile.lava);
    public static Resource sand = new PlantableResource("Sand", 2 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 550), Tile.sand, Tile.grass, Tile.dirt);
    public static Resource cactusFlower = new PlantableResource("Cactus", 4 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.cactusSapling, Tile.sand);
    public static Resource seeds = new PlantableResource("Seeds", 5 + 4 * 32, Graphics.Color.Get(-1, 10, 40, 50), Tile.wheat, Tile.farmland);
    public static Resource wheat = new("Wheat", 6 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550));
    public static Resource bread = new FoodResource("Bread", 8 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 550), 2, 5);
    public static Resource apple = new FoodResource("Apple", 9 + 4 * 32, Graphics.Color.Get(-1, 100, 300, 500), 1, 5);

    public static Resource coal = new("COAL", 10 + 4 * 32, Graphics.Color.Get(-1, 000, 111, 111));
    public static Resource ironOre = new("I.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static Resource goldOre = new("G.ORE", 10 + 4 * 32, Graphics.Color.Get(-1, 110, 440, 553));
    public static Resource ironIngot = new("IRON", 11 + 4 * 32, Graphics.Color.Get(-1, 100, 322, 544));
    public static Resource goldIngot = new("GOLD", 11 + 4 * 32, Graphics.Color.Get(-1, 110, 330, 553));

    public static Resource slime = new("SLIME", 10 + 4 * 32, Graphics.Color.Get(-1, 10, 30, 50));
    public static Resource glass = new("glass", 12 + 4 * 32, Graphics.Color.Get(-1, 555, 555, 555));
    public static Resource cloth = new("cloth", 1 + 4 * 32, Graphics.Color.Get(-1, 25, 252, 141));
    public static Resource cloud = new PlantableResource("cloud", 2 + 4 * 32, Graphics.Color.Get(-1, 222, 555, 444), Tile.cloud, Tile.infiniteFall);
    public static Resource gem = new("gem", 13 + 4 * 32, Graphics.Color.Get(-1, 101, 404, 545));

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
