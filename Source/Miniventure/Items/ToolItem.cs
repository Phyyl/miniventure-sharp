using Vildmark.Serialization;

namespace Miniventure.Items;

public class ToolItem : Item
{
    private readonly Random random = new();

    public static readonly int MAX_LEVEL = 5;
    public static readonly string[] LEVEL_NAMES = {
    "Wood", "Rock", "Iron", "Gold", "Gem"
    };

    public static readonly int[] LEVEL_COLORS = {
        Color.Get(-1, 100, 321, 431),
        Color.Get(-1, 100, 321, 111),
        Color.Get(-1, 100, 321, 555),
        Color.Get(-1, 100, 321, 550),
        Color.Get(-1, 100, 321, 055),
    };

    public ToolType Type { get; private set; }
    public ToolLevel Level { get; private set; }

    public ToolItem(ToolType type, ToolLevel level)
    {
        Type = type;
        Level = level;
    }

    private ToolItem() { }

    public override int GetColor()
    {
        return LEVEL_COLORS[(int)Level];
    }

    public override int GetSprite()
    {
        return Type.Sprite + 5 * 32;
    }

    public override void RenderIcon(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
    }

    public override void RenderInventory(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0);
        Font.Draw(GetName(), screen, x + 8, y, Color.Get(-1, 555, 555, 555));
    }

    public override string GetName()
    {
        return LEVEL_NAMES[(int)Level] + " " + Type.Name;
    }

    public override void OnTake(ItemEntity itemEntity)
    {
    }

    public override bool CanAttack()
    {
        return true;
    }

    public override int GetAttackDamageBonus(Entity e)
    {
        if (Type == ToolType.Axe)
        {
            return ((int)Level + 1) * 2 + random.NextInt(4);
        }
        if (Type == ToolType.Sword)
        {
            return ((int)Level + 1) * 3 + random.NextInt(2 + (int)Level * (int)Level * 2);
        }
        return 1;
    }

    public override bool Matches(Item item)
    {
        if (item is ToolItem other)
        {
            if (other.Type != Type)
            {
                return false;
            }

            if (other.Level != Level)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteString(Type.Name);
        writer.WriteValue(Level);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        Type = ToolType.All.GetValueOrDefault(reader.ReadString());
        Level = reader.ReadValue<ToolLevel>();
    }
}
