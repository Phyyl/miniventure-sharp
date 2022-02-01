using Miniventure.Items.Tools;

namespace Miniventure.Levels.Tiles;

public record class GrassTile : Tile
{
    public GrassTile(byte id)
        : base(id, true)
    {
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.GrassColor, level.GrassColor, level.GrassColor + 111, level.GrassColor + 111);
        int transitionColor = Color.Get(level.GrassColor - 111, level.GrassColor, level.GrassColor + 111, level.DirtColor);

        bool u = !level.GetTile(x, y - 1).ConnectsToGrass;
        bool d = !level.GetTile(x, y + 1).ConnectsToGrass;
        bool l = !level.GetTile(x - 1, y).ConnectsToGrass;
        bool r = !level.GetTile(x + 1, y).ConnectsToGrass;

        if (!u && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 0, 0, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 0, (l ? 11 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);
        }

        if (!u && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 0, 1, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 0, (r ? 13 : 12) + (u ? 0 : 1) * 32, transitionColor, 0);
        }

        if (!d && !l)
        {
            screen.Render(x * 16 + 0, y * 16 + 8, 2, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 0, y * 16 + 8, (l ? 11 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
        }

        if (!d && !r)
        {
            screen.Render(x * 16 + 8, y * 16 + 8, 3, col, 0);
        }
        else
        {
            screen.Render(x * 16 + 8, y * 16 + 8, (r ? 13 : 12) + (d ? 2 : 1) * 32, transitionColor, 0);
        }
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

        if (level.GetTile(xn, yn) == Dirt)
        {
            level.SetTile(xn, yn, this, 0);
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
                    AudioTracks.MonsterHurt.Play();
                    if (random.NextInt(5) == 0)
                    {

                        level.Add(new ItemEntity(new ResourceItem(Resource.Seeds), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                        return true;
                    }
                }
            }

            if (tool.Type == ToolType.Hoe)
            {
                if (player.PayStamina(4 - (int)tool.Level))
                {
                    AudioTracks.MonsterHurt.Play();
                    if (random.NextInt(5) == 0)
                    {

                        level.Add(new ItemEntity(new ResourceItem(Resource.Seeds), xt * 16 + random.NextInt(10) + 3, yt * 16 + random.NextInt(10) + 3));
                        return true;
                    }
                    level.SetTile(xt, yt, Farmland, 0);
                    return true;
                }
            }
        }

        return false;
    }
}
