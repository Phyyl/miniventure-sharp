using Miniventure.Items.Tools;

namespace Miniventure.Levels.Tiles;

public record class WheatTile : Tile
{
    public WheatTile(byte id)
        : base(id)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int age = level.GetData(x, y);
        int col = Color.Get(level.DirtColor - 121, level.DirtColor - 11, level.DirtColor, 50);
        int icon = age / 10;

        if (icon >= 3)
        {
            col = Color.Get(level.DirtColor - 121, level.DirtColor - 11, 50 + icon * 100, 40 + (icon - 3) * 2 * 100);

            if (age == 50)
            {
                col = Color.Get(0, 0, 50 + icon * 100, 40 + (icon - 3) * 2 * 100);
            }

            icon = 3;
        }

        screen.Render(x * 16 + 0, y * 16 + 0, 4 + 3 * 32 + icon, col, MirrorFlags.None);
        screen.Render(x * 16 + 8, y * 16 + 0, 4 + 3 * 32 + icon, col, MirrorFlags.None);
        screen.Render(x * 16 + 0, y * 16 + 8, 4 + 3 * 32 + icon, col, MirrorFlags.Horizontal);
        screen.Render(x * 16 + 8, y * 16 + 8, 4 + 3 * 32 + icon, col, MirrorFlags.Horizontal);
    }

    public override void Update(Level level, int xt, int yt)
    {
        if (random.NextBoolean() == false)
        {
            return;
        }

        byte age = level.GetData(xt, yt);

        if (age < 50)
        {
            level.SetData(xt, yt, (byte)(age + 1));
        }
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    level.SetTile(xt, yt, Dirt, 0);

                    return true;
                }
            }
        }

        return false;
    }

    public override void SteppedOn(Level level, int xt, int yt, Entity entity)
    {
        if (random.NextInt(60) != 0)
        {
            return;
        }

        if (level.GetData(xt, yt) < 2)
        {
            return;
        }

        Harvest(level, xt, yt);
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Harvest(level, x, y);
    }

    private void Harvest(Level level, int x, int y)
    {
        int age = level.GetData(x, y);
        int count = random.NextInt(2);

        for (int i = 0; i < count; i++)
        {
            level.Add(new ItemEntity(new ResourceItem(Resource.Seeds), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
        }

        count = 0;

        if (age == 50)
        {
            count = random.NextInt(3) + 2;
        }
        else if (age >= 40)
        {
            count = random.NextInt(2) + 1;
        }

        for (int i = 0; i < count; i++)
        {
            level.Add(new ItemEntity(new ResourceItem(Resource.Wheat), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
        }

        level.SetTile(x, y, Dirt, 0);
    }
}
