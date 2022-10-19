using Miniventure.Items.Tools;

namespace Miniventure.Levels.Tiles;

public record class CloudTile : Tile
{
    public CloudTile(byte id)
        : base(id)
    {
    }

    /* Oh boy, it's one of these more complicated connecting tiles classes. 
	 * 
	 * Sorry if I can't explain these well - David.
     */

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(444, 444, 555, 555);
        int transitionColor = Color.Get(333, 444, 555, -1);

        bool u = level.GetTile(x, y - 1) == InfiniteFall;
        bool d = level.GetTile(x, y + 1) == InfiniteFall;
        bool l = level.GetTile(x - 1, y) == InfiniteFall;
        bool r = level.GetTile(x + 1, y) == InfiniteFall;

        bool ul = level.GetTile(x - 1, y - 1) == InfiniteFall;
        bool dl = level.GetTile(x - 1, y + 1) == InfiniteFall;
        bool ur = level.GetTile(x + 1, y - 1) == InfiniteFall;
        bool dr = level.GetTile(x + 1, y + 1) == InfiniteFall;

        if (!u && !l)
        {
            if (!ul)
            {
                screen.Render(x * 16 + 0, y * 16 + 0, 17, col, MirrorFlags.None);
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
                screen.Render(x * 16 + 8, y * 16 + 0, 18, col, MirrorFlags.None);
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
                screen.Render(x * 16 + 0, y * 16 + 8, 20, col, MirrorFlags.None);
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
                screen.Render(x * 16 + 8, y * 16 + 8, 19, col, MirrorFlags.None);
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
        return true;
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(5))
                {
                    int count = random.NextInt(2) + 1;

                    for (int i = 0; i < count; i++)
                    {
                        level.Add(new ItemEntity(new ResourceItem(Resource.Cloud), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                    }

                    return true;
                }
            }
        }
        return false;
    }

}
