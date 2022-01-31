namespace Miniventure.Levels.Tiles;

public record class TreeTile : Tile
{
    public TreeTile(byte id)
        : base(id, true)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(10, 30, 151, level.GrassColor);
        int barkCol1 = Color.Get(10, 30, 430, level.GrassColor);
        int barkCol2 = Color.Get(10, 30, 320, level.GrassColor);

        bool u = level.GetTile(x, y - 1) == this;
        bool l = level.GetTile(x - 1, y) == this;
        bool r = level.GetTile(x + 1, y) == this;
        bool d = level.GetTile(x, y + 1) == this;
        bool ul = level.GetTile(x - 1, y - 1) == this;
        bool ur = level.GetTile(x + 1, y - 1) == this;
        bool dl = level.GetTile(x - 1, y + 1) == this;
        bool dr = level.GetTile(x + 1, y + 1) == this;

        if (u && ul && l)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 10 + 1 * 32, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 9 + 0 * 32, col, 0);
        }

        if (u && ur && r)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 10 + 2 * 32, barkCol2, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 10 + 0 * 32, col, 0);
        }

        if (d && dl && l)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 10 + 2 * 32, barkCol2, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 9 + 1 * 32, barkCol1, 0);
        }

        if (d && dr && r)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 10 + 1 * 32, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 10 + 3 * 32, barkCol2, 0);
        }
    }

    public override void Update(Level level, int xt, int yt)
    {
        byte damage = level.GetData(xt, yt);

        if (damage > 0)
        {
            level.SetData(xt, yt, (byte)(damage - 1));
        }
    }

    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, dmg);
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Axe)
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

    private void Hurt(Level level, int x, int y, int dmg)
    {
        {
            int count = random.NextInt(10) == 0 ? 1 : 0;
            for (int i = 0; i < count; i++)
            {
                level.Add(new ItemEntity(new ResourceItem(Resource.Apple), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }
        }

        byte damage = (byte)(level.GetData(x, y) + dmg);

        level.Add(new SmashParticle(x * 16 + 8, y * 16 + 8));
        level.Add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, Color.Get(-1, 500, 500, 500)));

        if (damage >= 20)
        {
            int count = random.NextInt(2) + 1;

            for (int i = 0; i < count; i++)
            {
                level.Add(new ItemEntity(new ResourceItem(Resource.Wood), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }

            count = random.NextInt(random.NextInt(4) + 1);

            for (int i = 0; i < count; i++)
            {
                level.Add(new ItemEntity(new ResourceItem(Resource.Acorn), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }

            level.SetTile(x, y, Grass, 0);
        }
        else
        {
            level.SetData(x, y, damage);
        }
    }
}
