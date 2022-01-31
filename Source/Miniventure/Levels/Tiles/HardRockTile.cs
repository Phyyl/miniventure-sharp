namespace Miniventure.Levels.Tiles;

public class HardRockTile : RockTile
{
    public HardRockTile(int id) : base(id)
    {
        mainColor = 334;
        darkColor = 001;
        t = this;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        hurt(level, x, y, 0);
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        {
            ToolItem tool = (ToolItem)item;
            if (tool.Type == ToolType.Pickaxe && (int)tool.Level == 4)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    hurt(level, xt, yt, random.NextInt(10) + (int)tool.Level * 5 + 10);
                    return true;
                }
            }
        }
        return false;
    }
}
