namespace Miniventure.Levels.Tiles;

public record class LavaTile : Tile
{
    public LavaTile(byte id)
        : base(id, connectsToSand: true, connectsToLava: true)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        Random random = new((TickCount + (x / 2 - y) * 4311) / 10 * 54687121 + x * 3271612 + y * 3412987161);

        int col = Color.Get(500, 500, 520, 550);
        int transitionColor1 = Color.Get(3, 500, level.DirtColor - 111, level.DirtColor);
        int transitionColor2 = Color.Get(3, 500, level.SandColor - 110, level.SandColor);

        bool u = !level.GetTile(x, y - 1).ConnectsToLava;
        bool d = !level.GetTile(x, y + 1).ConnectsToLava;
        bool l = !level.GetTile(x - 1, y).ConnectsToLava;
        bool r = !level.GetTile(x + 1, y).ConnectsToLava;

        bool su = u && level.GetTile(x, y - 1).ConnectsToSand;
        bool sd = d && level.GetTile(x, y + 1).ConnectsToSand;
        bool sl = l && level.GetTile(x - 1, y).ConnectsToSand;
        bool sr = r && level.GetTile(x + 1, y).ConnectsToSand;

        if (!u && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, random.NextInt(4), col, (MirrorFlags)random.NextInt(4));
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 0, (l ? 14 : 15) + (u ? 0 : 1) * 32, su || sl ? transitionColor2 : transitionColor1, 0);
        }

        if (!u && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, random.NextInt(4), col, (MirrorFlags)random.NextInt(4));
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 0, (r ? 16 : 15) + (u ? 0 : 1) * 32, su || sr ? transitionColor2 : transitionColor1, 0);
        }

        if (!d && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, random.NextInt(4), col, (MirrorFlags)random.NextInt(4));
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 8, (l ? 14 : 15) + (d ? 2 : 1) * 32, sd || sl ? transitionColor2 : transitionColor1, 0);
        }

        if (!d && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, random.NextInt(4), col, (MirrorFlags)random.NextInt(4));
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 8, (r ? 16 : 15) + (d ? 2 : 1) * 32, sd || sr ? transitionColor2 : transitionColor1, 0);
        }
    }

    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return e.CanSwim();
    }

    public override void Update(Level level, int xt, int yt)
    {
        int xn = xt;
        int yn = yt;

        if (random.NextBoolean())
        {
            xn += random.NextInt(2) * 2 - 1;
        }
        else
        {
            yn += random.NextInt(2) * 2 - 1;
        }

        if (level.GetTile(xn, yn) == Hole)
        {
            level.SetTile(xn, yn, this, 0);
        }
    }

    public override int GetLightRadius(Level level, int x, int y)
    {
        return 6;
    }
}
