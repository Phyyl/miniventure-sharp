namespace Miniventure.Levels.Tiles;

public record class HardRockTile : RockTile
{
    protected override int MainColor => 334;
    protected override int DarkColor => 001;

    public HardRockTile(byte id)
        : base(id)
    {
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, 0);
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Pickaxe && (int)tool.Level == 4)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    Hurt(level, xt, yt, random.NextInt(10) + (int)tool.Level * 5 + 10);

                    return true;
                }
            }
        }

        return false;
    }
}
