using Vildmark.Serialization;

namespace com.mojang.ld22.item;

public class FurnitureItem : Item
{
    public Furniture furniture;
    public bool placed = false;

    public FurnitureItem(Furniture furniture)
    {
        this.furniture = furniture;
    }

    public override int GetColor()
    {
        return furniture.col;
    }

    public override int GetSprite()
    {
        return furniture.sprite + (10 * 32);
    }

    public override void RenderIcon(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
    }

    public override void RenderInventory(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
        Font.Draw(furniture.name, screen, x + 8, y, Color.Get(-1, 555, 555, 555));
    }

    public override bool CanAttack()
    {
        return false;
    }

    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (tile.MayPass(level, xt, yt, furniture))
        {
            furniture.X = (xt * 16) + 8;
            furniture.Y = (yt * 16) + 8;
            level.Add(furniture);
            placed = true;
            return true;
        }
        return false;
    }


    public override bool IsDepleted()
    {
        return placed;
    }


    public override string GetName()
    {
        return furniture.name;
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteObject(furniture, true);
        writer.WriteValue(placed);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        reader.ReadObject<Furniture>(true);
        placed = reader.ReadValue<bool>();
    }
}