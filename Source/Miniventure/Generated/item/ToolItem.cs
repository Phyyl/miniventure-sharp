namespace com.mojang.ld22.item;



public class ToolItem : Item
{
    private Random random = new Random();

    public static readonly int MAX_LEVEL = 5; // How many different levels of tools there are
    public static readonly string[] LEVEL_NAMES = {
    "Wood", "Rock", "Iron", "Gold", "Gem" // The names of the different levels. Later levels means stronger tool
	};

    public static readonly int[] LEVEL_COLORS = {
	    Color.Get(-1, 100, 321, 431),
	    Color.Get(-1, 100, 321, 111),
	    Color.Get(-1, 100, 321, 555),
	    Color.Get(-1, 100, 321, 550),
	    Color.Get(-1, 100, 321, 055),
	};

    public ToolType Type { get; }
    public ToolLevel Level { get; }

    /** Tool Item, requires a tool type (ToolType.sword, ToolType.axe, ToolType.hoe, etc) and a level (0 = wood, 2 = iron, 4 = gem, etc) */
    public ToolItem(ToolType type, ToolLevel level)
    {
        Type = type; //type of tool for this item
        Level = level; //level of tool for this item
    }

    public override int GetColor()
    {
        //TODO: store the color in a record
        return LEVEL_COLORS[(int)Level];
    }

    public override int GetSprite()
    {
        return Type.Sprite + (5 * 32);
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
            return (((int)Level + 1) * 2) + random.NextInt(4); // axes: (level + 1) * 2 + random number beteween 0 and 3, do slightly less damage than swords.
        }
        if (Type == ToolType.Sword)
        {
            return (((int)Level + 1) * 3) + random.NextInt(2 + ((int)Level * (int)Level * 2)); //swords: (level + 1) * 3 + random number between 0 and (2 + level * level * 2)
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
}
