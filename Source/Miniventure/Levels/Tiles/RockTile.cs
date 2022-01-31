namespace Miniventure.Levels.Tiles;


public class RockTile : Tile
{
    public RockTile(int id) : base(id)
    {
        t = this;
    }

    public Tile t;
    public int mainColor = 444;
    public int darkColor = 111;

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(mainColor, mainColor, mainColor - 111, mainColor - 111);
        int transitionColor = Color.Get(darkColor, mainColor, mainColor + 111, level.DirtColor);

        bool u = level.GetTile(x, y - 1) != t;
        bool d = level.GetTile(x, y + 1) != t;
        bool l = level.GetTile(x - 1, y) != t;
        bool r = level.GetTile(x + 1, y) != t;

        bool ul = level.GetTile(x - 1, y - 1) != t;
        bool dl = level.GetTile(x - 1, y + 1) != t;
        bool ur = level.GetTile(x + 1, y - 1) != t;
        bool dr = level.GetTile(x + 1, y + 1) != t;

        if (!u && !l)
        {
            if (!ul)
            {
                screen.Render(x * 16 + 0, y * 16 + 0, 0, col, MirrorFlags.None);
            }
            else
            {
                screen.Render(x * 16 + 0, y * 16 + 0, 7 + 0 * 32, transitionColor, MirrorFlags.Both);
            }
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 0, (l ? 6 : 5) + (u ? 2 : 1) * 32, transitionColor, MirrorFlags.Both);
        }

        if (!u && !r)
        {
            if (!ur)
            {
                screen.Render(x * 16 + 8, y * 16 + 0, 1, col, MirrorFlags.None);
            }
            else
            {
                screen.Render(x * 16 + 8, y * 16 + 0, 8 + 0 * 32, transitionColor, MirrorFlags.Both);
            }
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 0, (r ? 4 : 5) + (u ? 2 : 1) * 32, transitionColor, MirrorFlags.Both);
        }

        if (!d && !l)
        {
            if (!dl)
            {
                screen.Render(x * 16 + 0, y * 16 + 8, 2, col, MirrorFlags.None);
            }
            else
            {
                screen.Render(x * 16 + 0, y * 16 + 8, 7 + 1 * 32, transitionColor, MirrorFlags.Both);
            }
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 8, (l ? 6 : 5) + (d ? 0 : 1) * 32, transitionColor, MirrorFlags.Both);
        }

        if (!d && !r)
        {
            if (!dr)
            {
                screen.Render(x * 16 + 8, y * 16 + 8, 3, col, MirrorFlags.None);
            }
            else
            {
                screen.Render(x * 16 + 8, y * 16 + 8, 8 + 1 * 32, transitionColor, MirrorFlags.Both);
            }
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 8, (r ? 4 : 5) + (d ? 0 : 1) * 32, transitionColor, MirrorFlags.Both);
        }
    }


    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }


    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        hurt(level, x, y, dmg);
    }


    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        {
            ToolItem tool = (ToolItem)item;
            if (tool.Type == ToolType.Pickaxe)
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

    public virtual void hurt(Level level, int x, int y, int dmg)
    {
        byte damage = (byte)(level.GetData(x, y) + dmg);
        level.Add(new SmashParticle(x * 16 + 8, y * 16 + 8));
        level.Add(new TextParticle("" + dmg, x * 16 + 8, y * 16 + 8, Color.Get(-1, 500, 500, 500)));
        if (damage >= 50)
        {
            int count = random.NextInt(4) + 1;
            for (int i = 0; i < count; i++)
            {

                level.Add(new ItemEntity(new ResourceItem(Resource.stone), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }
            count = random.NextInt(2);
            for (int i = 0; i < count; i++)
            {

                level.Add(new ItemEntity(new ResourceItem(Resource.coal), x * 16 + random.NextInt(10) + 3, y * 16 + random.NextInt(10) + 3));
            }
            level.SetTile(x, y, dirt, 0);
        }
        else
        {
            level.SetData(x, y, damage);
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
}
